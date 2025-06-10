namespace YasminClinic
{
    partial class Reservasi
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
            this.cmbPasien = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDokter = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSpesialisasi = new System.Windows.Forms.ComboBox();
            this.cmbJamReservasi = new System.Windows.Forms.ComboBox();
            this.dtpTanggalReservasi = new System.Windows.Forms.DateTimePicker();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btrnupdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvReservasi = new System.Windows.Forms.DataGridView();
            this.btnupdatestatusbatal = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPasien
            // 
            this.cmbPasien.FormattingEnabled = true;
            this.cmbPasien.Location = new System.Drawing.Point(226, 45);
            this.cmbPasien.Name = "cmbPasien";
            this.cmbPasien.Size = new System.Drawing.Size(252, 24);
            this.cmbPasien.TabIndex = 0;
            this.cmbPasien.SelectedIndexChanged += new System.EventHandler(this.cmbPasien_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pasien";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tanggal Reservasi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Jam Reservasi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(65, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pilihan Dokter";
            // 
            // cmbDokter
            // 
            this.cmbDokter.FormattingEnabled = true;
            this.cmbDokter.Location = new System.Drawing.Point(226, 219);
            this.cmbDokter.Name = "cmbDokter";
            this.cmbDokter.Size = new System.Drawing.Size(252, 24);
            this.cmbDokter.TabIndex = 0;
            this.cmbDokter.SelectedIndexChanged += new System.EventHandler(this.cmbDokter_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "Spesialisasi";
            // 
            // cmbSpesialisasi
            // 
            this.cmbSpesialisasi.FormattingEnabled = true;
            this.cmbSpesialisasi.Location = new System.Drawing.Point(226, 178);
            this.cmbSpesialisasi.Name = "cmbSpesialisasi";
            this.cmbSpesialisasi.Size = new System.Drawing.Size(252, 24);
            this.cmbSpesialisasi.TabIndex = 0;
            this.cmbSpesialisasi.SelectedIndexChanged += new System.EventHandler(this.cmbSpesialisasi_SelectedIndexChanged);
            // 
            // cmbJamReservasi
            // 
            this.cmbJamReservasi.FormattingEnabled = true;
            this.cmbJamReservasi.Location = new System.Drawing.Point(226, 135);
            this.cmbJamReservasi.Name = "cmbJamReservasi";
            this.cmbJamReservasi.Size = new System.Drawing.Size(252, 24);
            this.cmbJamReservasi.TabIndex = 0;
            this.cmbJamReservasi.SelectedIndexChanged += new System.EventHandler(this.cmbJamReservasi_SelectedIndexChanged);
            // 
            // dtpTanggalReservasi
            // 
            this.dtpTanggalReservasi.Location = new System.Drawing.Point(226, 91);
            this.dtpTanggalReservasi.Name = "dtpTanggalReservasi";
            this.dtpTanggalReservasi.Size = new System.Drawing.Size(252, 22);
            this.dtpTanggalReservasi.TabIndex = 2;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(69, 269);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 23);
            this.btnSimpan.TabIndex = 3;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(69, 315);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(153, 23);
            this.btnUpdateStatus.TabIndex = 3;
            this.btnUpdateStatus.Text = "Update Status selesai";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(190, 269);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 3;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btrnupdate
            // 
            this.btrnupdate.Location = new System.Drawing.Point(303, 269);
            this.btrnupdate.Name = "btrnupdate";
            this.btrnupdate.Size = new System.Drawing.Size(75, 23);
            this.btrnupdate.TabIndex = 3;
            this.btrnupdate.Text = "Update data";
            this.btrnupdate.UseVisualStyleBackColor = true;
            this.btrnupdate.Click += new System.EventHandler(this.btrnupdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(403, 269);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvReservasi
            // 
            this.dgvReservasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservasi.Location = new System.Drawing.Point(513, 45);
            this.dgvReservasi.Name = "dgvReservasi";
            this.dgvReservasi.RowHeadersWidth = 51;
            this.dgvReservasi.RowTemplate.Height = 24;
            this.dgvReservasi.Size = new System.Drawing.Size(525, 367);
            this.dgvReservasi.TabIndex = 4;
            this.dgvReservasi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReservasi_CellClick);
            // 
            // btnupdatestatusbatal
            // 
            this.btnupdatestatusbatal.Location = new System.Drawing.Point(275, 315);
            this.btnupdatestatusbatal.Name = "btnupdatestatusbatal";
            this.btnupdatestatusbatal.Size = new System.Drawing.Size(153, 23);
            this.btnupdatestatusbatal.TabIndex = 3;
            this.btnupdatestatusbatal.Text = "Update Status Batal";
            this.btnupdatestatusbatal.UseVisualStyleBackColor = true;
            this.btnupdatestatusbatal.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(36, 365);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(87, 47);
            this.btnBack.TabIndex = 27;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Reservasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 424);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvReservasi);
            this.Controls.Add(this.btnupdatestatusbatal);
            this.Controls.Add(this.btnUpdateStatus);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btrnupdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.dtpTanggalReservasi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbJamReservasi);
            this.Controls.Add(this.cmbSpesialisasi);
            this.Controls.Add(this.cmbDokter);
            this.Controls.Add(this.cmbPasien);
            this.Name = "Reservasi";
            this.Text = "Reservasi";
            this.Load += new System.EventHandler(this.Reservasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPasien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDokter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSpesialisasi;
        private System.Windows.Forms.ComboBox cmbJamReservasi;
        private System.Windows.Forms.DateTimePicker dtpTanggalReservasi;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btrnupdate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvReservasi;
        private System.Windows.Forms.Button btnupdatestatusbatal;
        private System.Windows.Forms.Button btnBack;
    }
}