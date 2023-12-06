using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public FrmLogin()
        {
            InitializeComponent();
            controller = new LoginController(); 
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnMasuk_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Username = txtUsername.Text;
            emp.Password = txtPassword.Text;
            bool result = controller.PerformLogin(emp);
            if (result) 
            {
                EnteredUsername = txtUsername.Text;
                this.Close();
            } else
            {
                //User is not valid
                txtUsername.Text = "";
                txtPassword.Text = "";
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
