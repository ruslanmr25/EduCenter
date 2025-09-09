using System;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : BaseRepository<Group>
{
    public GroupRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<Group>> GetAllAsync(int centerId, int page, int pageSize = 50)
    {
        var query = _context.Set<Group>().AsQueryable();
        var totalCount = await query.CountAsync();

        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Where(g => g.CenterId == centerId)
            .ToListAsync();

        return new PagedResult<Group>(result, totalCount, page, pageSize);
    }
}
