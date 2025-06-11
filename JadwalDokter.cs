using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class JadwalDokter : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        private int selectedJadwalID = 0;

        public JadwalDokter()
        {
            InitializeComponent();

            // Setup ComboBox Hari items
            cmbHari.Items.AddRange(new object[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" });
            cmbHari.DropDownStyle = ComboBoxStyle.DropDownList;

            // Setup DateTimePickers for Time Only
            dtmJamMulai.Format = DateTimePickerFormat.Custom;
            dtmJamMulai.CustomFormat = "HH:mm";
            dtmJamMulai.ShowUpDown = true;

            dtmJamSelesai.Format = DateTimePickerFormat.Custom;
            dtmJamSelesai.CustomFormat = "HH:mm";
            dtmJamSelesai.ShowUpDown = true;
        }

        private void JadwalDokter_Load(object sender, EventArgs e)
        {
            SetupFormControls();
            // LoadDokterComboBox() is no longer needed as cmbDokter is removed for doctor selection
            LoadJadwalGrid();
            dgvJadwal.CellClick += dgvJadwal_CellClick; // Ensure CellClick event is attached
        }

        private void SetupFormControls()
        {
            // Already configured in constructor.
        }

        private void LoadJadwalGrid()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllJadwalDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvJadwal.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This method is now OBSOLETE as doctor selection uses txtDokter
        /*
        private void LoadDokterComboBox()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("SELECT DokterID, Nama FROM Dokter ORDER BY Nama", connection))
                {
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    cmbDokter.DataSource = null;
                    cmbDokter.DataSource = dt;
                    cmbDokter.DisplayMember = "Nama";
                    cmbDokter.ValueMember = "DokterID";
                    cmbDokter.SelectedIndex = -1;
                    cmbDokter.Text = "Pilih Dokter...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dokter: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        // NEW: Helper method to get DokterID from Dokter Nama
        private int GetDokterIdByName(string dokterNama)
        {
            if (string.IsNullOrWhiteSpace(dokterNama))
            {
                return 0; // Return 0 or throw an exception if name is empty
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("SELECT DokterID FROM Dokter WHERE Nama = @Nama", connection))
                {
                    command.Parameters.AddWithValue("@Nama", dokterNama);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari DokterID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0; // Return 0 if not found or an error occurs
        }


        private void ClearInputFields()
        {
            txtDokter.Clear(); // Clear the text box for doctor name
            cmbHari.SelectedIndex = -1;
            dtmJamMulai.Value = DateTime.Today.AddHours(8);
            dtmJamSelesai.Value = DateTime.Today.AddHours(9);
            selectedJadwalID = 0;
            btnSimpan.Enabled = true;
            btnupdate.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // Get DokterID from the text box input
            int dokterID = GetDokterIdByName(txtDokter.Text.Trim());

            if (dokterID == 0) // Check if a valid DokterID was found
            {
                MessageBox.Show("Nama Dokter tidak valid atau tidak ditemukan. Harap masukkan nama dokter yang benar.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbHari.SelectedItem == null)
            {
                MessageBox.Show("Hari wajib dipilih.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtmJamSelesai.Value <= dtmJamMulai.Value)
            {
                MessageBox.Show("Jam Selesai harus setelah Jam Mulai.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddJadwalDokter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DokterID", dokterID); // Use the retrieved DokterID
                    command.Parameters.AddWithValue("@Hari", cmbHari.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@JamMulai", dtmJamMulai.Value.TimeOfDay);
                    command.Parameters.AddWithValue("@JamSelesai", dtmJamSelesai.Value.TimeOfDay);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Jadwal dokter berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadJadwalGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedJadwalID == 0)
            {
                MessageBox.Show("Pilih jadwal dari tabel untuk diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get DokterID from the text box input for update
            int dokterID = GetDokterIdByName(txtDokter.Text.Trim());

            if (dokterID == 0) // Check if a valid DokterID was found
            {
                MessageBox.Show("Nama Dokter tidak valid atau tidak ditemukan. Harap masukkan nama dokter yang benar.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtmJamSelesai.Value <= dtmJamMulai.Value)
            {
                MessageBox.Show("Jam Selesai harus setelah Jam Mulai.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateJadwalDokter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@JadwalID", selectedJadwalID);
                    command.Parameters.AddWithValue("@DokterID", dokterID); // Use the retrieved DokterID
                    command.Parameters.AddWithValue("@Hari", cmbHari.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@JamMulai", dtmJamMulai.Value.TimeOfDay);
                    command.Parameters.AddWithValue("@JamSelesai", dtmJamSelesai.Value.TimeOfDay);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Jadwal dokter berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadJadwalGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedJadwalID == 0)
            {
                MessageBox.Show("Pilih jadwal dari tabel untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus jadwal ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("sp_DeleteJadwalDokter", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JadwalID", selectedJadwalID);
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Jadwal berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadJadwalGrid();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus jadwal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadJadwalGrid();
            ClearInputFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin dashboard = new DashboardAdmin(); // Make sure DashboardAdmin exists
            dashboard.Show();
            this.Hide();
        }

        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvJadwal.Rows[e.RowIndex];
                selectedJadwalID = Convert.ToInt32(row.Cells["JadwalID"].Value);

                // Set the doctor name in the text box
                txtDokter.Text = row.Cells["Nama Dokter"].Value.ToString();

                cmbHari.SelectedItem = row.Cells["Hari"].Value.ToString();

                TimeSpan jamMulai = (TimeSpan)row.Cells["JamMulai"].Value;
                dtmJamMulai.Value = DateTime.Today.Add(jamMulai);

                TimeSpan jamSelesai = (TimeSpan)row.Cells["JamSelesai"].Value;
                dtmJamSelesai.Value = DateTime.Today.Add(jamSelesai);

                btnSimpan.Enabled = false;
                btnupdate.Enabled = true;
            }
        }

        // --- Empty event handlers ---
        private void cmbHari_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtmJamMulai_ValueChanged(object sender, EventArgs e) { }
        private void dtmJamSelesai_ValueChanged(object sender, EventArgs e) { }
        private void txtDokter_TextChanged(object sender, EventArgs e) { } // The new text changed event for the doctor name

        // --- Code for query analysis (remains unchanged) ---

        // Event handler for the Analyze button click
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string queryToAnalyze = "EXEC sp_GetAllJadwalDetails;";
            AnalyzeQuery(queryToAnalyze);
        }

        // Method to execute a query with STATISTICS IO and STATISTICS TIME enabled
        private void AnalyzeQuery(string sqlQuery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");

                    conn.Open();

                    string wrapper = @"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;
                    " + sqlQuery + @"
                    SET STATISTICS IO OFF;
                    SET STATISTICS TIME OFF;";

                    using (SqlCommand cmd = new SqlCommand(wrapper, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error menganalisis query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}