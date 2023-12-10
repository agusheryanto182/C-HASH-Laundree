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
using System.Xml.Linq;
using System.Security.Cryptography;

namespace Laundry.View
{
    public partial class FrmEmployee : Form
    {
        // deklarasi objek controller
        private EmployeeController controller;
        // deklarasi field untuk meyimpan objek mahasiswa
        private List<Employee> listOfEmployee = new List<Employee>();

        //private EmployeeController controller;
        public FrmEmployee()
        {
            InitializeComponent();
            controller = new EmployeeController();
            InisialisasiListView();
            LoadDataEmployee ();
            LoadDataEmployeeByClick();
            txtPassword.UseSystemPasswordChar = true;
        }

        // atur kolom listview
        private void InisialisasiListView()
        {
            lvwEmployee.View = System.Windows.Forms.View.Details;
            lvwEmployee.FullRowSelect = true;
            lvwEmployee.GridLines = true;
            lvwEmployee.Columns.Add("No.", 35, HorizontalAlignment.Center);
            lvwEmployee.Columns.Add("Username", 300, HorizontalAlignment.Center);
            lvwEmployee.Columns.Add("Name", 300, HorizontalAlignment.Left);
        }

        // method untuk menampilkan semua data mahasiswa
        private void LoadDataEmployee()
        {
            ClearTextBoxes();
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
                // tampilkan data mhs ke listview
                lvwEmployee.Items.Add(item);
            }
        }

        private void LoadDataEmployeeByClick()
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
                // tampilkan data mhs ke listview
                lvwEmployee.Items.Add(item);
            }
            lvwEmployee.Click += new EventHandler(lvwEmployee_Click);
        }

        private void LoadDataEmployeeByName(string name)
        {
            // kosongkan listview
            lvwEmployee.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfEmployee = controller.ReadByName(name);
            // ekstrak objek mhs dari collection
            foreach (var emp in listOfEmployee)
            {
                var noUrut = lvwEmployee.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(emp.Username);
                item.SubItems.Add(emp.Name);
                // tampilkan data mhs ke listview
                lvwEmployee.Items.Add(item);
            }
        }

        private void lvwEmployee_Click(object sender, EventArgs e)
        {
            if (lvwEmployee.SelectedItems.Count > 0)
            {
                // Mendapatkan item yang dipilih
                ListViewItem selectedItem = lvwEmployee.SelectedItems[0];

                // Mendapatkan data dari item yang dipilih
                string username = selectedItem.SubItems[1].Text;
                string name = selectedItem.SubItems[2].Text;

                // Menampilkan data ke TextBox
                txtUsername.Text = username;
                txtName.Text = name;
                // Di bagian deklarasi kelas atau konstruktor
              

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lvwMahasiswa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Fungsi untuk menghasilkan salt acak
        static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Fungsi untuk menghitung hash menggunakan SHA-256
        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Mengubah byte array menjadi string hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;

            if (loginName == "admin")
            {
                Employee emp = new Employee();
                // set nilai property objek mahasiswa yg diambil dari TextBox
                emp.Username = txtUsername.Text;
                emp.Name = txtName.Text;
                string password = txtPassword.Text;

                string salt = GenerateSalt();

                string combinedString = password + salt;

                string hashedPassword = ComputeSHA256Hash(combinedString);

                emp.Password = hashedPassword;

                emp.AuthPassword = salt;

                controller.Create(emp);
                LoadDataEmployee();

                ClearTextBoxes();
            } else
            {
                MessageBox.Show("Anda tidak memiliki izin !!!", "Peringatan",
           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void ClearTextBoxes()
        {
            // Bersihkan nilai teks di TextBox
            txtUsername.Text = "";
            txtName.Text = "";
            txtPassword.Text = "";
            txtSearch.Text = "";
        }
   
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            string loginName = frmHome.EnteredName;
            Console.WriteLine("di bagian delete : " + loginName);

            if (lvwEmployee.SelectedItems.Count > 0)
            {
                if (loginName == "admin")
                {
                    var konfirmasi = MessageBox.Show("Apakah data employee ingin dihapus ? ", "Konfirmasi",

                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (konfirmasi == DialogResult.Yes)
                    {
                        // ambil objek mhs yang mau dihapus dari collection
                        string username = txtUsername.Text;
                        var result = controller.Delete(username);
                        if (result > 0) LoadDataEmployee();
                    }

                }else
                {
                    MessageBox.Show("Anda tidak memiliki izin !!!", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
               
            }
            else 
            {
                MessageBox.Show("Data employee belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FrmHome frmHome = (FrmHome)Application.OpenForms["FrmHome"];
            if (frmHome != null)
            {
                string loginName = frmHome.EnteredName;

                // Memastikan loginName tidak null atau kosong
                if (string.IsNullOrEmpty(loginName))
                {
                    Console.WriteLine("LoginName is null or empty.");
                    return;
                }

                // Mengambil data dari database berdasarkan nama
                var r = controller.ReadDetailByName(loginName);

                // Memastikan hasil query tidak null
                if (r == null)
                {
                    Console.WriteLine("Data not found for loginName: " + loginName);
                    return;
                }

                Console.WriteLine("LoginName: " + loginName);
                Console.WriteLine("Name from database: " + r.Name);

                if (lvwEmployee.SelectedItems.Count > 0)
                {
                    LoadDataEmployeeByClick();

                    // set nilai property objek mahasiswa yg diambil dari TextBox
                    Employee emp = new Employee();
                    emp.Username = txtUsername.Text;

                    // Memastikan hasil query untuk username tidak null
                    var result = controller.ReadByUsername(emp.Username);
                    if (result != null)
                    {
                        // Memastikan bahwa hasil query untuk username ada dan sesuai
                        if (r.Username == emp.Username || loginName == "admin")
                        {
                            emp.Name = txtName.Text;

                            // Cek apakah password diubah atau tidak
                            if (string.IsNullOrEmpty(txtPassword.Text))
                            {
                                // Password tidak diubah, gunakan password dari database
                                emp.Password = result.Password;
                                emp.AuthPassword = result.AuthPassword;
                            }
                            else
                            {
                                // Password diubah, lakukan hashing
                                string password = txtPassword.Text;
                                string salt = GenerateSalt();
                                string combinedString = password + salt;
                                string hashedPassword = ComputeSHA256Hash(combinedString);

                                emp.Password = hashedPassword;
                                emp.AuthPassword = salt;
                            }

                            // Melakukan update ke database
                            controller.Update(emp);
                            LoadDataEmployee();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Anda tidak memiliki izin !!!", "Peringatan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Username not found in the database: " + emp.Username);
                    }
                }
                else // data belum dipilih
                {
                    MessageBox.Show("Data belum dipilih", "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                Console.WriteLine("FrmHome is null.");
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtSearch.Text;

            // Check if the name is null or empty
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    LoadDataEmployeeByName(name);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }
    }
}
