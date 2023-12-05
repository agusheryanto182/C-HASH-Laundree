using Laundry.Model.Context;
using Laundry.Model.Entity;
using Laundry.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laundry.Controller
{
    public class CustomerController
    {
        // deklarasi objek Repository untuk menjalankan operasi CRUD
        private CustomerRepository _repository;

        public Customer ReadByData(Customer cs)
        {
            Customer rc = new Customer();
            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new CustomerRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                rc = _repository.ReadByData(cs);
            }

            return rc;
        }

        public List<Customer> ReadByName(string name)
        {
            // membuat objek collection
            List<Customer> list = new List<Customer>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new CustomerRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                list = _repository.ReadByName(name);
            }

            return list;
        }

        public List<Customer> ReadAll()
        {
            // membuat objek collection
            List<Customer> list = new List<Customer>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new CustomerRepository(context);

                // panggil method ReadAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }

        public int Create(Customer cs)
        {
            int result = 0;

            if (string.IsNullOrEmpty(cs.Name))
            {
                MessageBox.Show("Name harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nama yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(cs.Address))
            {
                MessageBox.Show("Alamat harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek angkatan yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(cs.PhoneNumber))
            {
                MessageBox.Show("Nomer Telepon harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new CustomerRepository(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Create(cs);
            }

            if (result > 0)
            {
                MessageBox.Show("Data pelanggan berhasil disimpan !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data pelanggan gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Update(Customer cs)
        {
            int result = 0;

            if (string.IsNullOrEmpty(cs.Address))
            {
                MessageBox.Show("Alamat harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(cs.PhoneNumber))
            {
                MessageBox.Show("Nomer Telepon harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new CustomerRepository(context);

                // panggil method Update class repository untuk mengupdate data
                result = _repository.Update(cs);
            }

            if (result > 0)
            {
                MessageBox.Show("Data pelanggan berhasil diupdate !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data pelanggan gagal diupdate !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Delete(Customer cs)
        {
            int result = 0;

            if (string.IsNullOrEmpty(cs.Name))
            {
                MessageBox.Show("Name harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new CustomerRepository(context);

                // panggil method Delete class repository untuk menghapus data
                result = _repository.Delete(cs);
            }

            if (result > 0)
            {
                MessageBox.Show("Data pelanggan berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data pelanggan gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

    }
}
