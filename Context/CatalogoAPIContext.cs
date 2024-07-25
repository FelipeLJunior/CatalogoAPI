using Microsoft.EntityFrameworkCore;
using CatalogoAPI.Models;

namespace CatalogoAPI.Context;

public class CatalogoAPIContext : DbContext
{
    public CatalogoAPIContext(DbContextOptions<CatalogoAPIContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}