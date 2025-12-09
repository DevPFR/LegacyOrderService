using NUnit.Framework;
using LegacyOrderService.Data;
using System;
using System.Threading.Tasks;

namespace LegacyOrderService.Tests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private IProductRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new ProductRepository();
        }

        [Test]
        public async Task GetPriceAsync_ExistingProduct_ReturnsPrice()
        {
            double price = await _repo.GetPriceAsync("Widget");
            Assert.That(price, Is.EqualTo(12.99));
        }

        [Test]
        public void GetPriceAsync_NonExistingProduct_ThrowsException()
        {
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _repo.GetPriceAsync("NonExistingProduct");
            });
        }
    }
}