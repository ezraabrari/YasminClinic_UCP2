using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class Resepsionis : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        // Variabel untuk menyimpan ResepsionisID yang sedang dipilih di grid
        private int selectedResepsionisID = 0;

        public Resepsionis()
        {
            InitializeComponent();
        }

        private void Resepsionis_Load(object sender, EventArgs e)
        {
            // Atur agar password tersembunyi
            txtPassword.PasswordChar = '*';

            // Muat data saat form dibuka
            LoadDataResepsionis();
        }

        private void LoadDataResepsionis()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllResepsionis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvResepsionis.DataSource = dt; // Asumsikan nama DataGridView Anda dgvResepsionis
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data resepsionis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtNama.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            selectedResepsionisID = 0;
            btnSimpan.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Semua kolom wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddResepsionis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nama", txtNama.Text);
                    command.Parameters.AddWithValue("@Username", txtUsername.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Password", txtPassword.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Data resepsionis berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataResepsionis();
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
            if (selectedResepsionisID == 0)
            {
                MessageBox.Show("Pilih data dari tabel untuk diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateResepsionis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ResepsionisID", selectedResepsionisID);
                    command.Parameters.AddWithValue("@Nama", txtNama.Text);
                    // Update untuk Username, Email, dan Password bisa ditambahkan di sini
                    // dan di Stored Procedure 'sp_UpdateResepsionis' jika diperlukan.

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Data resepsionis berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataResepsionis();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedResepsionisID == 0)
            {
                MessageBox.Show("Pilih data dari tabel untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("sp_DeleteResepsionis", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResepsionisID", selectedResepsionisID);

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data resepsionis berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataResepsionis();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataResepsionis();
            ClearInputFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin dashboard = new DashboardAdmin();
            dashboard.Show();
            this.Hide();
        }

        private void dgvResepsionis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvResepsionis.Rows[e.RowIndex];
                selectedResepsionisID = Convert.ToInt32(row.Cells["ResepsionisID"].Value);
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPassword.Text = ""; // Kosongkan password untuk keamanan

                btnSimpan.Enabled = false; // Nonaktifkan simpan saat mode edit
                btnUpdate.Enabled = true;  // Aktifkan update
            }
        }

        // --- Event handler kosong lainnya ---
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}
