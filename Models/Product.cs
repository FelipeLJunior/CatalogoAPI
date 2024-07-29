using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CatalogoAPI.Validations;

namespace CatalogoAPI.Models;

[Table("Products")]
public class Product : IValidatableObject
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(80)]
    [UpperFirstLetter]
    public string? Name { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName="decimal(12,2)")]
    public decimal Price { get; set; }
    
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public float Stock { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
        
        if(this.Price <= 0) {
            yield return new ValidationResult("O Preço precisa ser maior que zero!", new []{nameof(this.Price)});
        }

        if(this.Stock < 0) {
            yield return new ValidationResult("O Estoque deve ser maior ou igual a zero!", new []{ nameof(this.Stock)});
        }
    }
}