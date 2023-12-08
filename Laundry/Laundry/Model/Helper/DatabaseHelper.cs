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
        private static string connectionString = @"Data Source=D:\SEMESTER 3\PEMROGRAMAN LANJUT\LAUNDRY\laundry v1\laundry\Database\Laundry.db";

        public static void InitializeDatabase()
        {
            if (!File.Exists(@"D:\SEMESTER 3\PEMROGRAMAN LANJUT\LAUNDRY\laundry v1\laundry\Database\Laundry.db"))
            {
                SQLiteConnection.CreateFile(@"D:\SEMESTER 3\PEMROGRAMAN LANJUT\LAUNDRY\laundry v1\laundry\Database\Laundry.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();  

                    // membuat tabel
                    string createEmployeesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS employees (
                    id VARCHAR(255) PRIMARY KEY,
                    username VARCHAR(50) NOT NULL,
                    name VARCHAR(255) NOT NULL,
                    password VARCHAR(100) NOT NULL
                    );";

                    string createCustomersTableQuery = @"
                    CREATE TABLE IF NOT EXISTS customers (
                    id VARCHAR(255) PRIMARY KEY,
                    name VARCHAR(255),
                    address VARCHAR(255),
                    phone_number VARCHAR(255),
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                    );";

                    string createServicesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS services (
                    id VARCHAR(255) PRIMARY KEY,
                    name VARCHAR(255),
                    price FLOAT,
                    duration VARCHAR(255),
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
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
                    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
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
                        connection.Close();

                    }

                }
            }
            

            InsertAdminUserIfNotExist();

        }

        public static void InsertAdminUserIfNotExist()
        {
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
