using System;
using System.Drawing;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class DashboardAdmin : Form
    {
        public DashboardAdmin()
        {
            InitializeComponent();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FormLogin FormLogin = new FormLogin();
                FormLogin.Show();
                this.Close();
            }
        }

        private void btnPasien_Click(object sender, EventArgs e)
        {
            KelolaPasien KelolaPasien = new KelolaPasien();
            KelolaPasien.Show();
            this.Hide();
        }

        private void btnKelolaDokter_Click(object sender, EventArgs e)
        {
            KelolaDokter KelolaDokter = new KelolaDokter();
            KelolaDokter.Show();
            this.Hide();
        }

        private void btnJadwalDokter_Click(object sender, EventArgs e)
        {
            JadwalDokter JadwalDokter = new JadwalDokter();
            JadwalDokter.Show();
            this.Hide();
        }

        private void btnKelolaResepsionis_Click(object sender, EventArgs e)
        {
            Resepsionis Resepsionis = new Resepsionis();
            Resepsionis.Show();
            this.Hide();
        }

        private void btnreservasi_Click(object sender, EventArgs e)
        {
            KelolaReservation KelolaReservation = new KelolaReservation();
            KelolaReservation.Show();
            this.Hide();
        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            KelolaRekamMedis KelolaRekamMedis = new KelolaRekamMedis();
            KelolaRekamMedis.Show();
            this.Hide();
        }

        private void DashboardAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnReport_Click_1(object sender, EventArgs e)
        {
            Report Report = new Report();
            Report.Show();
            this.Hide();
        }

        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            FormLogin FormLogin = new FormLogin();
            FormLogin.Show();
            this.Hide();
        }
    }
}
