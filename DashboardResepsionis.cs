using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class DashboardResepsionis: Form
    {

        static string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
        "Initial Catalog=YasminClinic2; Integrated Security=True";
        public DashboardResepsionis()
        {
            InitializeComponent();
        }

        private void btnreservation_Click(object sender, EventArgs e)
        {
            Reservasi Reservasi = new Reservasi();
            Reservasi.Show();
            this.Hide();

        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            LihatRekamMedis LihatRekamMedis = new LihatRekamMedis();
            LihatRekamMedis.Show();
            this.Hide();
        }

        private void btnPasien_Click(object sender, EventArgs e)
        {
            Pasien Pasien = new Pasien();
            Pasien.Show();
            this.Hide();
        }

        private void DashboardResepsionis_Load(object sender, EventArgs e)
        {

        }
    }
}
