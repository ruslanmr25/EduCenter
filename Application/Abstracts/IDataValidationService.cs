using System;

namespace Application.Abstracts;

public interface IDataValidationService
{
    public bool Exsist(Type type, object value);
    public bool Unique();
}
