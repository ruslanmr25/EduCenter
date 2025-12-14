using System;
using System.Linq.Expressions;
using Application.Results;
using Common.Queries;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public async Task<PagedResult<Student>> GetAllAsync(
        int centerId,
        StudentQuery studentQuery,
        Expression<Func<Student, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);

        var groupId = studentQuery.GroupId;
        var scienceId = studentQuery.ScienceId;
        var fullName = studentQuery.FullName;

        if (groupId is not null || scienceId is not null)
        {
            query = query.Where(s =>
                s.GroupStudentPaymentSycles.Any(g =>
                    (groupId == null || g.GroupId == groupId)
                    && (scienceId == null || g.Group!.ScienceId == scienceId)
                )
            );
        }

        if (!string.IsNullOrWhiteSpace(fullName))
        {
            query = query.Where(s => EF.Functions.Like(s.FullName, $"%{fullName}%"));
        }
        query = query.Where(s => s.CenterId == centerId);
        query = query.Include(s => s.GroupStudentPaymentSycles).ThenInclude(s => s.Group);

        return await GetPagedResult(query, studentQuery.Page, studentQuery.PageSize);
    }

    public async Task<Student?> GetAsync(int id, int centerId)
    {
        var query = BuildBaseQuery();
        return await query
            .Include(s => s.GroupStudentPaymentSycles.OrderByDescending(gs => gs.CreatedAt))
            .ThenInclude(gs => gs.StudentPayments.OrderByDescending(g => g.CreatedAt))
            .Where(s => s.CenterId == centerId)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
    }
}
