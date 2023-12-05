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
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username VARCHAR(50) NOT NULL,
                    name VARCHAR(255) NOT NULL,
                    password VARCHAR(100) NOT NULL
                    );";

                    string createCustomersTableQuery = @"
                    CREATE TABLE IF NOT EXISTS customers (
                    id INT PRIMARY KEY,
                    name VARCHAR(255),
                    address VARCHAR(255),
                    phone_number VARCHAR(255),
                    created_at TIMESTAMP,
                    updated_at TIMESTAMP
                    );";

                    string createServicesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS services (
                    id INT PRIMARY KEY,
                    name VARCHAR(255),
                    price INT,
                    duration VARCHAR(255),
                    created_at TIMESTAMP,
                    updated_at TIMESTAMP
                    );";

                    string createTransactionsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS transactions (
                    id INT PRIMARY KEY,
                    employee_id INT,
                    customer_id INT,
                    service_id INT,
                    weight INT,
                    order_date DATE,
                    finish_time DATETIME,
                    status VARCHAR(50),
                    total INT,
                    created_at TIMESTAMP,
                    updated_at TIMESTAMP,
                    FOREIGN KEY (employee_id) REFERENCES employees(id),
                    FOREIGN KEY (customer_id) REFERENCES customers(id),
                    FOREIGN KEY (service_id) REFERENCES services(id)
                    );";

                    string createItemsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS items (
                    id INT PRIMARY KEY,
                    transaction_id INT,
                    item_type VARCHAR(255),
                    quantity INT,
                    FOREIGN KEY (transaction_id) REFERENCES transactions(id)
                    );";

                    string createIncomesTableQuery = @"
                    CREATE TABLE IF NOT EXISTS incomes (
                    id INT PRIMARY KEY,
                    employee_id INT,
                    date DATE,
                    total INT,
                    created_at TIMESTAMP,
                    updated_at TIMESTAMP,
                    FOREIGN KEY (employee_id) REFERENCES employees(id)
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
                        command.CommandText = createItemsTableQuery;
                        command.ExecuteNonQuery();
                        command.CommandText = createIncomesTableQuery;
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
