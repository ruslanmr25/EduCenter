using System;
using System.Linq.Expressions;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScienceRepository : BaseRepository<Science>
{
    public ScienceRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<Science>> GetAllAsync(
        int centerId,
        int page,
        int pageSize = 50,
        Expression<Func<Science, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);
        query = query.Where(s => s.CenterId == centerId);

        return await GetPagedResult(query, page, pageSize);
    }

    public async Task<Science?> GetAsync(int centerId, int id)
    {
        Science? entity = await _context
            .Set<Science>()
            .Where(s => s.CenterId == centerId)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

        return entity;
    }
}
