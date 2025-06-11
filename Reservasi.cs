using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class Reservasi : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";
        private int selectedReservasiID = 0;
        private int loggedInResepsionisID = 1; // This should ideally come from a login session
        private string selectedReservasiStatus = "";

        public Reservasi()
        {
            InitializeComponent();
            // Inisialisasi DateTimePicker untuk TanggalReservasi ke tanggal hari ini
            dtpTanggalReservasi.Value = DateTime.Today;
        }

        private void Reservasi_Load(object sender, EventArgs e)
        {
            LoadInitialComboBoxes();
            LoadReservasiGrid();
            dgvReservasi.CellClick += dgvReservasi_CellClick;
        }

        private void LoadReservasiGrid()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllReservasiDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReservasi.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialComboBoxes()
        {
            LoadDataToComboBox("sp_GetAllPasien", cmbPasien, "Nama", "PasienID");
            LoadDataToComboBox("sp_GetAllSpesialisasi", cmbSpesialisasi, "Spesialisasi", "Spesialisasi");
            // Setelah initial load, kosongkan cmbDokter dan cmbJamReservasi
            cmbDokter.DataSource = null;
            cmbDokter.Text = "Pilih Dokter...";
            cmbJamReservasi.DataSource = null;
            cmbJamReservasi.Text = "Tidak ada jadwal";
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
                        command.Parameters.AddRange(parameters);

                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    comboBox.DataSource = null; // Selalu reset DataSource
                    comboBox.Items.Clear();     // Hapus juga item yang ada

                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;

                    if (dt.Rows.Count > 0)
                    {
                        comboBox.DataSource = dt;
                        comboBox.SelectedIndex = -1; // Jangan pilih otomatis item pertama
                                                     // Tidak perlu mengatur Text di sini, karena DisplayMember akan menampilkan data
                    }
                    else
                    {
                        // Menambahkan item placeholder jika tidak ada data
                        if (comboBox.Name == "cmbJamReservasi")
                        {
                            comboBox.Items.Add("Tidak ada jadwal"); // Tambahkan sebagai item
                            comboBox.SelectedIndex = 0; // Pilih item placeholder
                        }
                        else if (comboBox.Name == "cmbDokter")
                        {
                            comboBox.Items.Add("Tidak ada dokter");
                            comboBox.SelectedIndex = 0;
                        }
                        else
                        {
                            comboBox.Items.Add($"Tidak ada {displayMember}");
                            comboBox.SelectedIndex = 0;
                        }
                    }
                    comboBox.Refresh(); // Pastikan UI diperbarui
                    comboBox.Update();  // Pastikan UI diperbarui
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data untuk {comboBox.Name}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox.DataSource = null;
                comboBox.Items.Clear(); // Hapus item yang mungkin error
                comboBox.Text = "Error memuat data"; // Atur teks error
                comboBox.Refresh();
                comboBox.Update();
            }
        }

        private void cmbSpesialisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset cmbDokter dan cmbJamReservasi saat spesialisasi berubah
            cmbDokter.DataSource = null;
            cmbDokter.Text = "Pilih Dokter...";
            cmbJamReservasi.DataSource = null;
            cmbJamReservasi.Text = "Tidak ada jadwal";

            if (cmbSpesialisasi.SelectedValue == null) return;

            string spesialisasi = cmbSpesialisasi.SelectedValue.ToString();
            var parameter = new SqlParameter("@Spesialisasi", spesialisasi);
            LoadDataToComboBox("sp_GetDokterBySpesialisasi", cmbDokter, "Nama", "DokterID", parameter);
        }

        private void cmbDokter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset cmbJamReservasi saat dokter berubah
            cmbJamReservasi.DataSource = null;
            cmbJamReservasi.Text = "Tidak ada jadwal";

            if (cmbDokter.SelectedValue == null) return;
            LoadAvailableTimes(); // Muat ulang jam jika dokter dipilih
        }

        private void dtpTanggalReservasi_ValueChanged(object sender, EventArgs e)
        {
            // Reset cmbJamReservasi saat tanggal berubah
            cmbJamReservasi.DataSource = null;
            cmbJamReservasi.Text = "Tidak ada jadwal";

            if (cmbDokter.SelectedValue == null) return; // Muat ulang jam jika dokter sudah dipilih
            LoadAvailableTimes();
        }

        private void LoadAvailableTimes()
        {
            try
            {
                if (cmbDokter.SelectedValue == null)
                {
                    cmbJamReservasi.DataSource = null;
                    cmbJamReservasi.Text = "Pilih Dokter Dahulu";
                    return;
                }

                // Gunakan Convert.ToInt32 untuk menghindari SpecifiedCastException jika SelectedValue adalah DBNull
                int dokterID = Convert.ToInt32(cmbDokter.SelectedValue);
                DateTime tanggal = dtpTanggalReservasi.Value.Date;

                var paramDokterID = new SqlParameter("@DokterID", dokterID);
                var paramTanggal = new SqlParameter("@Tanggal", tanggal);

                // Memanggil SP untuk mendapatkan jam yang tersedia
                LoadDataToComboBox("sp_GetJamTersediaByDokter", cmbJamReservasi, "SlotWaktu", "SlotWaktu", paramDokterID, paramTanggal);
            }
            catch (InvalidCastException ex)
            {
                // Tangani error cast spesifik, misalnya jika SelectedValue bukan int
                MessageBox.Show("Error konversi tipe data (DokterID atau Tanggal): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbJamReservasi.DataSource = null;
                cmbJamReservasi.Text = "Error konversi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat jam tersedia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbJamReservasi.DataSource = null;
                cmbJamReservasi.Text = "Error memuat jadwal";
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbPasien.SelectedValue == null || cmbDokter.SelectedValue == null || cmbJamReservasi.SelectedValue == null)
            {
                MessageBox.Show("Harap lengkapi semua data reservasi (Pasien, Dokter, Jam).", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi tambahan: Pastikan ada jadwal yang benar-benar terpilih (bukan placeholder "Tidak ada jadwal")
            if (cmbJamReservasi.DataSource == null || cmbJamReservasi.SelectedValue.ToString() == "Tidak ada jadwal")
            {
                MessageBox.Show("Jam reservasi tidak valid. Harap pilih jam yang tersedia.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Explicitly start a transaction for atomic operation
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // Start transaction

                try
                {
                    using (var command = new SqlCommand("sp_AddReservasi", connection, transaction)) // Pass transaction to command
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PasienID", Convert.ToInt32(cmbPasien.SelectedValue));
                        command.Parameters.AddWithValue("@DokterID", Convert.ToInt32(cmbDokter.SelectedValue));
                        command.Parameters.AddWithValue("@TanggalReservasi", dtpTanggalReservasi.Value.Date);
                        command.Parameters.AddWithValue("@JamReservasi", (TimeSpan)cmbJamReservasi.SelectedValue);
                        command.Parameters.AddWithValue("@ResepsionisID", loggedInResepsionisID);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Commit transaction if successful
                    MessageBox.Show("Reservasi berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservasiGrid();
                    ClearInputFields();
                }
                catch (SqlException ex) // Catch specific SQL exceptions
                {
                    transaction.Rollback(); // Rollback transaction on error
                    // Check if the error is from the trigger (error number 50000 for RAISERROR)
                    if (ex.Number == 50000)
                    {
                        MessageBox.Show("Gagal menyimpan reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback transaction on any other error
                    MessageBox.Show("Gagal menyimpan reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedReservasiID == 0)
            {
                MessageBox.Show("Pilih reservasi dari tabel untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus reservasi ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        using (var command = new SqlCommand("sp_DeleteReservasi", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        MessageBox.Show("Reservasi berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReservasiGrid();
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Gagal menghapus reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            UpdateReservasiStatus("Selesai");
        }

        private void btnUpdateStatusBatal_Click(object sender, EventArgs e)
        {
            UpdateReservasiStatus("Batal");
        }

        private void UpdateReservasiStatus(string newStatus)
        {
            if (selectedReservasiID == 0)
            {
                MessageBox.Show("Pilih reservasi dari tabel untuk diupdate statusnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateStatusReservasi", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);
                    command.Parameters.AddWithValue("@Status", newStatus);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Status reservasi berhasil diubah menjadi '{newStatus}'.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservasiGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengupdate status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReservasiGrid();
            ClearInputFields();
        }

        private void dgvReservasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReservasi.Rows[e.RowIndex];

                selectedReservasiID = Convert.ToInt32(row.Cells["ReservasiID"].Value);
                selectedReservasiStatus = row.Cells["Status"].Value.ToString();

                // Retrieve IDs and other values from the grid row
                // Menggunakan Convert.ToInt32 dengan penanganan DBNull
                int pasienID = row.Cells["PasienID"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["PasienID"].Value);
                string spesialisasi = row.Cells["Spesialisasi"].Value.ToString();
                int dokterID = row.Cells["DokterID"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["DokterID"].Value);
                DateTime tanggalReservasi = Convert.ToDateTime(row.Cells["TanggalReservasi"].Value);
                TimeSpan jamReservasi = (TimeSpan)row.Cells["JamReservasi"].Value;

                // 1. Select Pasien
                if (pasienID > 0 && cmbPasien.Items.Cast<DataRowView>().Any(item => Convert.ToInt32(item["PasienID"]) == pasienID))
                {
                    cmbPasien.SelectedValue = pasienID;
                }
                else // Fallback jika ID tidak ditemukan atau data grid tidak lengkap
                {
                    string namaPasien = row.Cells["Nama Pasien"].Value.ToString();
                    foreach (DataRowView item in cmbPasien.Items)
                    {
                        if (item["Nama"].ToString() == namaPasien)
                        {
                            cmbPasien.SelectedItem = item;
                            break;
                        }
                    }
                }


                // 2. Select Spesialisasi (Ini akan memicu cmbSpesialisasi_SelectedIndexChanged)
                // Pastikan cmbSpesialisasi tidak null
                if (cmbSpesialisasi.Items.Cast<DataRowView>().Any(item => item["Spesialisasi"].ToString() == spesialisasi))
                {
                    cmbSpesialisasi.SelectedValue = spesialisasi;
                }
                else
                {
                    // Handle case where specialization from grid is not found in combobox
                    cmbSpesialisasi.SelectedIndex = -1; // Clear selection
                    cmbSpesialisasi.Text = spesialisasi + " (Not found)"; // Indicate missing
                }

                // 3. Set Tanggal Reservasi (Dilakukan sebelum memuat jam)
                dtpTanggalReservasi.Value = tanggalReservasi;

                // 4. Load ALL possible time slots for the selected doctor and date for display purposes
                // This uses sp_GetScheduleSlotsForDoctor (no availability filter)
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("sp_GetScheduleSlotsForDoctor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DokterID", dokterID);
                        command.Parameters.AddWithValue("@Tanggal", tanggalReservasi);

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        cmbJamReservasi.DataSource = null; // Clear previous
                        if (dt.Rows.Count > 0)
                        {
                            cmbJamReservasi.DataSource = dt;
                            cmbJamReservasi.DisplayMember = "SlotWaktu";
                            cmbJamReservasi.ValueMember = "SlotWaktu";
                            cmbJamReservasi.SelectedIndex = -1; // Default no selection
                            cmbJamReservasi.Text = "Pilih Jam..."; // Default placeholder
                        }
                        else
                        {
                            cmbJamReservasi.Text = "Tidak ada jadwal untuk tanggal ini";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error memuat semua slot jadwal untuk dokter: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbJamReservasi.DataSource = null;
                    cmbJamReservasi.Text = "Error memuat semua jadwal";
                }

                // 5. Select Dokter (after cmbSpesialisasi_SelectedIndexChanged might have reset it)
                // Pastikan cmbDokter tidak null dan memiliki item sebelum set SelectedValue
                if (dokterID > 0 && cmbDokter.Items.Cast<DataRowView>().Any(item => Convert.ToInt32(item["DokterID"]) == dokterID))
                {
                    cmbDokter.SelectedValue = dokterID;
                }
                else // Fallback jika ID tidak ditemukan atau data grid tidak lengkap
                {
                    string namaDokter = row.Cells["Nama Dokter"].Value.ToString();
                    foreach (DataRowView item in cmbDokter.Items)
                    {
                        if (item["Nama"].ToString() == namaDokter)
                        {
                            cmbDokter.SelectedItem = item;
                            break;
                        }
                    }
                }

                // 6. Select Jam Reservasi (now that all slots are loaded)
                if (cmbJamReservasi.DataSource != null)
                {
                    bool jamFound = false;
                    foreach (DataRowView item in cmbJamReservasi.Items)
                    {
                        // Ensure comparison is done on TimeSpan objects
                        if (item.Row["SlotWaktu"] is TimeSpan slotTime && slotTime == jamReservasi)
                        {
                            cmbJamReservasi.SelectedItem = item;
                            jamFound = true;
                            break;
                        }
                    }
                    if (!jamFound)
                    {
                        // Jika jam reservasi yang dipilih tidak ada dalam daftar slot yang dimuat
                        // Ini bisa terjadi jika jam tersebut di luar rentang jadwal yang dimuat sp_GetScheduleSlotsForDoctor
                        // Anda bisa memilih untuk tidak menampilkan apa-apa atau menampilkan pesan
                        cmbJamReservasi.Text = jamReservasi.ToString(@"hh\:mm") + " (Tersedia)"; // Menunjukkan jam reservasi
                    }
                }
                else
                {
                    // Jika DataSource cmbJamReservasi null, tetap tampilkan jam yang dipilih dari grid
                    cmbJamReservasi.Text = jamReservasi.ToString(@"hh\:mm");
                }
            }
        }

        private void ClearInputFields()
        {
            cmbPasien.SelectedIndex = -1;
            cmbJamReservasi.DataSource = null;
            cmbJamReservasi.Text = "Tidak ada jadwal"; // Set placeholder
            cmbSpesialisasi.SelectedIndex = -1;
            cmbDokter.DataSource = null;
            cmbDokter.Text = "Pilih Dokter..."; // Set placeholder
            dtpTanggalReservasi.Value = DateTime.Today; // Reset to today's date
            selectedReservasiID = 0;
            selectedReservasiStatus = "";
        }

        private void btrnupdate_Click(object sender, EventArgs e)
        {
            if (selectedReservasiID == 0)
            {
                MessageBox.Show("Pilih reservasi dari tabel untuk diupdate.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedReservasiStatus == "Selesai" || selectedReservasiStatus == "Batal")
            {
                MessageBox.Show($"Reservasi dengan status '{selectedReservasiStatus}' tidak dapat diubah.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbPasien.SelectedValue == null || cmbDokter.SelectedValue == null || cmbJamReservasi.SelectedValue == null)
            {
                MessageBox.Show("Harap lengkapi semua data reservasi (Pasien, Dokter, Jam).", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi tambahan: Pastikan ada jadwal yang benar-benar terpilih (bukan placeholder)
            if (cmbJamReservasi.DataSource == null || cmbJamReservasi.SelectedValue.ToString() == "Tidak ada jadwal")
            {
                MessageBox.Show("Jam reservasi tidak valid. Harap pilih jam yang tersedia.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Explicitly start a transaction for atomic operation
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // Start transaction

                try
                {
                    using (var command = new SqlCommand("sp_UpdateReservasi", connection, transaction)) // Pass transaction to command
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);
                        command.Parameters.AddWithValue("@PasienID", Convert.ToInt32(cmbPasien.SelectedValue));
                        command.Parameters.AddWithValue("@DokterID", Convert.ToInt32(cmbDokter.SelectedValue));
                        command.Parameters.AddWithValue("@TanggalReservasi", dtpTanggalReservasi.Value.Date);
                        command.Parameters.AddWithValue("@JamReservasi", (TimeSpan)cmbJamReservasi.SelectedValue);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Commit transaction if successful
                    MessageBox.Show("Reservasi berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservasiGrid();
                    ClearInputFields();
                }
                catch (SqlException ex) // Catch specific SQL exceptions
                {
                    transaction.Rollback(); // Rollback transaction on error
                    // Check if the error is from the trigger (error number 50000 for RAISERROR)
                    if (ex.Number == 50000)
                    {
                        MessageBox.Show("Gagal memperbarui reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback transaction on any other error
                    MessageBox.Show("Gagal memperbarui reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbPasien_SelectedIndexChanged(object sender, EventArgs e) { }

        private void cmbJamReservasi_SelectedIndexChanged(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardResepsionis dashboard = new DashboardResepsionis(); // Make sure DashboardResepsionis exists
            dashboard.Show();
            this.Hide(); // Sembunyikan form saat ini
        }
    }
}