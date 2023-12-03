using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laundry.Model.Entity;
using Laundry.Controller;

namespace Laundry.View
{
    public partial class FrmEmployee : Form
    {
        // deklarasi tipe data untuk event OnCreate dan OnUpdate
        public delegate void CreateUpdateEventHandler(Employee emp);
        // deklarasi objek controller
        private EmployeeController controller;
        // deklarasi field untuk meyimpan objek mahasiswa
        private Employee emp;
        private List<Employee> listOfEmployee = new List<Employee>();

        //private EmployeeController controller;
        public FrmEmployee()
        {
            InitializeComponent();
            controller = new EmployeeController();
            InisialisasiListView();
            LoadDataEmployee ();
        }

        // atur kolom listview
        private void InisialisasiListView()
        {
            lvwEmployee.View = System.Windows.Forms.View.Details;
            lvwEmployee.FullRowSelect = true;
            lvwEmployee.GridLines = true;
            lvwEmployee.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwEmployee.Columns.Add("Username", 91, HorizontalAlignment.Center);
            lvwEmployee.Columns.Add("Name", 350, HorizontalAlignment.Left);
            lvwEmployee.Columns.Add("Password", 80, HorizontalAlignment.Center);
        }

        // method untuk menampilkan semua data mahasiswa
        private void LoadDataEmployee()
        {
            // kosongkan listview
            lvwEmployee.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfEmployee = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var emp in listOfEmployee)
            {
                var noUrut = lvwEmployee.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(emp.Username);
                item.SubItems.Add(emp.Name);
                item.SubItems.Add(emp.Password);
                // tampilkan data mhs ke listview
                lvwEmployee.Items.Add(item);
            }
        }

        // method event handler untuk merespon event OnCreate,
        private void OnCreateEventHandler(Employee emp)
        {
            // tambahkan objek mhs yang baru ke dalam collection
            listOfEmployee.Add(emp);
            int noUrut = lvwEmployee.Items.Count + 1;
            // tampilkan data mhs yg baru ke list view
            ListViewItem item = new ListViewItem(noUrut.ToString());
            item.SubItems.Add(emp.Username);
            item.SubItems.Add(emp.Name);
            item.SubItems.Add(emp.Password);
            lvwEmployee.Items.Add(item);
        }
        // method event handler untuk merespon event OnUpdate,
        private void OnUpdateEventHandler(Employee emp)
        {
            // ambil index data mhs yang edit
            int index = lvwEmployee.SelectedIndices[0];
            // update informasi mhs di listview
            ListViewItem itemRow = lvwEmployee.Items[index];
            itemRow.SubItems[1].Text = emp.Username;
            itemRow.SubItems[2].Text = emp.Name;
            itemRow.SubItems[3].Text = emp.Password;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            // set nilai property objek mahasiswa yg diambil dari TextBox
            emp.Username = txtUsername.Text;
            emp.Name = txtName.Text;
            emp.Password = txtPassword.Text;

            int result = 0;
            result = controller.Create(emp);
        }

        private void btnPerbaiki_Click(object sender, EventArgs e)
        {
            if (lvwEmployee.SelectedItems.Count > 0)
            {
                // ambil objek mhs yang mau diedit dari collection
                Employee emp = listOfEmployee[lvwEmployee.SelectedIndices[0]];
                // buat objek form entry data mahasiswa
                FrmEntryEmployee frmEntry = new FrmEntryEmployee("Edit Data Mahasiswa", emp, controller);
                // mendaftarkan method event handler untuk merespon event OnUpdate
                frmEntry.OnUpdate += OnUpdateEventHandler;
                // tampilkan form entry mahasiswa
                frmEntry.ShowDialog();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan",
               MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }

        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwEmployee.SelectedItems.Count > 0)
            {
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus ? ", "Konfirmasi",
               
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil objek mhs yang mau dihapus dari collection
                    Employee emp =
                   listOfEmployee[lvwEmployee.SelectedIndices[0]];
                    // panggil operasi CRUD
                    var result = controller.Delete(emp);
                    if (result > 0) LoadDataEmployee();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmEmployee_Load(object sender, EventArgs e)
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
    }
}
