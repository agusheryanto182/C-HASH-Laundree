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
    public class TransactionController
    {
        // deklarasi objek Repository untuk menjalankan operasi CRUD
        private TransactionRepository _repository;
        private CustomerController _customerController;
        private ServiceController _serviceController;


        public List<Transactions> ReadByName(string name)
        {
            // membuat objek collection
            List<Transactions> list = new List<Transactions>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new TransactionRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                list = _repository.ReadByName(name);
            }

            return list;
        }

        public List<Transactions> ReadAll()
        {
            // membuat objek collection
            List<Transactions> list = new List<Transactions>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new TransactionRepository(context);

                // panggil method ReadAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }

        public int Create(Transactions t)
        {
            int result = 0;

            if (string.IsNullOrEmpty(t.CustomerId))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


            if (string.IsNullOrEmpty(t.ServiceId))
            {
                MessageBox.Show("Layanan harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (t.Weight == 0)
            {
                MessageBox.Show("Berat harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(t.Status))
            {
                t.Status = "SEDANG DIPROSES";
            }

            if (t.Total == 0)
            {
                MessageBox.Show("Total tidak boleh nol !!!", "Peringatan",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new TransactionRepository(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Create(t);
            }

            if (result > 0)
            {
                MessageBox.Show("Data transaksi berhasil disimpan !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data transaksi gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Update(Transactions t)
        {
            int result = 0;

            if (string.IsNullOrEmpty(t.CustomerId))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nama yang diinputkan tidak boleh kosong
            if (t.Weight == 0)
            {
                MessageBox.Show("Berat harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek angkatan yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(t.Status))
            {
                MessageBox.Show("Status harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new TransactionRepository(context);

                // panggil method Update class repository untuk mengupdate data
                result = _repository.Update(t);
            }

            if (result > 0)
            {
                MessageBox.Show("Data transaksi berhasil diupdate !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data transaksi gagal diupdate !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int UpdateStatus(Transactions t)
        {
            int result = 0;

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new TransactionRepository(context);

                // panggil method Update class repository untuk mengupdate data
                result = _repository.UpdateStatus(t);
            }

            if (result > 0 && t.Status == "LUNAS")
            {
                MessageBox.Show("Pembayaran berhasil !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Pembayaran gagal !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Delete(string id)
        {
            int result = 0;

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new TransactionRepository(context);

                // panggil method Delete class repository untuk menghapus data
                result = _repository.Delete(id);
            }

            if (result > 0)
            {
                MessageBox.Show("Data transaksi berhasil dihapus !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data transaksi gagal dihapus !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

    }
}

