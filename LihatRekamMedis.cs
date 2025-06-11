using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
                // PERBAIKAN: Menggunakan Stored Procedure yang sudah dioptimalkan
                // dan menambahkan ReservasiID untuk keperluan ekspor.
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllRekamMedisDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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

        private void SetupDataGridView()
        {
            dgvRekamMedis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRekamMedis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRekamMedis.MultiSelect = false;
            dgvRekamMedis.ReadOnly = true;
            dgvRekamMedis.AllowUserToAddRows = false;

            // Sembunyikan kolom ID yang tidak perlu dilihat pengguna
            if (dgvRekamMedis.Columns["RekamMedisID"] != null)
                dgvRekamMedis.Columns["RekamMedisID"].Visible = false;

            if (dgvRekamMedis.Columns["ReservasiID"] != null)
                dgvRekamMedis.Columns["ReservasiID"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataRekamMedis();
        }

        // --- PENAMBAHAN FUNGSI EKSPOR ---
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvRekamMedis.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diekspor.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Excel Files|*.xlsx";
                saveFile.Title = "Simpan File Rekam Medis";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IWorkbook workbook = new XSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("RekamMedis");

                        // Buat Header
                        IRow headerRow = sheet.CreateRow(0);
                        DataTable dt = (DataTable)dgvRekamMedis.DataSource;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                        }

                        // Isi Data
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            IRow dataRow = sheet.CreateRow(i + 1);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j]?.ToString() ?? "");
                            }
                        }

                        using (FileStream fs = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.Write))
                        {
                            workbook.Write(fs);
                        }
                        MessageBox.Show("Data berhasil diekspor!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal mengekspor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Excel Files|*.xlsx;*.xls";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    ReadAndPreviewExcel(openFile.FileName);
                }
            }
        }

        private void ReadAndPreviewExcel(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                    {
                        dt.Columns.Add(cell.ToString());
                    }

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.Cells.Count == 0 || string.IsNullOrWhiteSpace(row.Cells[0].ToString())) continue;

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Pastikan tidak error jika sel kosong
                            dr[j] = row.GetCell(j)?.ToString() ?? "";
                        }
                        dt.Rows.Add(dr);
                    }

                    // Kirim data ke form preview
                    PreviewForm previewForm = new PreviewForm(dt);
                    previewForm.ShowDialog();

                    // Muat ulang grid setelah form preview ditutup (jika ada data yang diimpor)
                    LoadDataRekamMedis();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error membaca file Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardResepsionis dashboard = new DashboardResepsionis();
            dashboard.Show();
            this.Hide();
        }
    }
}