using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace YasminClinic
{
    public partial class DataDokter : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        public DataDokter()
        {
            InitializeComponent();
            // Membuat form terbuka dalam mode fullscreen
            this.WindowState = FormWindowState.Maximized;
        }

        private void DataDokter_Load(object sender, EventArgs e)
        {
            // Panggil method utama untuk membangun UI dan memuat data
            SetupAndLoadUI();
        }

        private void SetupAndLoadUI()
        {
            // Membersihkan form dari kontrol yang mungkin sudah ada
            this.Controls.Clear();
            this.Text = "Data Dokter Klinik Yasmin";
            this.BackColor = Color.FromArgb(245, 249, 255);

            // Membuat Panel utama yang bisa di-scroll
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };
            this.Controls.Add(mainPanel);

            // Daftar spesialisasi yang akan ditampilkan
            var specializations = new List<string> { "Umum", "Gigi", "Anak", "Kandungan", "Gizi" };
            int currentYPosition = 20; // Posisi vertikal awal

            foreach (var sp in specializations)
            {
                // Membuat Label Judul untuk setiap spesialisasi
                Label titleLabel = new Label
                {
                    Text = $"Dokter Spesialis {sp}",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    Location = new Point(20, currentYPosition),
                    AutoSize = true
                };
                mainPanel.Controls.Add(titleLabel);
                currentYPosition += titleLabel.Height + 10;

                // Membuat DataGridView untuk setiap spesialisasi
                DataGridView dgv = new DataGridView
                {
                    Name = $"dgvDokter{sp}",
                    Location = new Point(20, currentYPosition),
                    Width = mainPanel.Width - 60, // Lebar menyesuaikan panel
                    Height = 150, // Tinggi awal, bisa disesuaikan
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    AllowUserToAddRows = false,
                    ReadOnly = true,
                    BackgroundColor = Color.White,
                    ColumnHeadersDefaultCellStyle = { Font = new Font("Segoe UI", 10F, FontStyle.Bold) }
                };
                mainPanel.Controls.Add(dgv);
                currentYPosition += dgv.Height + 30; // Jarak antar tabel

                // Memuat data ke dalam DataGridView yang baru dibuat
                LoadDokterData(sp, dgv);
            }

            // Menambahkan Tombol Kembali dan Refresh
            Button btnBack = new Button { Text = "Kembali", Location = new Point(20, currentYPosition), Size = new Size(100, 30) };
            btnBack.Click += btnBack_Click; // Menghubungkan event handler
            mainPanel.Controls.Add(btnBack);

            Button btnRefresh = new Button { Text = "Refresh", Location = new Point(130, currentYPosition), Size = new Size(100, 30) };
            btnRefresh.Click += btnRefresh_Click; // Menghubungkan event handler
            mainPanel.Controls.Add(btnRefresh);
        }

        private void LoadDokterData(string spesialisasi, DataGridView dgv)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Gunakan query langsung sebagai alternatif
                    string query = @"SELECT 
                                DokterID, 
                                Nama,
                                NoHP,
                                Username,
                                Email
                            FROM Dokter 
                            WHERE Spesialisasi = @Spesialisasi
                            ORDER BY Nama";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Spesialisasi", spesialisasi);

                        var adapter = new SqlDataAdapter(command);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        dgv.DataSource = dt;
                        SetupDataGridViewColumns(dgv);

                        // Informasi jumlah data
                        Console.WriteLine($"Loaded {dt.Rows.Count} dokter untuk spesialisasi {spesialisasi}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data untuk Dokter {spesialisasi}: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Setup untuk kolom DokterID
            if (dgv.Columns["DokterID"] != null)
            {
                dgv.Columns["DokterID"].HeaderText = "ID";
                dgv.Columns["DokterID"].FillWeight = 15; // Lebih kecil karena ID biasanya pendek
                dgv.Columns["DokterID"].Width = 50;
            }

            // Setup untuk kolom Nama
            if (dgv.Columns["Nama"] != null)
            {
                dgv.Columns["Nama"].HeaderText = "Nama Dokter";
                dgv.Columns["Nama"].FillWeight = 30;
                dgv.Columns["Nama"].MinimumWidth = 150;
            }

            // Setup untuk kolom NoHP
            if (dgv.Columns["NoHP"] != null)
            {
                dgv.Columns["NoHP"].HeaderText = "No. Telepon";
                dgv.Columns["NoHP"].FillWeight = 20;
                dgv.Columns["NoHP"].MinimumWidth = 120;
            }

            // Setup untuk kolom Username
            if (dgv.Columns["Username"] != null)
            {
                dgv.Columns["Username"].HeaderText = "Username";
                dgv.Columns["Username"].FillWeight = 20;
                dgv.Columns["Username"].MinimumWidth = 100;
            }

            // Setup untuk kolom Email
            if (dgv.Columns["Email"] != null)
            {
                dgv.Columns["Email"].HeaderText = "Email";
                dgv.Columns["Email"].FillWeight = 35;
                dgv.Columns["Email"].MinimumWidth = 180;
            }

            // Atur tinggi baris agar lebih rapi
            dgv.RowTemplate.Height = 25;

            // Style untuk header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Style untuk sel biasa
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alternating row colors untuk kemudahan membaca
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Memuat ulang seluruh UI dan data
            SetupAndLoadUI();
            MessageBox.Show("Data dokter telah disegarkan.", "Refresh Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Kembali ke form KelolaDokter
            KelolaDokter kelolaDokterForm = new KelolaDokter();
            kelolaDokterForm.Show();
            this.Hide();
        }
    }
}
