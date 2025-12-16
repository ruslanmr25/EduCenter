using Application.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Application.Attributes;

public class UniqueAttribute<Type> : ValidationAttribute
    where Type : class
{
    protected Expression<Func<Type, bool>> predicate;

    public UniqueAttribute(Expression<Func<Type, bool>> predicate)
    {
        this.predicate = predicate;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var service =
            validationContext.GetService(typeof(IDataValidationService)) as IDataValidationService;

        if (service is null)
            throw new NullReferenceException("Serves biriktirilmagan");

        bool response = service.Unique(predicate);

        if (response)
            return new ValidationResult($"foydalanuvchi nomi allaqachon olingan");

        return ValidationResult.Success;
    }
}
