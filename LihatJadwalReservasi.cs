using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class LihatJadwalReservasi : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        public LihatJadwalReservasi()
        {
            InitializeComponent();
        }

        private void LihatJadwalReservasi_Load(object sender, EventArgs e)
        {
            LoadDataReservasi();
            SetupDataGridView();
        }

        private void LoadDataReservasi()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Menggunakan stored procedure yang sudah ada
                    using (var command = new SqlCommand("sp_GetAllReservasiDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Tidak ada data reservasi yang ditemukan.", "Informasi",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Berhasil memuat {dt.Rows.Count} data reservasi.", "Sukses",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dgvReservasi.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data reservasi: " + ex.Message + "\n\nDetail: " + ex.StackTrace,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                // Konfigurasi tampilan DataGridView
                dgvReservasi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvReservasi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvReservasi.MultiSelect = false;
                dgvReservasi.ReadOnly = true;
                dgvReservasi.AllowUserToAddRows = false;
                dgvReservasi.AllowUserToDeleteRows = false;

                // Sembunyikan kolom ID jika ada
                if (dgvReservasi.Columns["ReservasiID"] != null)
                {
                    dgvReservasi.Columns["ReservasiID"].Visible = false;
                }

                // Atur lebar kolom
                if (dgvReservasi.Columns["Nama Pasien"] != null)
                    dgvReservasi.Columns["Nama Pasien"].FillWeight = 90;

                if (dgvReservasi.Columns["Nama Dokter"] != null)
                    dgvReservasi.Columns["Nama Dokter"].FillWeight = 90;

                if (dgvReservasi.Columns["Spesialisasi"] != null)
                    dgvReservasi.Columns["Spesialisasi"].FillWeight = 70;

                if (dgvReservasi.Columns["TanggalReservasi"] != null)
                {
                    dgvReservasi.Columns["TanggalReservasi"].FillWeight = 70;
                    dgvReservasi.Columns["TanggalReservasi"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvReservasi.Columns["TanggalReservasi"].HeaderText = "Tanggal";
                }

                if (dgvReservasi.Columns["JamReservasi"] != null)
                {
                    dgvReservasi.Columns["JamReservasi"].FillWeight = 60;
                    dgvReservasi.Columns["JamReservasi"].DefaultCellStyle.Format = "HH:mm";
                    dgvReservasi.Columns["JamReservasi"].HeaderText = "Jam";
                }

                if (dgvReservasi.Columns["Status"] != null)
                {
                    dgvReservasi.Columns["Status"].FillWeight = 60;

                    // Atur warna berdasarkan status
                    dgvReservasi.CellFormatting += (sender, e) =>
                    {
                        if (e.ColumnIndex == dgvReservasi.Columns["Status"].Index && e.Value != null)
                        {
                            string status = e.Value.ToString();
                            switch (status)
                            {
                                case "Terjadwal":
                                    e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                                    e.CellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                                    break;
                                case "Selesai":
                                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                                    e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                                    break;
                                case "Batal":
                                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                                    e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                                    break;
                            }
                        }
                    };
                }

                if (dgvReservasi.Columns["Dibuat Oleh (Resepsionis)"] != null)
                {
                    dgvReservasi.Columns["Dibuat Oleh (Resepsionis)"].FillWeight = 80;
                    dgvReservasi.Columns["Dibuat Oleh (Resepsionis)"].HeaderText = "Resepsionis";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dalam setup DataGridView: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReservasi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event handler untuk klik sel - menampilkan detail reservasi
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReservasi.Rows[e.RowIndex];

                string namaPasien = row.Cells["Nama Pasien"].Value?.ToString() ?? "";
                string namaDokter = row.Cells["Nama Dokter"].Value?.ToString() ?? "";
                string spesialisasi = row.Cells["Spesialisasi"].Value?.ToString() ?? "";
                string tanggal = row.Cells["TanggalReservasi"].Value?.ToString() ?? "";
                string jam = row.Cells["JamReservasi"].Value?.ToString() ?? "";
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                string resepsionis = row.Cells["Dibuat Oleh (Resepsionis)"].Value?.ToString() ?? "";

                // Format tanggal untuk tampilan
                if (DateTime.TryParse(tanggal, out DateTime tgl))
                {
                    tanggal = tgl.ToString("dd/MM/yyyy");
                }

                // Format jam untuk tampilan
                if (TimeSpan.TryParse(jam, out TimeSpan jamParsed))
                {
                    jam = jamParsed.ToString(@"hh\:mm");
                }

                string detailMessage = $"=== DETAIL RESERVASI ===\n\n" +
                                     $"Pasien: {namaPasien}\n" +
                                     $"Dokter: {namaDokter}\n" +
                                     $"Spesialisasi: {spesialisasi}\n" +
                                     $"Tanggal: {tanggal}\n" +
                                     $"Jam: {jam}\n" +
                                     $"Status: {status}\n" +
                                     $"Dibuat oleh: {resepsionis}";

                MessageBox.Show(detailMessage, "Detail Reservasi",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataReservasi();
            MessageBox.Show("Data reservasi telah diperbarui!", "Refresh",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke dashboard resepsionis
            DashboardDokter DashboardDokter = new DashboardDokter();
            DashboardDokter.Show();
            this.Hide();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            // Method untuk debugging - menampilkan statistik reservasi
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var queries = new[]
                    {
                        "SELECT COUNT(*) FROM Reservasi",
                        "SELECT COUNT(*) FROM Reservasi WHERE Status = 'Terjadwal'",
                        "SELECT COUNT(*) FROM Reservasi WHERE Status = 'Selesai'",
                        "SELECT COUNT(*) FROM Reservasi WHERE Status = 'Batal'",
                        "SELECT COUNT(*) FROM Pasien",
                        "SELECT COUNT(*) FROM Dokter"
                    };

                    var labels = new[]
                    {
                        "Total Reservasi",
                        "Reservasi Terjadwal",
                        "Reservasi Selesai",
                        "Reservasi Batal",
                        "Total Pasien",
                        "Total Dokter"
                    };

                    string result = "=== STATISTIK RESERVASI ===\n\n";

                    for (int i = 0; i < queries.Length; i++)
                    {
                        using (var cmd = new SqlCommand(queries[i], connection))
                        {
                            int count = (int)cmd.ExecuteScalar();
                            result += $"{labels[i]}: {count}\n";
                        }
                    }

                    // Tambahkan reservasi hari ini
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Reservasi WHERE TanggalReservasi = CAST(GETDATE() AS DATE)", connection))
                    {
                        int todayCount = (int)cmd.ExecuteScalar();
                        result += $"Reservasi Hari Ini: {todayCount}\n";
                    }

                    // Tambahkan reservasi minggu ini
                    using (var cmd = new SqlCommand(@"
                        SELECT COUNT(*) FROM Reservasi 
                        WHERE TanggalReservasi >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) 
                        AND TanggalReservasi < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0)", connection))
                    {
                        int weekCount = (int)cmd.ExecuteScalar();
                        result += $"Reservasi Minggu Ini: {weekCount}\n";
                    }

                    MessageBox.Show(result, "Debug Info - Statistik Reservasi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debug error: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tombol untuk filter berdasarkan status
            FilterReservasiByStatus();
        }

        private void FilterReservasiByStatus()
        {
            try
            {
                // Tampilkan dialog untuk memilih status
                string[] statusOptions = { "Semua", "Terjadwal", "Selesai", "Batal" };

                using (var form = new Form())
                {
                    form.Text = "Filter Berdasarkan Status";
                    form.Size = new System.Drawing.Size(300, 150);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.FormBorderStyle = FormBorderStyle.FixedDialog;
                    form.MaximizeBox = false;
                    form.MinimizeBox = false;

                    var comboBox = new ComboBox()
                    {
                        Location = new System.Drawing.Point(20, 20),
                        Size = new System.Drawing.Size(200, 25),
                        DropDownStyle = ComboBoxStyle.DropDownList
                    };
                    comboBox.Items.AddRange(statusOptions);
                    comboBox.SelectedIndex = 0;

                    var btnOK = new Button()
                    {
                        Text = "OK",
                        Location = new System.Drawing.Point(145, 60),
                        Size = new System.Drawing.Size(75, 25),
                        DialogResult = DialogResult.OK
                    };

                    form.Controls.Add(comboBox);
                    form.Controls.Add(btnOK);
                    form.AcceptButton = btnOK;

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string selectedStatus = comboBox.SelectedItem.ToString();
                        LoadFilteredReservasi(selectedStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dalam filter: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilteredReservasi(string status)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT
                            r.ReservasiID,
                            p.Nama AS [Nama Pasien],
                            d.Nama AS [Nama Dokter],
                            d.Spesialisasi,
                            r.TanggalReservasi,
                            r.JamReservasi,
                            r.Status,
                            rs.Nama AS [Dibuat Oleh (Resepsionis)]
                        FROM Reservasi r
                        JOIN Pasien p ON r.PasienID = p.PasienID
                        JOIN Dokter d ON r.DokterID = d.DokterID
                        JOIN Resepsionis rs ON r.ResepsionisID = rs.ResepsionisID";

                    if (status != "Semua")
                    {
                        query += " WHERE r.Status = @Status";
                    }

                    query += " ORDER BY r.TanggalReservasi DESC, r.JamReservasi DESC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        if (status != "Semua")
                        {
                            command.Parameters.AddWithValue("@Status", status);
                        }

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        dgvReservasi.DataSource = dt;

                        string message = status == "Semua" ?
                            $"Menampilkan semua reservasi: {dt.Rows.Count} data" :
                            $"Menampilkan reservasi dengan status '{status}': {dt.Rows.Count} data";

                        MessageBox.Show(message, "Filter Berhasil",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dalam memuat data filter: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}