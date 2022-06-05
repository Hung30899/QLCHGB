using iTextSharp.text;
using iTextSharp.text.pdf;
using QLCHGB.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Metadata;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;

namespace QLCHGB
{
    public partial class frmDSHD : Form
    {
        DataTable tblHD;

        public String strMaHD, txtTien,lblTien;
        public Char flag;

        public frmDSHD()
        {
            InitializeComponent();
        }

        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            frmHD frmHD = new frmHD();
            frmHD.MdiParent = this.ParentForm;
            frmHD.Dock = DockStyle.Fill;
            frmHD.Show();
        }

        private void frmDSHD_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            Functions.FillCombo("SELECT MaHD FROM HoaDon", cboMaHD, "MaHD", "MaHD");
            cboMaHD.SelectedIndex = -1;
            ResetValues();
        }

        private void ResetValues()
        {
            cboMaHD.Text = "";
            dtpThoiGian.Text = "";
        }

        private void LoadDataGridView()
        {
            String sql;
            sql = @"
                SELECT a.MaHD,a.ThoiGian,c.Tong
                FROM HoaDon AS a
                Left join ViewTongTienHD as c on a.MaHD = c.MaHD order by a.ThoiGian desc";
            tblHD = Functions.GetDataToTable(sql);
            dgvHD.DataSource = tblHD;
            dgvHD.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHD.Columns[1].HeaderText = "Thời gian";
            dgvHD.Columns[2].HeaderText = "Tổng tiền";

            dgvHD.Columns[0].Width = 200;
            dgvHD.Columns[1].Width = 200;
            dgvHD.Columns[2].Width = 250;

            dgvHD.AllowUserToAddRows = false;
            dgvHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + cboMaHD.Text.Trim() + "'";
                    DataTable table = Functions.GetDataToTable(sql);
                    for (int i = 0; i <= table.Rows.Count - 1; i++)
                    {
                        double sl, slcon, slxoa; //số lượng, số lượng còn, số lượng xóa

                        sql = "SELECT MaGB,SoLuong FROM CTHoaDon WHERE MaHD = N'" + cboMaHD.Text + "'";
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
                        sql = "DELETE CTHoaDon WHERE MaHD=N'" + cboMaHD.Text.Trim() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                    }
                    sql = "DELETE HoaDon WHERE MaHD=N'" + cboMaHD.Text.Trim() + "'";
                    Functions.RunSQLDel(sql);

                    LoadDataGridView();
                    ResetValues();
                    cboMaHD.Text = "";
                }
            }
        }

        private void dgvHD_Click(object sender, EventArgs e)
        {
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cboMaHD.Text = dgvHD.CurrentRow.Cells[0].Value.ToString();
            dtpThoiGian.Text = dgvHD.CurrentRow.Cells[1].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            frmDSHD_Load(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            strMaHD = cboMaHD.Text.Trim();
            frmHD frm = new frmHD();
            frm.MdiParent = this.ParentForm;
            frm.strMaHD = strMaHD;
            frm.flag = 's';
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
        private void LoadDateGirdViewCT(string str)
        {

            String sql;
            //     sql = "SELECT * FROM CTPhieuNhap WHERE MaPN = N'" + txtMaPN.Text.Trim() + "'";
            sql = "SELECT a.MaGB,b.TenGB,a.SoLuong,a.DonGia,a.SoLuong * a.DonGia AS ThanhTien FROM CTHoaDon AS a,GauBong AS b WHERE a.MaHD = N'" + str + "' AND a.MaGB=b.MaGB";
            DataTable tblHDCT;
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

        private void dgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            strMaHD = cboMaHD.Text.Trim();
            frmHD frm = new frmHD();
            frm.MdiParent = this.ParentForm;
            frm.strMaHD = strMaHD;
            frm.flag = 'd';
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if (ckbThoiGian.Checked)
                sql = @"
                SELECT a.MaHD,a.ThoiGian,c.Tong
                FROM HoaDon AS a
                Left join ViewTongTienHD as c on a.MaHD = c.MaHD
                Where a.MaHD Like N'%"+ cboMaHD.Text.Trim() + "%' and a.ThoiGian = '"+ dtpThoiGian.Text.Trim() + "' order by a.ThoiGian desc";
            else
                sql = @"
                SELECT a.MaHD,a.ThoiGian,c.Tong
                FROM HoaDon AS a
                Left join ViewTongTienHD as c on a.MaHD = c.MaHD
                Where a.MaHD Like N'%" + cboMaHD.Text.Trim() + "%' order by a.ThoiGian desc";
            tblHD = Functions.GetDataToTable(sql);

            if (tblHD.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblHD.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvHD.DataSource = tblHD;
        }

        private void ckbThoiGian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TongTien()
        {

            if (dgvCTHD.Rows.Count > 0)
            {
                string sql = "SELECT SUM(SoLuong * DonGia) FROM CTHoaDon WHERE MaHD = N'" + cboMaHD.Text.Trim() + "'";
                txtTien = Functions.GetFieldValues(sql);

            }
            else txtTien = "0";
            lblTien = Functions.ChuyenSoSangChuoi(double.Parse(txtTien));
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LoadDateGirdViewCT(cboMaHD.Text.Trim());
            if (dgvCTHD.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = cboMaHD.Text.Trim() + ".pdf";

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
                            Chunk c_mahd = new Chunk("[" + cboMaHD.Text.Trim() + "]\n", f_12_nomal);
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
                            PdfPCell cell_tien = new PdfPCell(new Phrase(txtTien, f_12_nomal));
                            PdfPCell cell_tienchu = new PdfPCell(new Phrase("Số tiền viết bằng chữ: " + lblTien, f_12_nomal));
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
    }
}
