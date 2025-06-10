namespace YasminClinic
{
    partial class KelolaReservation
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
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvReservasi = new System.Windows.Forms.DataGridView();
            this.btnupdatestatusbatal = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btrnupdate = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dtpTanggalReservasi = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbJamReservasi = new System.Windows.Forms.ComboBox();
            this.cmbSpesialisasi = new System.Windows.Forms.ComboBox();
            this.cmbDokter = new System.Windows.Forms.ComboBox();
            this.cmbPasien = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(28, 365);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(87, 47);
            this.btnBack.TabIndex = 45;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // dgvReservasi
            // 
            this.dgvReservasi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservasi.Location = new System.Drawing.Point(484, 45);
            this.dgvReservasi.Name = "dgvReservasi";
            this.dgvReservasi.RowHeadersWidth = 51;
            this.dgvReservasi.RowTemplate.Height = 24;
            this.dgvReservasi.Size = new System.Drawing.Size(525, 367);
            this.dgvReservasi.TabIndex = 44;
            // 
            // btnupdatestatusbatal
            // 
            this.btnupdatestatusbatal.Location = new System.Drawing.Point(246, 315);
            this.btnupdatestatusbatal.Name = "btnupdatestatusbatal";
            this.btnupdatestatusbatal.Size = new System.Drawing.Size(153, 23);
            this.btnupdatestatusbatal.TabIndex = 42;
            this.btnupdatestatusbatal.Text = "Update Status Batal";
            this.btnupdatestatusbatal.UseVisualStyleBackColor = true;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(40, 315);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(153, 23);
            this.btnUpdateStatus.TabIndex = 41;
            this.btnUpdateStatus.Text = "Update Status selesai";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(374, 269);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 40;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btrnupdate
            // 
            this.btrnupdate.Location = new System.Drawing.Point(274, 269);
            this.btrnupdate.Name = "btrnupdate";
            this.btrnupdate.Size = new System.Drawing.Size(75, 23);
            this.btrnupdate.TabIndex = 39;
            this.btrnupdate.Text = "Update data";
            this.btrnupdate.UseVisualStyleBackColor = true;
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(161, 269);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(75, 23);
            this.btnHapus.TabIndex = 38;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(40, 269);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 23);
            this.btnSimpan.TabIndex = 43;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            // 
            // dtpTanggalReservasi
            // 
            this.dtpTanggalReservasi.Location = new System.Drawing.Point(197, 91);
            this.dtpTanggalReservasi.Name = "dtpTanggalReservasi";
            this.dtpTanggalReservasi.Size = new System.Drawing.Size(252, 22);
            this.dtpTanggalReservasi.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 36;
            this.label4.Text = "Pilihan Dokter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 24);
            this.label5.TabIndex = 35;
            this.label5.Text = "Spesialisasi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 34;
            this.label3.Text = "Jam Reservasi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 24);
            this.label2.TabIndex = 33;
            this.label2.Text = "Tanggal Reservasi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 32;
            this.label1.Text = "Pasien";
            // 
            // cmbJamReservasi
            // 
            this.cmbJamReservasi.FormattingEnabled = true;
            this.cmbJamReservasi.Location = new System.Drawing.Point(197, 135);
            this.cmbJamReservasi.Name = "cmbJamReservasi";
            this.cmbJamReservasi.Size = new System.Drawing.Size(252, 24);
            this.cmbJamReservasi.TabIndex = 30;
            // 
            // cmbSpesialisasi
            // 
            this.cmbSpesialisasi.FormattingEnabled = true;
            this.cmbSpesialisasi.Location = new System.Drawing.Point(197, 178);
            this.cmbSpesialisasi.Name = "cmbSpesialisasi";
            this.cmbSpesialisasi.Size = new System.Drawing.Size(252, 24);
            this.cmbSpesialisasi.TabIndex = 29;
            // 
            // cmbDokter
            // 
            this.cmbDokter.FormattingEnabled = true;
            this.cmbDokter.Location = new System.Drawing.Point(197, 219);
            this.cmbDokter.Name = "cmbDokter";
            this.cmbDokter.Size = new System.Drawing.Size(252, 24);
            this.cmbDokter.TabIndex = 31;
            // 
            // cmbPasien
            // 
            this.cmbPasien.FormattingEnabled = true;
            this.cmbPasien.Location = new System.Drawing.Point(197, 45);
            this.cmbPasien.Name = "cmbPasien";
            this.cmbPasien.Size = new System.Drawing.Size(252, 24);
            this.cmbPasien.TabIndex = 28;
            // 
            // KelolaReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 450);
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
            this.Name = "KelolaReservation";
            this.Text = "KelolaReservation";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservasi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dgvReservasi;
        private System.Windows.Forms.Button btnupdatestatusbatal;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btrnupdate;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.DateTimePicker dtpTanggalReservasi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbJamReservasi;
        private System.Windows.Forms.ComboBox cmbSpesialisasi;
        private System.Windows.Forms.ComboBox cmbDokter;
        private System.Windows.Forms.ComboBox cmbPasien;
    }
}