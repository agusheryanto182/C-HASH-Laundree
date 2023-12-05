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
    public partial class FrmService : Form
    {

        // deklarasi objek controller
        private ServiceController controller;
        // deklarasi field untuk meyimpan objek mahasiswa
        private List<Service> listOfService = new List<Service>();
        public FrmService()
        {
            InitializeComponent();
            InisialisasiListView();
            LoadDataService();
            LoadDataServiceByClick();
        }

        private void FrmService_Load(object sender, EventArgs e)
        {

        }

        private void btnTransaction_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
        }

        private void btnService_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FrmService s = new FrmService();
            s.ShowDialog();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
       
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void InisialisasiListView()
        {
            lvwService.View = System.Windows.Forms.View.Details;
            lvwService.FullRowSelect = true;
            lvwService.GridLines = true;
            lvwService.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwService.Columns.Add("Nama", 200, HorizontalAlignment.Center);
            lvwService.Columns.Add("Harga", 200, HorizontalAlignment.Left);
            lvwService.Columns.Add("Durasi", 200, HorizontalAlignment.Center);
        }

        // method untuk menampilkan semua data mahasiswa
        private void LoadDataService()
        {
           
        }

        private void ClearTextBoxes()
        {
            // Bersihkan nilai teks di TextBox
            txtName.Text = "";
            txtDuration.Text = "";
            txtPrice.Text = "";
        }

        private void LoadDataServiceByClick()
        {
           
        }

        private void lvwService_Click(object sender, EventArgs e)
        {
    
        }

        private void lvwService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
