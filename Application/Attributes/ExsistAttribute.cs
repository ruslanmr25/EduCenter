using System;
using System.ComponentModel.DataAnnotations;
using Application.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Attributes;

public class ExsistAttribute : ValidationAttribute
{
    private readonly Type _type;

    private readonly string _propertName;

    public ExsistAttribute(Type entityType, string propertyName)
    {
        _type = entityType;
        _propertName = propertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult($"{_type.Name} ID kiritilishi kerak");
        }

        var service =
            validationContext.GetService(typeof(IDataValidationService)) as IDataValidationService;

        if (service is null)
            throw new NullReferenceException("Service biriktirilmagan");

        bool response = service.Exsist(_type, value);

        if (response == false)
            return new ValidationResult($"berilgan {value}  {_type} da mavjud emas");

        return ValidationResult.Success;
    }
}
