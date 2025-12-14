using System;
using System.Linq.Expressions;

namespace Application.Abstracts;

public interface IDataValidationService
{
    public bool Exsist<Type>(object value)
        where Type : class;
    bool Unique<T>(Expression<Func<T, bool>> predicate)
        where T : class;
}
