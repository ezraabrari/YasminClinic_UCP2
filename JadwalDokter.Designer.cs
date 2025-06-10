namespace YasminClinic
{
    partial class JadwalDokter
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnupdate = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.cmbDokter = new System.Windows.Forms.ComboBox();
            this.cmbHari = new System.Windows.Forms.ComboBox();
            this.dtmJamMulai = new System.Windows.Forms.DateTimePicker();
            this.dtmJamSelesai = new System.Windows.Forms.DateTimePicker();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvJadwal = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dokter";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Hari";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Jam Mulai";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(61, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "Jam Selesai";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(397, 259);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 38);
            this.btnRefresh.TabIndex = 46;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.Location = new System.Drawing.Point(297, 259);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(92, 38);
            this.btnupdate.TabIndex = 45;
            this.btnupdate.Text = "Update data";
            this.btnupdate.UseVisualStyleBackColor = true;
            this.btnupdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(184, 259);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(92, 38);
            this.btnHapus.TabIndex = 44;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(71, 259);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(92, 38);
            this.btnSimpan.TabIndex = 47;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // cmbDokter
            // 
            this.cmbDokter.FormattingEnabled = true;
            this.cmbDokter.Location = new System.Drawing.Point(215, 42);
            this.cmbDokter.Name = "cmbDokter";
            this.cmbDokter.Size = new System.Drawing.Size(293, 24);
            this.cmbDokter.TabIndex = 48;
            this.cmbDokter.SelectedIndexChanged += new System.EventHandler(this.cmbDokter_SelectedIndexChanged);
            // 
            // cmbHari
            // 
            this.cmbHari.FormattingEnabled = true;
            this.cmbHari.Location = new System.Drawing.Point(215, 81);
            this.cmbHari.Name = "cmbHari";
            this.cmbHari.Size = new System.Drawing.Size(293, 24);
            this.cmbHari.TabIndex = 48;
            this.cmbHari.SelectedIndexChanged += new System.EventHandler(this.cmbHari_SelectedIndexChanged);
            // 
            // dtmJamMulai
            // 
            this.dtmJamMulai.Location = new System.Drawing.Point(215, 125);
            this.dtmJamMulai.Name = "dtmJamMulai";
            this.dtmJamMulai.Size = new System.Drawing.Size(293, 22);
            this.dtmJamMulai.TabIndex = 49;
            this.dtmJamMulai.ValueChanged += new System.EventHandler(this.dtmJamMulai_ValueChanged);
            // 
            // dtmJamSelesai
            // 
            this.dtmJamSelesai.Location = new System.Drawing.Point(215, 166);
            this.dtmJamSelesai.Name = "dtmJamSelesai";
            this.dtmJamSelesai.Size = new System.Drawing.Size(293, 22);
            this.dtmJamSelesai.TabIndex = 49;
            this.dtmJamSelesai.ValueChanged += new System.EventHandler(this.dtmJamSelesai_ValueChanged);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(38, 386);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(97, 52);
            this.btnBack.TabIndex = 50;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvJadwal
            // 
            this.dgvJadwal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJadwal.Location = new System.Drawing.Point(573, 42);
            this.dgvJadwal.Name = "dgvJadwal";
            this.dgvJadwal.RowHeadersWidth = 51;
            this.dgvJadwal.RowTemplate.Height = 24;
            this.dgvJadwal.Size = new System.Drawing.Size(595, 386);
            this.dgvJadwal.TabIndex = 51;
            this.dgvJadwal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJadwal_CellClick);
            // 
            // JadwalDokter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 450);
            this.Controls.Add(this.dgvJadwal);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dtmJamSelesai);
            this.Controls.Add(this.dtmJamMulai);
            this.Controls.Add(this.cmbHari);
            this.Controls.Add(this.cmbDokter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "JadwalDokter";
            this.Text = "JadwalDokter";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJadwal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnupdate;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.ComboBox cmbDokter;
        private System.Windows.Forms.ComboBox cmbHari;
        private System.Windows.Forms.DateTimePicker dtmJamMulai;
        private System.Windows.Forms.DateTimePicker dtmJamSelesai;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dgvJadwal;
    }
}