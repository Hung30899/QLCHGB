using QLCHGB.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmKH : Form
    {
        DataTable tblKH;
        char btn, rbt;
        public frmKH()
        {
            InitializeComponent();
        }

        private void frmKH_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            LoadDataGridView();
            Disable();
            rbnUser_CheckedChanged(sender, e);
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM KhachHang";

            tblKH = Functions.GetDataToTable(sql);
            dgvKH.DataSource = tblKH;
            dgvKH.Columns[0].HeaderText = "Mã khách hàng";
            dgvKH.Columns[1].HeaderText = "Tên khách hàng";
            dgvKH.Columns[2].HeaderText = "Địa chỉ";
            dgvKH.Columns[3].HeaderText = "Số điện thoại";

            dgvKH.Columns[0].Width = 200;
            dgvKH.Columns[1].Width = 200;
            dgvKH.Columns[2].Width = 200;
            dgvKH.Columns[3].Width = 250;

            dgvKH.AllowUserToAddRows = false;
            dgvKH.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        // Ẩn các txt
        private void Disable()
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtSDTKH.Enabled = false;
            txtDiaChiKH.Enabled = false;
        }

        private void rbnUser_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 'm';
            sql = "SELECT MaKH fROM KhachHang";
            Functions.FillCombo(sql, cboSearch, "MaKH", "MaKH");
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
            txtMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            Enable();
            txtMaKH.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE KhachHang WHERE MaKH='" + txtMaKH.Text.Trim() + "'";
                Functions.RunSQLDel(sql);
                LoadDataGridView();
                ResetValues();
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void rbnTen_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 't';
            sql = "SELECT TenKH fROM KhachHang";
            Functions.FillCombo(sql, cboSearch, "TenKH", "TenKH");
            cboSearch.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;

            if (rbt == 'm')
            {
                sql = "SELECT * FROM KhachHang WHERE MaKH LIKE N'%" + cboSearch.Text + "%'";
                tblKH = Functions.GetDataToTable(sql);

            }
            if (rbt == 't') //Tìm kiếm theo Tên
            {
                sql = "SELECT * FROM KhachHang WHERE TenKH LIKE N'%" + cboSearch.Text + "%'";
                tblKH = Functions.GetDataToTable(sql);

            }
            if (tblKH.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (cboSearch.Text == "")
            {
                dgvKH.DataSource = tblKH;
                ResetValues();
                return;
            }
            else MessageBox.Show("Có " + tblKH.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvKH.DataSource = tblKH;
            ResetValues();
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

        private void dgvKH_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKH.Text = dgvKH.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dgvKH.CurrentRow.Cells[1].Value.ToString();
            txtDiaChiKH.Text = dgvKH.CurrentRow.Cells[2].Value.ToString();
            txtSDTKH.Text = dgvKH.CurrentRow.Cells[3].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            Disable();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {
                if (txtMaKH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKH.Focus();
                }
                if (txtTenKH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKH.Focus();
                }
                if (txtDiaChiKH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiaChiKH.Focus();
                }
                if (txtSDTKH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDTKH.Focus();
                }

                //Thêm 
                if (btn == 't')
                {
                    sql = "SELECT MaKH FROM KhachHang WHERE MaKH=N'" + txtMaKH.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã khách hàng này đã có, bạn phải nhập mã khách hàng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaKH.Focus();
                        txtMaKH.Text = "";
                        return;
                    }

                    sql = "INSERT INTO KhachHang VALUES" +
                        "(N'" + txtMaKH.Text.Trim() + "',N'" + txtTenKH.Text.Trim() + "',N'" + txtDiaChiKH.Text.Trim() + "',N'" + txtSDTKH.Text.Trim() + "')";
                    Functions.RunSQL(sql);
                    LoadDataGridView();
                    MessageBox.Show("Đã thêm khách hàng mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    Disable();
                }
                //Sửa 
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        sql = "UPDATE KhachHang SET TenKH=N'" + txtTenKH.Text.Trim() + "',DiaChi=N'" + txtDiaChiKH.Text.Trim() +
                            "', SDT=N'" + txtSDTKH.Text.Trim() + "' WHERE MaKH=N'" + txtMaKH.Text + "'";
                        Functions.RunSQL(sql);
                        LoadDataGridView();
                        MessageBox.Show("Đã cập nhật khách hàng mã:" + txtMaKH.Text + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtSDTKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        //Hiện các txt
        private void Enable()
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtSDTKH.Enabled = true;
            txtDiaChiKH.Enabled = true;
        }

        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDTKH.Text = "";
            txtDiaChiKH.Text = "";
        }

    }
}
