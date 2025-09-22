using System;
using Application.Results;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<User>> GetAllTeacher(int centerId, int page, int pageSize = 50)
    {
        var query = _context.Set<User>().AsQueryable();
        var totalCount = await query.CountAsync();

        var result = await query
            .Skip((page - 1) * pageSize)
            .Where(u => u.Role == Role.Teacher)
            .Where(u => u.Centers.Any(c => c.Id == centerId))
            .Take(pageSize)
            .ToListAsync();

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
