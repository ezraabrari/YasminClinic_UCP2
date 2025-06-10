namespace YasminClinic
{
    partial class RekamMedis
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbNamaPasien = new System.Windows.Forms.ComboBox();
            this.cmbTanggal = new System.Windows.Forms.ComboBox();
            this.txtKeluhan = new System.Windows.Forms.TextBox();
            this.txtDiagnosa = new System.Windows.Forms.TextBox();
            this.txtTindakan = new System.Windows.Forms.TextBox();
            this.txtResep = new System.Windows.Forms.TextBox();
            this.dgvRekamMedis = new System.Windows.Forms.DataGridView();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama Pasien";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Keluhan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "diagnosa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(44, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tindakan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(44, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "Resep";
            // 
            // cmbNamaPasien
            // 
            this.cmbNamaPasien.FormattingEnabled = true;
            this.cmbNamaPasien.Location = new System.Drawing.Point(210, 40);
            this.cmbNamaPasien.Name = "cmbNamaPasien";
            this.cmbNamaPasien.Size = new System.Drawing.Size(261, 24);
            this.cmbNamaPasien.TabIndex = 1;
            this.cmbNamaPasien.SelectedIndexChanged += new System.EventHandler(this.cmbNamaPasien_SelectedIndexChanged);
            // 
            // cmbTanggal
            // 
            this.cmbTanggal.FormattingEnabled = true;
            this.cmbTanggal.Location = new System.Drawing.Point(210, 84);
            this.cmbTanggal.Name = "cmbTanggal";
            this.cmbTanggal.Size = new System.Drawing.Size(261, 24);
            this.cmbTanggal.TabIndex = 1;
            this.cmbTanggal.SelectedIndexChanged += new System.EventHandler(this.cmbTanggal_SelectedIndexChanged);
            // 
            // txtKeluhan
            // 
            this.txtKeluhan.Location = new System.Drawing.Point(210, 128);
            this.txtKeluhan.Name = "txtKeluhan";
            this.txtKeluhan.Size = new System.Drawing.Size(261, 22);
            this.txtKeluhan.TabIndex = 2;
            this.txtKeluhan.TextChanged += new System.EventHandler(this.txtKeluhan_TextChanged);
            // 
            // txtDiagnosa
            // 
            this.txtDiagnosa.Location = new System.Drawing.Point(210, 172);
            this.txtDiagnosa.Name = "txtDiagnosa";
            this.txtDiagnosa.Size = new System.Drawing.Size(261, 22);
            this.txtDiagnosa.TabIndex = 2;
            this.txtDiagnosa.TextChanged += new System.EventHandler(this.txtDiagnosa_TextChanged);
            // 
            // txtTindakan
            // 
            this.txtTindakan.Location = new System.Drawing.Point(210, 211);
            this.txtTindakan.Name = "txtTindakan";
            this.txtTindakan.Size = new System.Drawing.Size(261, 22);
            this.txtTindakan.TabIndex = 2;
            this.txtTindakan.TextChanged += new System.EventHandler(this.txtTindakan_TextChanged);
            // 
            // txtResep
            // 
            this.txtResep.Location = new System.Drawing.Point(210, 251);
            this.txtResep.Name = "txtResep";
            this.txtResep.Size = new System.Drawing.Size(261, 22);
            this.txtResep.TabIndex = 2;
            this.txtResep.TextChanged += new System.EventHandler(this.txtResep_TextChanged);
            // 
            // dgvRekamMedis
            // 
            this.dgvRekamMedis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRekamMedis.Location = new System.Drawing.Point(493, 40);
            this.dgvRekamMedis.Name = "dgvRekamMedis";
            this.dgvRekamMedis.RowHeadersWidth = 51;
            this.dgvRekamMedis.RowTemplate.Height = 24;
            this.dgvRekamMedis.Size = new System.Drawing.Size(653, 329);
            this.dgvRekamMedis.TabIndex = 4;
            this.dgvRekamMedis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRekamMedis_CellClick);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(48, 309);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(86, 36);
            this.btnSimpan.TabIndex = 28;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(177, 309);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 36);
            this.btnUpdate.TabIndex = 28;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(357, 309);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 36);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(36, 396);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(87, 42);
            this.btnBack.TabIndex = 27;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // RekamMedis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvRekamMedis);
            this.Controls.Add(this.txtResep);
            this.Controls.Add(this.txtTindakan);
            this.Controls.Add(this.txtDiagnosa);
            this.Controls.Add(this.txtKeluhan);
            this.Controls.Add(this.cmbTanggal);
            this.Controls.Add(this.cmbNamaPasien);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RekamMedis";
            this.Text = "RekamMedis";
            this.Load += new System.EventHandler(this.RekamMedis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbNamaPasien;
        private System.Windows.Forms.ComboBox cmbTanggal;
        private System.Windows.Forms.TextBox txtKeluhan;
        private System.Windows.Forms.TextBox txtDiagnosa;
        private System.Windows.Forms.TextBox txtTindakan;
        private System.Windows.Forms.TextBox txtResep;
        private System.Windows.Forms.DataGridView dgvRekamMedis;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRefresh;
    }
}