using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class KelolaReservation : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";
        private int selectedReservasiID = 0;
        private int loggedInResepsionisID = 1;
        private string selectedReservasiStatus = "";

        public KelolaReservation()
        {
            InitializeComponent();
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
                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data untuk {comboBox.Name}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSpesialisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpesialisasi.SelectedValue == null) return;

            string spesialisasi = cmbSpesialisasi.SelectedValue.ToString();
            var parameter = new SqlParameter("@Spesialisasi", spesialisasi);
            LoadDataToComboBox("sp_GetDokterBySpesialisasi", cmbDokter, "Nama", "DokterID", parameter);
            cmbJamReservasi.DataSource = null;
        }

        private void cmbDokter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDokter.SelectedValue == null) return;
            LoadAvailableTimes();
        }

        private void dtpTanggalReservasi_ValueChanged(object sender, EventArgs e)
        {
            if (cmbDokter.SelectedValue == null) return;
            LoadAvailableTimes();
        }

        private void LoadAvailableTimes()
        {
            try
            {
                int dokterID = (int)cmbDokter.SelectedValue;
                DateTime tanggal = dtpTanggalReservasi.Value.Date;

                var paramDokterID = new SqlParameter("@DokterID", dokterID);
                var paramTanggal = new SqlParameter("@Tanggal", tanggal);

                LoadDataToComboBox("sp_GetJamTersediaByDokter", cmbJamReservasi, "SlotWaktu", "SlotWaktu", paramDokterID, paramTanggal);
            }
            catch (Exception)
            {
                cmbJamReservasi.DataSource = null;
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbPasien.SelectedValue == null || cmbDokter.SelectedValue == null || cmbJamReservasi.SelectedValue == null)
            {
                MessageBox.Show("Harap lengkapi semua data reservasi (Pasien, Dokter, Jam).", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddReservasi", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PasienID", (int)cmbPasien.SelectedValue);
                    command.Parameters.AddWithValue("@DokterID", (int)cmbDokter.SelectedValue);
                    command.Parameters.AddWithValue("@TanggalReservasi", dtpTanggalReservasi.Value.Date);
                    command.Parameters.AddWithValue("@JamReservasi", (TimeSpan)cmbJamReservasi.SelectedValue);
                    command.Parameters.AddWithValue("@ResepsionisID", loggedInResepsionisID);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Reservasi berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservasiGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("sp_DeleteReservasi", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Reservasi berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReservasiGrid();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string namaPasien = row.Cells["Nama Pasien"].Value.ToString();
                string spesialisasi = row.Cells["Spesialisasi"].Value.ToString();
                string namaDokter = row.Cells["Nama Dokter"].Value.ToString();
                DateTime tanggalReservasi = Convert.ToDateTime(row.Cells["TanggalReservasi"].Value);
                TimeSpan jamReservasi = (TimeSpan)row.Cells["JamReservasi"].Value;

                foreach (DataRowView item in cmbPasien.Items)
                {
                    if (item["Nama"].ToString() == namaPasien)
                    {
                        cmbPasien.SelectedItem = item;
                        break;
                    }
                }

                cmbSpesialisasi.SelectedValue = spesialisasi;
                cmbSpesialisasi_SelectedIndexChanged(null, null);

                foreach (DataRowView item in cmbDokter.Items)
                {
                    if (item["Nama"].ToString() == namaDokter)
                    {
                        cmbDokter.SelectedItem = item;
                        break;
                    }
                }

                dtpTanggalReservasi.Value = tanggalReservasi;
                LoadAvailableTimes();

                foreach (DataRowView item in cmbJamReservasi.Items)
                {
                    if (item["SlotWaktu"].ToString() == jamReservasi.ToString(@"hh\:mm"))
                    {
                        cmbJamReservasi.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void ClearInputFields()
        {
            cmbPasien.SelectedIndex = -1;
            cmbJamReservasi.DataSource = null;
            cmbSpesialisasi.SelectedIndex = -1;
            cmbDokter.DataSource = null;
            dtpTanggalReservasi.Value = DateTime.Now;
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

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateReservasi", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservasiID", selectedReservasiID);
                    command.Parameters.AddWithValue("@PasienID", (int)cmbPasien.SelectedValue);
                    command.Parameters.AddWithValue("@DokterID", (int)cmbDokter.SelectedValue);
                    command.Parameters.AddWithValue("@TanggalReservasi", dtpTanggalReservasi.Value.Date);
                    command.Parameters.AddWithValue("@JamReservasi", (TimeSpan)cmbJamReservasi.SelectedValue);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Reservasi berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReservasiGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui reservasi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbPasien_SelectedIndexChanged(object sender, EventArgs e) { }

        private void cmbJamReservasi_SelectedIndexChanged(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide();
        }
    }
}
