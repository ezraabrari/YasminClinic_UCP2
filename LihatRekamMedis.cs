using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class LihatRekamMedis : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        public LihatRekamMedis()
        {
            InitializeComponent();
        }

        private void LihatRekamMedis_Load(object sender, EventArgs e)
        {
            LoadDataRekamMedis();
            SetupDataGridView();
        }

        private void LoadDataRekamMedis()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Gunakan query langsung untuk debugging
                    string query = @"
                        SELECT 
                            rm.RekamMedisID,
                            p.Nama AS [Nama Pasien],
                            d.Nama AS [Nama Dokter],
                            d.Spesialisasi,
                            rm.Tanggal,
                            rm.Keluhan,
                            rm.Diagnosa,
                            rm.Tindakan,
                            rm.Resep
                        FROM RekamMedis rm
                        INNER JOIN Pasien p ON rm.PasienID = p.PasienID
                        INNER JOIN Dokter d ON rm.DokterID = d.DokterID
                        ORDER BY rm.Tanggal DESC, p.Nama";

                    using (var command = new SqlCommand(query, connection))
                    {
                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Tidak ada data rekam medis yang ditemukan.", "Informasi",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Berhasil memuat {dt.Rows.Count} data rekam medis.", "Sukses",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dgvRekamMedis.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message + "\n\nDetail: " + ex.StackTrace,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                // Konfigurasi tampilan DataGridView
                dgvRekamMedis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRekamMedis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRekamMedis.MultiSelect = false;
                dgvRekamMedis.ReadOnly = true;
                dgvRekamMedis.AllowUserToAddRows = false;
                dgvRekamMedis.AllowUserToDeleteRows = false;

                // Sembunyikan kolom ID jika ada
                if (dgvRekamMedis.Columns["RekamMedisID"] != null)
                {
                    dgvRekamMedis.Columns["RekamMedisID"].Visible = false;
                }

                // Atur lebar kolom
                if (dgvRekamMedis.Columns["Nama Pasien"] != null)
                    dgvRekamMedis.Columns["Nama Pasien"].FillWeight = 80;

                if (dgvRekamMedis.Columns["Nama Dokter"] != null)
                    dgvRekamMedis.Columns["Nama Dokter"].FillWeight = 80;

                if (dgvRekamMedis.Columns["Spesialisasi"] != null)
                    dgvRekamMedis.Columns["Spesialisasi"].FillWeight = 60;

                if (dgvRekamMedis.Columns["Tanggal"] != null)
                {
                    dgvRekamMedis.Columns["Tanggal"].FillWeight = 60;
                    dgvRekamMedis.Columns["Tanggal"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgvRekamMedis.Columns["Keluhan"] != null)
                    dgvRekamMedis.Columns["Keluhan"].FillWeight = 100;

                if (dgvRekamMedis.Columns["Diagnosa"] != null)
                    dgvRekamMedis.Columns["Diagnosa"].FillWeight = 100;

                if (dgvRekamMedis.Columns["Tindakan"] != null)
                    dgvRekamMedis.Columns["Tindakan"].FillWeight = 100;

                if (dgvRekamMedis.Columns["Resep"] != null)
                    dgvRekamMedis.Columns["Resep"].FillWeight = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dalam setup DataGridView: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataRekamMedis();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardResepsionis dashboard = new DashboardResepsionis();
            dashboard.Show();
            this.Hide();
        }

        private void dgvRekamMedis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event handler untuk klik sel - bisa dikembangkan untuk detail view
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];
                string namaPasien = row.Cells["Nama Pasien"].Value?.ToString() ?? "";
                string tanggal = row.Cells["Tanggal"].Value?.ToString() ?? "";

                MessageBox.Show($"Detail Rekam Medis:\nPasien: {namaPasien}\nTanggal: {tanggal}",
                              "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Method untuk debugging - bisa dihapus setelah selesai
        private void btnDebugCheck_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Cek jumlah data di setiap tabel
                    var commands = new[]
                    {
                        "SELECT COUNT(*) FROM RekamMedis",
                        "SELECT COUNT(*) FROM Pasien",
                        "SELECT COUNT(*) FROM Dokter"
                    };

                    string result = "Debug Info:\n";

                    foreach (var cmdText in commands)
                    {
                        using (var cmd = new SqlCommand(cmdText, connection))
                        {
                            int count = (int)cmd.ExecuteScalar();
                            result += $"{cmdText}: {count} rows\n";
                        }
                    }

                    // Cek data rekam medis spesifik
                    using (var cmd = new SqlCommand("SELECT TOP 5 * FROM RekamMedis", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            result += "\nSample RekamMedis data:\n";
                            while (reader.Read())
                            {
                                result += $"ID: {reader["RekamMedisID"]}, PasienID: {reader["PasienID"]}, DokterID: {reader["DokterID"]}\n";
                            }
                        }
                    }

                    MessageBox.Show(result, "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debug error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}