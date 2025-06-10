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
    public partial class DashboardDokter: Form
    {
        public DashboardDokter()
        {
            InitializeComponent();
        }

        private void btnRekamMedis_Click(object sender, EventArgs e)
        {
            RekamMedis RekamMedis = new RekamMedis();
            RekamMedis.Show();
            this.Hide();
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            LihatJadwalReservasi LihatJadwalReservasi = new LihatJadwalReservasi();
            LihatJadwalReservasi.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormLogin FormLogin = new FormLogin();
            FormLogin.Show();
            this.Hide();
        }
    }
}
