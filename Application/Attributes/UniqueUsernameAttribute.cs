using System;
using System.ComponentModel.DataAnnotations;
using Application.Abstracts;
using Domain.Entities;

namespace Application.Attributes;

public class UniqueUsernameAttribute : ValidationAttribute
{
    public UniqueUsernameAttribute() { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult($"Value  null bulmasligi kerak");
        }

        var service =
            validationContext.GetService(typeof(IDataValidationService)) as IDataValidationService;

        if (service is null)
            throw new NullReferenceException("Serves biriktirilmagan");

        bool response = service.Unique<User>(u => u.Username == (string)value);

        if (response)
            return new ValidationResult($"foydalanuvchi nomi allaqachon olingan");

        return ValidationResult.Success;
    }
}
