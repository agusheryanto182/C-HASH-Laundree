using System;
using System.Collections.Generic;

using System.Data.SQLite;
using Laundry.Model.Entity;
using Laundry.Model.Context;


namespace Laundry.Model.Repository
{
    public class EmployeeRepository
    {
        // deklarsi objek connection
        private SQLiteConnection _conn;

        // constructor
        public EmployeeRepository(DbContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        public int Create(Employee emp)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into employees (username, name, password)
                           values (@username, @name, @password)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@username", emp.Username);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@password", emp.Password);

                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }

            return result;
        }

        public int Update(Employee mhs)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update mahasiswa set name = @name, password = @password
                           where username = @username";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@name", mhs.Name);
                cmd.Parameters.AddWithValue("@password", mhs.Password);
                cmd.Parameters.AddWithValue("@username", mhs.Username);

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

        public int Delete(Employee emp)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from employees
                           where username = @username";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@username", emp.Username);

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

        public List<Employee> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Employee> list = new List<Employee>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select username, name, password
                               from employees 
                               order by name";

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
                            Employee emp = new Employee();
                            emp.Username = dtr["username"].ToString();
                            emp.Name = dtr["name"].ToString();
                            emp.Password = dtr["password"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(emp);
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
        public List<Employee> ReadByName(string name)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Employee> list = new List<Employee>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select username, name 
                               from employee 
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
                            Employee emp = new Employee();
                            emp.Username = dtr["username"].ToString();
                            emp.Name = dtr["name"].ToString();

                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(emp);
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
