using System;
using Application.Abstracts;
using Infrastructure.Context;

namespace Infrastructure.InfrastrcurtureServices;

public class DataValidationSerive : IDataValidationService
{
    private AppDbContext _context;

    public DataValidationSerive(AppDbContext context)
    {
        _context = context;
    }

    public bool Exsist(Type type, object value)
    {
        var entity = _context.Find(type, value);

        return !(entity == null);
    }

    public bool Unique()
    {
        throw new NotImplementedException();
    }
}
