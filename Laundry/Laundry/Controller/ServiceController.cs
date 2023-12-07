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
    public class ServiceController
    {
        // deklarasi objek Repository untuk menjalankan operasi CRUD
        private ServiceRepository _repository;

        public Service ReadById(string id)
        {
            // membuat objek collection
            Service c = new Service();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new ServiceRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                c = _repository.ReadById(id);
            }

            return c;
        }


        public Service ReadByName(string name)
        {
            // membuat objek collection
            Service s = new Service();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new ServiceRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                s = _repository.ReadByName(name);
            }

            return s;
        }

        public List<Service> ReadAll()
        {
            // membuat objek collection
            List<Service> list = new List<Service>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new ServiceRepository(context);

                // panggil method ReadAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }

        public int Create(Service s)
        {
            int result = 0;

            if (string.IsNullOrEmpty(s.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nama yang diinputkan tidak boleh kosong
            if (s.Price == 0)
            {
                MessageBox.Show("Harga harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek angkatan yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(s.Duration))
            {
                MessageBox.Show("Durasi harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new ServiceRepository(context);

                // panggil method Create class repository untuk menambahkan data
                result = _repository.Create(s);
            }

            if (result > 0)
            {
                MessageBox.Show("Data layaanan berhasil disimpan !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data layanan gagal disimpan !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Update(Service s)
        {
            int result = 0;

            if (string.IsNullOrEmpty(s.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek nama yang diinputkan tidak boleh kosong
            if (s.Price == 0)
            {
                MessageBox.Show("Harga harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // cek angkatan yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(s.Duration))
            {
                MessageBox.Show("Durasi harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new ServiceRepository(context);

                // panggil method Update class repository untuk mengupdate data
                result = _repository.Update(s);
            }

            if (result > 0)
            {
                MessageBox.Show("Data layanan berhasil diupdate !", "Informasi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data layanan gagal diupdate !!!", "Peringatan",
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
                _repository = new ServiceRepository(context);

                // panggil method Delete class repository untuk menghapus data
                result = _repository.Delete(id);
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
