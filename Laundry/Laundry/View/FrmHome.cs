using Laundry.Controller;
using Laundry.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laundry.View
{

    public partial class FrmHome : Form
    {
        public string EnteredName { get; private set; }
        private EmployeeController employeeController;
        public FrmHome()
        {
            InitializeComponent();
            employeeController = new EmployeeController();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            FrmLogin Login = new FrmLogin();   
            Login.ShowDialog();

            string usernameFromLogin = Login.EnteredUsername;
            var r = employeeController.ReadByUsername(usernameFromLogin);
            lblAdmin.Text = r.Name;
            EnteredName = lblAdmin.Text;

        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            if (loginName == "admin")
            {
                MessageBox.Show("Silahkan masuk dengan akun karyawan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            if (loginName != "admin")
            {
                MessageBox.Show("Silahkan masuk dengan akun admin !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmEmployee frmEmployee = new FrmEmployee();
            frmEmployee.ShowDialog();   
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            if (loginName == "admin")
            {
                MessageBox.Show("Silahkan masuk dengan akun karyawan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmService frmService = new FrmService();
            frmService.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            if (loginName == "admin")
            {
                MessageBox.Show("Silahkan masuk dengan akun karyawan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmReport frmReport = new FrmReport();
            frmReport.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            if (loginName == "admin")
            {
                MessageBox.Show("Silahkan masuk dengan akun karyawan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmCustomer frmCustomer = new FrmCustomer();
            frmCustomer.ShowDialog();
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            FrmLogin Login = new FrmLogin();
            Login.ShowDialog();

            string usernameFromLogin = Login.EnteredUsername;
            var r = employeeController.ReadByUsername(usernameFromLogin);
            lblAdmin.Text = r.Name;
            EnteredName = lblAdmin.Text;

        }

    }
}
