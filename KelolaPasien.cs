using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class KelolaPasien : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        private int selectedPasienID = 0;

        public KelolaPasien()
        {
            InitializeComponent();

            // Isi ComboBox Jenis Kelamin
            cmbJenisKelamin.Items.Clear();
            cmbJenisKelamin.Items.Add("Laki-laki");
            cmbJenisKelamin.Items.Add("Perempuan");
            cmbJenisKelamin.DropDownStyle = ComboBoxStyle.DropDownList;

            // Hubungkan event klik pada tabel
            dgvPasien.CellClick += dgvPasien_CellClick;
        }

        private void Pasien_Load(object sender, EventArgs e)
        {
            LoadDataPasien();
            dgvPasien.ClearSelection();
        }

        private void LoadDataPasien()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetAllPasien", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvPasien.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data pasien: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtNama.Clear();
            txtNoTelepon.Clear();
            txtAlamat.Clear();
            cmbJenisKelamin.SelectedIndex = -1;
            dtpTanggalLahir.Value = DateTime.Now;
            selectedPasienID = 0;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || cmbJenisKelamin.SelectedItem == null || string.IsNullOrWhiteSpace(txtNoTelepon.Text))
            {
                MessageBox.Show("Nama, Jenis Kelamin, dan No Telepon wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_AddPasien", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nama", txtNama.Text);
                        command.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                        command.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                        command.Parameters.AddWithValue("@NoHP", txtNoTelepon.Text);

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data pasien berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataPasien();
                        ClearInputFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedPasienID == 0)
            {
                MessageBox.Show("Silakan pilih pasien dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdatePasien", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PasienID", selectedPasienID);
                        command.Parameters.AddWithValue("@Nama", txtNama.Text);
                        command.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                        command.Parameters.AddWithValue("@JenisKelamin", cmbJenisKelamin.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                        command.Parameters.AddWithValue("@NoHP", txtNoTelepon.Text);

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Data pasien berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataPasien();
                        ClearInputFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedPasienID == 0)
            {
                MessageBox.Show("Silakan pilih pasien dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data pasien ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("sp_DeletePasien", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PasienID", selectedPasienID);

                            connection.Open();
                            command.ExecuteNonQuery();

                            MessageBox.Show("Data pasien berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataPasien();
                            ClearInputFields();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPasien();
            ClearInputFields();
            MessageBox.Show("Data telah disegarkan.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ✅ Fungsi klik pada baris DataGridView
        private void dgvPasien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPasien.Rows[e.RowIndex];

                selectedPasienID = row.Cells["PasienID"].Value != DBNull.Value
                    ? Convert.ToInt32(row.Cells["PasienID"].Value)
                    : 0;

                txtNama.Text = row.Cells["Nama"].Value?.ToString() ?? "";
                txtNoTelepon.Text = row.Cells["NoHP"].Value?.ToString() ?? "";
                txtAlamat.Text = row.Cells["Alamat"].Value?.ToString() ?? "";

                if (DateTime.TryParse(row.Cells["TanggalLahir"].Value?.ToString(), out DateTime tanggalLahir))
                {
                    dtpTanggalLahir.Value = tanggalLahir;
                }

                string jenisKelamin = row.Cells["JenisKelamin"].Value?.ToString();
                if (!string.IsNullOrEmpty(jenisKelamin) && cmbJenisKelamin.Items.Contains(jenisKelamin))
                {
                    cmbJenisKelamin.SelectedItem = jenisKelamin;
                }
                else
                {
                    cmbJenisKelamin.SelectedIndex = -1;
                }
            }
        }

        // Kosongkan jika tidak digunakan
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void dtpTanggalLahir_ValueChanged(object sender, EventArgs e) { }
        private void cmbJenisKelamin_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtNoTelepon_TextChanged(object sender, EventArgs e) { }
        private void txtAlamat_TextChanged(object sender, EventArgs e) { }
        private void dgvPasien_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void Pasien_Load_1(object sender, EventArgs e)
        {

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide(); // Sembunyikan form saat ini
        }
    }
}
