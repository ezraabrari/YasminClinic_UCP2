using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class KelolaDokter : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        // Variabel untuk menyimpan ID Dokter yang akan diupdate/dihapus
        // Anda perlu menambahkan TextBox bernama txtDokterID di form Anda
        // dan sebuah tombol Cari untuk mengaktifkan mode Update/Hapus.
        private int selectedDokterID = 0;

        public KelolaDokter()
        {
            InitializeComponent();
        }

        private void KelolaDokter_Load(object sender, EventArgs e)
        {
            // Mengisi ComboBox Spesialisasi
            cmbSpesialisasi.Items.AddRange(new object[] { "Umum", "Gigi", "Anak", "Kandungan", "Gizi" });
            cmbSpesialisasi.DropDownStyle = ComboBoxStyle.DropDownList;
            txtPassword.PasswordChar = '*';
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            // txtDokterID.Clear(); // Jika Anda menambahkan TextBox untuk ID
            txtNama.Clear();
            cmbSpesialisasi.SelectedIndex = -1;
            txtNoTelepon.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();

            selectedDokterID = 0;
            txtPassword.Enabled = true;
            btnSimpan.Enabled = true;
            btnUpdate.Enabled = false;
            btnHapus.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || cmbSpesialisasi.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtNoTelepon.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Semua kolom wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddDokter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nama", txtNama.Text);
                    command.Parameters.AddWithValue("@Spesialisasi", cmbSpesialisasi.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@NoHP", txtNoTelepon.Text);
                    command.Parameters.AddWithValue("@Username", txtUsername.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Password", txtPassword.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Data dokter berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Untuk Update, Anda perlu cara untuk memilih DokterID.
            // Cara terbaik adalah melalui form "Lihat Data Dokter", lalu membuka form ini
            // dengan data yang sudah terisi. Untuk sementara, logika ini dinonaktifkan
            // hingga ada cara memilih dokter.
            MessageBox.Show("Fungsi Update perlu cara untuk memilih dokter terlebih dahulu (misal dari form Lihat Data).", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // Sama seperti Update, fungsi Hapus perlu cara untuk memilih DokterID.
            MessageBox.Show("Fungsi Hapus perlu cara untuk memilih dokter terlebih dahulu.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide();
        }

        private void btnLihatDataDokter_Click(object sender, EventArgs e)
        {
            DataDokter formDataDokter = new DataDokter();
            formDataDokter.Show();
            this.Hide();
        }

        // --- Event Handler Kosong untuk Memperbaiki Error Designer ---
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void cmbSpesialisasi_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtNoTelepon_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}
