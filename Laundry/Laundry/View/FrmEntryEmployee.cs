using Laundry.Model.Entity;
using Laundry.Controller;
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
    public partial class FrmEntryEmployee : Form
    {
        // deklarasi tipe data untuk event OnCreate dan OnUpdate
        public delegate void CreateUpdateEventHandler(Employee emp);
        // deklarasi event ketika terjadi proses input data baru
        public event CreateUpdateEventHandler OnCreate;
        // deklarasi event ketika terjadi proses update data
        public event CreateUpdateEventHandler OnUpdate;
        // deklarasi objek controller
        private EmployeeController controller;
        // deklarasi field untuk menyimpan status entry data (input baru atau update)
        private bool isNewData = true;
        // deklarasi field untuk meyimpan objek mahasiswa
        private Employee emp;
        public FrmEntryEmployee()
        {
            InitializeComponent();
        }

        // constructor untuk inisialisasi data ketika entri data baru
        public FrmEntryEmployee(string title, EmployeeController controller)
         : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;
        }
        // constructor untuk inisialisasi data ketika mengedit data
        public FrmEntryEmployee(string title, Employee obj, EmployeeController
        controller)
         : this()
        {
            // ganti text/judul form
            this.Text = title;
            this.controller = controller;
            isNewData = false; // set status edit data
            emp = obj; // set objek mhs yang akan diedit
                       // untuk edit data, tampilkan data lama
            txtUsername.Text = emp.Username;
            txtName.Text = emp.Name;
            txtPassword.Text = emp.Password;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmEntryMahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // jika data baru, inisialisasi objek mahasiswa
            if (isNewData) emp = new Employee();
            // set nilai property objek mahasiswa yg diambil dari TextBox
            emp.Username = txtUsername.Text;
            emp.Name = txtName.Text;
            emp.Password = txtPassword.Text;
            int result = 0;
            if (isNewData) // tambah data baru, panggil method Create
            {
                // panggil operasi CRUD
                result = controller.Create(emp);
                if (result > 0) // tambah data berhasil
                {
                    OnCreate(emp); // panggil event OnCreate
                                   // reset form input, utk persiapan input data berikutnya
                    txtUsername.Clear();
                    txtName.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            else // edit data, panggil method Update
            {
                // panggil operasi CRUD
                result = controller.Update(emp);
                if (result > 0)
                {
                    OnUpdate(emp); // panggil event OnUpdate
                    this.Close();
                }
            }
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
