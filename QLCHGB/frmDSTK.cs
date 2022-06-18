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
    public partial class frmDSTK : Form
    {
        private string sql;
        public frmDSTK()
        {
            InitializeComponent();
        }

        private void btnTKHN_Click(object sender, EventArgs e)
        {
            frmTK frmTK = new frmTK();
            frmTK.loai = "Hàng nhập";
            frmTK.MdiParent = this.ParentForm;
            frmTK.Dock = DockStyle.Fill;
            frmTK.Show();
        }

        private void frmDSTK_Load(object sender, EventArgs e)
        {
            KhachHang();
            DoanhThu();
            DoanhThuHN();
            TN();
        }

        private void DoanhThu()
        {
            sql = @"select isnull(sum(b.DonGia*b.SoLuong),0) from HoaDon as a
                    Left join CTHoaDon as b on a.MaHD = b.MaHD
                    Where Year(a.ThoiGian) = Year(GETDATE())";

            lblDT.Text = Functions.GetFieldValues(sql) + " VNĐ";
        }
        private void KhachHang()
        {
            sql = "Select count(*) from KhachHang";
            lblKH.Text = Functions.GetFieldValues(sql) + " Khách hàng";
        }
        private void DoanhThuHN()
        {
            sql = @"select isnull(sum(b.DonGia*b.SoLuong),0) from HoaDon as a
                    Left join CTHoaDon as b on a.MaHD = b.MaHD
                    where a.ThoiGian = '"+DateTime.Today+"';";
            lblDTHN.Text = Functions.GetFieldValues(sql) + " VNĐ";
        }
        private void TN()
        {
            sql = @"select isnull(sum(b.DonGia*b.SoLuong),0) from PhieuNhap as a
                    Left join CTPhieuNhap as b on a.MaPN = b.MaPN
                    Where Year(a.ThoiGian) = Year(GETDATE());";
            lblTN.Text = Functions.GetFieldValues(sql) + " VNĐ";
        }

        private void btnTKDT_Click(object sender, EventArgs e)
        {
            frmTK frmTK = new frmTK();
            frmTK.loai = "Doanh thu";
            frmTK.MdiParent = this.ParentForm;
            frmTK.Dock = DockStyle.Fill;
            frmTK.Show();
        }

        private void btnBCTK_Click(object sender, EventArgs e)
        {
            frmBCTonKho frmBCTonKho = new frmBCTonKho();

            frmBCTonKho.MdiParent = this.ParentForm;
            frmBCTonKho.Dock = DockStyle.Fill;
            frmBCTonKho.Show();
        }
    }
}
