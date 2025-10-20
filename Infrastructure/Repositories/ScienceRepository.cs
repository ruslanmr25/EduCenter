using System;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScienceRepository : BaseRepository<Science>
{
    public ScienceRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<Science>> GetAllAsync(int centerId, int page, int pageSize = 50)
    {
        var query = _context.Set<Science>().AsQueryable().Where(s => s.CenterId == centerId);
        var totalCount = await query.CountAsync();

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Science>(result, totalCount, page, pageSize);
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
