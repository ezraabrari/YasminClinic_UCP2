using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class PreviewForm : Form
    {
        private string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;Initial Catalog=YasminClinic2;Integrated Security=True";
        private DataTable dataTable;

        public PreviewForm(DataTable data)
        {
            InitializeComponent();
            dgvPreview.DataSource = data;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoResizeColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda ingin mengimpor data ini ke database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ImportDataToDatabase();
            }
        }

        private bool ValidateRow(DataRow row)
        {
            // Validasi dasar: pastikan kolom tidak kosong
            if (string.IsNullOrWhiteSpace(row["PasienID"].ToString()) ||
                string.IsNullOrWhiteSpace(row["DokterID"].ToString()) ||
                string.IsNullOrWhiteSpace(row["Tanggal"].ToString()))
            {
                MessageBox.Show("PasienID, DokterID, dan Tanggal wajib diisi.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    string namaPasien = row["NamaPasien"].ToString();
                    int dokterID;

                    if (!int.TryParse(row["DokterID"].ToString(), out dokterID))
                    {
                        MessageBox.Show($"DokterID tidak valid untuk pasien {namaPasien}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    int pasienID = GetPasienIDByName(namaPasien);
                    if (pasienID == -1)
                    {
                        MessageBox.Show($"Pasien '{namaPasien}' tidak ditemukan di database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    string query = @"INSERT INTO RekamMedis 
                             (PasienID, DokterID, Tanggal, Keluhan, Diagnosa, Tindakan, Resep)
                             VALUES (@PasienID, @DokterID, @Tanggal, @Keluhan, @Diagnosa, @Tindakan, @Resep)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@PasienID", pasienID);
                            cmd.Parameters.AddWithValue("@DokterID", dokterID);
                            cmd.Parameters.AddWithValue("@Tanggal", Convert.ToDateTime(row["Tanggal"]));
                            cmd.Parameters.AddWithValue("@Keluhan", row["Keluhan"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Diagnosa", row["Diagnosa"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Tindakan", row["Tindakan"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Resep", row["Resep"]?.ToString() ?? "");
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data berhasil diimpor ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetPasienIDByName(string nama)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT PasienID FROM Pasien WHERE Nama = @Nama";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nama", nama);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }


        private void dgvPreview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tidak digunakan, bisa dihapus jika tidak diperlukan
        }
    }
}
