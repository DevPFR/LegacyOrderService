using NUnit.Framework;
using LegacyOrderService.Data;
using LegacyOrderService.Models;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.Sqlite;

namespace LegacyOrderService.Tests.Repositories
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private IOrderRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new OrderRepository();
        }

        [Test]
        public async Task SaveAsync_ValidOrder_SavesSuccessfully()
        {
            var order = new Order
            {
                CustomerName = "Test Customer",
                ProductName = "Widget",
                Quantity = 1,
                Price = 10.0
            };

            // Ensure db file exists
            var dbPath = Path.Combine(System.AppContext.BaseDirectory, "orders.db");
            if (!File.Exists(dbPath))
            {
                using var conn = new SqliteConnection($"Data Source={dbPath}");
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Orders(
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    CustomerName TEXT,
                                    ProductName TEXT,
                                    Quantity INTEGER,
                                    Price REAL)";
                cmd.ExecuteNonQuery();
            }

            Assert.DoesNotThrowAsync(async () => await _repo.SaveAsync(order));
        }

        [Test]
        public async Task SeedBadDataAsync_InsertsDataWithoutException()
        {
            Assert.DoesNotThrowAsync(async () => await _repo.SeedBadDataAsync());
        }
    }
}