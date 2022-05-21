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
    public partial class frmHD : Form
    {
        public frmHD()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmMainMenu frmMainMenu = new frmMainMenu();
            frmPN frmPN = new frmPN();
            frmPN.MdiParent = this.ParentForm;
            frmPN.Dock = DockStyle.Fill;
            frmPN.Show();
        }
    }
}
