// Data/ProductRepository.cs
using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyOrderService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, double> _productPrices = new()
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public async Task<double> GetPriceAsync(string productName)

        {
            // Simulate an expensive lookup
            await Task.Delay(500);

            if (_productPrices.TryGetValue(productName, out var price))
                return price;

            throw new Exception("Product not found");
        }
    }
}
