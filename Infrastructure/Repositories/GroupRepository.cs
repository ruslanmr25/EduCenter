using System;
using System.Linq.Expressions;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : BaseRepository<Group>
{
    public GroupRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<Group>> GetAllAsync(
        int centerId,
        int page,
        int pageSize = 50,
        List<Expression<Func<Group, bool>>>? conditions = null,
        Expression<Func<Group, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);

        query = query
            .Where(g => g.CenterId == centerId)
            .Include(g => g.Teacher)
            .Include(g => g.Science);

        Console.WriteLine("BU yerga keldi");
        Console.WriteLine("________________________________");

        if (conditions is not null && conditions.Count > 0)
        {
            foreach (var condition in conditions)
            {
                query = query.Where(condition);
            }
        }

        return await GetPagedResult(query, page, pageSize);
    }

    public async Task<List<Group>> GetAllAsyncByIds(List<int> groupIds)
    {
        return await _context
            .Groups.Where(g => groupIds.Contains(g.Id))
            .OrderBy(g => g.CreatedAt)
            .ToListAsync();
    }

    public override async Task<Group?> GetAsync(int id)
    {
        Group? entity = await _context
            .Set<Group>()
            .Include(g => g.Teacher)
            .Include(g => g.Science)
            .Include(g => g.Teacher)
            .Include(g => g.Students)
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync();

        return entity;
    }
}
