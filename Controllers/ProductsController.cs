using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase 
{
    private readonly CatalogoAPIContext _context;

    public ProductsController(CatalogoAPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<ICollection<Product>> Get()
    {
        List<Product>? products = _context.Products?.AsNoTracking().ToList();

        if(products is null)
        {
            return NotFound("Não há produtos...");
        }

        return products;
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public ActionResult<Product> Get(int id)
    {
        Product? product = _context.Products?.AsNoTracking().FirstOrDefault(product => product.ProductId == id);

        if(product is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        if(product is null)
        {
            return BadRequest();
        }

        _context.Products?.Add(product);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetProduct", new {id = product.ProductId}, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product) {
        if(id != product.ProductId) {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) {
        Product? product = _context.Products?.FirstOrDefault(product => product.ProductId == id);

        if(product is null) {
            return NotFound();
        }

        _context.Products?.Remove(product);
        _context.SaveChanges();

        return Ok(product);
    }
}