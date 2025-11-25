using System;
using System.Linq.Expressions;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public override async Task<PagedResult<Student>> GetAllAsync(
        int page,
        int pageSize = 50,
        Expression<Func<Student, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);
        query = query.Include(s => s.Groups);

        return await GetPagedResult(query, page, pageSize);
    }

    public override async Task<Student?> GetAsync(int id)
    {
        return await _context
            .Set<Student>()
            .Include(s => s.Groups.OrderByDescending(g => g.CreatedAt))
            .Include(s => s.GroupStudentPaymentSycles.OrderByDescending(gs => gs.CreatedAt))
            .ThenInclude(gs => gs.StudentPayments.OrderByDescending(g => g.CreatedAt))
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
