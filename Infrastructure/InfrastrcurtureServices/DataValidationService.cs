using Application.Abstracts;
using Infrastructure.Context;
using System.Linq.Expressions;

namespace Infrastructure.InfrastrcurtureServices;

public class DataValidationSerive : IDataValidationService
{
    private AppDbContext _context;

    public DataValidationSerive(AppDbContext context)
    {
        _context = context;
    }

    public bool Exsist<Type>(object value)
        where Type : class
    {
        var entity = _context.Set<Type>().Find(value);

        return !(entity == null);
    }

    public bool Unique<T>(Expression<Func<T, bool>> predicate)
        where T : class
    {
        return _context.Set<T>().Any(predicate);
    }
}
