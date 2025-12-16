using Application.Results;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<User>> GetAllCenterAdminAsync(
        int page,
        int pageSize = 50,
        Expression<Func<User, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);
        query = query.Where(u => u.Role == Role.CenterAdmin).Include(u => u.Center);

        return await GetPagedResult(query, page, pageSize);
    }

    public async Task<PagedResult<User>> GetAllTeacherAsync(
        int centerId,
        int page,
        int pageSize = 50,
        Expression<Func<User, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = _context
            .Set<User>()
            .Where(u => u.Role == Role.Teacher)
            .Where(u => u.Centers.Any(c => c.Id == centerId))
            .Include(u => u.Centers)
            .AsQueryable();

        if (orderBy is not null)
        {
            query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        }
        else
        {
            query = query.OrderByDescending(u => u.CreatedAt);
        }

        var totalCount = await query.CountAsync();

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<User>(result, totalCount, page, pageSize);
    }

    public async Task<User?> GetTeacherAsync(int centerId, int id)
    {
        User? entity = await _context
            .Set<User>()
            .FirstOrDefaultAsync(u =>
                u.Id == id && u.Centers.Any(c => c.Id == centerId) && u.Role == Role.Teacher
            );

        return entity;
    }
}
