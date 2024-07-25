using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogoAPI.Models;
using CatalogoAPI.Context;

namespace CatalogoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase {
    private readonly CatalogoAPIContext _context;

    public CategoriesController(CatalogoAPIContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Category>>> GetAsync() {
        List<Category>? categories = await _context.Categories.AsNoTracking().ToListAsync();

        if(categories is null) {
            return NotFound();
        }

        return categories;
    }

    [HttpGet("products")]
    public async Task<ActionResult<ICollection<Category>>> GetProductsCategoryAsync() {
        List<Category>? categories = await _context.Categories.Include(table => table.Products).AsNoTracking().ToListAsync();

        if(categories is null) {
            return NotFound();
        }

        return categories;
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetAsync(int id) {
        Category? category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(category => category.CategoryId == id);

        if(category is null) {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult Post(Category category) {
        if(category is null) {
            return BadRequest();
        }

        _context.Categories?.Add(category);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetCategory", new {id = category.CategoryId}, category);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Category category) {
        if(id != category.CategoryId) {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(category);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id) {
        Category? category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);

        if(category is null) {
            return NotFound();
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();

        return Ok(category);
    }
} 