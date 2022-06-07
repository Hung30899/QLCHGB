using QLCHGB.Class;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLCHGB
{
    public partial class frmGB : Form
    {
        DataTable tblGB;
        char btn, rbt;
        public frmGB()
        {
            InitializeComponent();
        }

        private void frmGB_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            Disable();
            LoadDataGridView();
            rbnMa_CheckedChanged(sender, e);
        }

        private void Disable()
        {
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            cboLoai.Enabled = false;
            txtAnh.Enabled = false;
            picAnh.Enabled = false;
            btnChonAnh.Enabled = false;
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
            txtMa.Focus();
        }

        private void Enable()
        {
            txtMa.Enabled = true;
            txtTen.Enabled = true;
            cboLoai.Enabled = true;
            txtAnh.Enabled = false;
            picAnh.Enabled = false;
            btnChonAnh.Enabled = true;
        }

        private void ResetValues()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cboLoai.Text = "";
            txtAnh.Text = "";
            picAnh.Image = null;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btn = 's';
            if (tblGB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMa.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            Enable();
            txtMa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblGB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMa.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE GauBong WHERE MaGB=N'" + txtMa.Text.Trim() + "'";
                Functions.RunSQLDel(sql);
                LoadDataGridView();
                ResetValues();
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            {
                if (txtMa.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return;
                }
                if (txtTen.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTen.Focus();
                    return;
                }
                if (cboLoai.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập loại gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboLoai.Focus();
                    return;
                }
                
                if (txtAnh.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn ảnh cho gấu bông!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnChonAnh.Focus();
                    return;
                }

                //Thêm gạch
                if (btn == 't')
                {
                    sql = "SELECT MaGB FROM GauBong WHERE MaGB=N'" + txtMa.Text.Trim() + "'";
                    if (Functions.CheckKey(sql))
                    {
                        MessageBox.Show("Mã gấu bông này đã có, bạn phải nhập mã gấu bông khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMa.Focus();
                        txtMa.Text = "";
                        return;
                    }

                    sql = "INSERT INTO GauBong(MaGB,TenGB,LoaiGB,Anh) VALUES" +
                        "(N'" + txtMa.Text.Trim() + "',N'" + txtTen.Text.Trim() + "'," +
                        "N'" + cboLoai.Text.Trim() + "',N'" +txtAnh.Text.Trim() + "')";
                    MessageBox.Show("Đã lưu gấu bông mã: " + txtMa.Text.Trim() + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Functions.RunSQL(sql);
                    LoadDataGridView();

                    ResetValues();
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = false;
                    btnHuy.Enabled = false;
                    btnLuu.Enabled = false;
                    Disable();
                    //txtMa.Enabled = false;
                }
                //Sửa 
                if (btn == 's')
                {
                    if (MessageBox.Show("Bạn có muốn lưu thông tin chỉnh sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        sql = "UPDATE GauBong SET TenGB=N'" + txtTen.Text.Trim() + "',LoaiGB=N'" + cboLoai.Text.Trim() + "'," +
                        "Anh=N'" + txtAnh.Text.Trim() + "' WHERE MaGB=N'" + txtMa.Text.Trim() + "'";
                        Functions.RunSQL(sql);
                        LoadDataGridView();
                        MessageBox.Show("Đã cập nhật gấu bông mã: " + txtMa.Text.Trim() + " thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetValues();
                        btnXoa.Enabled = false;
                        btnThem.Enabled = true;
                        btnSua.Enabled = false;
                        btnHuy.Enabled = false;
                        btnLuu.Enabled = false;
                        Disable();
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            Disable();
        }

        private void rbnMa_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 'm';
            sql = "SELECT * fROM GauBong";
            tblGB = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblGB;
            cboSearch.DisplayMember = "MaGB";
            cboSearch.ValueMember = "MaGB";
            cboSearch.Text = "";
        }

        private void rbnTen_CheckedChanged(object sender, EventArgs e)
        {
            string sql;
            rbt = 't';
            sql = "SELECT * fROM GauBong";
            tblGB = Functions.GetDataToTable(sql); //Lấy dữ liệu
            cboSearch.DataSource = tblGB;
            cboSearch.DisplayMember = "TenGB";
            cboSearch.ValueMember = "TenGB";
            cboSearch.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
         
            if (rbt == 'm') 
            {
                sql = "SELECT * FROM GauBong WHERE MaGB LIKE N'%" + cboSearch.Text.Trim() + "%'";
                tblGB = Functions.GetDataToTable(sql);
            }
            if (rbt == 't') 
            {
                sql = "SELECT * FROM GauBong WHERE TenGB LIKE N'%" + cboSearch.Text.Trim() + "%'";
                tblGB = Functions.GetDataToTable(sql);
            }
            if (tblGB.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Có " + tblGB.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvGB.DataSource = tblGB;
            ResetValues();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);

                txtAnh.Text = Functions.GetPath2("Resources\\image\\") + txtMa.Text.Trim() + ".jpg";
                picAnh.Image.Save(txtAnh.Text);
            }
        }

        private void dgvGB_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tblGB.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMa.Text = dgvGB.CurrentRow.Cells["MaGB"].Value.ToString();
            txtTen.Text = dgvGB.CurrentRow.Cells["TenGB"].Value.ToString();
            cboLoai.Text = dgvGB.CurrentRow.Cells["LoaiGB"].Value.ToString();
            txtAnh.Text = dgvGB.CurrentRow.Cells["Anh"].Value.ToString();
            if (txtAnh.Text != "")
            {

                picAnh.Image = Image.FromFile(txtAnh.Text);
            }
            else
                picAnh.Image = null;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            Disable();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaGB,TenGB,LoaiGB,Anh, SLN-SLB as SL FROM GauBong";
            tblGB = Functions.GetDataToTable(sql);
            dgvGB.DataSource = tblGB;
            dgvGB.Columns[0].HeaderText = "Mã gấu bông";
            dgvGB.Columns[1].HeaderText = "Tên gấu bông";
            dgvGB.Columns[2].HeaderText = "Loại gấu bông";
            dgvGB.Columns[3].HeaderText = "Ảnh";
            dgvGB.Columns[4].HeaderText = "Số lượng";


            dgvGB.Columns[0].Width = 150;
            dgvGB.Columns[1].Width = 150;
            dgvGB.Columns[2].Width = 150;
            dgvGB.Columns[3].Width = 150;
            dgvGB.Columns[4].Width = 150;

            dgvGB.AllowUserToAddRows = false;
            dgvGB.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
