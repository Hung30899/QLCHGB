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
    public partial class frmDSHD : Form
    {
        public frmDSHD()
        {
            InitializeComponent();
        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            frmMainMenu frmMainMenu = new frmMainMenu();
            frmHD frmHD = new frmHD();
            frmHD.MdiParent = this.ParentForm;
            frmHD.Dock = DockStyle.Fill;
            frmHD.Show();
        }

        private void frmDSHD_Load(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            frmMainMenu frmMainMenu = new frmMainMenu();
            frmHD frmHD = new frmHD();
            frmHD.MdiParent = this.ParentForm;
            frmHD.Dock = DockStyle.Fill;
            frmHD.Show();
        }
    }
}
