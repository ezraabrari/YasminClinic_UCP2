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

        // GANTI INI dengan ID Dokter yang login dari form sebelumnya
        private int loggedInDokterID = 2; // Contoh: ID untuk Nina Yusuf

        private int selectedRekamMedisID = 0;
        private int selectedReservasiID = 0;

        public RekamMedis()
        {
            InitializeComponent();
        }

        private void RekamMedis_Load(object sender, EventArgs e)
        {
            LoadPasienComboBox();
            LoadRekamMedisGrid();
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
            if (cmbNamaPasien.SelectedValue == null || cmbNamaPasien.SelectedValue is DataRowView) return;

            try
            {
                int pasienID = Convert.ToInt32(cmbNamaPasien.SelectedValue);
                var paramDokter = new SqlParameter("@DokterID", loggedInDokterID);
                var paramPasien = new SqlParameter("@PasienID", pasienID);
                // PERBAIKAN: Menggunakan ReservasiID sebagai ValueMember untuk cmbTanggal
                LoadDataToComboBox("sp_GetTanggalKonsultasi", cmbTanggal, "TanggalReservasi", "ReservasiID", paramDokter, paramPasien);
            }
            catch (Exception)
            {
                cmbTanggal.DataSource = null;
            }
        }

        private void cmbTanggal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTanggal.SelectedValue != null && !(cmbTanggal.SelectedValue is DataRowView))
            {
                try
                {
                    selectedReservasiID = Convert.ToInt32(cmbTanggal.SelectedValue);
                }
                catch (Exception)
                {
                    selectedReservasiID = 0;
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbNamaPasien.SelectedValue == null || cmbTanggal.SelectedValue == null || string.IsNullOrWhiteSpace(txtDiagnosa.Text))
            {
                MessageBox.Show("Pasien, Tanggal, dan Diagnosa wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddRekamMedis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PasienID", Convert.ToInt32(cmbNamaPasien.SelectedValue));
                    command.Parameters.AddWithValue("@DokterID", loggedInDokterID);
                    command.Parameters.AddWithValue("@Tanggal", Convert.ToDateTime(cmbTanggal.Text)); // Mengambil teks tanggal yang terlihat
                    command.Parameters.AddWithValue("@Keluhan", txtKeluhan.Text);
                    command.Parameters.AddWithValue("@Diagnosa", txtDiagnosa.Text);
                    command.Parameters.AddWithValue("@Tindakan", txtTindakan.Text);
                    command.Parameters.AddWithValue("@Resep", txtResep.Text);

                    // PERBAIKAN: Menambahkan parameter @ReservasiID yang hilang
                    command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rekam medis berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRekamMedisGrid();
                    ClearInputFields();
                    LoadPasienComboBox();
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
            ClearInputFields();
            LoadRekamMedisGrid();
            LoadPasienComboBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardDokter dashboard = new DashboardDokter();
            dashboard.Show();
            this.Hide();
        }

        private void dgvRekamMedis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];

                selectedRekamMedisID = Convert.ToInt32(row.Cells["RekamMedisID"].Value);

                cmbNamaPasien.Enabled = false;
                cmbTanggal.Enabled = false;

                cmbNamaPasien.Text = row.Cells["Nama Pasien"].Value.ToString();
                cmbTanggal.Text = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToShortDateString();
                txtKeluhan.Text = row.Cells["Keluhan"].Value.ToString();
                txtDiagnosa.Text = row.Cells["Diagnosa"].Value.ToString();
                txtTindakan.Text = row.Cells["Tindakan"].Value.ToString();
                txtResep.Text = row.Cells["Resep"].Value.ToString();

                btnSimpan.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }

        private void ClearInputFields()
        {
            selectedRekamMedisID = 0;
            selectedReservasiID = 0;
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
            btnSimpan.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void txtKeluhan_TextChanged(object sender, EventArgs e) { }
        private void txtDiagnosa_TextChanged(object sender, EventArgs e) { }
        private void txtTindakan_TextChanged(object sender, EventArgs e) { }
        private void txtResep_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
