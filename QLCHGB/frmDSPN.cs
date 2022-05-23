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
    public partial class frmDSPN : Form
    {
        DataTable tblPN;
        char btn;
        String rbn, soluongc;

        public String strMaPN;
        public Char flag;
        public frmDSPN()
        {
            InitializeComponent();
        }

        private void btnLapPN_Click(object sender, EventArgs e)
        {      
            frmPN frmPN = new frmPN();
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
            //Disable();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if (ckbThoiGian.Checked)    
                sql = "Select * From ViewDSPN Where MaPN Like N'%"+cboMaPN.Text.Trim()+"%' and ThoiGian ='"+dtpThoiGian.Text.Trim()+"'";
            else
                sql = "Select * From ViewDSPN Where MaPN Like N'%"+cboMaPN.Text.Trim()+"%'";
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

        private void LoadDataGridView()
        {
            String sql;
            sql = "SELECT a.MaPN,a.ThoiGian,a.MaNCC,b.TenNCC,c.Tong " +
                "FROM PhieuNhap AS a, NCC AS b,ViewTongTien as c " +
                "WHERE a.MaNCC = b.MaNCC and a.MaPN = c.MaPN";

            tblPN = Functions.GetDataToTable(sql);
            dgvPN.DataSource = tblPN;
            dgvPN.Columns[0].HeaderText = "Mã phiếu nhập";
            dgvPN.Columns[2].HeaderText = "Thời gian";
            dgvPN.Columns[1].HeaderText = "Mã nhà cung cấp";
            dgvPN.Columns[3].HeaderText = "Tên nhà cung cấp";
            dgvPN.Columns[4].HeaderText = "Tổng tiền";

            dgvPN.Columns[0].Width = 200;
            dgvPN.Columns[1].Width = 200;
            dgvPN.Columns[2].Width = 200;
            dgvPN.Columns[3].Width = 250;
            dgvPN.Columns[4].Width = 250;

            dgvPN.AllowUserToAddRows = false;
            dgvPN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
