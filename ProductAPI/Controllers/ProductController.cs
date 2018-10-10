using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using System;
using System.Linq;

namespace ProductAPI.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{search}")]
        public IActionResult Get(string search)
        {
            var keyWord = Uri.UnescapeDataString(search);
            var results = _context.AllProducts.Where(p =>
               p.ProductCode.Contains(keyWord, StringComparison.InvariantCultureIgnoreCase)
               || p.Name.Contains(keyWord, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if (results.Any()) { return Ok(results); }
            else { return NotFound(); }
        }

        [HttpGet]
        [Route("ByProductCode/{pc}")]
        public IActionResult GetByCode(string pc)
        {
            var productCode = Uri.UnescapeDataString(pc);
            var results = _context.AllProducts.Where(p => string.Compare(p.ProductCode, productCode, true) == 0).ToList();
            if (results.Any()) { return Ok(results); }
            else { return NotFound(); }
        }
    }
}