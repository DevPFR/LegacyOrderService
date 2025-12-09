using System;
using Microsoft.Extensions.DependencyInjection;
using LegacyOrderService.Models;
using LegacyOrderService.Data;


namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {

            // Setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .BuildServiceProvider();

            Console.WriteLine("Welcome to Order Processor!");
            string name = null;
            do
            {
                Console.WriteLine("Enter customer name:");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Customer name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            string product = null;
            do
            {
                Console.WriteLine("Enter product name:");
                product = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(product))
                {
                    Console.WriteLine("Product name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(product));
            var productRepo = serviceProvider.GetService<IProductRepository>();
            double price = 0.0;
            try
            {
                price = productRepo.GetPrice(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }


            Console.WriteLine("Enter quantity:");

            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            Console.WriteLine("Processing order...");

            Order order = new Order
            {
                CustomerName = name,
                ProductName = product,
                Quantity = qty,
                Price = price
            };

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine($"Total: ${total}");

            Console.WriteLine("Saving order to database...");
            var repo = serviceProvider.GetService<IOrderRepository>();
            try
            {
                repo.Save(order);
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save order: {ex.Message}");
            }

        }
    }
}
