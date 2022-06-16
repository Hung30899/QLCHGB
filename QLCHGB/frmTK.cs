using QLCHGB.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmTK : Form
    {
        DataTable tblTK;
        string sql;
        public string loai;
        public frmTK()
        {
            InitializeComponent();
        }

        private void frmTK_Load(object sender, EventArgs e)
        {
            rbnNgay_CheckedChanged(sender,e);
            Functions.FillCombo("SELECT Distinct Year(ThoiGian) as year From HoaDon", cboNam, "year", "year");
            cboNam.SelectedIndex = -1;
            if (loai=="Doanh thu")
            {
                lblTieuDe.Text = "THỐNG KÊ DOANH THU:";
                lblTongSL.Text = "Tống số lượng bán:";
            }
            else
            {
                lblTieuDe.Text = "THỐNG KÊ HÀNG NHẬP:";
                lblTongSL.Text = "Tống số lượng nhập:";
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (loai == "Doanh thu")
            {
                TKDT();
            }
            else
            {
                TKHN();
            }
        }

        private void TKDT()
        {
            string s = "";

            if (rbnNgay.Checked)
            {
                s = "Where a.ThoiGian >= '" + dtpStartDate.Text.Trim() + "' and a.ThoiGian <= '" + dtpEndDate.Text.Trim() + "'";
                lbl.Text = "Thời gian từ: " + dtpStartDate.Text.Trim() + "      đến: " + dtpEndDate.Text.Trim() + "";
            }
            if (rbnThang.Checked)
            {
                s = "Where Month(a.ThoiGian) = Month('" + dtpThang.Text.Trim() + "')";

                lbl.Text = "Tháng: " + Functions.GetFieldValues("Select month('" + dtpThang.Text.Trim() + "')");
            }

            if (rbnQuy.Checked)
            {
                if (cboQuy.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa chọn quý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cboQuy.Text.Trim() == "Quý 1")
                {
                    s = "Where Month(a.ThoiGian) >= 1 and  Month(a.ThoiGian) <=3";
                }
                if (cboQuy.Text.Trim() == "Quý 2")
                {
                    s = "Where Month(a.ThoiGian) >= 4 and  Month(a.ThoiGian) <=6";
                }
                if (cboQuy.Text.Trim() == "Quý 3")
                {
                    s = "Where Month(a.ThoiGian) >= 7 and  Month(a.ThoiGian) <=9";
                }
                if (cboQuy.Text.Trim() == "Quý 4")
                {
                    s = "Where Month(a.ThoiGian) >= 10 and  Month(a.ThoiGian) <=12";
                }

                lbl.Text = cboQuy.Text.Trim();
            }
            if (rbnNam.Checked)
            {
                if (cboNam.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa chọn năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                s = "Where Year(a.ThoiGian) = " + cboNam.Text.Trim() + "";
                lbl.Text = "Năm: " + cboNam.Text.Trim();
            }


            sql = @"Select c.TenGB,b.MaGB,a.MaHD,a.ThoiGian,b.SoLuong,b.DonGia, Sum(b.SoLuong*b.DonGia) as Tong
                    From HoaDon as a
                    left join CTHoaDon as b on a.MaHD =b.MaHD 
                    left join GauBong as c on b.MaGB = c.MaGB " + s +
                    " Group by c.TenGB,b.MaGB, b.SoLuong,b.DonGia,a.MaHD,a.ThoiGian ";

            tblTK = Functions.GetDataToTable(sql);
            dgvTK.DataSource = tblTK;
            dgvTK.Columns[0].HeaderText = "Tên gấu bông";
            dgvTK.Columns[1].HeaderText = "Mã gấu bông";
            dgvTK.Columns[2].HeaderText = "Mã hóa đơn";
            dgvTK.Columns[3].HeaderText = "Thời gian";
            dgvTK.Columns[4].HeaderText = "Số lượng";
            dgvTK.Columns[5].HeaderText = "Đơn giá";
            dgvTK.Columns[6].HeaderText = "Tổng";

            dgvTK.AllowUserToAddRows = false;
            dgvTK.EditMode = DataGridViewEditMode.EditProgrammatically;

            tblTK = Functions.GetDataToTable(sql);
            dgvTK.DataSource = tblTK;

            if (dgvTK.Rows.Count > 0)
            {
                sql = @"Select Sum(b.SoLuong*b.DonGia) as Tong
                    From HoaDon as a
                    left join CTHoaDon as b on a.MaHD =b.MaHD 
                    left join GauBong as c on b.MaGB = c.MaGB " + s + "";
                lblTongTien.Text = Functions.GetFieldValues(sql);

                sql = @"Select Sum(b.SoLuong)
                    From HoaDon as a
                    left join CTHoaDon as b on a.MaHD =b.MaHD 
                    left join GauBong as c on b.MaGB = c.MaGB " + s + "";
                lblSLB.Text = Functions.GetFieldValues(sql);
            }
            else
            {
                lblTongTien.Text = "0";
                lblSLB.Text = "0";
            }
            lblTien.Text = Functions.ChuyenSoSangChuoi(double.Parse(lblTongTien.Text));
        }

        private void TKHN()
        {
            string s = "";

            if (rbnNgay.Checked)
            {
                s = "Where a.ThoiGian >= '" + dtpStartDate.Text.Trim() + "' and a.ThoiGian <= '" + dtpEndDate.Text.Trim() + "'";
                lbl.Text = "Thời gian từ: " + dtpStartDate.Text.Trim() + "      đến: " + dtpEndDate.Text.Trim() + "";
            }
            if (rbnThang.Checked)
            {
                s = "Where Month(a.ThoiGian) = Month('" + dtpThang.Text.Trim() + "')";

                lbl.Text = "Tháng: " + Functions.GetFieldValues("Select month('" + dtpThang.Text.Trim() + "')");
            }

            if (rbnQuy.Checked)
            {
                if (cboQuy.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa chọn quý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cboQuy.Text.Trim() == "Quý 1")
                {
                    s = "Where Month(a.ThoiGian) >= 1 and  Month(a.ThoiGian) <=3";
                }
                if (cboQuy.Text.Trim() == "Quý 2")
                {
                    s = "Where Month(a.ThoiGian) >= 4 and  Month(a.ThoiGian) <=6";
                }
                if (cboQuy.Text.Trim() == "Quý 3")
                {
                    s = "Where Month(a.ThoiGian) >= 7 and  Month(a.ThoiGian) <=9";
                }
                if (cboQuy.Text.Trim() == "Quý 4")
                {
                    s = "Where Month(a.ThoiGian) >= 10 and  Month(a.ThoiGian) <=12";
                }

                lbl.Text = cboQuy.Text.Trim();
            }
            if (rbnNam.Checked)
            {
                if (cboNam.Text.Trim() == "")
                {
                    MessageBox.Show("Bạn chưa chọn năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                s = "Where Year(a.ThoiGian) = " + cboNam.Text.Trim() + "";
                lbl.Text = "Năm: " + cboNam.Text.Trim();
            }


            sql = @"Select c.TenGB,b.MaGB,a.MaPN,a.ThoiGian,b.SoLuong,b.DonGia, Sum(b.SoLuong*b.DonGia) as Tong
                    From PhieuNhap as a
                    left join CTPhieuNhap as b on a.MaPN =b.MaPN 
                    left join GauBong as c on b.MaGB = c.MaGB " + s +
                    " Group by c.TenGB,b.MaGB, b.SoLuong,b.DonGia,a.MaPN,a.ThoiGian ";

            tblTK = Functions.GetDataToTable(sql);
            dgvTK.DataSource = tblTK;
            dgvTK.Columns[0].HeaderText = "Tên gấu bông";
            dgvTK.Columns[1].HeaderText = "Mã gấu bông";
            dgvTK.Columns[2].HeaderText = "Mã phiếu nhập";
            dgvTK.Columns[3].HeaderText = "Thời gian";
            dgvTK.Columns[4].HeaderText = "Số lượng";
            dgvTK.Columns[5].HeaderText = "Đơn giá";
            dgvTK.Columns[6].HeaderText = "Tổng";

            dgvTK.AllowUserToAddRows = false;
            dgvTK.EditMode = DataGridViewEditMode.EditProgrammatically;

            tblTK = Functions.GetDataToTable(sql);
            dgvTK.DataSource = tblTK;

            if (dgvTK.Rows.Count > 0)
            {
                sql = @"Select Sum(b.SoLuong*b.DonGia) as Tong
                    From PhieuNhap as a
                    left join CTPhieuNhap as b on a.MaPN =b.MaPN 
                    left join GauBong as c on b.MaGB = c.MaGB " + s + "";
                lblTongTien.Text = Functions.GetFieldValues(sql);

                sql = @"Select Sum(b.SoLuong)
                    From PhieuNhap as a
                    left join CTPhieuNhap as b on a.MaPN =b.MaPN 
                    left join GauBong as c on b.MaGB = c.MaGB " + s + "";
                lblSLB.Text = Functions.GetFieldValues(sql);
            }
            else
            {
                lblTongTien.Text = "0";
                lblSLB.Text = "0";
            }
            lblTien.Text = Functions.ChuyenSoSangChuoi(double.Parse(lblTongTien.Text));
        }

        private void rbnNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Visible = true;
            dtpEndDate.Visible = true;
            lblTu.Visible = true;
            lblDen.Visible = true;

            dtpThang.Visible = false;
            cboNam.Visible = false;
            cboQuy.Visible = false;

        }

        private void rbnThang_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Visible = false;
            dtpEndDate.Visible = false;
            lblTu.Visible = false;
            lblDen.Visible = false;

            dtpThang.Visible = true;
            cboNam.Visible = false;
            cboQuy.Visible = false;

        }

        private void rbnQuy_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Visible = false;
            dtpEndDate.Visible = false;
            lblTu.Visible = false;
            lblDen.Visible = false;

            dtpThang.Visible = false;
            cboNam.Visible = false;
            cboQuy.Visible = true;

        }

        private void rbnNam_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Visible = false;
            dtpEndDate.Visible = false;
            lblTu.Visible = false;
            lblDen.Visible = false;

            dtpThang.Visible = false;
            cboNam.Visible = true;
            cboQuy.Visible = false;
        }

        private void dgvTK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
