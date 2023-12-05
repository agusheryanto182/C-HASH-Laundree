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
    public partial class FrmCustomer : Form
    {
        // deklarasi objek controller
        private CustomerController controller;
        // deklarasi field untuk meyimpan objek mahasiswa
        private List<Customer> LC = new List<Customer>();

        public FrmCustomer()
        {
            InitializeComponent();
            controller = new CustomerController();
            LoadDataCustomer();
            LoadDataCustomerByClick();
            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwCustomer.View = System.Windows.Forms.View.Details;
            lvwCustomer.FullRowSelect = true;
            lvwCustomer.GridLines = true;
            lvwCustomer.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwCustomer.Columns.Add("ID Pelanggan", 200, HorizontalAlignment.Center);
            lvwCustomer.Columns.Add("Nama", 200, HorizontalAlignment.Center);
            lvwCustomer.Columns.Add("Alamat", 200, HorizontalAlignment.Left);
            lvwCustomer.Columns.Add("Nomer Telepon", 200, HorizontalAlignment.Center);
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {

        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmTransactions frmTransactions = new FrmTransactions();
            frmTransactions.ShowDialog();
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

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmEmployee frmEmployee = new FrmEmployee();
            frmEmployee.ShowDialog();

        }

        private void LoadDataCustomer()
        {
            ClearTextBoxes();
            // kosongkan listview
            lvwCustomer.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            LC = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var cs in LC)
            {
                var noUrut = lvwCustomer.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(cs.IdPelanggan);
                item.SubItems.Add(cs.Name);
                item.SubItems.Add(cs.Address);
                item.SubItems.Add(cs.PhoneNumber);
                // tampilkan data mhs ke listview
                lvwCustomer.Items.Add(item);
            }
        }

        private void LoadDataCustomerByClick()
        {
            // kosongkan listview
            lvwCustomer.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            LC = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var cs in LC)
            {
                var noUrut = lvwCustomer.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(cs.IdPelanggan);
                item.SubItems.Add(cs.Name);
                item.SubItems.Add(cs.Address);
                item.SubItems.Add(cs.PhoneNumber);
                // tampilkan data mhs ke listview
                lvwCustomer.Items.Add(item);
            }
            lvwCustomer.Click += new EventHandler(lvwCustomer_Click);
        }


        private void lvwCustomer_Click(object sender, EventArgs e)
        {
            if (lvwCustomer.SelectedItems.Count > 0)
            {
                // Mendapatkan item yang dipilih
                ListViewItem selectedItem = lvwCustomer.SelectedItems[0];
                Customer cs = new Customer();
                // Mendapatkan data dari item yang dipilih
                cs.IdPelanggan = selectedItem.SubItems[1].Text;
                cs.Name = selectedItem.SubItems[2].Text;
                cs.Address = selectedItem.SubItems[3].Text;
                cs.PhoneNumber = selectedItem.SubItems[4].Text;

                // Menampilkan data ke TextBox
                lblNoPelanggan.Text = cs.IdPelanggan;
                txtName.Text = cs.Name;
                txtAddress.Text = cs.Address;
                txtHP.Text = cs.PhoneNumber;
            }
        }

        private void ClearTextBoxes()
        {
            // Bersihkan nilai teks di TextBox
            lblNoPelanggan.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtHP.Text = "";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            Customer cs = new Customer();
            // set nilai property objek mahasiswa yg diambil dari TextBox
            cs.Name = txtName.Text;
            cs.Address = txtAddress.Text;
            cs.PhoneNumber = txtHP.Text;

            controller.Create(cs);
            LoadDataCustomer();

            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (lvwCustomer.SelectedItems.Count > 0)
            {
                LoadDataCustomerByClick();
                // set nilai property objek mahasiswa yg diambil dari TextBox
                Customer cs = new Customer();
                cs.IdPelanggan = lblNoPelanggan.Text;
                cs.Name = txtName.Text;
                cs.Address = txtAddress.Text;
                cs.PhoneNumber = txtHP.Text;

                controller.Update(cs);

                LoadDataCustomer();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvwCustomer.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data pelanggan ingin dihapus ? ", "Konfirmasi",

                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil objek mhs yang mau dihapus dari collection
                    Customer emp =
                   LC[lvwCustomer.SelectedIndices[0]];
                    // panggil operasi CRUD
                    var result = controller.Delete(emp);
                    if (result > 0) LoadDataCustomer();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data customer belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lvwEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
