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
            //Disable();
            //rbnUser_CheckedChanged(sender, e);
        }

        private void cboMaGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaGB.Text == "")
            {
                txtTen.Text = "";
            }
            // Khi chọn Mã KH thì các trường khác tự động hiện ra
            sql = "Select TenKH from GauBong where MaGB =N'" + cboMaGB.SelectedValue + "'";
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
            txtTen.Enabled = false;
            txtDonGia.Enabled = false;
            dtpNgay.Enabled = false;
        }

        //Hiện các txt
        private void Enable()
        {
            cboMaGB.Enabled = true;
            txtTen.Enabled = true;
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

        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.Ngay, a.MaGB, b.TenGB, a.DonGia FROM GiaBan as a, GauBong as b Where a.MaGB = b.MaGB";
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
