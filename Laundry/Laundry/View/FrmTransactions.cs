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
            lvwTransactions.Columns.Add("Kasir", 200, HorizontalAlignment.Center);
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

                var cashierName = ec.ReadById(t.EmployeeId);
                item.SubItems.Add(cashierName.Name);

                var serviceName = sc.ReadById(t.ServiceId);
                item.SubItems.Add(serviceName.Name);

                item.SubItems.Add(t.Weight.ToString());
                item.SubItems.Add(t.Status);
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

                var cashierName = ec.ReadById(t.EmployeeId);
                item.SubItems.Add(cashierName.Name);

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
                t.ServiceId = selectedItem.SubItems[4].Text;

                if (int.TryParse(selectedItem.SubItems[5].Text, out int weight))
                {
                    t.Weight = weight;
                }
                else
                {
                    MessageBox.Show("Invalid price value in the selected item.");
                    return;
                }

                t.Status = selectedItem.SubItems[6].Text;

                if (decimal.TryParse(selectedItem.SubItems[7].Text, out decimal total))
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

                var customerName = cc.ReadById(t.CustomerId);
                item.SubItems.Add(customerName.Name);

                var cashierName = ec.ReadById(t.EmployeeId);
                item.SubItems.Add(cashierName.Name);

                var serviceName = sc.ReadById(t.ServiceId);
                item.SubItems.Add(serviceName.Name);

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
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            var r = ec.ReadDetailByName(loginName);
            t.EmployeeId = r.Id;

            if (int.TryParse(txtWeight.Text, out int weight))
            {
                t.Weight = weight;
            }
            else
            {
                MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            

            decimal dWeight = weight;

            decimal servicePrice = rServiceId.Price; 

            decimal total = dWeight * servicePrice;

            lblTotal.Text = total.ToString();

            t.Total = dWeight * servicePrice;

            t.Order = DateTime.Now;

            tc.Create(t);

            LoadData();

            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                if (txtStatus.Text == "LUNAS")
                {
                    MessageBox.Show("Data tidak bisa diubah!!!", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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

                FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
                string loginName = frmHome.EnteredName;
                var r = ec.ReadDetailByName(loginName);
                t.EmployeeId = r.Id;

                if (int.TryParse(txtWeight.Text, out int weight))
                {
                    t.Weight = weight;
                }
                else
                {
                    MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
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
            else 
            {
                MessageBox.Show("Data belum dipilih", "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                return;
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
            else 
            {
                MessageBox.Show("Data layanan belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = cc.ReadDetailByName(txtSearch.Text);
            if (name == null)
            {
                MessageBox.Show("Pencarian tidak membuahkan hasil.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!string.IsNullOrEmpty(name.Id))
            {
                LoadDataByName(name.Id);
                return;
            }
            else
            {
                // Inform the user that the name is required
                MessageBox.Show("Silahkan masukkan nama.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

       

        private void btnPay_Click_1(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwTransactions.SelectedItems[0];
                LoadDataByClick();
                Transactions t = new Transactions();
                var id = selectedItem.SubItems[1].Text;
                t.Id = id;

                FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
                string loginName = frmHome.EnteredName;
                var r = ec.ReadDetailByName(loginName);
                t.EmployeeId = r.Id;

                decimal dTotal = 0;
                if (decimal.TryParse(lblTotal.Text, out decimal total))
                {
                    dTotal = total;
                }
                else
                {
                    MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                decimal dPay = 0;
                if (decimal.TryParse(txtPay.Text, out decimal pay))
                {
                    dPay = pay;
                }
                else
                {
                    MessageBox.Show("Masukkan berat dalam format numerik.", "Peringatan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtStatus.Text == "LUNAS")
                {
                    MessageBox.Show($"Sudah dibayar !!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                if (dPay >= dTotal)
                {
                    decimal change = dPay - dTotal;
                    MessageBox.Show($"Pembayaran berhasil! Kembalian: {change:C}", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    t.Status = "LUNAS";

                }
                else
                {
                    MessageBox.Show("Pembayaran tidak mencukupi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t.Status = "BELUM LUNAS";

                }

                t.Finish = DateTime.Now;

                tc.UpdateStatus(t);

                LoadData();
            }
            else 
            {
                MessageBox.Show("Data belum dipilih", "Peringatan",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (lvwTransactions.SelectedItems.Count > 0)
            {
                // Mendapatkan item yang dipilih
                ListViewItem selectedItem = lvwTransactions.SelectedItems[0];
                Transactions t = new Transactions();

                // Mendapatkan data dari item yang dipilih
                t.Id = selectedItem.SubItems[1].Text;
                t.CustomerId = selectedItem.SubItems[2].Text;
                t.EmployeeId  = selectedItem.SubItems[3].Text;
                t.ServiceId = selectedItem.SubItems[4].Text;

                if (int.TryParse(selectedItem.SubItems[5].Text, out int weight))
                {
                    t.Weight = weight;
                }
                else
                {
                    MessageBox.Show("Invalid weight value in the selected item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                t.Status = selectedItem.SubItems[6].Text;

                if (decimal.TryParse(selectedItem.SubItems[7].Text, out decimal total))
                {
                    t.Total = total;
                }
                else
                {
                    MessageBox.Show("Invalid total value in the selected item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generate receipt text
                string receiptText = GenerateReceiptText(t);

                // Copy text to clipboard
                Clipboard.SetText(receiptText);

                // Notify user and allow them to paste the text
                MessageBox.Show("Resi transaksi telah tersalin ke clipboard.", "Resi transaksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silahkan pilih item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GenerateReceiptText(Transactions transaction)
        {

            // Create an instance of the TransactionReceipt class
            TransactionReceipt receipt = new TransactionReceipt
            {
                Header = "SUKABUMI LAUNDRY\n\nKel. Condongcatur, Kec. Depok, Kab. Sleman, DI Yogyakarta\nNo. HP 0881 2211 3311\ns",
                TransactionInfo = $"Tanggal : {DateTime.Now:dd/MM/yyyy - HH:mm}\nNo Nota : {transaction.Id}\nKasir : {transaction.EmployeeId}",
                ItemsInfo = $"Layanan : {transaction.ServiceId}",
                SummaryInfo = $"Subtotal = Rp. {transaction.Total:C}",
                Footer = "Status : Menunggu diambil\n\n1. Tanpa request cuci pisah atau cuci manual, Pakaian luntur atau rusak bukan menjadi tanggung jawab laundry.\n2. Komplain pakaian kami layani 1 jam, sejak pakaian diambil.\n3. Bila tidak memberikan catatan jumlah pcs laundry, maka kami anggap benar sesuai jumlah kiloan yang masuk.\n4. Pengambilan laundry wajib menunjukkan nota yang sudah dikirimkan melalui Whatsapp.\n5. Laundry yang tidak diambil jangka waktu 1 bulan, jika terjadi kerusakan bukan menjadi tanggung jawab pemilik.\n6. Pembayaran dilakukan ketika pengambilan laundry, jika ada yang mengatasnamakan SUKABUMI LAUNDRY maka itu bukan pihak kami. \n7. Kami mengucapkan banyak terima kasih telah melakukan transaksi laundry. Semoga anda selalu diberi kelancaran rizky dan semoga sehat selalu"
            };

            // Generate the receipt text
            return receipt.GenerateReceiptText();
        }
    }
}
