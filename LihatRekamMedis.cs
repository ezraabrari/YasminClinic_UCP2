using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
// PERBAIKAN: Menambahkan using directive untuk NPOI
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
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

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

                        dgvRekamMedis.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                dgvRekamMedis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRekamMedis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRekamMedis.MultiSelect = false;
                dgvRekamMedis.ReadOnly = true;
                dgvRekamMedis.AllowUserToAddRows = false;

                if (dgvRekamMedis.Columns["RekamMedisID"] != null)
                {
                    dgvRekamMedis.Columns["RekamMedisID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error dalam setup DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];
                string namaPasien = row.Cells["Nama Pasien"].Value?.ToString() ?? "";
                string tanggal = Convert.ToDateTime(row.Cells["Tanggal"].Value).ToString("dd MMMM yyyy");

                MessageBox.Show($"Detail Rekam Medis:\nPasien: {namaPasien}\nTanggal: {tanggal}",
                                 "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Excel Files|*.xlsx;*.xls";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    PreviewData(openFile.FileName);
                }
            }
        }

        private void PreviewData(string filePath)
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
                        if (row == null) continue;

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count && j < row.Cells.Count; j++)
                        {
                            dr[j] = row.Cells[j]?.ToString() ?? "";
                        }
                        dt.Rows.Add(dr);
                    }

        
                     PreviewForm previewForm = new PreviewForm(dt);
                     previewForm.ShowDialog();
                    MessageBox.Show("Data dari Excel berhasil dibaca dan siap diproses.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error membaca file Excel: " + ex.Message);
            }
        }
    }
}
