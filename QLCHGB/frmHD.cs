using iTextSharp.text;
using iTextSharp.text.pdf;
using QLCHGB.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmHD : Form
    {
        DataTable tblHDCT;
        char btn;
        String rbn, soluongc;

        public String strMaHD;
        public Char flag;
        public frmHD()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cboMaGB.Text = "";
            txtTenGB.Text = "";
            txtSL.Text = "";
            txtDonGia.Text = "";
            txtTien.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btn = 't';
            flag = 't';
            Enable();
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnHuy.Enabled = true;
            txtMaHD.Text = Functions.CreateKey("HD");
            ResetValues();
        }

        private void frmHD_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtSL.Text = "0";
            txtDonGia.Text = "0";
            txtTien.Text = "0";

            Disable();
            Functions.FillCombo("SELECT MaGB, TenGB FROM GauBong", cboMaGB, "MaGB", "MaGB");
            cboMaGB.SelectedIndex = -1;

            if (flag == 's')
            {
                btnThem.Enabled = false;
                txtMaHD.Text = strMaHD;
                LoadDateGirdViewCT(strMaHD);
            }
            else if(flag == 'd')
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                txtMaHD.Text = strMaHD;
                LoadDateGirdViewCT(strMaHD);
            }    
            else
            {
                string str = "";
                LoadDateGirdViewCT(str);
                btnXoa.Enabled = false;
            }
        }
        private void Disable()
        {
            txtMaHD.ReadOnly = true;
            cboMaGB.Enabled = false;
            txtSL.Enabled = false;
            dtpThoiGian.Enabled = false;
        }

        private void Enable()
        {
            cboMaGB.Enabled = true;
            txtSL.Enabled = true;
            txtDonGia.Enabled = true;
            dtpThoiGian.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            txtSL.Enabled = true;
            txtDonGia.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            cboMaGB.Enabled = true;
            soluongc = txtSL.Text.Trim();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;    
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text.Trim() + "'";

                    DataTable table = Functions.GetDataToTable(sql);
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa;

                        sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text + "'";
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
                        sql = "DELETE CTHoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                sql = "DELETE HoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                Functions.RunSQLDel(sql);

                LoadDateGirdViewCT(txtMaHD.Text.Trim());
                ResetValues();
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = true;
                txtMaHD.Text = "";
                this.Close();
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {

                if (cboMaGB.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Chưa chọn mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaGB.Focus();
                    return;
                }
                if (txtSL.Text == "0")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSL.Focus();
                    return;
                }
                if (txtDonGia.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn chưa thiết lập giá bán cho gấu bông mã: " + cboMaGB.Text.Trim() + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (btn == 't')
                {
                    //Lưu thông tin của các mặt hàng
                    if (cboMaGB.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        return;
                    }

                    if (txtSL.Text.Trim() == "")
                    {
                        MessageBox.Show("Bạn phải nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSL.Focus();
                        return;
                    }

                    sql = "SELECT MaGB FROM CTHoaDon WHERE MaGB = N'" + cboMaGB.SelectedValue + "' AND MaHD = N'" + txtMaHD.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã gấu bông này đã có, bạn phải nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaGB.Focus();
                        cboMaGB.Text = "";
                        return;
                    }
                    // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
                    double sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN-SLB as SL FROM GauBong WHERE MaGB = N'" + cboMaGB.SelectedValue + "'"));
                    if (Convert.ToDouble(txtSL.Text) > sl)
                    {
                        MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSL.Text = "";
                        txtSL.Focus();
                        return;
                    }

                    sql = "SELECT MaHD FROM HoaDon WHERE MaHD=N'" + txtMaHD.Text.Trim() + "'";
                    if (!Functions.CheckKey(sql))//Chưa có mã HD
                    {
                        sql = "INSERT INTO HoaDon(MaHD, ThoiGian) VALUES " +
                            "(N'" + txtMaHD.Text.Trim() + "',N'" + dtpThoiGian.Text.Trim() + "')";
                        Functions.RunSQL(sql);
                    }

                    sql = "INSERT INTO CTHoaDon(MaHD, MaGB, SoLuong, DonGia)" +
                        " VALUES(N'" + txtMaHD.Text.Trim() + "',N'" + cboMaGB.SelectedValue + "',N'" + txtSL.Text.Trim() + "',N'" + txtDonGia.Text.Trim() + "')";
                    Functions.RunSQL(sql);
                    sql = "UPDATE GauBong SET SLB = (SLB + " + txtSL.Text.Trim() + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                    Functions.RunSQL(sql);
                    LoadDateGirdViewCT(txtMaHD.Text.Trim());
                }


                //Sửa
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        double sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLN-SLB as SL FROM GauBong WHERE MaGB = N'" + cboMaGB.SelectedValue + "'"));
                        sl = sl + Convert.ToDouble(soluongc);
                        if (Convert.ToDouble(txtSL.Text) > sl)
                        {
                            MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSL.Text = "";
                            txtSL.Focus();
                            return;
                        }
                        sql = "UPDATE CTHoaDon SET SoLuong=N'" + txtSL.Text.Trim() + "' WHERE MaHD=N'" + txtMaHD.Text + "' AND MaGB= N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        MessageBox.Show("Đã cập nhật số lượng mã gấu bông: " + cboMaGB.SelectedValue + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sql = "UPDATE GauBong SET SLB = (SLB + " + txtSL.Text.Trim() + " - " + soluongc + ") Where MaGB = N'" + cboMaGB.SelectedValue + "'";
                        Functions.RunSQL(sql);
                        LoadDateGirdViewCT(txtMaHD.Text.Trim());
                 
                        btnThem.Enabled = false;
                        btnSua.Enabled = false;
                        btnHuy.Enabled = false;
                        btnLuu.Enabled = false;
                        Disable();
                    }
                }
            }
        }

        private void LoadDateGirdViewCT(string str)
        {

            String sql;
            //     sql = "SELECT * FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text.Trim() + "'";
            sql = "SELECT a.MaGB,b.TenGB,a.SoLuong,a.DonGia,a.SoLuong * a.DonGia AS ThanhTien FROM CTHoaDon AS a,GauBong AS b WHERE a.MaHD = N'" + str + "' AND a.MaGB=b.MaGB";

            tblHDCT = Functions.GetDataToTable(sql);
            dgvCTHD.DataSource = tblHDCT;

            dgvCTHD.Columns[0].HeaderText = "Mã gấu bông";
            dgvCTHD.Columns[1].HeaderText = "Tên gấu bông";
            dgvCTHD.Columns[2].HeaderText = "Số lượng";
            dgvCTHD.Columns[3].HeaderText = "Đơn giá";
            dgvCTHD.Columns[4].HeaderText = "Thành tiền";

            dgvCTHD.Columns[0].Width = 150;
            dgvCTHD.Columns[1].Width = 150;
            dgvCTHD.Columns[2].Width = 150;
            dgvCTHD.Columns[3].Width = 150;
            dgvCTHD.Columns[4].Width = 250;
            dgvCTHD.AllowUserToAddRows = false;
            dgvCTHD.EditMode = DataGridViewEditMode.EditProgrammatically;
            TongTien();
        }

        private void TongTien()
        {

            if (dgvCTHD.Rows.Count > 0)
            {
                string sql = "SELECT SUM(SoLuong * DonGia) FROM CTHoaDon WHERE MaHD = N'" + txtMaHD.Text.Trim() + "'";
                txtTongTien.Text = Functions.GetFieldValues(sql);

            }
            else txtTongTien.Text = "0";
            lblTien.Text = Functions.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dgvCTHD.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = txtMaHD.Text.Trim() + ".pdf";

                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {

                        Document doc = new Document(PageSize.A4.Rotate());

                        BaseFont arial = BaseFont.CreateFont(Functions.GetPath("/Resources/ARIAL.TTF"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font f_15_bold = new iTextSharp.text.Font(arial, 15, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font f_12_nomal = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.NORMAL);
                        iTextSharp.text.Font f_25_bold = new iTextSharp.text.Font(arial, 25, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font f_12_italic = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.ITALIC);
                        iTextSharp.text.Font f_12_bold = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.BOLD);


                        FileStream os = new FileStream(sfd.FileName, FileMode.Create);

                        using (os)
                        {
                            PdfWriter.GetInstance(doc, os);
                            doc.Open();

                            PdfPTable tbl1 = new PdfPTable(2);
                            //Ảnh
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(Properties.Resources.bear__1_, System.Drawing.Imaging.ImageFormat.Png);
                            PdfPCell cell1 = new PdfPCell(image);
                            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            //Thông tin
                            PdfPCell cell2 = new PdfPCell();
                            Chunk c1 = new Chunk("CỬA HÀNG GẤU BÔNG Teddy Bear", f_15_bold);
                            Chunk c2 = new Chunk("Địa chỉ: 135 Hùng Vương, Vĩnh Yên, Vĩnh Phúc \nĐiện thoại: 0325698233 \nEmail: teddyBear@gmail.com", f_12_nomal);
                            cell2.AddElement(c1);
                            cell2.AddElement(c2);
                            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;

                            //Hoa don
                            string[] partsDay;
                            partsDay = dtpThoiGian.Text.Split('/');
                            Paragraph p_hoadon = new Paragraph();
                            Chunk c_hoadon = new Chunk("HÓA ĐƠN BÁN HÀNG\n", f_25_bold);
                            Chunk c_mahd = new Chunk("["+txtMaHD.Text.Trim()+"]\n", f_12_nomal);
                            Chunk c_thoigian = new Chunk("Ngày " + partsDay[1] + " tháng " + partsDay[0] + " năm " + partsDay[2], f_12_italic);
                            p_hoadon.Add(c_hoadon);
                            p_hoadon.Add(c_mahd);
                            p_hoadon.Add(c_thoigian);
                            p_hoadon.Alignment = Element.ALIGN_CENTER;
                            p_hoadon.SpacingAfter = 20;
                            p_hoadon.SpacingBefore = 30;
                            tbl1.AddCell(cell1);
                            tbl1.AddCell(cell2);
                            doc.Add(tbl1);
                            doc.Add(p_hoadon);

                            //Thông tin khách hàng
                            //   Paragraph p_kh = new Paragraph("Họ và tên khách hàng: " + txtTenKH.Text + "\nSố điện thoại: " + txtSDT.Text + "\nĐịa chỉ: " + txtDiaChi.Text, f_12_nomal);
                            //   doc.Add(p_kh);

                            //Bảng CTHD
                            PdfPTable talCTHD = new PdfPTable(dgvCTHD.Columns.Count);
                            talCTHD.DefaultCell.Padding = 6;
                            talCTHD.WidthPercentage = 100;
                            talCTHD.HorizontalAlignment = Element.ALIGN_LEFT;


                            foreach (DataGridViewColumn column in dgvCTHD.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, f_12_nomal));
                                talCTHD.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvCTHD.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    talCTHD.AddCell(cell.Value.ToString());
                                }
                            }
                            talCTHD.SpacingBefore = 20;
                            doc.Add(talCTHD);

                            //Tổng tiền
                            PdfPTable tal_tien = new PdfPTable(2);
                            PdfPCell cell_tongtien = new PdfPCell(new Phrase("Tổng tiền ", f_12_nomal));
                            PdfPCell cell_tien = new PdfPCell(new Phrase(txtTongTien.Text, f_12_nomal));
                            PdfPCell cell_tienchu = new PdfPCell(new Phrase("Số tiền viết bằng chữ: " + lblTien.Text, f_12_nomal));
                            cell_tongtien.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell_tongtien.PaddingRight = 8;
                            float[] width = new float[] { 400, 100 };
                            tal_tien.WidthPercentage = 100;
                            tal_tien.SetWidths(width);
                            tal_tien.AddCell(cell_tongtien);
                            tal_tien.AddCell(cell_tien);
                            PdfPTable tal_tienchu = new PdfPTable(1);
                            tal_tienchu.WidthPercentage = 100;
                            tal_tienchu.SetWidths(new float[] { 500 });
                            tal_tienchu.AddCell(cell_tienchu);

                            doc.Add(tal_tien);
                            doc.Add(tal_tienchu);

                            //Chữ ký
                            PdfPTable tal_chuky = new PdfPTable(2);
                            PdfPCell cell_mua = new PdfPCell();
                            c1 = new Chunk("  Người mua hàng", f_12_bold);
                            c2 = new Chunk("(Ký, ghi rõ họ , tên)", f_12_italic);
                            cell_mua.AddElement(c1);
                            cell_mua.AddElement(c2);
                            cell_mua.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            cell_mua.PaddingLeft = 100;
                            PdfPCell cell_ban = new PdfPCell();
                            c1 = new Chunk("  Người bán hàng", f_12_bold);
                            c2 = new Chunk("(Ký, ghi rõ họ , tên)", f_12_italic);
                            cell_ban.AddElement(c1);
                            cell_ban.AddElement(c2);
                            cell_ban.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            cell_ban.PaddingLeft = 150;
                            tal_chuky.AddCell(cell_mua);
                            tal_chuky.AddCell(cell_ban);
                            tal_chuky.WidthPercentage = 100;
                            tal_chuky.SpacingBefore = 20;
                            doc.Add(tal_chuky);

                            doc.Close();

                            //mở doc
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được Export!!!", "Info");
            }

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đóng phiếu nhập không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg;

            if (txtSL.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSL.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg * 1000;
            txtTien.Text = tt.ToString();
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void cboMaGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaGB.Text == "")
            {
                txtTenGB.Text = "";
            }
            sql = "Select TenGB from GauBong where MaGB =N'" + cboMaGB.SelectedValue + "'";
            txtTenGB.Text = Functions.GetFieldValues(sql);

            sql = "Select Top 1 IsNULL(DonGia,0) as DonGia from GiaBan where MaGB =N'" + cboMaGB.SelectedValue + "' and Ngay <= N'" + dtpThoiGian.Text + "' order by Ngay desc";
            txtDonGia.Text = Functions.GetFieldValues(sql);
            txtSL_TextChanged(sender, e);
        }

        private void dgvCTHD_Click(object sender, EventArgs e)
        {
            if (flag == 's')
            {
                if (tblHDCT.Rows.Count == 0)
                {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                }
   
                cboMaGB.Text = dgvCTHD.CurrentRow.Cells[0].Value.ToString();
                txtSL.Text = dgvCTHD.CurrentRow.Cells[2].Value.ToString();
                cboMaGB_SelectedIndexChanged(sender, e);
                txtSL_TextChanged(sender, e);
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnHuy.Enabled = true;
                Disable();
            }
        }

        private void dtpThoiGian_ValueChanged(object sender, EventArgs e)
        {
            string sql = "Select Top 1 IsNULL(DonGia,0) as DonGia from GiaBan where MaGB =N'" + cboMaGB.SelectedValue + "' and Ngay <= N'" + dtpThoiGian.Text + "' order by Ngay desc";
            txtDonGia.Text = Functions.GetFieldValues(sql);
            txtSL_TextChanged(sender, e);
        }

        private void dgvCTHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaGB, sql;
            double slxoa, slcon, sl;
            if (flag == 't' || flag == 's')
            {
                if (tblHDCT.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    MaGB = dgvCTHD.CurrentRow.Cells["MaGB"].Value.ToString();
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SLB FROM GauBong WHERE MaGB = N'" + MaGB + "'"));
                    slxoa = Convert.ToDouble(dgvCTHD.CurrentRow.Cells["SoLuong"].Value.ToString());
                    slcon = sl - slxoa;
                    sql = "UPDATE GauBong SET SLB =" + slcon + " WHERE MaGB= N'" + MaGB + "'";
                    Functions.RunSQL(sql);

                    sql = "DELETE CTHoaDon WHERE MaHD=N'" + txtMaHD.Text + "' AND MaGB = N'" + MaGB + "'";
                    Functions.RunSQL(sql);

                    LoadDateGirdViewCT(txtMaHD.Text.Trim());
                }
            }
        }

        private void ResetValues()
        {
            dtpThoiGian.Text = "";
            cboMaGB.Text = "";
            txtTenGB.Text = "";
            txtSL.Text = "0";
            txtDonGia.Text = "0";
            txtTien.Text = "0";
        }
    }
}
