using Laundry.Controller;
using Laundry.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Laundry.View
{
    public partial class FrmTransactions : Form
    {
        // deklarasi objek controller
        private TransactionController tc;

        private CustomerController cc;

        private ServiceController sc;

        private EmployeeController ec;


        // deklarasi field untuk meyimpan objek mahasiswa
        private List<Transactions> listOfTransaction = new List<Transactions>();
        public FrmTransactions()
        {
            InitializeComponent();
            tc = new TransactionController();
            cc = new CustomerController();
            sc = new ServiceController();
            ec = new EmployeeController();
            InisialisasiListView();
            LoadData();
            LoadDataByClick();
            FillComboBoxCustomer();
            FillComboBoxService();
        }


        private void InisialisasiListView()
        {
            lvwTransactions.View = System.Windows.Forms.View.Details;
            lvwTransactions.FullRowSelect = true;
            lvwTransactions.GridLines = true;
            lvwTransactions.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwTransactions.Columns.Add("ID Transaction", 200, HorizontalAlignment.Center);
            lvwTransactions.Columns.Add("Nama Pelanggan", 200, HorizontalAlignment.Center);
            lvwTransactions.Columns.Add("Layanan", 200, HorizontalAlignment.Left);
            lvwTransactions.Columns.Add("Berat", 200, HorizontalAlignment.Center);
            lvwTransactions.Columns.Add("Status", 200, HorizontalAlignment.Center);
            lvwTransactions.Columns.Add("Total", 200, HorizontalAlignment.Center);
        }

        private void FillComboBoxCustomer()
        {
            // Buat DataTable untuk menyimpan data
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));

            // Ambil data dari metode cc.ReadAll()
            var resultCustomer = cc.ReadAll();

            // Tambahkan data ke DataTable dari hasil pemanggilan metode cc.ReadAll()
            foreach (var customer in resultCustomer)
            {
                dt.Rows.Add(customer.Id, customer.Name);
            }

            // Set data source ComboBox dengan DataTable
            cbCustomer.DataSource = dt;
            cbCustomer.DisplayMember = "Name"; // Kolom yang akan ditampilkan di ComboBox
            cbCustomer.ValueMember = "ID";     // Nilai yang akan diambil saat item dipilih
        }

        private void FillComboBoxService()
        {
            // Buat DataTable untuk menyimpan data
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));

            // Ambil data dari metode cc.ReadAll()
            var resultService = sc.ReadAll();

            // Tambahkan data ke DataTable dari hasil pemanggilan metode cc.ReadAll()
            foreach (var s in resultService)
            {
                dt.Rows.Add(s.Id, s.Name);
            }

            // Set data source ComboBox dengan DataTable
            cbService.DataSource = dt;
            cbService.DisplayMember = "Name"; // Kolom yang akan ditampilkan di ComboBox
            cbService.ValueMember = "ID";     // Nilai yang akan diambil saat item dipilih
        }


        private void LoadData()
        {
            ClearTextBoxes();
            // kosongkan listview
            lvwTransactions.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfTransaction = tc.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var t in listOfTransaction)
            {
                var noUrut = lvwTransactions.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(t.Id);

                var customerName = cc.ReadById(t.CustomerId);
                item.SubItems.Add(customerName.Name);

                var serviceName = sc.ReadById(t.ServiceId);
                item.SubItems.Add(serviceName.Name);

                item.SubItems.Add(t.Weight.ToString());
                item.SubItems.Add(t.Status);
                Console.WriteLine($"Jumlah total di loaddata: {t.Total}");
                item.SubItems.Add(t.Total.ToString());

                // tampilkan data mhs ke listview
                lvwTransactions.Items.Add(item);
            }
        }

        private void LoadDataByClick()
        {
            lvwTransactions.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfTransaction = tc.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var t in listOfTransaction)
            {
                var noUrut = lvwTransactions.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(t.Id);

                var customerName = cc.ReadById(t.CustomerId);
                item.SubItems.Add(customerName.Name);

                var serviceName = sc.ReadById(t.ServiceId);
                item.SubItems.Add(serviceName.Name);

                item.SubItems.Add(t.Weight.ToString());
                item.SubItems.Add(t.Status);
                item.SubItems.Add(t.Total.ToString());

                // tampilkan data mhs ke listview
                lvwTransactions.Items.Add(item);
            }
                lvwTransactions.Click += new EventHandler(lvwTransaction_click);
        }

        private void lvwTransaction_click(object sender, EventArgs e)
        {

            if (lvwTransactions.SelectedItems.Count > 0)
            {
                // Mendapatkan item yang dipilih
                ListViewItem selectedItem = lvwTransactions.SelectedItems[0];
                Transactions t = new Transactions();

                // Mendapatkan data dari item yang dipilih

                t.Id = selectedItem.SubItems[1].Text;
                t.CustomerId = selectedItem.SubItems[2].Text;
                t.ServiceId = selectedItem.SubItems[3].Text;

                if (int.TryParse(selectedItem.SubItems[4].Text, out int weight))
                {
                    t.Weight = weight;
                }
                else
                {
                    MessageBox.Show("Invalid price value in the selected item.");
                    return;
                }

                t.Status = selectedItem.SubItems[5].Text;

                if (decimal.TryParse(selectedItem.SubItems[6].Text, out decimal total))
                {
                    t.Total = total;
                }
                else
                {
                    MessageBox.Show("Invalid price value in the selected item.");
                    return;
                }

                // Menampilkan data ke TextBox

                cbCustomer.Text = t.CustomerId;
                cbService.Text = t.ServiceId;
                txtWeight.Text = t.Weight.ToString(); 
                txtStatus.Text = t.Status;
                lblTotal.Text = t.Total.ToString();
            }
        }

        private void LoadDataByName(string name)
        {
            lvwTransactions.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfTransaction = tc.ReadByName(name);
            // ekstrak objek mhs dari collection
            foreach (var t in listOfTransaction)
            {
                var noUrut = lvwTransactions.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(t.Id);
                var r = cc.ReadById(t.CustomerId);
                item.SubItems.Add(r.Name);

                var layanan = sc.ReadById(t.ServiceId);
                item.SubItems.Add(layanan.Name);

                item.SubItems.Add(t.Weight.ToString());

                item.SubItems.Add(t.Status);

                item.SubItems.Add(t.Total.ToString());
                // tampilkan data mhs ke listview
                lvwTransactions.Items.Add(item);
            }
        }


        private void ClearTextBoxes()
        {
            // Bersihkan nilai teks di TextBox
            cbCustomer.Text = "";
            cbService.Text = "";
            txtPay.Text = "";
            txtWeight.Text = "";
            txtStatus.Text = "";
            lblTotal.Text = "";
        }

        private void FrmTransactions_Load(object sender, EventArgs e)
        {
            LoadData();
            FillComboBoxCustomer();
            FillComboBoxService();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Transactions t = new Transactions();
            var rCustomerId = cc.ReadDetailByName(cbCustomer.Text);
            if (rCustomerId != null)
            {
                t.CustomerId = rCustomerId.Id;
            }
            else
            {
                MessageBox.Show("Pelanggan tidak ditemukan.", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var rServiceId = sc.ReadByName(cbService.Text);
            if (rServiceId != null)
            {
                t.ServiceId = rServiceId.Id;
            }
            else
            {
                MessageBox.Show("Layanan tidak ditemukan.", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FrmLogin Login = new FrmLogin();
            string usernameFromLogin = Login.EnteredUsername;
            var r = ec.ReadByUsername(usernameFromLogin);
            t.EmployeeId = r.Id;

            if (int.TryParse(txtWeight.Text, out int weight))
            {
                t.Weight = weight;
            }
            else
            {
                MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            t.Status = txtStatus.Text;

            decimal dWeight = weight;

            decimal servicePrice = rServiceId.Price; 

            decimal total = dWeight * servicePrice;

            lblTotal.Text = total.ToString();

            t.Total = dWeight * servicePrice;
            

            tc.Create(t);

            LoadData();

            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwTransactions.SelectedItems[0];
                LoadDataByClick();
                Transactions t = new Transactions();
                var id = selectedItem.SubItems[1].Text;
                t.Id = id;
                var rCustomerId = cc.ReadDetailByName(cbCustomer.Text);
                if (rCustomerId != null)
                {
                    t.CustomerId = rCustomerId.Id;
                }
                else
                {
                    MessageBox.Show("Pelanggan tidak ditemukan.", "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var rServiceId = sc.ReadByName(cbService.Text);
                if (rServiceId != null)
                {
                    t.ServiceId = rServiceId.Id;
                }
                else
                {
                    MessageBox.Show("Layanan tidak ditemukan.", "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                FrmLogin Login = new FrmLogin();
                string usernameFromLogin = Login.EnteredUsername;
                var r = ec.ReadByUsername(usernameFromLogin);
                t.EmployeeId = r.Id;

                Console.WriteLine($"informasi customer dari txt: {t.Id}");

                Console.WriteLine($"informasi customer dari txt: {cbCustomer.Text}");

                Console.WriteLine($"informasi service dari txt: {cbService.Text}");

                Console.WriteLine($"informasi status dari txt: {txtStatus.Text}");

                Console.WriteLine($"informasi berat dari txt: {txtWeight.Text}");

                Console.WriteLine($"informasi berat dari txt: {lblTotal.Text}");



                if (int.TryParse(txtWeight.Text, out int weight))
                {
                    t.Weight = weight;
                }
                else
                {
                    MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                

                t.Status = txtStatus.Text;

                decimal dWeight = weight;

                decimal servicePrice = rServiceId.Price;

                decimal total = dWeight * servicePrice;

                lblTotal.Text = total.ToString();

                t.Total = dWeight * servicePrice;

                tc.Update(t);

                LoadData();
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
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwTransactions.SelectedItems[0];
                var konfirmasi = MessageBox.Show("Apakah data transaksi ingin dihapus ? ", "Konfirmasi",

                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    var id = selectedItem.SubItems[1].Text;
                    var result = tc.Delete(id);
                    if (result > 0) LoadData();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data layanan belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = cc.ReadDetailByName(txtSearch.Text);

            // Check if the name is null or empty
            if (!string.IsNullOrEmpty(name.Id))
            {
                try
                {
                    LoadDataByName(name.Id);
                }
                catch (Exception ex)
                {
                    // Handle exception (display a message, log, etc.)
                    MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Inform the user that the name is required
                MessageBox.Show("Please enter a name.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
