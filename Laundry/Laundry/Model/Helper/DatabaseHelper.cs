using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Input;

namespace Laundry.Model.Helper
{
    public static class DatabaseHelper
    {

        public static void InitializeDatabase()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string databasePath = Path.Combine(currentDirectory, "Laundry.db");
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Check if tables exist
                bool tablesExist = CheckIfTablesExist(connection);

                if (!tablesExist)
                {
                    CreateTables(connection);
                }

                connection.Close();
            }

            InsertAdminUserIfNotExist();
        }

        private static bool CheckIfTablesExist(SQLiteConnection connection)
        {
            string checkTablesQuery = "SELECT name FROM sqlite_master WHERE type='table';";
            using (var checkTablesCommand = new SQLiteCommand(checkTablesQuery, connection))
            {
                using (var reader = checkTablesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        if (tableName == "employees" || tableName == "customers" || tableName == "services" || tableName == "transactions")
                        {
                            return true; 
                        }
                    }
                }
            }

            return false; 
        }

        private static void CreateTables(SQLiteConnection connection)
        {
            // Membuat tabel
            string createEmployeesTableQuery = @"
        CREATE TABLE IF NOT EXISTS employees (
        id VARCHAR(255) PRIMARY KEY,
        username VARCHAR(50) NOT NULL,
        name VARCHAR(255) NOT NULL,
        password VARCHAR(255) NOT NULL,
        auth_password VARCHAR(255) 
        );";

            string createCustomersTableQuery = @"
        CREATE TABLE IF NOT EXISTS customers (
        id VARCHAR(255) PRIMARY KEY,
        name VARCHAR(255),
        address VARCHAR(255),
        phone_number VARCHAR(255)
        );";

            string createServicesTableQuery = @"
        CREATE TABLE IF NOT EXISTS services (
        id VARCHAR(255) PRIMARY KEY,
        name VARCHAR(255),
        price FLOAT,
        duration VARCHAR(255)
        );";

            string createTransactionsTableQuery = @"
        CREATE TABLE IF NOT EXISTS transactions (
        id VARCHAR(255) PRIMARY KEY,
        employee_id VARCHAR(255),
        customer_id VARCHAR(255),
        service_id VARCHAR(255),
        weight INT,
        status VARCHAR(255),
        total DECIMAL(10, 2),
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        finished_at TIMESTAMP,
        FOREIGN KEY (employee_id) REFERENCES employees(id),
        FOREIGN KEY (customer_id) REFERENCES customers(id),
        FOREIGN KEY (service_id) REFERENCES services(id)
        );";

            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = createEmployeesTableQuery;
                command.ExecuteNonQuery();
                command.CommandText = createCustomersTableQuery;
                command.ExecuteNonQuery();
                command.CommandText = createServicesTableQuery;
                command.ExecuteNonQuery();
                command.CommandText = createTransactionsTableQuery;
                command.ExecuteNonQuery();
            }
        }




        public static void InsertAdminUserIfNotExist()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string databasePath = Path.Combine(currentDirectory,"Laundry.db");

            string connectionString = $"Data Source={databasePath};Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkAdminQuery = "SELECT COUNT(*) FROM employees WHERE username = 'admin'";
                using (var checkCommand = new SQLiteCommand(checkAdminQuery, connection))
                {
                    int adminCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (adminCount == 0)
                    {
                        // Admin user does not exist, insert it
                        string insertAdminQuery = "INSERT INTO employees (username, name, password) VALUES ('admin', 'admin', 'admin')";
                        using (var insertCommand = new SQLiteCommand(insertAdminQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                             connection.Close();
                        }
                    }
                }
            }


        }
    }
}
