using System.ComponentModel.DataAnnotations;

namespace OCP5.Attributes.Validation;

public class FileValidationAttribute(string[] contentTypes, long maxSize) : ValidationAttribute
{
    public string[] ContentTypes { get; } = contentTypes;
    public long MaxSize { get; } = maxSize;

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            if (file.Length > 0 && file.Length <= MaxSize)
            {
                return ContentTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase) 
                    ? ValidationResult.Success 
                    : new ValidationResult(ErrorMessage);
            }

            return new ValidationResult("Le fichier doit faire moins de 2 Mo.");
        }

        return ValidationResult.Success;
    }
}