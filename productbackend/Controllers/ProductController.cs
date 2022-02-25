using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductBackend;
using ProductBackend.Models;

namespace productbackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpPost("RecreateDatebase")]
    public async Task RecreateDatebase()
    {
        using var db = new ProductContext();
        db.Database.EnsureDeleted();
        await db.Database.MigrateAsync();
        
    }

    [HttpPost("Populate")]
    public async Task Populate()
    {
        var products = new List<Product>();
        var product1 = new Product() { Id = Guid.NewGuid(), Name = "Alma", Price = 7.14, Rating = 5, Active = true, };
        products.Add(product1);
        var product2 = new Product() { Id = Guid.NewGuid(), Name = "Körte", Price = 4.84, Rating = 8, Active = true, };
        products.Add(product2);
        var product3 = new Product() { Id = Guid.NewGuid(), Name = "Barack", Price = 5, Rating = 2, Active = true, };
        products.Add(product3);

        using var db = new ProductContext();
        db.AddRange(products);
        var product4 = new Product() { Id = Guid.NewGuid(), Name = "Szõlõ", Price = 2, Rating = 23, Active = false, };
        db.Add(product4);

        await db.SaveChangesAsync();
    }
    [HttpGet]
    public async Task<List<Product>> GetAsync(string? search)
    {
        using var db = new ProductContext();

        var query = db.Products
            .Where(x => x.Active);
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query
                .Where(x => x.Name.ToLower().Contains(search.ToLower()));

        }
        query = query
            .OrderByDescending(x => x.Rating);
        var res2 = query.ToList();


        //var res2 = db.Products
        //    .Where(x => x.Name.ToLower().Contains(search.ToLower()))
        //    .Where(x => x.Active)
        //    .OrderByDescending(x => x.Rating)
        //    .ToList();
        await Task.Delay(500);
        return res2;
    }

    [HttpGet("Get2")]
    public List<Product> Get2(string search)
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
