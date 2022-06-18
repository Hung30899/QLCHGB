using QLCHGB.Class;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmMainMenu : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;

        public string user="";

        public string loai="";
        public frmMainMenu()
        {
            InitializeComponent();
            random = new Random();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            if (user != "")
            {
                btnLogout.Text = "   " + user;
            }
            if (loai == "Nhân viên")
            {
                btnPN.Visible = false;
                btnND.Visible = false;
                btnGiaBan.Visible = false;
                btnNCC.Visible = false;
                btnGB.Visible = false;
                btnTK.Visible = false;
            }
            else
            {
                btnPN.Visible = true;
                btnND.Visible = true;
                btnGiaBan.Visible = true;
                btnNCC.Visible = true;
                btnGB.Visible = true;
                btnTK.Visible = true;
            }
  
        }

        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while(tempIndex == index)
            {
               index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Mongolian Baiti", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    btnLOGO.BackColor = ThemeColor.ChangeColorBrightness(color,-0.5);
                }
            }
        }

        private void DisableButton()
        {
            foreach(Control previousBtn in panelMenu.Controls)
            {
                if(previousBtn.GetType()== typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Mongolian Baiti", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void btnGB_Click(object sender, EventArgs e)
        {
            lblHome.Text = "GẤU BÔNG";
            Functions.CloseForm(this);
            frmGB frmGB = new frmGB();
            Functions.MenuClick(frmGB, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);
           
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            lblHome.Text = "NHÀ CUNG CẤP";
            Functions.CloseForm(this);
            frmNCC frmNCC = new frmNCC();
            Functions.MenuClick(frmNCC, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);

        }

        private void btnPN_Click(object sender, EventArgs e)
        {
            lblHome.Text = "PHIẾU NHẬP";
            Functions.CloseForm(this);
            frmDSPN frmDSPN = new frmDSPN();
            frmDSPN.user = user;
            Functions.MenuClick(frmDSPN, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);


        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            lblHome.Text = "HÓA ĐƠN";
            Functions.CloseForm(this);
            frmDSHD frmDSHD = new frmDSHD();
            frmDSHD.user = user;
            Functions.MenuClick(frmDSHD, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            lblHome.Text = "THỐNG KÊ";
            Functions.CloseForm(this);
            frmDSTK frmDSTK = new frmDSTK();
            Functions.MenuClick(frmDSTK, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);

        }

        private void btnGiaBan_Click(object sender, EventArgs e)
        {
            lblHome.Text = "GIÁ BÁN";
            Functions.CloseForm(this);
            frmGiaBan frmGiaBan = new frmGiaBan();
            Functions.MenuClick(frmGiaBan, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);

        }

        private void btnND_Click(object sender, EventArgs e)
        {
            lblHome.Text = "NGƯỜI DÙNG";
            Functions.CloseForm(this);
            frmND frmND = new frmND();
            Functions.MenuClick(frmND, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);
        }

        private void btnLOGO_Click(object sender, EventArgs e)
        {
            lblHome.Text = "HOME";
            Functions.CloseForm(this);
            panelDesktop.Visible = true;
            ActivateButton(sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            btnLogout.Text = "";
            this.Hide();
            frmMain frm = new frmMain();
            frm.Show();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            lblHome.Text = "KHÁCH HÀNG";
            Functions.CloseForm(this);
            frmKH frmKH = new frmKH();
            Functions.MenuClick(frmKH, this);
            panelDesktop.Visible = false;
            ActivateButton(sender);
        }
    }
}
