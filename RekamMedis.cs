using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class RekamMedis : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        private int loggedInDokterID = 1;

        private int selectedRekamMedisID = 0;

        public RekamMedis()
        {
            InitializeComponent();
        }

        private void RekamMedis_Load(object sender, EventArgs e)
        {
            // Atur judul form sesuai dokter yang login untuk kejelasan
            this.Text = $"Manajemen Rekam Medis - Dokter ID: {loggedInDokterID}";
            LoadRekamMedisGrid();
            LoadPasienComboBox();
            ClearInputFields();
        }

        private void LoadRekamMedisGrid()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetRekamMedisByDokter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DokterID", loggedInDokterID);
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRekamMedis.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPasienComboBox()
        {
            LoadDataToComboBox("sp_GetPasienForRekamMedis", cmbNamaPasien, "Nama", "PasienID",
                new SqlParameter("@DokterID", loggedInDokterID));
        }

        private void LoadDataToComboBox(string storedProcedure, ComboBox comboBox, string displayMember, string valueMember, params SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                    comboBox.Text = "Pilih...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data untuk {comboBox.Name}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbNamaPasien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNamaPasien.SelectedValue == null || cmbNamaPasien.SelectedValue is DataRowView)
            {
                cmbTanggal.DataSource = null; // Kosongkan tanggal jika pasien tidak valid
                cmbTanggal.Text = "Pilih Tanggal...";
                return;
            }

            try
            {
                int pasienID = Convert.ToInt32(cmbNamaPasien.SelectedValue);
                var paramDokter = new SqlParameter("@DokterID", loggedInDokterID);
                var paramPasien = new SqlParameter("@PasienID", pasienID);

                // Menggunakan 'TampilanTanggal' sebagai DisplayMember dari SP yang sudah diperbaiki
                LoadDataToComboBox("sp_GetTanggalKonsultasi", cmbTanggal, "TampilanTanggal", "ReservasiID", paramDokter, paramPasien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi error saat memuat tanggal konsultasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTanggal.DataSource = null;
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // Validasi sekarang berdasarkan ID Reservasi dari cmbTanggal
            if (cmbTanggal.SelectedValue == null || string.IsNullOrWhiteSpace(txtDiagnosa.Text))
            {
                MessageBox.Show("Harap pilih Pasien, Tanggal Konsultasi, dan isi Diagnosa.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddRekamMedis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Cukup kirim parameter yang dibutuhkan oleh SP baru yang lebih cerdas
                    command.Parameters.AddWithValue("@ReservasiID", Convert.ToInt32(cmbTanggal.SelectedValue));
                    command.Parameters.AddWithValue("@Keluhan", txtKeluhan.Text);
                    command.Parameters.AddWithValue("@Diagnosa", txtDiagnosa.Text);
                    command.Parameters.AddWithValue("@Tindakan", txtTindakan.Text);
                    command.Parameters.AddWithValue("@Resep", txtResep.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rekam medis berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Muat ulang semua data untuk menampilkan kondisi terbaru
                    LoadRekamMedisGrid();
                    LoadPasienComboBox(); // Pasien yang baru dibuatkan rekam medis akan otomatis hilang dari list
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRekamMedisID == 0)
            {
                MessageBox.Show("Pilih data dari tabel untuk diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnosa.Text))
            {
                MessageBox.Show("Diagnosa tidak boleh kosong.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateRekamMedis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RekamMedisID", selectedRekamMedisID);
                    command.Parameters.AddWithValue("@Keluhan", txtKeluhan.Text);
                    command.Parameters.AddWithValue("@Diagnosa", txtDiagnosa.Text);
                    command.Parameters.AddWithValue("@Tindakan", txtTindakan.Text);
                    command.Parameters.AddWithValue("@Resep", txtResep.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rekam medis berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRekamMedisGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRekamMedisGrid();
            LoadPasienComboBox();
            ClearInputFields();
        }

        private void dgvRekamMedis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];

                selectedRekamMedisID = Convert.ToInt32(row.Cells["RekamMedisID"].Value);

                // Non-aktifkan input yang tidak boleh diubah saat mode update
                cmbNamaPasien.Enabled = false;
                cmbTanggal.Enabled = false;

                // Tampilkan data yang dipilih di form
                cmbNamaPasien.Text = row.Cells["Nama Pasien"].Value.ToString();
                cmbTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("dd/MM/yyyy");
                txtKeluhan.Text = row.Cells["Keluhan"].Value.ToString();
                txtDiagnosa.Text = row.Cells["Diagnosa"].Value.ToString();
                txtTindakan.Text = row.Cells["Tindakan"].Value.ToString();
                txtResep.Text = row.Cells["Resep"].Value.ToString();

                // Ganti mode tombol Simpan/Update
                btnSimpan.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }

        private void ClearInputFields()
        {
            selectedRekamMedisID = 0;

            // Aktifkan kembali semua input untuk mode tambah data
            cmbNamaPasien.Enabled = true;
            cmbTanggal.Enabled = true;

            cmbNamaPasien.SelectedIndex = -1;
            cmbTanggal.DataSource = null;
            cmbNamaPasien.Text = "Pilih Pasien...";
            cmbTanggal.Text = "Pilih Tanggal...";

            txtKeluhan.Clear();
            txtDiagnosa.Clear();
            txtTindakan.Clear();
            txtResep.Clear();

            // Atur ulang mode tombol
            btnSimpan.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardDokter dashboard = new DashboardDokter();
            dashboard.Show();
            this.Hide();
        }

        // Method-method event yang tidak memiliki logika bisa dibiarkan kosong
        private void cmbTanggal_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtKeluhan_TextChanged(object sender, EventArgs e) { }
        private void txtDiagnosa_TextChanged(object sender, EventArgs e) { }
        private void txtTindakan_TextChanged(object sender, EventArgs e) { }
        private void txtResep_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}