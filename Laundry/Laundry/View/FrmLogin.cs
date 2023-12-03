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
                FrmEmployee frmEmployee = new FrmEmployee();
                frmEmployee.ShowDialog();
            } else
            {
                //User is not valid
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            
        }
    }
}
