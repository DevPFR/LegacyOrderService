using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var productRepo = new ProductRepository();
            double price = productRepo.GetPrice(product);


            Console.WriteLine("Enter quantity:");
            int qty = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");

            Order order = new Order();
            order.CustomerName = name;
            order.ProductName = product;
            order.Quantity = qty;
            order.Price = price;

            double total = order.Quantity * order.Price;

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + price);

            Console.WriteLine("Saving order to database...");
            var repo = new OrderRepository();
            repo.Save(order);
            Console.WriteLine("Done.");
        }
    }
}
