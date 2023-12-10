using Laundry.Model.Context;
using Laundry.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model.Repository
{
    public class ServiceRepository
    {
        // deklarsi objek connection
        private SQLiteConnection _conn;

        // constructor
        public ServiceRepository(DbContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        public Service ReadById(string id)
        {
            Service s = new Service();

            try
            {
                // deklarasi perintah SQL
                string sql = @"SELECT id, name, price, duration 
                       FROM services
                       WHERE id = @id
                       LIMIT 1"; // Hanya ambil satu baris

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@id", id);

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        if (dtr.Read())
                        {
                            s.Id = dtr["id"].ToString();
                            s.Name = dtr["name"].ToString();

                            // Konversi nilai "price" ke tipe data int
                            int price;
                            if (int.TryParse(dtr["price"].ToString(), out price))
                            {
                                s.Price = price;
                            }
                            else
                            {
                                s.Price = 0; // Nilai default jika konversi gagal
                            }

                            s.Duration = dtr["duration"].ToString(); // Perhatikan penggunaan huruf kecil pada "duration"
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadById error: {0}", ex.Message);
            }

            return s;
        }


        private int GetServiceCount()
        {
            string countSql = "SELECT COUNT(*) FROM services";

            using (SQLiteCommand countCmd = new SQLiteCommand(countSql, _conn))
            {
                int serviceCount = Convert.ToInt32(countCmd.ExecuteScalar());

                return serviceCount;
            }
        }

        private string GenerateId()
        {
            int count = GetServiceCount();
            int incrementedCount = count + 1;

            // Menggunakan ticks sebagai bagian dari ID untuk memastikan keunikan
            string id = $"ID-SV-{incrementedCount}-{DateTime.Now.Ticks}";

            return id;
        }


        public int Create(Service s)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into services (id, name, price, duration)
                   values (@id, @name, @price, @duration)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                string newCustomerId = GenerateId();

                cmd.Parameters.AddWithValue("@id", newCustomerId);
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@price", s.Price);
                cmd.Parameters.AddWithValue("@duration", s.Duration);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return result;
        }

        public int Update(Service s)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update services SET name = @name, price = @price, duration = @duration
                           where id = @id";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id", s.Id);
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@price", s.Price);
                cmd.Parameters.AddWithValue("@duration", s.Duration);

                try
                {
                    // jalankan perintah UPDATE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }

            return result;
        }

        public int Delete(string id)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from services
                           where id = @id";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    // jalankan perintah DELETE dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }

            return result;
        }

        public List<Service> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Service> list = new List<Service>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select * from services";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Service s = new Service();
                            s.Id = dtr["id"].ToString();
                            s.Name = dtr["name"].ToString();

                            // Konversi nilai "price" ke tipe data int
                            int price;
                            if (int.TryParse(dtr["price"].ToString(), out price))
                            {
                                s.Price = price;
                            }
                            else
                            {
                                s.Price = 0; // Nilai default jika konversi gagal
                            }

                            s.Duration = dtr["Duration"].ToString();

                            // tambahkan objek customer ke dalam collection
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }

        // Method untuk menampilkan data mahasiwa berdasarkan pencarian nama
        public Service ReadByName(string name)
        {
            Service service = null;

            try
            {
                // deklarasi perintah SQL
                string sql = @"SELECT id, name, price, duration 
                       FROM services
                       WHERE name LIKE @name
                       ORDER BY name";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@name", string.Format("%{0}%", name));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari baris result set ke objek
                            service = new Service();
                            service.Id = dtr["id"].ToString();
                            service.Name = dtr["name"].ToString();

                            // Konversi nilai "price" ke tipe data yang sesuai
                            if (int.TryParse(dtr["price"].ToString(), out int price))
                            {
                                service.Price = price;
                            }
                            else
                            {
                                service.Price = 0; // Nilai default jika konversi gagal
                            }

                            service.Duration = dtr["duration"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
                // Jangan lupa untuk menangani atau melaporkan kesalahan dengan benar
            }

            return service;
        }

        public List<Service> ReadByNames(string name)
        {
            List<Service> list = new List<Service>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"SELECT id, name, price, duration 
                       FROM services
                       WHERE name LIKE @name
                       ORDER BY name";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@name", string.Format("%{0}%", name));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            Service service = new Service();
                            // proses konversi dari baris result set ke objek
                            service = new Service();
                            service.Id = dtr["id"].ToString();
                            service.Name = dtr["name"].ToString();

                            // Konversi nilai "price" ke tipe data yang sesuai
                            if (int.TryParse(dtr["price"].ToString(), out int price))
                            {
                                service.Price = price;
                            }
                            else
                            {
                                service.Price = 0; // Nilai default jika konversi gagal
                            }

                            service.Duration = dtr["duration"].ToString();

                            list.Add(service);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
                // Jangan lupa untuk menangani atau melaporkan kesalahan dengan benar
            }

            return list;
        }

    }
}
