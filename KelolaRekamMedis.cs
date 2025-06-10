using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class KelolaRekamMedis : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        private int selectedRekamMedisID = 0;

        public KelolaRekamMedis()
        {
            InitializeComponent();

            LoadDataToComboBox("sp_GetAllPasien", cmbNamaPasien, "Nama", "PasienID");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllRekamMedisDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRekamMedis.DataSource = dt;

                    if (dgvRekamMedis.Columns["PasienID"] != null) dgvRekamMedis.Columns["PasienID"].Visible = false;
                    if (dgvRekamMedis.Columns["DokterID"] != null) dgvRekamMedis.Columns["DokterID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KelolaRekamMedis_Load(object sender, EventArgs e)
        {
            LoadPasienComboBox();
            LoadDokterComboBox();
            LoadRekamMedisGrid();
            ClearInputFields();
        }

        private void LoadRekamMedisGrid()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetAllRekamMedisDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRekamMedis.DataSource = dt;

                    if (dgvRekamMedis.Columns["PasienID"] != null) dgvRekamMedis.Columns["PasienID"].Visible = false;
                    if (dgvRekamMedis.Columns["DokterID"] != null) dgvRekamMedis.Columns["DokterID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPasienComboBox()
        {
            LoadDataToComboBox("sp_GetAllPasien", cmbNamaPasien, "Nama", "PasienID");
        }

        private void LoadDokterComboBox()
        {
        }

        private void LoadDataToComboBox(string queryOrSp, ComboBox comboBox, string displayMember, string valueMember)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(queryOrSp, connection))
                {
                    if (!queryOrSp.Trim().ToUpper().StartsWith("SELECT"))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    }
                    var adapter = new SqlDataAdapter(command);
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                    comboBox.Text = "Pilih...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data untuk {comboBox.Name}: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            selectedRekamMedisID = 0;
            cmbNamaPasien.SelectedIndex = -1;
            txtKeluhan.Clear();
            txtDiagnosa.Clear();
            txtTindakan.Clear();
            txtResep.Clear();

            btnSimpan.Enabled = true;
            btnUpdate.Enabled = false;
            btnHapus.Enabled = false;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cmbNamaPasien.SelectedValue == null || string.IsNullOrWhiteSpace(txtDiagnosa.Text))
            {
                MessageBox.Show("Pasien, Dokter, dan Diagnosa wajib diisi.", "Input Tidak Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_AddRekamMedis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PasienID", (int)cmbNamaPasien.SelectedValue);
                    command.Parameters.AddWithValue("@Keluhan", txtKeluhan.Text);
                    command.Parameters.AddWithValue("@Diagnosa", txtDiagnosa.Text);
                    command.Parameters.AddWithValue("@Tindakan", txtTindakan.Text);
                    command.Parameters.AddWithValue("@Resep", txtResep.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rekam medis berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRekamMedisGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRekamMedisID == 0)
            {
                MessageBox.Show("Pilih data dari tabel untuk diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_UpdateRekamMedis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RekamMedisID", selectedRekamMedisID);
                    command.Parameters.AddWithValue("@Keluhan", txtKeluhan.Text);
                    command.Parameters.AddWithValue("@Diagnosa", txtDiagnosa.Text);
                    command.Parameters.AddWithValue("@Tindakan", txtTindakan.Text);
                    command.Parameters.AddWithValue("@Resep", txtResep.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rekam medis berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRekamMedisGrid();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedRekamMedisID == 0)
            {
                MessageBox.Show("Pilih data dari tabel untuk dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand("sp_DeleteRekamMedis", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RekamMedisID", selectedRekamMedisID);
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Rekam medis berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRekamMedisGrid();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus rekam medis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadRekamMedisGrid();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashboardAdmin DashboardAdmin = new DashboardAdmin();
            DashboardAdmin.Show();
            this.Hide();
        }

        private void dgvRekamMedis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRekamMedis.Rows[e.RowIndex];

                selectedRekamMedisID = Convert.ToInt32(row.Cells["RekamMedisID"].Value);

                cmbNamaPasien.SelectedValue = row.Cells["PasienID"].Value;
                txtKeluhan.Text = row.Cells["Keluhan"].Value.ToString();
                txtDiagnosa.Text = row.Cells["Diagnosa"].Value.ToString();
                txtTindakan.Text = row.Cells["Tindakan"].Value.ToString();
                txtResep.Text = row.Cells["Resep"].Value.ToString();

                btnSimpan.Enabled = false;
                btnUpdate.Enabled = true;
                btnHapus.Enabled = true;
            }
        }

        // --- Event handler kosong untuk desainer ---
        private void cmbNamaPasien_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbDokter_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpTanggal_ValueChanged(object sender, EventArgs e) { }
        private void txtKeluhan_TextChanged(object sender, EventArgs e) { }
        private void txtDiagnosa_TextChanged(object sender, EventArgs e) { }
        private void txtTindakan_TextChanged(object sender, EventArgs e) { }
        private void txtResep_TextChanged(object sender, EventArgs e) { }
        private void dgvRekamMedis_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
