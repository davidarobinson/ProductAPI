using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductAPI.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void ShouldReturnOkResultWithDataOnSuccessfullKeywordSearchInName()
        {
            string keyWord = "motherboard";
            var products = TestHelpers.products.Where(m => m.Name.Contains(keyWord, StringComparison.InvariantCultureIgnoreCase));
            ProductController productController = BuildController(products);

            IActionResult actionResult = productController.Get(keyWord);

            Assert.IsType<OkObjectResult>(actionResult);
            var contentData = (actionResult as OkObjectResult).Value as List<Product>;
            Assert.Equal(products, contentData);
        }

        [Theory()]
        [InlineData("COM9904")]
        [InlineData("10-2408")]
        public void ShouldReturnOkResultWithDataOnSuccessfullKeywordSearchInProductCode(string keyWord)
        {
            keyWord = Uri.UnescapeDataString(keyWord);
            var products = TestHelpers.products.Where(m => m.ProductCode.Contains(keyWord, StringComparison.InvariantCultureIgnoreCase));
            ProductController productController = BuildController(products);

            IActionResult actionResult = productController.Get(keyWord);

            Assert.IsType<OkObjectResult>(actionResult);
            var contentData = (actionResult as OkObjectResult).Value as List<Product>;
            Assert.Equal(products, contentData);
        }

        [Fact]
        public void ShouldReturnNotFoundResultOnUnsuccessfulKeywordSearch()
        {
            var products = new List<Product>();
            var productController = BuildController(products);

            IActionResult actionResult = productController.Get("*************");

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Theory()]
        [InlineData("10-2408-01")]
        [InlineData("COM990405")]
        public void ShouldReturnOkResultWithSingleProductOnValidByCodeProductSearch(string productCode)
        {
            productCode = Uri.UnescapeDataString(productCode);
            var products = TestHelpers.products.Where(m => string.Compare(m.ProductCode, productCode, true) == 0);
            ProductController productController = BuildController(products);

            IActionResult actionResult = productController.GetByCode(productCode);

            Assert.IsType<OkObjectResult>(actionResult);
            var contentData = (actionResult as OkObjectResult).Value as List<Product>;
            Assert.Single(contentData);
            Assert.Equal(products, contentData);
        }

        [Fact]
        public void ShouldReturnNotFoundResultOnInvalidByCodeProductSearch()
        {
            var products = new List<Product>();
            var productController = BuildController(products);

            IActionResult actionResult = productController.GetByCode("INVALID_PRODUCT_CODE");

            Assert.IsType<NotFoundResult>(actionResult);
        }



        private static ProductController BuildController(IEnumerable<Product> products)
        {
            var productDBSetMock = TestHelpers.CreateDbSetMock(products);
            var productContextMock = new Mock<ProductDbContext>();
            productContextMock.Setup(x => x.AllProducts).Returns(productDBSetMock.Object);
            var productController = new ProductController(productContextMock.Object);
            return productController;
        }
    }
}
