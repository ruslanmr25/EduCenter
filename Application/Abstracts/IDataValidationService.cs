using System;

namespace Application.Abstracts;

public interface IDataValidationService
{
    public bool Exsist(Type type, object value);
    bool Unique<T>(Func<T, bool> predicate)
        where T : class;
}
