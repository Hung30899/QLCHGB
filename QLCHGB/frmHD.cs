using QLCHGB.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmHD : Form
    {
        DataTable tblHDCT;
        char btn;
        String rbn, soluongc;

        public String strMaHD;
        public Char flag;
        public frmHD()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cboMaGB.Text = "";
            txtTenGB.Text = "";
            txtSL.Text = "";
            txtDonGia.Text = "";
            txtTien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btn = 't';
            Enable();
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            txtMaHD.Text = Functions.CreateKey("HD");
            ResetValues();
        }

        private void frmHD_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtSL.Text = "0";
            txtDonGia.Text = "0";
            txtTien.Text = "0";

            Disable();
            Functions.FillCombo("SELECT MaGB, TenGB FROM GauBong", cboMaGB, "MaGB", "MaGB");
            cboMaGB.SelectedIndex = -1;

            if (flag == 's')
            {
                btnThem.Enabled = false;
                txtMaHD.Text = strMaHD;
                LoadDateGirdViewCT(strMaHD);
            }
            else
            {
                string str = "";
                LoadDateGirdViewCT(str);
                btnXoa.Enabled = false;
            }
        }
        private void Disable()
        {
            txtMaHD.ReadOnly = true;
            cboMaGB.Enabled = false;
            txtSL.Enabled = false;
            dtpThoiGian.Enabled = false;
        }

        private void Enable()
        {
            cboMaGB.Enabled = true;
            txtSL.Enabled = true;
            txtDonGia.Enabled = true;
            dtpThoiGian.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            txtSL.Enabled = true;
            txtDonGia.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            cboMaGB.Enabled = true;
            soluongc = txtSL.Text.Trim();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHDCT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text.Trim() + "'";

                    DataTable table = Functions.GetDataToTable(sql);
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa;

                        sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text + "'";
                        DataTable tblHang = Functions.GetDataToTable(sql);
                        for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                        {
                            // Cập nhật lại số lượng cho các mặt hàng
                            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLB FROM GauBong WHERE MaGB = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                            slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                            slcon = sl - slxoa;
                            sql = "UPDATE GauBong SET SLB =" + slcon + " WHERE MaGB= N'" + tblHang.Rows[hang][0].ToString() + "'";
                            Functions.RunSQL(sql);
                        }
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = Functions.Con;
                        sql = "DELETE CTHoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                sql = "DELETE HoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                Functions.RunSQLDel(sql);

                LoadDateGirdViewCT(txtMaHD.Text.Trim());
                ResetValues();
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = true;
                txtMaHD.Text = "";
                this.Close();
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {

                if (cboMaGB.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaGB.Focus();
                    return;
                }
                if (txtSL.Text == "0")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSL.Focus();
                    return;
                }
                if (txtDonGia.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn chưa thiết lập giá bán cho gấu bông mã: " + cboMaGB.Text.Trim() + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (btn == 't')
                {
                    //Lưu thông tin của các mặt hàng
                    if (cboMaGB.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        return;
                    }

                    if (txtSL.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSL.Focus();
                        return;
                    }

                    sql = "SELECT MaGB FROM CTHoaDon WHERE MaGB = N'" + cboMaGB.SelectedValue + "' AND MaHD = N'" + txtMaHD.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã gấu bông này đã có, bạn phải nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        cboMaGB.Text = "";
                        return;
                    }
                    // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
                    double sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN-SLB as SL FROM GauBong WHERE MaGB = N'" + cboMaGB.SelectedValue + "'"));
                    if (Convert.ToDouble(txtSL.Text) > sl)
                    {
                        MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSL.Text = "";
                        txtSL.Focus();
                        return;
                    }

                    sql = "SELECT MaHD FROM HoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                    if (!Functions.CheckKey(sql))//Chưa có mã HD
                    {
                        sql = "INSERT INTO HoaDon(MaHD, ThoiGian) VALUES " +
                            "(N'" + txtMaHD.Text.Trim() + "',N'" + dtpThoiGian.Text.Trim() + "')";
                        Functions.RunSQL(sql);
                    }

                    sql = "INSERT INTO CTHoaDon(MaHD, MaGB, SoLuong, DonGia)" +
                        " VALUES(N'" + txtMaHD.Text.Trim() + "',N'" + cboMaGB.SelectedValue + "',N'" + txtSL.Text.Trim() + "',N'" + txtDonGia.Text.Trim() + "')";
                    Functions.RunSQL(sql);
                    sql = "UPDATE GauBong SET SLB = (SLB + " + txtSL.Text.Trim() + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                    Functions.RunSQL(sql);
                    LoadDateGirdViewCT(txtMaHD.Text.Trim());
                }


                //Sửa
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        double sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN-SLB as SL FROM GauBong WHERE MaGB = N'" + cboMaGB.SelectedValue + "'"));
                        sl = sl + Convert.ToDouble(soluongc);
                        if (Convert.ToDouble(txtSL.Text) > sl)
                        {
                            MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSL.Text = "";
                            txtSL.Focus();
                            return;
                        }
                        sql = "UPDATE CTHoaDon SET SoLuong=N'" + txtSL.Text.Trim() + "' WHERE MaHD=N'" + txtMaHD.Text + "' AND MaGB= N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        MessageBox.Show("Đã cập nhật số lượng mã gấu bông: " + cboMaGB.SelectedValue + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sql = "UPDATE GauBong SET SLB = (SLB + " + txtSL.Text.Trim() + " - " + soluongc + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        LoadDateGirdViewCT(txtMaHD.Text.Trim());
                        btnXoa.Enabled = false;
                        btnThem.Enabled = true;
                        btnSua.Enabled = false;
                        btnHuy.Enabled = false;
                        btnLuu.Enabled = false;
                        Disable();
                    }
                }
            }
        }

        private void LoadDateGirdViewCT(string str)
        {

            String sql;
            //     sql = "SELECT * FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text.Trim() + "'";
            sql = "SELECT a.MaGB,b.TenGB,a.SoLuong,a.DonGia,a.SoLuong * a.DonGia AS ThanhTien FROM CTHoaDon AS a,GauBong AS b WHERE a.MaHD = N'" + str + "' AND a.MaGB=b.MaGB";

            tblHDCT = Functions.GetDataToTable(sql);
            dgvCTHD.DataSource = tblHDCT;

            dgvCTHD.Columns[0].HeaderText = "Mã gấu bông";
            dgvCTHD.Columns[1].HeaderText = "Tên gấu bông";
            dgvCTHD.Columns[2].HeaderText = "Số lượng";
            dgvCTHD.Columns[3].HeaderText = "Đơn giá";
            dgvCTHD.Columns[4].HeaderText = "Thành tiền";

            dgvCTHD.Columns[0].Width = 150;
            dgvCTHD.Columns[1].Width = 150;
            dgvCTHD.Columns[2].Width = 150;
            dgvCTHD.Columns[3].Width = 150;
            dgvCTHD.Columns[4].Width = 250;
            dgvCTHD.AllowUserToAddRows = false;
            dgvCTHD.EditMode = DataGridViewEditMode.EditProgrammatically;
            TongTien();
        }

        private void TongTien()
        {

            if (dgvCTHD.Rows.Count > 0)
            {
                string sql = "SELECT SUM(SoLuong * DonGia) FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text.Trim() + "'";
                txtTongTien.Text = Functions.GetFieldValues(sql);

            }
            else txtTongTien.Text = "0";
            lblTien.Text = Functions.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng phiếu nhập không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg;

            if (txtSL.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSL.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg * 1000;
            txtTien.Text = tt.ToString();
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void cboMaGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaGB.Text == "")
            {
                txtTenGB.Text = "";
            }
            sql = "Select TenGB from GauBong where MaGB =N'" + cboMaGB.SelectedValue + "'";
            txtTenGB.Text = Functions.GetFieldValues(sql);

            sql = "Select Top 1 IsNULL(DonGia,0) as DonGia from GiaBan where MaGB =N'" + cboMaGB.SelectedValue + "' and Ngay <= N'" + dtpThoiGian.Text + "' order by Ngay desc";
            txtDonGia.Text = Functions.GetFieldValues(sql);
            txtSL_TextChanged(sender, e);
        }

        private void dgvCTHD_Click(object sender, EventArgs e)
        {
            if (tblHDCT.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (flag == 's')
            {
                cboMaGB.Text = dgvCTHD.CurrentRow.Cells[0].Value.ToString();
                txtSL.Text = dgvCTHD.CurrentRow.Cells[2].Value.ToString();
                cboMaGB_SelectedIndexChanged(sender, e);
                txtSL_TextChanged(sender, e);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = true;
                Disable();
            }
        }

        private void dtpThoiGian_ValueChanged(object sender, EventArgs e)
        {
            string sql = "Select Top 1 IsNULL(DonGia,0) as DonGia from GiaBan where MaGB =N'" + cboMaGB.SelectedValue + "' and Ngay <= N'" + dtpThoiGian.Text + "' order by Ngay desc";
            txtDonGia.Text = Functions.GetFieldValues(sql);
            txtSL_TextChanged(sender, e);
        }

        private void ResetValues()
        {
            dtpThoiGian.Text = "";
            cboMaGB.Text = "";
            txtTenGB.Text = "";
            txtSL.Text = "0";
            txtDonGia.Text = "0";
            txtTien.Text = "0";
        }
    }
}
