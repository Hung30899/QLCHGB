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
    public partial class frmPN : Form
    {
      //  DataTable tblPN;
        DataTable tblPNCT;
        char btn;
        String rbn, soluongc;

        public String strMaPN;
        public Char flag;

        public frmPN()
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
            flag = 't';
            Enable();
            cboMaNCC.Focus();
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            txtMaPN.Text = Functions.CreateKey("PN");
            ResetValues();
        }

        private void Disable()
        {
            txtMaPN.Enabled = false;
            cboMaNCC.Enabled = false;
            cboMaGB.Enabled = false;
            txtSL.Enabled = false;
            txtDonGia.Enabled = false;
            dtpThoiGian.Enabled = false;
        }

        private void ResetValues()
        {
            dtpThoiGian.Text = "";
            cboMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChiNCC.Text = "";
            txtSDTNCC.Text = "";
            cboMaGB.Text = "";
            txtTenGB.Text = "";
            txtSL.Text = "0";
            txtDonGia.Text = "0";
            txtTien.Text = "0";
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
           
            if (txtMaPN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "SELECT MaGB,SoLuong FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text.Trim() + "'";

                DataTable table = Functions.GetDataToTable(sql);
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa;

                        sql = "SELECT MaGB,SoLuong FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text + "'";
                        DataTable tblHang = Functions.GetDataToTable(sql);
                        for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                        {
                            // Cập nhật lại số lượng cho các mặt hàng
                            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN FROM GauBong WHERE MaGB = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                            slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                            slcon = sl - slxoa;
                            sql = "UPDATE GauBong SET SLN =" + slcon + " WHERE MaGB= N'" + tblHang.Rows[hang][0].ToString() + "'";
                            Functions.RunSQL(sql);
                        }
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = Functions.Con;
                        sql = "DELETE CTPhieuNhap WHERE MaPN=N'" + txtMaPN.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                sql = "DELETE PhieuNhap WHERE MaPN=N'" + txtMaPN.Text.Trim() + "'";
                Functions.RunSQLDel(sql);
                ResetValues();
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {
                if (cboMaNCC.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã NCC!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNCC.Focus();
                    return;
                }
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
                if (txtDonGia.Text == "0")
                {
                    MessageBox.Show("Bạn chưa nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGia.Focus();
                    return;
                }
                if (btn == 't')
                {
                    sql = "SELECT MaPN FROM PhieuNhap WHERE MaPN=N'" + txtMaPN.Text.Trim() + "'";
                    if (!Functions.CheckKey(sql))//Chưa có mã PN
                    {
                        sql = "INSERT INTO PhieuNhap(MaPN, ThoiGian, MaNCC) VALUES " +
                            "(N'" + txtMaPN.Text.Trim() + "',N'" + dtpThoiGian.Text.Trim() + "',N'" + cboMaNCC.SelectedValue + "')";
                        Functions.RunSQL(sql);
                    }

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

                    if (txtDonGia.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDonGia.Focus();
                        return;
                    }

                    sql = "SELECT MaGB FROM CTPhieuNhap WHERE MaGB = N'" + cboMaGB.SelectedValue + "' AND MaPN = N'" + txtMaPN.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã gấu bông này đã có, bạn phải nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        cboMaGB.Text = "";
                        return;
                    }

                    sql = "INSERT INTO CTPhieuNhap(MaPN, MaGB, SoLuong, DonGia)" +
                        " VALUES(N'" + txtMaPN.Text.Trim() + "',N'" + cboMaGB.SelectedValue + "',N'" + txtSL.Text.Trim() + "',N'" + txtDonGia.Text.Trim() + "')";
                    Functions.RunSQL(sql);
                    sql = "UPDATE GauBong SET SLN = (SLN + " + txtSL.Text.Trim() + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                    Functions.RunSQL(sql);
                    LoadDateGirdViewCT(txtMaPN.Text.Trim());
                    cboMaGB.Text = "";
                    txtTenGB.Text = "";
                    txtSL.Text = "0";
                    txtDonGia.Text = "0";
                    txtTien.Text = "0";
                    btnXoa.Enabled = true;
                }

                //Sửa
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        sql = "UPDATE CTPhieuNhap SET SoLuong=N'" + txtSL.Text.Trim() + "' WHERE MaPN=N'" + txtMaPN.Text + "' AND MaGB= N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        MessageBox.Show("Đã cập nhật số lượng mã gấu bông: " + cboMaGB.SelectedValue + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sql = "UPDATE GauBong SET SLN = (SLN + " + txtSL.Text.Trim() + " - " + soluongc + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        LoadDateGirdViewCT(txtMaPN.Text.Trim());
                     //   btnXoa.Enabled = true;
                     //   btnThem.Enabled = true;
                        btnSua.Enabled = false;
                        btnDong.Enabled = true;
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
            sql = "SELECT a.MaGB,b.TenGB,a.SoLuong,a.DonGia,a.SoLuong * a.DonGia AS ThanhTien FROM CTPhieuNhap AS a,GauBong AS b WHERE a.MaPN = N'" + str + "' AND a.MaGB=b.MaGB";

            tblPNCT = Functions.GetDataToTable(sql);
            dgvPNCT.DataSource = tblPNCT;

            dgvPNCT.Columns[0].HeaderText = "Mã gấu bông";
            dgvPNCT.Columns[1].HeaderText = "Tên gấu bông";
            dgvPNCT.Columns[2].HeaderText = "Số lượng";
            dgvPNCT.Columns[3].HeaderText = "Đơn giá";
            dgvPNCT.Columns[4].HeaderText = "Thành tiền";

            dgvPNCT.Columns[0].Width = 150;
            dgvPNCT.Columns[1].Width = 150;
            dgvPNCT.Columns[2].Width = 150;
            dgvPNCT.Columns[3].Width = 150;
            dgvPNCT.Columns[4].Width = 250;
            dgvPNCT.AllowUserToAddRows = false;
            dgvPNCT.EditMode = DataGridViewEditMode.EditProgrammatically;
            TongTien();
        }

        private void TongTien()
        {
            String sql;

            if (dgvPNCT.Rows.Count > 0)
            {
                sql = "SELECT SUM(SoLuong * DonGia) FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text.Trim() + "'";
                txtTongTien.Text = Functions.GetFieldValues(sql);
            }
            else txtTongTien.Text = "0";
            lblTien.Text = Functions.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }

        private void cboMaNCC_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaNCC.Text == "")
            {
                txtTenNCC.Text = "";
                txtDiaChiNCC.Text = "";
                txtSDTNCC.Text = "";
            }
            // Khi chọn Mã NCC thì các trường khác tự động hiện ra
            sql = "Select TenNCC from NCC where MaNCC =N'" + cboMaNCC.SelectedValue + "'";
            txtTenNCC.Text = Functions.GetFieldValues(sql);
            sql = "Select DiaChi from NCC where MaNCC =N'" + cboMaNCC.SelectedValue + "'";
            txtDiaChiNCC.Text = Functions.GetFieldValues(sql);
            sql = "Select SDT from NCC where MaNCC =N'" + cboMaNCC.SelectedValue + "'";
            txtSDTNCC.Text = Functions.GetFieldValues(sql);
        }

        private void cboMaGB_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaGB.Text == "")
            {
                txtTenGB.Text = "";
            }
            sql = "Select TenGB from GauBong where MaGB =N'" + cboMaGB.SelectedValue + "'";
            txtTenGB.Text = Functions.GetFieldValues(sql);
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

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            txtSL_TextChanged(sender, e);
        }

        private void frmPN_Load(object sender, EventArgs e)
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
            Functions.FillCombo("SELECT MaNCC, TenNCC FROM NCC", cboMaNCC, "MaNCC", "MaNCC");
            cboMaNCC.SelectedIndex = -1;
            if (flag == 's')
            {
                btnThem.Enabled = false;
                txtMaPN.Text = strMaPN;
                cboMaNCC.Text = Functions.GetFieldValues("SELECT MaNCC FROM PhieuNhap WHERE MaPN = N'" + strMaPN + "'");
                cboMaNCC_TextChanged(sender, e);
                LoadDateGirdViewCT(strMaPN);
            }
            else if (flag == 'd')
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                txtMaPN.Text = strMaPN;
                
                cboMaNCC.Text = Functions.GetFieldValues("SELECT MaNCC FROM PhieuNhap WHERE MaPN = N'" + strMaPN + "'");
                cboMaNCC_TextChanged(sender, e);
                LoadDateGirdViewCT(strMaPN);
            }
            else
            {
                string str = "";
                LoadDateGirdViewCT(str);
                btnXoa.Enabled = false;
            }
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void dgvPNCT_Click(object sender, EventArgs e)
        {
            //if (btnThem.Enabled == false)
            //{
            //    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cboMaGB.Focus();
            //    return;
            //}
            if (flag == 's')
            {
                if (tblPNCT.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                cboMaGB.Text = dgvPNCT.CurrentRow.Cells[0].Value.ToString();
                txtSL.Text = dgvPNCT.CurrentRow.Cells[2].Value.ToString();
                txtDonGia.Text = dgvPNCT.CurrentRow.Cells[3].Value.ToString();

                cboMaGB_TextChanged(sender, e);
                txtSL_TextChanged(sender, e);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnDong.Enabled = true;
                Disable();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng phiếu nhập không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void dgvPNCT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaGB, sql;
            double sl, slcon, slxoa;
            if(flag == 't' || flag == 's')
            {
                if (tblPNCT.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    //Xóa mã gấu bông và cập nhật lại số lượng
                    MaGB = dgvPNCT.CurrentRow.Cells["MaGB"].Value.ToString(); // lấy mã G

                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN FROM GauBong WHERE MaGB = N'" + MaGB + "'"));
                    slxoa = Convert.ToDouble(dgvPNCT.CurrentRow.Cells["SoLuong"].Value.ToString());

                    slcon = sl - slxoa;
                    sql = "UPDATE GauBong SET SLN =" + slcon + " WHERE MaGB= N'" + MaGB + "'";
                    Functions.RunSQL(sql);

                    sql = "DELETE CTPhieuNhap WHERE MaPN=N'" + txtMaPN.Text + "' AND MaGB = N'" + MaGB + "'";
                    Functions.RunSQL(sql);

                    LoadDateGirdViewCT(txtMaPN.Text);
                }
            }
        }

        private void Enable()
        {
            cboMaNCC.Enabled = true;
            cboMaGB.Enabled = true;
            txtSL.Enabled = true;
            txtDonGia.Enabled = true;
            dtpThoiGian.Enabled = true;
        }
    }
}
