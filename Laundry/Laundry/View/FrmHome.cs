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
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            frmEmployee.ShowDialog();   
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            FrmService frmService = new FrmService();
            frmService.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FrmReport frmReport = new FrmReport();
            frmReport.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            frmCustomer.ShowDialog();
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = "";
            FrmLogin Login = new FrmLogin();
            Login.ShowDialog();

            string usernameFromLogin = Login.EnteredUsername;
            var r = employeeController.ReadByUsername(usernameFromLogin);
            lblAdmin.Text = r.Name;

        }

    }
}
