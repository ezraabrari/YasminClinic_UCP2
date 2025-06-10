namespace YasminClinic
{
    partial class FormLogin
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoginAdmin = new System.Windows.Forms.Button();
            this.btnlogindokter = new System.Windows.Forms.Button();
            this.btnloginResepsionis = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("Plus Jakarta Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(228, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Login Sebagai Dokter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Plus Jakarta Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Login Sebagai Resepsionis";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 102);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Highlight;
            this.label3.Font = new System.Drawing.Font("Plus Jakarta Sans SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 33);
            this.label3.TabIndex = 1;
            this.label3.Text = "LOGIN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Font = new System.Drawing.Font("Plus Jakarta Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(414, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 22);
            this.label4.TabIndex = 5;
            this.label4.Text = "Login Sebagai Admin";
            // 
            // btnLoginAdmin
            // 
            this.btnLoginAdmin.BackgroundImage = global::YasminClinic.Properties.Resources.administrator;
            this.btnLoginAdmin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLoginAdmin.Location = new System.Drawing.Point(411, 125);
            this.btnLoginAdmin.Name = "btnLoginAdmin";
            this.btnLoginAdmin.Size = new System.Drawing.Size(144, 116);
            this.btnLoginAdmin.TabIndex = 3;
            this.btnLoginAdmin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLoginAdmin.UseVisualStyleBackColor = true;
            this.btnLoginAdmin.Click += new System.EventHandler(this.btnLoginAdmin_Click);
            // 
            // btnlogindokter
            // 
            this.btnlogindokter.BackgroundImage = global::YasminClinic.Properties.Resources.doctor;
            this.btnlogindokter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnlogindokter.Location = new System.Drawing.Point(225, 125);
            this.btnlogindokter.Name = "btnlogindokter";
            this.btnlogindokter.Size = new System.Drawing.Size(144, 116);
            this.btnlogindokter.TabIndex = 3;
            this.btnlogindokter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnlogindokter.UseVisualStyleBackColor = true;
            this.btnlogindokter.Click += new System.EventHandler(this.btnlogindokter_Click);
            // 
            // btnloginResepsionis
            // 
            this.btnloginResepsionis.BackgroundImage = global::YasminClinic.Properties.Resources.receptionist;
            this.btnloginResepsionis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnloginResepsionis.Location = new System.Drawing.Point(30, 125);
            this.btnloginResepsionis.Name = "btnloginResepsionis";
            this.btnloginResepsionis.Size = new System.Drawing.Size(144, 116);
            this.btnloginResepsionis.TabIndex = 4;
            this.btnloginResepsionis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnloginResepsionis.UseVisualStyleBackColor = true;
            this.btnloginResepsionis.Click += new System.EventHandler(this.btnloginpasien_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 282);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoginAdmin);
            this.Controls.Add(this.btnlogindokter);
            this.Controls.Add(this.btnloginResepsionis);
            this.Controls.Add(this.panel1);
            this.Name = "FormLogin";
            this.Text = "FormLogin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnlogindokter;
        private System.Windows.Forms.Button btnloginResepsionis;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoginAdmin;
        private System.Windows.Forms.Label label4;
    }
}