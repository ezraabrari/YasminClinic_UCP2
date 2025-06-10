using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class Login : Form
    {
        private static readonly string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
                                                          "Initial Catalog=YasminClinic2; Integrated Security=True";

        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string userRole = AuthenticateUser(username, password);

                if (userRole != null)
                {
                    this.Hide();
                    switch (userRole)
                    {
                        case "Admin":
                            MessageBox.Show("Login sebagai Admin berhasil!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DashboardAdmin adminForm = new DashboardAdmin();
                            adminForm.Show();
                            break;
                        case "Dokter":
                            MessageBox.Show("Login sebagai Dokter berhasil!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DashboardDokter dokterForm = new DashboardDokter();
                            dokterForm.Show();
                            break;
                        case "Resepsionis":
                            MessageBox.Show("Login sebagai Resepsionis berhasil!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DashboardResepsionis resepsionisForm = new DashboardResepsionis();
                            resepsionisForm.Show();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Username atau Password salah.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menghubungkan ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private string AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_UnifiedLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    object result = command.ExecuteScalar();

                    return result?.ToString();
                }
            }
        }
    }
}
