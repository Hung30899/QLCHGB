using QLCHGB.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmND : Form
    {
        DataTable tblUser; // Lưu dữ liệu bảng người dùng;
        Char btn, rbt;
        public frmND()
        {
            InitializeComponent();
        }

        private void frmND_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
            Disable();
            rbnUser_CheckedChanged(sender, e);
        }
        private void Disable()
        {
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            txtHoTen.Enabled = false;
            txtSDT.Enabled = false;
            cboLoai.Enabled = false;
            txtDiaChi.Enabled = false;
            dtpNgaySinh.Enabled = false;

            rbnNam.Enabled = false;
            rbnNu.Enabled = false;
        }

        private void rbnUser_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 'u';
            sql = "SELECT * fROM NguoiDung";
            tblUser = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblUser;
            cboSearch.DisplayMember = "Username";
            cboSearch.ValueMember = "Username";
            cboSearch.Text = "";
        }

        private void rbnTen_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 'n';
            sql = "SELECT * fROM NguoiDung";
            tblUser = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblUser;
            cboSearch.DisplayMember = "Ten";
            cboSearch.ValueMember = "Ten";
            cboSearch.Text = "";
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
            txtUser.Focus();
        }
        private void ResetValues()
        {
            txtUser.Text = "";
            txtPass.Text = "";
            txtDiaChi.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            cboLoai.Text = "";
            dtpNgaySinh.Text = "";
            rbnNam.Checked = true;
        }
        private void Enable()
        {
            txtUser.Enabled = true;
            txtPass.Enabled = true;
            txtHoTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            cboLoai.Enabled = true;
            dtpNgaySinh.Enabled = true;

            rbnNam.Enabled = true;
            rbnNu.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            if (tblUser.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUser.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUser.Text == "Admin")
            {
                MessageBox.Show("Bạn không thể sửa tài khoản Admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            Enable();
            txtUser.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblUser.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUser.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUser.Text == "Admin")
            {
                MessageBox.Show("Bạn không thể xóa tài khoản Admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE NguoiDung WHERE Username=N'" + txtUser.Text + "'";
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
            string sql, gt;
            {
                if (txtUser.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập Username!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Focus();
                    return;
                }
                if (txtPass.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập password!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return;
                }

                if (txtHoTen.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (cboLoai.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn loại tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLoai.Focus();
                    return;
                }
                if (txtDiaChi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiaChi.Focus();
                    return;
                }
             
                if (txtSDT.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }
                if (rbnNam.Checked == true)
                    gt = "Nam";
                else
                    gt = "Nữ";
                //Thêm người dùng
                if (btn == 't')
                {
                    sql = "SELECT Username FROM NguoiDung WHERE Username=N'" + txtUser.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Username này đã có, bạn phải nhập Username khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Focus();
                        txtUser.Text = "";
                        return;
                    }

                    sql = "INSERT INTO NguoiDung(Username,Password,Ten,Loai,NgaySinh,GioiTinh,DiaChi,SDT) VALUES" +
                        "(N'" + txtUser.Text.Trim() + "',N'" + txtPass.Text.Trim() + "'," +
                        "N'"+ txtHoTen.Text.Trim() + "',N'"+cboLoai.Text.Trim()+"','" + dtpNgaySinh.Text + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + txtSDT.Text.Trim() + "')";

                    Functions.RunSQL(sql);
                    LoadDataGridView();
                    MessageBox.Show("Đã thêm người dùng mới: " + txtUser.Text + " !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    Disable();
                }
                //Sửa người dùng
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        sql = "UPDATE NguoiDung SET Password=N'" + txtPass.Text.Trim() + "',"+
                        "Ten=N'" + txtHoTen.Text.Trim() + "',Loai = N'"+cboLoai.Text.Trim()+"',NgaySinh='" + dtpNgaySinh.Text + "'," +
                        "GioiTinh=N'" + gt + "',DiaChi=N'" + txtDiaChi.Text.Trim() + "',SDT=N'" + txtSDT.Text.Trim() + "' WHERE Username=N'" + txtUser.Text + "'";

                        Functions.RunSQL(sql);
                        LoadDataGridView();
                        MessageBox.Show("Đã cập nhật User: " + txtUser.Text + " !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvUser_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser.Focus();
                return;
            }
            if (tblUser.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtUser.Text = dgvUser.CurrentRow.Cells[0].Value.ToString();
            txtPass.Text = dgvUser.CurrentRow.Cells[1].Value.ToString();
            txtHoTen.Text = dgvUser.CurrentRow.Cells[2].Value.ToString();
            cboLoai.Text = dgvUser.CurrentRow.Cells[3].Value.ToString();
            dtpNgaySinh.Text = dgvUser.CurrentRow.Cells[4].Value.ToString();
            if (dgvUser.CurrentRow.Cells[4].Value.ToString() == "Nam")
                rbnNam.Checked = true;
            else 
                rbnNu.Checked = true;
            txtDiaChi.Text = dgvUser.CurrentRow.Cells[5].Value.ToString();
            txtSDT.Text = dgvUser.CurrentRow.Cells[6].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            Disable();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;

            if (rbt == 'u') //Tìm kiếm theo Username
            {
                sql = "SELECT * FROM NguoiDung WHERE Username LIKE N'%" + cboSearch.Text + "%'";
                tblUser = Functions.GetDataToTable(sql);
            }
            if (rbt == 'n') //Tìm kiếm theo Tên
            {
                sql = "SELECT * FROM NguoiDung WHERE Ten LIKE N'%" + cboSearch.Text + "%'";
                tblUser = Functions.GetDataToTable(sql);
            }
            if (tblUser.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblUser.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgvUser.DataSource = tblUser;
            ResetValues();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void LoadDataGridView()
        {

            string sql;
            sql = "SELECT * FROM NguoiDung";
            tblUser = Functions.GetDataToTable(sql); //Lấy dữ liệu
            dgvUser.DataSource = tblUser; //Hiển thị vào dataGridView
            dgvUser.Columns[0].HeaderText = "Username";
            dgvUser.Columns[1].HeaderText = "Password";
            dgvUser.Columns[2].HeaderText = "Họ tên";
            dgvUser.Columns[3].HeaderText = "Loại tài khoản";
            dgvUser.Columns[4].HeaderText = "Ngày Sinh";
            dgvUser.Columns[5].HeaderText = "Giới tính";
            dgvUser.Columns[6].HeaderText = "Địa chỉ";
            dgvUser.Columns[7].HeaderText = "SDT";

            dgvUser.Columns[0].Width = 150;
            dgvUser.Columns[1].Width = 150;
            dgvUser.Columns[2].Width = 150;
            dgvUser.Columns[3].Width = 150;
            dgvUser.Columns[4].Width = 150;
            dgvUser.Columns[5].Width = 150;
            dgvUser.Columns[6].Width = 150;
            dgvUser.Columns[7].Width = 150;

            dgvUser.AllowUserToAddRows = false;
            dgvUser.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
