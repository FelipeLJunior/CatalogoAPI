using System.ComponentModel.DataAnnotations;

namespace CatalogoAPI.Validations;

public class UpperFirstLetterAttribute : ValidationAttribute {
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
        if(value == null || string.IsNullOrEmpty(value.ToString())) {
            return ValidationResult.Success!;
        }

        if(value.ToString()![0] != value.ToString()!.ToUpper()[0]) {
            return new ValidationResult("A primeira letra deve ser maiúscula");
        }

        return ValidationResult.Success!;
    }
}