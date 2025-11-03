using System.ComponentModel.DataAnnotations;

namespace Application.Attributes;

public class CompareDateAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public CompareDateAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var currentValue = (DateOnly)value!;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            return new ValidationResult($"Property {_comparisonProperty} topilmadi.");

        var comparisonValue = (DateOnly)property.GetValue(validationContext.ObjectInstance)!;

        if (currentValue <= comparisonValue)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}
