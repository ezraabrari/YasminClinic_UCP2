namespace YasminClinic
{
    partial class KelolaRekamMedis
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.dgvRekamMedis = new System.Windows.Forms.DataGridView();
            this.txtResep = new System.Windows.Forms.TextBox();
            this.txtTindakan = new System.Windows.Forms.TextBox();
            this.txtDiagnosa = new System.Windows.Forms.TextBox();
            this.txtKeluhan = new System.Windows.Forms.TextBox();
            this.cmbNamaPasien = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(348, 295);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 36);
            this.btnRefresh.TabIndex = 45;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(242, 295);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 36);
            this.btnUpdate.TabIndex = 44;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(45, 295);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(86, 36);
            this.btnSimpan.TabIndex = 43;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // dgvRekamMedis
            // 
            this.dgvRekamMedis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRekamMedis.Location = new System.Drawing.Point(490, 26);
            this.dgvRekamMedis.Name = "dgvRekamMedis";
            this.dgvRekamMedis.RowHeadersWidth = 51;
            this.dgvRekamMedis.RowTemplate.Height = 24;
            this.dgvRekamMedis.Size = new System.Drawing.Size(653, 329);
            this.dgvRekamMedis.TabIndex = 41;
            this.dgvRekamMedis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRekamMedis_CellContentClick);
            // 
            // txtResep
            // 
            this.txtResep.Location = new System.Drawing.Point(207, 202);
            this.txtResep.Name = "txtResep";
            this.txtResep.Size = new System.Drawing.Size(261, 22);
            this.txtResep.TabIndex = 39;
            this.txtResep.TextChanged += new System.EventHandler(this.txtResep_TextChanged);
            // 
            // txtTindakan
            // 
            this.txtTindakan.Location = new System.Drawing.Point(207, 162);
            this.txtTindakan.Name = "txtTindakan";
            this.txtTindakan.Size = new System.Drawing.Size(261, 22);
            this.txtTindakan.TabIndex = 38;
            this.txtTindakan.TextChanged += new System.EventHandler(this.txtTindakan_TextChanged);
            // 
            // txtDiagnosa
            // 
            this.txtDiagnosa.Location = new System.Drawing.Point(207, 123);
            this.txtDiagnosa.Name = "txtDiagnosa";
            this.txtDiagnosa.Size = new System.Drawing.Size(261, 22);
            this.txtDiagnosa.TabIndex = 40;
            this.txtDiagnosa.TextChanged += new System.EventHandler(this.txtDiagnosa_TextChanged);
            // 
            // txtKeluhan
            // 
            this.txtKeluhan.Location = new System.Drawing.Point(207, 79);
            this.txtKeluhan.Name = "txtKeluhan";
            this.txtKeluhan.Size = new System.Drawing.Size(261, 22);
            this.txtKeluhan.TabIndex = 37;
            this.txtKeluhan.TextChanged += new System.EventHandler(this.txtKeluhan_TextChanged);
            // 
            // cmbNamaPasien
            // 
            this.cmbNamaPasien.FormattingEnabled = true;
            this.cmbNamaPasien.Location = new System.Drawing.Point(207, 26);
            this.cmbNamaPasien.Name = "cmbNamaPasien";
            this.cmbNamaPasien.Size = new System.Drawing.Size(261, 24);
            this.cmbNamaPasien.TabIndex = 35;
            this.cmbNamaPasien.SelectedIndexChanged += new System.EventHandler(this.cmbNamaPasien_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 24);
            this.label6.TabIndex = 33;
            this.label6.Text = "Resep";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 24);
            this.label5.TabIndex = 32;
            this.label5.Text = "Tindakan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 24);
            this.label4.TabIndex = 31;
            this.label4.Text = "diagnosa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 24);
            this.label3.TabIndex = 30;
            this.label3.Text = "Keluhan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Nama Pasien";
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(33, 382);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(87, 42);
            this.btnBack.TabIndex = 42;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(148, 295);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(86, 36);
            this.btnHapus.TabIndex = 44;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // KelolaRekamMedis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvRekamMedis);
            this.Controls.Add(this.txtResep);
            this.Controls.Add(this.txtTindakan);
            this.Controls.Add(this.txtDiagnosa);
            this.Controls.Add(this.txtKeluhan);
            this.Controls.Add(this.cmbNamaPasien);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "KelolaRekamMedis";
            this.Text = "KelolaRekamMedis";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridView dgvRekamMedis;
        private System.Windows.Forms.TextBox txtResep;
        private System.Windows.Forms.TextBox txtTindakan;
        private System.Windows.Forms.TextBox txtDiagnosa;
        private System.Windows.Forms.TextBox txtKeluhan;
        private System.Windows.Forms.ComboBox cmbNamaPasien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHapus;
    }
}