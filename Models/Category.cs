using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models;

[Table("Categories")]
public class Category 
{
    [Key]
    public int CategoryId { get; set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; }
}