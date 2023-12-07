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
        private Service detailS = new Service();
        public FrmService()
        {
            InitializeComponent();
            controller = new ServiceController();
            InisialisasiListView();
            LoadDataService();
            LoadDataServiceByClick();
        }

        private void FrmService_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Service s = new Service();
            // set nilai property objek mahasiswa yg diambil dari TextBox
            s.Name = txtName.Text;
            if (int.TryParse(txtPrice.Text, out int price))
            {
                s.Price = price;
            }
            else
            {
                MessageBox.Show("Invalid price value. Please enter a valid number.");
                return; 
            }
            s.Duration = txtDuration.Text;

            controller.Create(s);
            LoadDataService();

            ClearTextBoxes();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvwService.SelectedItems.Count > 0)
            {
                LoadDataServiceByClick();
                Service s = new Service();
                s.Id = lblNoService.Text;
                s.Name = txtName.Text;
                if (int.TryParse(txtPrice.Text, out int price))
                {
                    s.Price = price;
                }
                else
                {
                    MessageBox.Show("Invalid price value. Please enter a valid number.");
                    return;
                }
                s.Duration = txtDuration.Text;
                s.Duration = txtDuration.Text;

                controller.Update(s);

                LoadDataService();
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
            if (lvwService.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data layanan ingin dihapus ? ", "Konfirmasi",

                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    var noService = lblNoService.Text;
                    var result = controller.Delete(noService);
                    if (result > 0) LoadDataService();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data layanan belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void InisialisasiListView()
        {
            lvwService.View = System.Windows.Forms.View.Details;
            lvwService.FullRowSelect = true;
            lvwService.GridLines = true;
            lvwService.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwService.Columns.Add("No Service.", 200, HorizontalAlignment.Center);
            lvwService.Columns.Add("Nama", 200, HorizontalAlignment.Center);
            lvwService.Columns.Add("Harga", 200, HorizontalAlignment.Left);
            lvwService.Columns.Add("Durasi", 200, HorizontalAlignment.Center);
        }

        // method untuk menampilkan semua data mahasiswa
        private void LoadDataService()
        {
            ClearTextBoxes();
            // kosongkan listview
            lvwService.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfService = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var s in listOfService)
            {
                var noUrut = lvwService.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(s.Id);
                item.SubItems.Add(s.Name);
                item.SubItems.Add(s.Price.ToString());
                item.SubItems.Add(s.Duration);
                // tampilkan data mhs ke listview
                lvwService.Items.Add(item);
            }
        }

        private void ClearTextBoxes()
        {
            // Bersihkan nilai teks di TextBox
            lblNoService.Text = "";
            txtName.Text = "";
            txtDuration.Text = "";
            txtPrice.Text = "";
        }

        private void LoadDataServiceByClick()
        {
            // kosongkan listview
            lvwService.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfService = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var s in listOfService)
            {
                var noUrut = lvwService.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(s.Id);
                item.SubItems.Add(s.Name);
                item.SubItems.Add(s.Price.ToString());
                item.SubItems.Add(s.Duration);
                // tampilkan data mhs ke listview
                lvwService.Items.Add(item);
            }
            lvwService.Click += new EventHandler(lvwService_Click);
        }

        private void LoadDataServiceByName(string name)
        {
            lvwService.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            detailS = controller.ReadByName(name);
            // ekstrak objek mhs dari collection
            foreach (var s in listOfService)
            {
                var noUrut = lvwService.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(s.Id);
                item.SubItems.Add(s.Name);
                item.SubItems.Add(s.Price.ToString());
                item.SubItems.Add(s.Duration);
                // tampilkan data mhs ke listview
                lvwService.Items.Add(item);
            }
        }

        private void lvwService_Click(object sender, EventArgs e)
        {
            if (lvwService.SelectedItems.Count > 0)
            {
                // Mendapatkan item yang dipilih
                ListViewItem selectedItem = lvwService.SelectedItems[0];
                Service s = new Service();

                // Mendapatkan data dari item yang dipilih
                s.Id = selectedItem.SubItems[1].Text;
                s.Name = selectedItem.SubItems[2].Text;

                // Konversi nilai dari string ke float untuk properti Price
                if (int.TryParse(selectedItem.SubItems[3].Text, out int price))
                {
                    s.Price = price;
                }
                else
                {
                    MessageBox.Show("Invalid price value in the selected item.");
                    return;
                }

                s.Duration = selectedItem.SubItems[4].Text;

                // Menampilkan data ke TextBox
                lblNoService.Text = s.Id;
                txtName.Text = s.Name;
                txtPrice.Text = s.Price.ToString(); // Konversi float ke string
                txtDuration.Text = s.Duration;
                txtDuration.Text = s.Duration;
            }
        }

        private void lvwService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtSearch.Text;

            // Check if the name is null or empty
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    LoadDataServiceByName(name);
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
