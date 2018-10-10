using Microsoft.EntityFrameworkCore;
using Moq;
using ProductAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPI.Tests
{
    public static class TestHelpers
    {
        public static IEnumerable<Product> products = new List<Product>
        {
            new Product{ProductCode="10-2408-01", Name="Beanie hat"},
            new Product{ProductCode="COM990404", Name="Altek low power motherboard"},
            new Product{ProductCode="COM990405", Name="Trust pentium motherboard"},
            new Product{ProductCode="15-3768-22", Name="Black denim jeans"},
            new Product{ProductCode="20-1748-34", Name="Blue denim jeans"}
        };



        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
