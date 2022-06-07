using QLCHGB.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmDangNhap : Form
    {
        public string user = "";

        public frmDangNhap()
        {
            InitializeComponent();
        }
   
        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            DataTable tblLogin;
            string sql;

            if (txtUser.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập Username!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }
            if (txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập Password!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }
            sql = "SELECT * FROM NguoiDung WHERE Username =N'" + txtUser.Text.Trim() + "' AND Password=N'" + txtPass.Text.Trim() + "'";
            tblLogin = Functions.GetDataToTable(sql);

            if (tblLogin.Rows.Count > 0)
            {
                frmMainMenu frm = new frmMainMenu();
                frm.user = txtUser.Text.Trim();
                frm.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai toài khoản hoặc mật khẩu!");
                txtUser.Focus();
            }
        }

        private void btnHuybo_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMain frmMain = new frmMain();
            frmMain.Show();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            Functions.Connect();
        }
    }
}
