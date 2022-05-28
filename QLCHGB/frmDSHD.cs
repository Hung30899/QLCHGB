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
    public partial class frmDSHD : Form
    {
        DataTable tblHD;
        char btn;
        String rbn, soluongc;

        public String strMaHD;
        public Char flag;
        public frmDSHD()
        {
            InitializeComponent();
        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            frmHD frmHD = new frmHD();
            frmHD.MdiParent = this.ParentForm;
            frmHD.Dock = DockStyle.Fill;
            frmHD.Show();
        }

        private void frmDSHD_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            Functions.FillCombo("SELECT MaHD FROM PhieuNhap", cboMaHD, "MaHD", "MaHD");
            cboMaHD.SelectedIndex = -1;
            ResetValues();
        }

        private void ResetValues()
        {
            cboMaHD.Text = "";
            dtpThoiGian.Text = "";
        }

        private void LoadDataGridView()
        {
            String sql;
            sql = @"
                SELECT a.MaHD,a.ThoiGian,c.Tong
                FROM HoaDon AS a
                Left join ViewTongTienHD as c on a.MaHD = c.MaHD";
            tblHD = Functions.GetDataToTable(sql);
            dgvHD.DataSource = tblHD;
            dgvHD.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHD.Columns[1].HeaderText = "Thời gian";
            dgvHD.Columns[2].HeaderText = "Tổng tiền";

            dgvHD.Columns[0].Width = 200;
            dgvHD.Columns[1].Width = 200;
            dgvHD.Columns[2].Width = 250;

            dgvHD.AllowUserToAddRows = false;
            dgvHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + cboMaHD.Text.Trim() + "'";
                    DataTable table = Functions.GetDataToTable(sql);
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa;

                        sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + cboMaHD.Text + "'";
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
                        sql = "DELETE CTHoaDon WHERE MaHD=N'" + cboMaHD.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                    }
                    sql = "DELETE HoaDon WHERE MaHD=N'" + cboMaHD.Text.Trim() + "'";
                    Functions.RunSQLDel(sql);

                    LoadDataGridView();
                    ResetValues();
                    cboMaHD.Text = "";
                }
            }
        }

        private void dgvHD_Click(object sender, EventArgs e)
        {
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMaHD.Text = dgvHD.CurrentRow.Cells[0].Value.ToString();
            dtpThoiGian.Text = dgvHD.CurrentRow.Cells[1].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            frmDSHD_Load(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strMaHD = cboMaHD.Text.Trim();
            frmHD frm = new frmHD();
            frm.MdiParent = this.ParentForm;
            frm.strMaHD = strMaHD;
            frm.flag = 's';
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
    }
}
