using System;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public override async Task<PagedResult<Student>> GetAllAsync(int page, int pageSize = 50)
    {
        var query = _context.Set<Student>().Include(s => s.Groups).AsQueryable();
        var totalCount = await query.CountAsync();

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Student>(result, totalCount, page, pageSize);
    }

    public override async Task<Student?> GetAsync(int id)
    {
        return await _context
            .Set<Student>()
            .Include(s => s.Groups)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
