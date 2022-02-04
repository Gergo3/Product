using Microsoft.AspNetCore.Mvc;
using ProductBackend.Models;

namespace productbackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    [HttpGet]
    public List<Product> Get(string search)
    {
        var res = new List<Product>();
        var product1 = new Product() { Id = Guid.NewGuid(), Name = "Alma", Price = 7.14, Rating = 5, Active = true, };
        res.Add(product1);
        var product2 = new Product() { Id = Guid.NewGuid(), Name = "Körte", Price = 4.84, Rating = 8, Active = true, };
        res.Add(product2);
        var product3 = new Product() { Id = Guid.NewGuid(), Name = "Barack", Price = 5, Rating = 2, Active = true, };
        res.Add(product3);


        var res2 = res
            .Where(x => x.Name.ToLower().Contains(search.ToLower()))
            .Where(x => x.Active)
            .OrderByDescending(x => x.Rating)
            .ToList();
        return res2;
    }
}
