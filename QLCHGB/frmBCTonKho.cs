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
    public partial class frmBCTonKho : Form
    {
        DataTable tblGB;
        public frmBCTonKho()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
           
                LoadDataGridView();
            
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaGB,TenGB,LoaiGB,SLN,SLB, SLN-SLB as SL FROM GauBong";
            if (rbnHangCon.Checked)
            {
                sql = sql + " Where SLN-SLB > 0";
            }
            if (rbnHaHet.Checked)
            {
                sql = sql + " Where SLN-SLB = 0";
            }

            tblGB = Functions.GetDataToTable(sql);
            dgvGB.DataSource = tblGB;
            dgvGB.Columns[0].HeaderText = "Mã gấu bông";
            dgvGB.Columns[1].HeaderText = "Tên gấu bông";
            dgvGB.Columns[2].HeaderText = "Loại gấu bông";
            dgvGB.Columns[3].HeaderText = "Số lượng nhập";
            dgvGB.Columns[4].HeaderText = "Số lượng bán";
            dgvGB.Columns[5].HeaderText = "Tồn kho";



            dgvGB.Columns[0].Width = 150;
            dgvGB.Columns[1].Width = 150;
            dgvGB.Columns[2].Width = 150;
            dgvGB.Columns[3].Width = 150;
            dgvGB.Columns[4].Width = 150;
            dgvGB.Columns[5].Width = 150;

            dgvGB.AllowUserToAddRows = false;
            dgvGB.EditMode = DataGridViewEditMode.EditProgrammatically;

            foreach (DataGridViewRow row in dgvGB.Rows)
            {
                if (row.Cells[5].Value.ToString() == "0")
                {
                    dgvGB.Rows[row.Index].DefaultCellStyle.ForeColor = Color.OrangeRed;
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
