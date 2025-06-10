using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YasminClinic
{
    public partial class FormLogin: Form
    {

        static string connectionString = "Data Source=LAPTOP-D0UNNI5Q\\ZYAA;" +
        "Initial Catalog=YasminClinic2; Integrated Security=True";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnlogindokter_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void btnloginpasien_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
