using Laundry.Model.Context;
using Laundry.Model.Entity;
using Laundry.View;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model.Repository
{
   public class TransactionRepository
    {
        // deklarsi objek connection
        private SQLiteConnection _conn;

        // constructor
        public TransactionRepository(DbContext context)
        {
            // inisialisasi objek connection
            _conn = context.Conn;
        }

        private int GetCount()
        {
            string countSql = "SELECT COUNT(*) FROM transactions";

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
            string id = $"ID-TRC-{incrementedCount}-{DateTime.Now.Ticks}";

            return id;
        }


        public int Create(Transactions t)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"insert into transactions (id, employee_id, customer_id, service_id, weight, status, total, created_at)
                   values (@id, @employee_id, @customer_id, @service_id, @weight, @status, @total, @created_at)";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {
                string newId = GenerateId();

                cmd.Parameters.AddWithValue("@id", newId);
                cmd.Parameters.AddWithValue("@employee_id", t.EmployeeId);
                cmd.Parameters.AddWithValue("@customer_id", t.CustomerId);
                cmd.Parameters.AddWithValue("@service_id", t.ServiceId);
                cmd.Parameters.AddWithValue("@weight", t.Weight);
                cmd.Parameters.AddWithValue("@status", t.Status);
                cmd.Parameters.AddWithValue("@total", t.Total);
                cmd.Parameters.AddWithValue("@created_at", t.Order);


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

        public int Update(Transactions t)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update transactions SET service_id = @service_id, employee_id = @employee_id, weight = @weight, status = @status, total = @total
                           where id = @id";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {

                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id", t.Id);
                cmd.Parameters.AddWithValue("@service_id", t.ServiceId);
                cmd.Parameters.AddWithValue("@employee_id", t.EmployeeId);
                cmd.Parameters.AddWithValue("@weight", t.Weight);
                cmd.Parameters.AddWithValue("@status", t.Status);
                cmd.Parameters.AddWithValue("@total", t.Total);


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

        public int UpdateStatus(Transactions t)
        {
            int result = 0;

            // deklarasi perintah SQL
            string sql = @"update transactions SET employee_id = @employee_id, status = @status, finished_at = @finished_at
                           where id = @id";

            // membuat objek command menggunakan blok using
            using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
            {

                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@id", t.Id);
                cmd.Parameters.AddWithValue("@employee_id", t.EmployeeId);
                cmd.Parameters.AddWithValue("@status", t.Status);
                cmd.Parameters.AddWithValue("@finished_at", t.Finish);

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
            string sql = @"delete from transactions
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

        public List<Transactions> ReadAll()
        {
            List<Transactions> list = new List<Transactions>();

            try
            {
                string sql = @"SELECT id, employee_id, customer_id, service_id, weight, status, total, created_at, finished_at
                        FROM transactions";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            Transactions t = new Transactions();
                            t.Id = dtr["id"].ToString();
                            t.EmployeeId = dtr["employee_id"].ToString();
                            t.CustomerId = dtr["customer_id"].ToString();
                            t.ServiceId = dtr["service_id"].ToString();
                            t.Weight = dtr["weight"] == DBNull.Value ? 0 : Convert.ToInt32(dtr["weight"]);
                            t.Status = dtr["status"].ToString();
                            t.Total = dtr["total"] == DBNull.Value ? 0 : Convert.ToDecimal(dtr["total"]);

                            // Check for DBNull before converting to DateTime
                            t.Order = dtr["created_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtr["created_at"]);
                            t.Finish = dtr["finished_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtr["finished_at"]);

                            list.Add(t);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Handle the exception using a logging framework or Console.WriteLine
                Console.WriteLine("ReadAll error: {0}", ex.Message);
            }

            return list;
        }





        // Method untuk menampilkan data mahasiwa berdasarkan pencarian nama
        public List<Transactions> ReadByName(string customerId)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Transactions> list = new List<Transactions>();

            try
            {
                // deklarasi perintah SQL
                string sql = @"select id, employee_id, customer_id, service_id, weight, status, total 
                               from transactions
                               where customer_id like @customer_id";

                // membuat objek command menggunakan blok using
                using (SQLiteCommand cmd = new SQLiteCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@customer_id", string.Format("%{0}%", customerId));

                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                    using (SQLiteDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                        while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Transactions t = new Transactions();
                            t.Id = dtr["id"].ToString();
                            t.EmployeeId = dtr["employee_id"].ToString();
                            t.CustomerId = dtr["customer_id"].ToString();
                            t.ServiceId = dtr["service_id"].ToString();

                            int weight;
                            if (int.TryParse(dtr["weight"].ToString(), out weight))
                            {
                                t.Weight = weight;
                            }
                            else
                            {
                                t.Weight = 0;
                            }

                            t.Status = dtr["status"].ToString();

                            decimal total;
                            if (decimal.TryParse(dtr["total"].ToString(), out total))
                            {
                                t.Total = total;
                            }
                            else
                            {
                                t.Total = 0;
                            }


                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(t);
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
