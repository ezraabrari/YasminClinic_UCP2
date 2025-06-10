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

            cmbHari.Items.AddRange(new object[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" });
            cmbHari.DropDownStyle = ComboBoxStyle.DropDownList;

            // Atur DateTimePicker untuk hanya menampilkan format Waktu
            dtmJamMulai.Format = DateTimePickerFormat.Custom;
            dtmJamMulai.CustomFormat = "HH:mm";
            dtmJamMulai.ShowUpDown = true;

            dtmJamSelesai.Format = DateTimePickerFormat.Custom;
            dtmJamSelesai.CustomFormat = "HH:mm";
            dtmJamSelesai.ShowUpDown = true;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("SELECT DokterID, Nama FROM Dokter ORDER BY Nama", connection))
                {
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    // PERBAIKAN: Memastikan DataSource di-reset sebelum diisi ulang
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

        private void JadwalDokter_Load(object sender, EventArgs e)
        {
            SetupFormControls();
            LoadDokterComboBox();
            LoadJadwalGrid();
        }

        private void SetupFormControls()
        {
            // Atur ComboBox Hari
            cmbHari.Items.AddRange(new object[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" });
            cmbHari.DropDownStyle = ComboBoxStyle.DropDownList;

            // Atur DateTimePicker untuk hanya menampilkan format Waktu
            dtmJamMulai.Format = DateTimePickerFormat.Custom;
            dtmJamMulai.CustomFormat = "HH:mm";
            dtmJamMulai.ShowUpDown = true;

            dtmJamSelesai.Format = DateTimePickerFormat.Custom;
            dtmJamSelesai.CustomFormat = "HH:mm";
            dtmJamSelesai.ShowUpDown = true;
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

                    // PERBAIKAN: Memastikan DataSource di-reset sebelum diisi ulang
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

        private void ClearInputFields()
        {
            cmbDokter.SelectedIndex = -1;
            cmbHari.SelectedIndex = -1;
            dtmJamMulai.Value = DateTime.Today.AddHours(8);
            dtmJamSelesai.Value = DateTime.Today.AddHours(9);
            selectedJadwalID = 0;
            btnSimpan.Enabled = true;
            btnupdate.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbDokter.SelectedValue == null || cmbHari.SelectedItem == null)
            {
                MessageBox.Show("Dokter dan Hari wajib dipilih.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    command.Parameters.AddWithValue("@DokterID", (int)cmbDokter.SelectedValue);
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
                    command.Parameters.AddWithValue("@DokterID", (int)cmbDokter.SelectedValue);
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
            DashboardAdmin dashboard = new DashboardAdmin();
            dashboard.Show();
            this.Hide();
        }

        private void dgvJadwal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvJadwal.Rows[e.RowIndex];
                selectedJadwalID = Convert.ToInt32(row.Cells["JadwalID"].Value);

                cmbDokter.Text = row.Cells["Nama Dokter"].Value.ToString();
                cmbHari.SelectedItem = row.Cells["Hari"].Value.ToString();

                TimeSpan jamMulai = (TimeSpan)row.Cells["JamMulai"].Value;
                dtmJamMulai.Value = DateTime.Today.Add(jamMulai);

                TimeSpan jamSelesai = (TimeSpan)row.Cells["JamSelesai"].Value;
                dtmJamSelesai.Value = DateTime.Today.Add(jamSelesai);

                btnSimpan.Enabled = false;
                btnupdate.Enabled = true;
            }
        }

        // --- Event handler kosong lainnya ---
        private void cmbDokter_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbHari_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtmJamMulai_ValueChanged(object sender, EventArgs e) { }
        private void dtmJamSelesai_ValueChanged(object sender, EventArgs e) { }
    }
}
