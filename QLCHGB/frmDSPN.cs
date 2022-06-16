using QLCHGB.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmDSPN : Form
    {
        DataTable tblPN;

        public string user = "";
        public String strMaPN;
        public Char flag;
        public frmDSPN()
        {
            InitializeComponent();
        }

        private void btnLapPN_Click(object sender, EventArgs e)
        {
            frmPN frmPN = new frmPN();
            frmPN.user = user;
            frmPN.MdiParent = this.ParentForm;
            frmPN.Dock = DockStyle.Fill;
            frmPN.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cboMaPN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strMaPN = cboMaPN.Text.Trim();
            frmPN frmPN = new frmPN();
            frmPN.MdiParent = this.ParentForm;
            frmPN.strMaPN = strMaPN;
            frmPN.user = user;
            frmPN.flag = 's';
            frmPN.Dock = DockStyle.Fill;
            frmPN.Show();
        }

        private void frmDSPN_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            Functions.FillCombo("SELECT MaPN FROM PhieuNhap", cboMaPN, "MaPN", "MaPN");
            cboMaPN.SelectedIndex = -1;
            ResetValues();
        }

        private void dgvPN_Click(object sender, EventArgs e)
        {
            if (tblPN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMaPN.Text = dgvPN.CurrentRow.Cells[0].Value.ToString();
            dtpThoiGian.Text = dgvPN.CurrentRow.Cells[1].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if (ckbThoiGian.Checked)
                sql = "Select * From ViewDSPN Where MaPN Like N'%" + cboMaPN.Text.Trim() + "%' and ThoiGian ='" + dtpThoiGian.Text.Trim() + "' order by ThoiGian desc";
            else
                sql = "Select * From ViewDSPN Where MaPN Like N'%" + cboMaPN.Text.Trim() + "%' order by ThoiGian desc";
            tblPN = Functions.GetDataToTable(sql);

            if (tblPN.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblPN.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvPN.DataSource = tblPN;
        }
        private void ResetValues()
        {
            cboMaPN.Text = "";
            dtpThoiGian.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblPN.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaPN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "SELECT MaGB,SoLuong FROM CTPhieuNhap WHERE MaPN = N'" + cboMaPN.Text.Trim() + "'";

                DataTable table = Functions.GetDataToTable(sql);
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa;

                        sql = "SELECT MaGB,SoLuong FROM CTPhieuNhap WHERE MaPN = N'" + cboMaPN.Text + "'";
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
                        sql = "DELETE CTPhieuNhap WHERE MaPN=N'" + cboMaPN.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();                
                    }
                }
                sql = "DELETE PhieuNhap WHERE MaPN=N'" + cboMaPN.Text.Trim() + "'";
                Functions.RunSQLDel(sql);
                LoadDataGridView();
                ResetValues();
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            frmDSPN_Load(sender, e);
        }

        private void dgvPN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            strMaPN = cboMaPN.Text.Trim();
            frmPN frmPN = new frmPN();
            frmPN.MdiParent = this.ParentForm;
            frmPN.strMaPN = strMaPN;
            frmPN.flag = 'd'; //CellDoubleClick
            frmPN.Dock = DockStyle.Fill;
            frmPN.Show();
        }

        private void ckbThoiGian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadDataGridView()
        {
            String sql;
            sql = @"
                SELECT a.MaPN,a.ThoiGian,a.MaNCC,b.TenNCC,c.Tong,a.Username
                FROM PhieuNhap AS a
                Left join NCC AS b on a.MaNCC = b.MaNCC
                Left join ViewTongTien as c on a.MaPN = c.MaPN
                order by a.ThoiGian desc";
            tblPN = Functions.GetDataToTable(sql);
            dgvPN.DataSource = tblPN;
            dgvPN.Columns[0].HeaderText = "Mã phiếu nhập";
            dgvPN.Columns[1].HeaderText = "Thời gian";
            dgvPN.Columns[2].HeaderText = "Mã nhà cung cấp";
            dgvPN.Columns[3].HeaderText = "Tên nhà cung cấp";
            dgvPN.Columns[4].HeaderText = "Tổng tiền";
            dgvPN.Columns[5].HeaderText = "Người lập phiếu nhập";

            dgvPN.Columns[0].Width = 200;
            dgvPN.Columns[1].Width = 200;
            dgvPN.Columns[2].Width = 200;
            dgvPN.Columns[3].Width = 250;
            dgvPN.Columns[4].Width = 250;
            dgvPN.Columns[5].Width = 250;

            dgvPN.AllowUserToAddRows = false;
            dgvPN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
