namespace YasminClinic
{
    partial class DashboardResepsionis
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
            this.btnPasien = new System.Windows.Forms.Button();
            this.btnreservation = new System.Windows.Forms.Button();
            this.btnRekamMedis = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPasien
            // 
            this.btnPasien.BackgroundImage = global::YasminClinic.Properties.Resources.patient;
            this.btnPasien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPasien.Location = new System.Drawing.Point(64, 75);
            this.btnPasien.Name = "btnPasien";
            this.btnPasien.Size = new System.Drawing.Size(151, 89);
            this.btnPasien.TabIndex = 3;
            this.btnPasien.UseVisualStyleBackColor = true;
            this.btnPasien.Click += new System.EventHandler(this.btnPasien_Click);
            // 
            // btnreservation
            // 
            this.btnreservation.BackgroundImage = global::YasminClinic.Properties.Resources.reservation;
            this.btnreservation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnreservation.Location = new System.Drawing.Point(465, 75);
            this.btnreservation.Name = "btnreservation";
            this.btnreservation.Size = new System.Drawing.Size(151, 89);
            this.btnreservation.TabIndex = 3;
            this.btnreservation.UseVisualStyleBackColor = true;
            this.btnreservation.Click += new System.EventHandler(this.btnreservation_Click);
            // 
            // btnRekamMedis
            // 
            this.btnRekamMedis.BackgroundImage = global::YasminClinic.Properties.Resources.browser;
            this.btnRekamMedis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRekamMedis.Location = new System.Drawing.Point(269, 75);
            this.btnRekamMedis.Name = "btnRekamMedis";
            this.btnRekamMedis.Size = new System.Drawing.Size(151, 89);
            this.btnRekamMedis.TabIndex = 3;
            this.btnRekamMedis.UseVisualStyleBackColor = true;
            this.btnRekamMedis.Click += new System.EventHandler(this.btnRekamMedis_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kelola pasien";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(264, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lihat Rekam Medis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(460, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "Membuat janji temu";
            // 
            // DashboardResepsionis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 226);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPasien);
            this.Controls.Add(this.btnreservation);
            this.Controls.Add(this.btnRekamMedis);
            this.Name = "DashboardResepsionis";
            this.Text = "DashboardResepsionis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRekamMedis;
        private System.Windows.Forms.Button btnreservation;
        private System.Windows.Forms.Button btnPasien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}