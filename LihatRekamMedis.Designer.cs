namespace YasminClinic
{
    partial class LihatRekamMedis
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
            this.dgvRekamMedis = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRekamMedis
            // 
            this.dgvRekamMedis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRekamMedis.Location = new System.Drawing.Point(37, 22);
            this.dgvRekamMedis.Name = "dgvRekamMedis";
            this.dgvRekamMedis.RowHeadersWidth = 51;
            this.dgvRekamMedis.RowTemplate.Height = 24;
            this.dgvRekamMedis.Size = new System.Drawing.Size(726, 339);
            this.dgvRekamMedis.TabIndex = 0;
            this.dgvRekamMedis.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRekamMedis_CellContentClick);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::YasminClinic.Properties.Resources.undo;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBack.Location = new System.Drawing.Point(37, 386);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(97, 52);
            this.btnBack.TabIndex = 1;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(468, 386);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 2;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebugCheck_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // LihatRekamMedis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvRekamMedis);
            this.Name = "LihatRekamMedis";
            this.Text = "LihatRekamMedis";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRekamMedis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRekamMedis;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button button1;
    }
}