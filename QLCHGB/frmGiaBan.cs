using QLCHGB.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmGiaBan : Form
    {
        DataTable tblGiaBan; 
        Char btn, rbt;
        public frmGiaBan()
        {
            InitializeComponent();
        }

        private void frmGiaBan_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
            Disable();
            rbnMa_CheckedChanged(sender, e);
            fillCboMaGB();
        }

        private void fillCboMaGB()
        {
            string sql;
            sql = "SELECT * fROM GauBong";
            tblGiaBan = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboMaGB.DataSource = tblGiaBan;
            cboMaGB.DisplayMember = "MaGB";
            cboMaGB.ValueMember = "MaGB";
            cboMaGB.Text = "";
        }

        private void cboMaGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaGB.Text == "")
            {
                txtTen.Text = "";
            }
            // Khi chọn Mã KH thì các trường khác tự động hiện ra
            sql = "Select TenGB from GauBong where MaGB =N'" + cboMaGB.SelectedValue + "'";
            txtTen.Text = Functions.GetFieldValues(sql);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btn = 't';
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            Enable();
            cboMaGB.Focus();
        }
        private void Disable()
        {
            cboMaGB.Enabled = false;
            txtDonGia.Enabled = false;
            dtpNgay.Enabled = false;
        }

        //Hiện các txt
        private void Enable()
        {
            cboMaGB.Enabled = true;
            txtDonGia.Enabled = true;
            dtpNgay.Enabled = true;
        }

        private void ResetValues()
        {
            cboMaGB.Text = "";
            txtTen.Text = "";
            txtDonGia.Text = "";
            dtpNgay.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            if (tblGiaBan.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaGB.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            cboMaGB.Enabled = false;
            txtDonGia.Enabled = true;
            dtpNgay.Enabled = false;
        }

        private void dgvGiaBan_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tblGiaBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dtpNgay.Text = dgvGiaBan.CurrentRow.Cells["Ngay"].Value.ToString();
            cboMaGB.Text = dgvGiaBan.CurrentRow.Cells["MaGB"].Value.ToString();
            txtTen.Text = dgvGiaBan.CurrentRow.Cells["TenGB"].Value.ToString();
            txtDonGia.Text = dgvGiaBan.CurrentRow.Cells["DonGia"].Value.ToString();
            
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            Disable();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblGiaBan.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaGB.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE GiaBan WHERE Ngay=N'" + dtpNgay.Text.Trim() + "' and MaGB = N'"+cboMaGB.Text.Trim()+"'";
                Functions.RunSQLDel(sql);
                LoadDataGridView();
                ResetValues();
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {
                if (cboMaGB.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaGB.Focus();
                    return;
                }
               
                if (txtDonGia.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGia.Focus();
                    return;
                }
               
                if (btn == 't')
                {
                    sql = "SELECT * FROM GiaBan WHERE Ngay=N'" + dtpNgay.Text.Trim() + "' and MaGB=N'"+cboMaGB.Text.Trim()+"'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Đã tồn tại đơn giá ngày:"+ dtpNgay.Text.Trim()+ "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        cboMaGB.Text = "";
                        return;
                    }

                    sql = "Select Sum(DonGia*SoLuong)/Sum(Soluong) From CTPhieuNhap Where MaGB = N'"+cboMaGB.Text.Trim()+"' Group by MaGB";
                    string giaNhapTB = Functions.GetFieldValues(sql);

                    if (giaNhapTB == "")
                    {
                        MessageBox.Show("Mặt hàng này chưa có hàng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }    
                    
                    Double giaTB = Math.Round(Convert.ToDouble(giaNhapTB));
                    Double giaBan = Convert.ToDouble(txtDonGia.Text.Trim());

                    if (giaBan <= giaTB)
                    {
                        MessageBox.Show("Giá bán được thiết lập đang nhỏ hơn giá nhập trung bình: "+giaTB.ToString()+" VNĐ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDonGia.Focus();    
                        return;
                    }

                    sql = "INSERT INTO GiaBan(Ngay,MaGB,DonGia) VALUES" +
                        "(N'" + dtpNgay.Text.Trim() + "',N'" + cboMaGB.Text.Trim() + "',N'" + txtDonGia.Text.Trim() + "')";
                    MessageBox.Show("Đã đặt giá cho mã gấu bông: " + cboMaGB.Text.Trim() + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Functions.RunSQL(sql);
                    LoadDataGridView();

                    ResetValues();
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    Disable();
                    //txtMa.Enabled = false;
                }
                //Sửa 
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu thông tin chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        sql = "Select Sum(DonGia*Soluong)/Sum(Soluong) From CTPhieuNhap Where MaGB = N'" + cboMaGB.Text.Trim() + "' Group by MaGB";
                        string giaNhapTB = Functions.GetFieldValues(sql);

                        Double giaTB = Math.Round(Convert.ToDouble(giaNhapTB));
                        Double giaBan = Convert.ToDouble(txtDonGia.Text.Trim());

                        if (giaBan <= giaTB)
                        {
                            MessageBox.Show("Giá bán được thiết lập đang nhỏ hơn giá nhập trung bình: " +  giaTB.ToString() + " VNĐ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtDonGia.Focus();
                            return;
                        }

                        sql = "UPDATE GiaBan SET DonGia = "+txtDonGia.Text.Trim()+"  Where Ngay=N'" + dtpNgay.Text.Trim() + "' and MaGB=N'" + cboMaGB.Text.Trim() + "'";

                        Functions.RunSQL(sql);
                        LoadDataGridView();
                        MessageBox.Show("Đã cập nhật giá bán gấu bông mã: " + cboMaGB.Text.Trim() + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetValues();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            Disable();
        }

        private void rbnMa_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 'm';
            sql = "SELECT DISTINCT MaGB fROM GiaBan";
            tblGiaBan = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblGiaBan;
            cboSearch.DisplayMember = "MaGB";
            cboSearch.ValueMember = "MaGB";
            cboSearch.Text = "";
        }

        private void rbnTen_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 't';
            sql = "SELECT DISTINCT GiaBan.MaGB, GauBong.TenGB " +
                "FROM GiaBan " +
                "LEFT JOIN GauBong " +
                "ON GiaBan.MaGB = GauBong.MaGB";
            tblGiaBan = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblGiaBan;
            cboSearch.DisplayMember = "TenGB";
            cboSearch.ValueMember = "TenGB";
            cboSearch.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            
            if (rbt == 'm') //Tìm kiếm theo mã GH
            {
                sql = "SELECT a.Ngay, a.MaGB, b.TenGB, a.DonGia" +
                    " FROM GiaBan as a, GauBong as b Where a.MaGB = b.MaGB and a.MaGB Like N'%"+cboSearch.Text.Trim()+"%' Order by a.Ngay desc";
                tblGiaBan = Functions.GetDataToTable(sql);
            }
            if (rbt == 't') //Tìm kiếm theo Tên GH
            {
                sql = "SELECT a.Ngay, a.MaGB, b.TenGB, a.DonGia" +
                    " FROM GiaBan as a, GauBong as b Where a.MaGB = b.MaGB and b.TenGB Like N'%" + cboSearch.Text.Trim() + "%' Order by a.Ngay desc";
                tblGiaBan = Functions.GetDataToTable(sql);
            }
            if (tblGiaBan.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblGiaBan.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvGiaBan.DataSource = tblGiaBan;
            ResetValues();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.Ngay, a.MaGB, b.TenGB, a.DonGia FROM GiaBan as a, GauBong as b Where a.MaGB = b.MaGB Order by a.Ngay DESC";
            tblGiaBan = Functions.GetDataToTable(sql); //Lấy dữ liệu
            dgvGiaBan.DataSource = tblGiaBan; //Hiển thị vào dataGridView
            dgvGiaBan.Columns[0].HeaderText = "Ngày";
            dgvGiaBan.Columns[1].HeaderText = "Mã gấu bông";
            dgvGiaBan.Columns[2].HeaderText = "Tên gấu bông";
            dgvGiaBan.Columns[3].HeaderText = "Đơn giá";
            
            dgvGiaBan.Columns[0].Width = 150;
            dgvGiaBan.Columns[1].Width = 150;
            dgvGiaBan.Columns[2].Width = 150;
            dgvGiaBan.Columns[3].Width = 150;

            dgvGiaBan.AllowUserToAddRows = false;
            dgvGiaBan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
