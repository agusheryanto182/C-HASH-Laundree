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
        public FrmHome()
        {
            InitializeComponent();
        }
      
        private void FrmHome_Load(object sender, EventArgs e)
        {
            FrmLogin Login = new FrmLogin();   
            Login.ShowDialog();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmEmployee frmEmployee = new FrmEmployee();
            frmEmployee.ShowDialog();   
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmService frmService = new FrmService();
            frmService.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmReport frmReport = new FrmReport();
            frmReport.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCustomer frmCustomer = new FrmCustomer();
            frmCustomer.ShowDialog();
        }
    }
}
