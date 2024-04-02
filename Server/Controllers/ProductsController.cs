using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.Interfaces;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IAppDbContext _context;

        public ProductsController(IAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            var mappedProds = products.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                ReleaseDate = x.ReleaseDate,
                Image = x.Image,
                IsSmart = x.IsSmart ? "Yes" : "No",
                Price = $"${x.Price}",
                NextMaintenanceDate = x.NextMaintenanceDate
            }).ToList();

            return Ok(mappedProds);
        }
    }
}
