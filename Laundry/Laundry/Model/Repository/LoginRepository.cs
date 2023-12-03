using System;
using System.Data.SQLite;
using Laundry.Model.Entity;
using Laundry.Model.Context;
using System.Data;

namespace Laundry.Model.Repository
{
    public class LoginRepository
    {
        private SQLiteConnection connection;

        public LoginRepository(DbContext context)
        {
            connection = context.Conn;
        }

        public bool VerifyLogin(Employee emp)
        {
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM employees WHERE username = @username AND password = @password", connection))
            {

                command.Parameters.AddWithValue("@username", emp.Username);
                command.Parameters.AddWithValue("@password", emp.Password);

                try
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    // Tangani pengecualian (log, lempar, dll.)
                    Console.WriteLine($"Error in VerifyLogin: {ex.Message}");
                    return false;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
