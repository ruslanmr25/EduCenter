using System;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CenterRepository : BaseRepository<Center>
{
    public CenterRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public override async Task<PagedResult<Center>> GetAllAsync(int page, int pageSize = 50)
    {
        var query = _context.Set<Center>().AsQueryable();
        var totalCount = await query.CountAsync();

        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(c => c.CenterAdmin)
            .ToListAsync();

        return new PagedResult<Center>(result, totalCount, page, pageSize);
    }
}
