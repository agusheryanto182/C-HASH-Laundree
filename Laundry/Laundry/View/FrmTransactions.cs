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
    public partial class FrmTransactions : Form
    {
        public FrmTransactions()
        {
            InitializeComponent();
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
        }


        private void btnService_Click_1(object sender, EventArgs e)
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

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmEmployee frmEmployee = new FrmEmployee();
            frmEmployee.ShowDialog();
        }
    }
}
