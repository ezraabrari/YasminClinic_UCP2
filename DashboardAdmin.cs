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
            Pasien Pasien = new Pasien();
            Pasien.Show();
            this.Hide();
        }

        private void btnKelolaDokter_Click(object sender, EventArgs e)
        {

        }

        private void btnJadwalDokter_Click(object sender, EventArgs e)
        {
            Resepsionis Resepsionis = new Resepsionis();
            Resepsionis.Show();
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
            Reservasi Reservasi = new Reservasi();
            Reservasi.Show();
            this.Hide();


        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {

        }
    }
}
