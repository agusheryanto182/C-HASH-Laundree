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
    public class CustomerRepository
    {
        // deklarsi objek connection
        private SQLiteConnection _conn;

        // constructor
        public CustomerRepository(DbContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        private int GetCustomerCount()
        {
            string countSql = "SELECT COUNT(*) FROM customers";

            using (SQLiteCommand countCmd = new SQLiteCommand(countSql, _conn))
            {
                int customerCount = Convert.ToInt32(countCmd.ExecuteScalar());

                if (customerCount == 0)
                    {
                    customerCount = customerCount + 1;
                }
                return customerCount;
            }
        }

        private string GenerateCustomerId()
        {
            int currentCustomerCount = GetCustomerCount();
            int newCustomerIdNumber = currentCustomerCount + 1;

            // Format ID pelanggan sesuai dengan keinginan Anda (misalnya, "ID-PEL001")
            string newCustomerId = "ID-PEL-" + newCustomerIdNumber + "LAUNDREE";

            return newCustomerId;
        }

        public int Create(Customer cs)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into customers (id_pelanggan, name, address, phone_number)
                   values (@id_pelanggan, @name, @address, @phone_number)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                string newCustomerId = GenerateCustomerId();

                cmd.Parameters.AddWithValue("@id_pelanggan", newCustomerId);
                cmd.Parameters.AddWithValue("@name", cs.Name);
                cmd.Parameters.AddWithValue("@address", cs.Address);
                cmd.Parameters.AddWithValue("@phone_number", cs.PhoneNumber);

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

        public int Update(Customer cs)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update customers SET name = @name, address = @address, phone_number = @phone_number
                           where id_pelanggan = @id_pelanggan";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id_pelanggan", cs.IdPelanggan);
                cmd.Parameters.AddWithValue("@name", cs.Name);
                cmd.Parameters.AddWithValue("@address", cs.Address);
                cmd.Parameters.AddWithValue("@phone_number", cs.PhoneNumber);

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

        public int Delete(Customer cs)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from customers
                           where name = @name";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@name", cs.Name);

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

        public List<Customer> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Customer> list = new List<Customer>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id_pelanggan, name, address, phone_number
                               from customers";

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
                            Customer cs = new Customer();
                            cs.IdPelanggan = dtr["id_pelanggan"].ToString();
                            cs.Name = dtr["name"].ToString();
                            cs.Address = dtr["address"].ToString();
                            cs.PhoneNumber = dtr["phone_number"].ToString();

                            // tambahkan objek customer ke dalam collection
                            list.Add(cs);
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
        public List<Customer> ReadByName(string name)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Customer> list = new List<Customer>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select name, address, phone_number 
                               from customers
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
                            Customer cs = new Customer();
                            cs.Name = dtr["name"].ToString();
                            cs.Address = dtr["address"].ToString();
                            cs.PhoneNumber = dtr["phone_number"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(cs);
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

        public Customer ReadByData(Customer cs)
        {
            Customer rc = new Customer();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, name, address, phone_number 
                               from customers
                               where name = @name AND address = @address AND phone_number = @phone_number";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@name", string.Format("%{0}%", cs.Name));
                    cmd.Parameters.AddWithValue("@address", string.Format("%{0}%", cs.Address));
                    cmd.Parameters.AddWithValue("@phone_number", string.Format("%{0}%", cs.PhoneNumber));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            rc.IdPelanggan = dtr["id"].ToString();   
                            rc.Name = dtr["name"].ToString();
                            rc.Address = dtr["address"].ToString();
                            rc.PhoneNumber = dtr["phone_number"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
            }

            return rc;
        }

    }
}
