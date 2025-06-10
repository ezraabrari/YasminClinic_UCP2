using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace YasminClinic
{
    public partial class Report : Form
    {
        // Pastikan connection string ini benar
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        // PERBAIKAN: Deklarasi manual 'reportViewer1' dihapus untuk menghilangkan ambiguitas
        // private ReportViewer reportViewer1; 

        public Report()
        {
            InitializeComponent();
            // Panggilan SetupUI() dihapus karena UI sudah dibuat oleh desainer
        }

        private void Report_Load(object sender, EventArgs e)
        {
            // Memanggil method untuk setup dan memuat data laporan
            SetupReportViewer();

            // Me-refresh report viewer untuk menampilkan data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                // 1. Query SQL untuk mengambil data yang dibutuhkan
                string query = @"
                    SELECT
                        r.ReservasiID,
                        p.Nama AS [Nama_Pasien],
                        d.Nama AS [Nama_Dokter],
                        d.Spesialisasi,
                        r.TanggalReservasi,
                        r.JamReservasi,
                        r.Status,
                        rs.Nama AS [Dibuat_Oleh_Resepsionis]
                    FROM
                        Reservasi AS r
                    INNER JOIN
                        Pasien AS p ON r.PasienID = p.PasienID
                    INNER JOIN
                        Dokter AS d ON r.DokterID = d.DokterID
                    INNER JOIN
                        Resepsionis AS rs ON r.ResepsionisID = rs.ResepsionisID
                    ORDER BY
                        r.TanggalReservasi DESC, r.JamReservasi DESC;
                ";

                // 2. Buat DataTable untuk menampung data dari query
                DataTable dt = new DataTable();

                // 3. Gunakan SqlDataAdapter untuk mengisi DataTable
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // 4. Buat ReportDataSource
                //    PENTING: Nama "DataSetReservasi" harus sama persis dengan nama DataSet di dalam file .rdlc Anda.
                ReportDataSource rds = new ReportDataSource("DataSetKlinik", dt);

                // 5. Konfigurasi ReportViewer
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Menggunakan EmbeddedResource. Pastikan file .rdlc Anda propertinya
                // 'Build Action' diatur ke 'Embedded Resource'.
                reportViewer1.LocalReport.ReportEmbeddedResource = "YasminClinic.ReservasiReport.rdlc";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide();
        }
    }
}
