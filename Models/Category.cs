using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CatalogoAPI.Validations;

namespace CatalogoAPI.Models;

[Table("Categories")]
public class Category 
{
    [Key]
    public int CategoryId { get; set; }
    
    [Required]
    [StringLength(80)]
    [UpperFirstLetter]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    [JsonIgnore]
    public ICollection<Product>? Products { get; set; }
}