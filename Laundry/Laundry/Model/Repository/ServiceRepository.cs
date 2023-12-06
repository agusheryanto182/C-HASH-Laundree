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

        private int GetServiceCount()
        {
            string countSql = "SELECT COUNT(*) FROM services";

            using (SQLiteCommand countCmd = new SQLiteCommand(countSql, _conn))
            {
                int serviceCount = Convert.ToInt32(countCmd.ExecuteScalar());

                return serviceCount;
            }
        }

        private string GenerateCustomerId()
        {
            int currentServiceCount = GetServiceCount();
            int newServiceIdNumber = currentServiceCount + 1;

            // Format ID pelanggan sesuai dengan keinginan Anda (misalnya, "ID-PEL001")
            string result = "ID-SERV-" + newServiceIdNumber + "-LAUNDREE";

            return result;
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
                string newCustomerId = GenerateCustomerId();

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
                            float price;
                            if (float.TryParse(dtr["price"].ToString(), out price))
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
        public List<Service> ReadByName(string name)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Service> list = new List<Service>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, name, price, duration 
                               from services
                               where name like @name
                               order by name";

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
                            // proses konversi dari row result set ke object
                            Service s = new Service();
                            s.Id = dtr["id"].ToString();
                            s.Name = dtr["name"].ToString();

                            // Konversi nilai "price" ke tipe data int
                            float price;
                            if (float.TryParse(dtr["price"].ToString(), out price))
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
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
            }

            return list;
        }
    }
}
