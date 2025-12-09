using System;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";

        public async Task SaveAsync(Order order)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES (@customer, @product, @quantity, @price)";
                    command.Parameters.AddWithValue("@customer", order.CustomerName);
                    command.Parameters.AddWithValue("@product", order.ProductName);
                    command.Parameters.AddWithValue("@quantity", order.Quantity);
                    command.Parameters.AddWithValue("@price", order.Price);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SeedBadDataAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES ('John', 'Widget', 9999, 9.99)";
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


    }
}

