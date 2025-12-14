using System;
using System.ComponentModel.DataAnnotations;
using Application.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Attributes;

public class ExsistAttribute<Type> : ValidationAttribute
    where Type : class
{
    public ExsistAttribute() { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }
        var service =
            validationContext.GetService(typeof(IDataValidationService)) as IDataValidationService;

        if (service is null)
            throw new NullReferenceException("Service biriktirilmagan");

        bool response = service.Exsist<Type>(value);

        if (response == false)
            return new ValidationResult($"berilgan {value}  {nameof(Type)} da mavjud emas");

        return ValidationResult.Success;
    }
}
