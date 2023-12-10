using System;
using System.Collections.Generic;

using System.Data.SQLite;
using Laundry.Model.Entity;
using Laundry.Model.Context;
using System.Windows.Forms;

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

        private int GetCount()
        {
            string countSql = "SELECT COUNT(*) FROM employees";

            using (SQLiteCommand countCmd = new SQLiteCommand(countSql, _conn))
            {
                int c = Convert.ToInt32(countCmd.ExecuteScalar());

                return c;
            }
        }

        private string GenerateId()
        {
            int count = GetCount();
            int incrementedCount = count + 1;

            // Menggunakan ticks sebagai bagian dari ID untuk memastikan keunikan
            string id = $"ID-EMP-{incrementedCount}-{DateTime.Now.Ticks}";

            return id;
        }


        public int Create(Employee emp)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into employees (id, username, name, password, auth_password)
                           values (@id, @username, @name, @password, @auth_password)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                string newId = GenerateId();
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id", newId);
                cmd.Parameters.AddWithValue("@username", emp.Username);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@password", emp.Password);
                cmd.Parameters.AddWithValue("@auth_password", emp.AuthPassword);


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

        public int Update(Employee emp)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update employees set name = @name, password = @password, auth_password = @auth_password
                           where username = @username";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@username", emp.Username);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@password", emp.Password);
                cmd.Parameters.AddWithValue("@auth_password", emp.AuthPassword);

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

        public int Delete(string username)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"delete from employees
                           where username = @username";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@username", username);

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
                string sql = @"select username, name, password 
                               from employees 
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
                            emp.Password = dtr["password"].ToString();

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

        public Employee ReadDetailByName(string name)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            Employee emp = new  Employee();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, username, name, password 
                               from employees 
                               where name = @name";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@name", name);

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        if (dtr.Read())
                        {
                            emp.Id = dtr["id"].ToString();
                            emp.Username = dtr["username"].ToString();
                            emp.Name = dtr["name"].ToString();
                            emp.Password = dtr["password"].ToString();
                        }
                        else
                        {
                            emp = null; // atau atur propertinya menjadi string kosong atau nilai default lainnya
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByName error: {0}", ex.Message);
            }

            return emp;
        }

        public Employee ReadByUsername(string username)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            Employee remp = new Employee();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, username, name, password, auth_password
                               from employees
                               where username = @username";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@username", string.Format(username));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Employee emp = new Employee();
                            emp.Id = dtr["id"].ToString();
                            emp.Username = dtr["username"].ToString();
                            emp.Name = dtr["name"].ToString();
                            emp.Password = dtr["password"].ToString();
                            emp.AuthPassword = dtr["auth_password"].ToString();


                            remp = emp;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByUsername error: {0}", ex.Message);
            }

            return remp;
        }

        public Employee ReadByEmployeeId(string employeeId)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            Employee remp = new Employee();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, username, name, password, auth_password
                               from employees
                               where id = @id";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Employee emp = new Employee();
                            emp.Id = dtr["id"].ToString();
                            emp.Username = dtr["username"].ToString();
                            emp.Name = dtr["name"].ToString();
                            emp.Password = dtr["password"].ToString();
                            emp.AuthPassword = dtr["auth_password"].ToString();


                            remp = emp;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByUsername error: {0}", ex.Message);
            }

            return remp;
        }
    }
}
