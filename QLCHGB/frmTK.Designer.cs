
namespace QLCHGB
{
    partial class frmTK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnThang = new System.Windows.Forms.RadioButton();
            this.rbnQuy = new System.Windows.Forms.RadioButton();
            this.rbnNgay = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.dgvTK = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.lblTongSL = new System.Windows.Forms.Label();
            this.lblTien = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSLB = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbnNam = new System.Windows.Forms.RadioButton();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.cboQuy = new System.Windows.Forms.ComboBox();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTK)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1283, 118);
            this.panel1.TabIndex = 20;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDong);
            this.groupBox2.Controls.Add(this.cboNam);
            this.groupBox2.Controls.Add(this.cboQuy);
            this.groupBox2.Controls.Add(this.dtpThang);
            this.groupBox2.Controls.Add(this.lblDen);
            this.groupBox2.Controls.Add(this.rbnThang);
            this.groupBox2.Controls.Add(this.rbnNam);
            this.groupBox2.Controls.Add(this.rbnQuy);
            this.groupBox2.Controls.Add(this.rbnNgay);
            this.groupBox2.Controls.Add(this.dtpEndDate);
            this.groupBox2.Controls.Add(this.dtpStartDate);
            this.groupBox2.Controls.Add(this.lblTu);
            this.groupBox2.Controls.Add(this.btnThongKe);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1283, 118);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin chung";
            // 
            // rbnThang
            // 
            this.rbnThang.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbnThang.AutoSize = true;
            this.rbnThang.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbnThang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnThang.Location = new System.Drawing.Point(381, 29);
            this.rbnThang.Name = "rbnThang";
            this.rbnThang.Size = new System.Drawing.Size(97, 23);
            this.rbnThang.TabIndex = 2;
            this.rbnThang.Text = "Theo tháng:";
            this.rbnThang.UseVisualStyleBackColor = true;
            this.rbnThang.CheckedChanged += new System.EventHandler(this.rbnThang_CheckedChanged);
            // 
            // rbnQuy
            // 
            this.rbnQuy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbnQuy.AutoSize = true;
            this.rbnQuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbnQuy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnQuy.Location = new System.Drawing.Point(495, 29);
            this.rbnQuy.Name = "rbnQuy";
            this.rbnQuy.Size = new System.Drawing.Size(87, 23);
            this.rbnQuy.TabIndex = 3;
            this.rbnQuy.Text = "Theo quý:";
            this.rbnQuy.UseVisualStyleBackColor = true;
            this.rbnQuy.CheckedChanged += new System.EventHandler(this.rbnQuy_CheckedChanged);
            // 
            // rbnNgay
            // 
            this.rbnNgay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbnNgay.AutoSize = true;
            this.rbnNgay.Checked = true;
            this.rbnNgay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbnNgay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnNgay.Location = new System.Drawing.Point(275, 29);
            this.rbnNgay.Name = "rbnNgay";
            this.rbnNgay.Size = new System.Drawing.Size(93, 23);
            this.rbnNgay.TabIndex = 1;
            this.rbnNgay.TabStop = true;
            this.rbnNgay.Text = "Theo ngày:";
            this.rbnNgay.UseVisualStyleBackColor = true;
            this.rbnNgay.CheckedChanged += new System.EventHandler(this.rbnNgay_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.lbl);
            this.panel2.Controls.Add(this.lblTieuDe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 118);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1283, 54);
            this.panel2.TabIndex = 24;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lbl
            // 
            this.lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl.Location = new System.Drawing.Point(325, 18);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(0, 21);
            this.lbl.TabIndex = 28;
            this.lbl.Click += new System.EventHandler(this.lbl_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTieuDe.Location = new System.Drawing.Point(50, 15);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(265, 24);
            this.lblTieuDe.TabIndex = 27;
            this.lblTieuDe.Text = "THỐNG KÊ DOANG THU: ";
            // 
            // dgvTK
            // 
            this.dgvTK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTK.BackgroundColor = System.Drawing.Color.White;
            this.dgvTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTK.Location = new System.Drawing.Point(0, 172);
            this.dgvTK.Name = "dgvTK";
            this.dgvTK.RowHeadersWidth = 51;
            this.dgvTK.Size = new System.Drawing.Size(1283, 406);
            this.dgvTK.TabIndex = 25;
            this.dgvTK.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTK_CellContentClick);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Image = global::QLCHGB.Properties.Resources.padlock;
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDong.Location = new System.Drawing.Point(871, 74);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(80, 30);
            this.btnDong.TabIndex = 33;
            this.btnDong.Text = "  Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThongKe.BackColor = System.Drawing.Color.White;
            this.btnThongKe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.Image = global::QLCHGB.Properties.Resources.statistics__2_;
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.Location = new System.Drawing.Point(797, 27);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(154, 38);
            this.btnThongKe.TabIndex = 5;
            this.btnThongKe.Text = "   Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // lblTongSL
            // 
            this.lblTongSL.AutoSize = true;
            this.lblTongSL.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTongSL.Location = new System.Drawing.Point(21, 16);
            this.lblTongSL.Name = "lblTongSL";
            this.lblTongSL.Size = new System.Drawing.Size(161, 21);
            this.lblTongSL.TabIndex = 26;
            this.lblTongSL.Text = "Tổng số lượng bán: ";
            // 
            // lblTien
            // 
            this.lblTien.AutoSize = true;
            this.lblTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTien.Location = new System.Drawing.Point(430, 45);
            this.lblTien.Name = "lblTien";
            this.lblTien.Size = new System.Drawing.Size(0, 20);
            this.lblTien.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(341, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 21);
            this.label2.TabIndex = 30;
            this.label2.Text = "Tổng tiền:";
            // 
            // lblSLB
            // 
            this.lblSLB.AutoSize = true;
            this.lblSLB.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSLB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSLB.Location = new System.Drawing.Point(184, 16);
            this.lblSLB.Name = "lblSLB";
            this.lblSLB.Size = new System.Drawing.Size(0, 21);
            this.lblSLB.TabIndex = 31;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTongTien.Location = new System.Drawing.Point(431, 16);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(0, 21);
            this.lblTongTien.TabIndex = 32;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.lblTongTien);
            this.panel3.Controls.Add(this.lblSLB);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblTien);
            this.panel3.Controls.Add(this.lblTongSL);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 578);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1283, 74);
            this.panel3.TabIndex = 22;
            // 
            // rbnNam
            // 
            this.rbnNam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbnNam.AutoSize = true;
            this.rbnNam.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbnNam.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbnNam.Location = new System.Drawing.Point(608, 29);
            this.rbnNam.Name = "rbnNam";
            this.rbnNam.Size = new System.Drawing.Size(90, 23);
            this.rbnNam.TabIndex = 4;
            this.rbnNam.Text = "Theo năm:";
            this.rbnNam.UseVisualStyleBackColor = true;
            this.rbnNam.CheckedChanged += new System.EventHandler(this.rbnNam_CheckedChanged);
            // 
            // lblTu
            // 
            this.lblTu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTu.AutoSize = true;
            this.lblTu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTu.Location = new System.Drawing.Point(271, 76);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(30, 19);
            this.lblTu.TabIndex = 14;
            this.lblTu.Text = "Từ:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpStartDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(304, 69);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(195, 26);
            this.dtpStartDate.TabIndex = 13;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpEndDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(564, 69);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(195, 26);
            this.dtpEndDate.TabIndex = 16;
            // 
            // lblDen
            // 
            this.lblDen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDen.AutoSize = true;
            this.lblDen.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDen.Location = new System.Drawing.Point(521, 76);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(34, 19);
            this.lblDen.TabIndex = 21;
            this.lblDen.Text = "đến:";
            // 
            // dtpThang
            // 
            this.dtpThang.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpThang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpThang.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpThang.Location = new System.Drawing.Point(398, 69);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(195, 26);
            this.dtpThang.TabIndex = 22;
            // 
            // cboQuy
            // 
            this.cboQuy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboQuy.FormattingEnabled = true;
            this.cboQuy.Items.AddRange(new object[] {
            "Quý 1",
            "Quý 2",
            "Quý 3",
            "Quý 4"});
            this.cboQuy.Location = new System.Drawing.Point(510, 68);
            this.cboQuy.Name = "cboQuy";
            this.cboQuy.Size = new System.Drawing.Size(121, 27);
            this.cboQuy.TabIndex = 23;
            // 
            // cboNam
            // 
            this.cboNam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(624, 68);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(121, 27);
            this.cboNam.TabIndex = 24;
            this.cboNam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboNam_KeyPress);
            // 
            // frmTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 652);
            this.Controls.Add(this.dgvTK);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTK";
            this.Text = "frmTK";
            this.Load += new System.EventHandler(this.frmTK_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTK)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.RadioButton rbnQuy;
        private System.Windows.Forms.RadioButton rbnNgay;
        private System.Windows.Forms.RadioButton rbnThang;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.DataGridView dgvTK;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.ComboBox cboQuy;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.RadioButton rbnNam;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.Label lblTongSL;
        private System.Windows.Forms.Label lblTien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSLB;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Panel panel3;
    }
}