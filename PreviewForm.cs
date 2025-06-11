using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class PreviewForm : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        public PreviewForm(DataTable data)
        {
            InitializeComponent();
            dgvPreview.DataSource = data;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            dgvPreview.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnImportToDb_Click(object sender, EventArgs e) // Ganti nama tombol di designer menjadi btnImportToDb
        {
            DataTable dt = (DataTable)dgvPreview.DataSource;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diimpor.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Anda akan mengimpor {dt.Rows.Count} baris data. Lanjutkan?", "Konfirmasi Impor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ImportDataToDatabase(dt);
            }
        }

        private void ImportDataToDatabase(DataTable dt)
        {
            int successCount = 0;
            int failedCount = 0;

            // PERBAIKAN TOTAL: Menggunakan transaksi agar proses impor bersifat "All or Nothing"
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Mulai transaksi

                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Validasi baris data
                        if (!int.TryParse(row["ReservasiID"]?.ToString(), out int reservasiID))
                        {
                            // Lewati baris jika ReservasiID tidak valid
                            failedCount++;
                            continue;
                        }

                        // Panggil Stored Procedure yang sudah kita buat
                        using (SqlCommand cmd = new SqlCommand("sp_AddRekamMedis", conn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ReservasiID", reservasiID);
                            cmd.Parameters.AddWithValue("@Keluhan", row["Keluhan"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Diagnosa", row["Diagnosa"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Tindakan", row["Tindakan"]?.ToString() ?? "");
                            cmd.Parameters.AddWithValue("@Resep", row["Resep"]?.ToString() ?? "");

                            cmd.ExecuteNonQuery();
                            successCount++;
                        }
                    }

                    transaction.Commit(); // Jika semua berhasil, simpan perubahan
                    MessageBox.Show($"Impor Selesai.\n\nBerhasil: {successCount} baris\nGagal/Dilewati: {failedCount} baris", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Jika ada satu saja error, batalkan semua
                    MessageBox.Show("Gagal mengimpor data karena error database. Semua perubahan telah dibatalkan.\n\nError: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Batalkan juga untuk error lainnya
                    MessageBox.Show("Gagal mengimpor data. Semua perubahan telah dibatalkan.\n\nError: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}