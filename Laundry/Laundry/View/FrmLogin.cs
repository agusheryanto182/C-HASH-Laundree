using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Laundry.Controller;
using Laundry.Model.Entity;

namespace Laundry.View
{
    public partial class FrmLogin : Form

    {
        public string EnteredUsername { get; private set; }
        private LoginController controller;
        private EmployeeController ec;
        public FrmLogin()
        {
            InitializeComponent();
            controller = new LoginController(); 
            ec = new EmployeeController();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Mengubah byte array menjadi string hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void btnMasuk_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Username = txtUsername.Text;

            if (emp.Username == "admin")
            {
                emp.Password = txtPassword.Text;


                bool result = controller.PerformLogin(emp);
                if (result)
                {
                    EnteredUsername = txtUsername.Text;
                    this.Close();
                }
                else
                {
                    //User is not valid
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }

            } else
            {
                string password = txtPassword.Text;

                var salt = ec.ReadByUsername(emp.Username);

                string combinedString = password + salt.AuthPassword;

                string hashedPassword = ComputeSHA256Hash(combinedString);

                emp.Password = hashedPassword;

                bool result = controller.PerformLogin(emp);
                if (result)
                {
                    EnteredUsername = txtUsername.Text;
                    this.Close();
                }
                else
                {
                    //User is not valid
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
