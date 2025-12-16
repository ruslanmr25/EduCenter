using Application.Results;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<Group?> GetAsync(int id, int centerId)
    {
        var query = BuildBaseQuery();

        Group? entity = await query
            .Include(g => g.Teacher)
            .Include(g => g.Science)
            .Include(g => g.GroupStudentPaymentSycles)
            .ThenInclude(gs => gs.Student)
            .Include(g => g.GroupStudentPaymentSycles)
            .ThenInclude(gs => gs.StudentPayments)
            .Where(g => g.Id == id && g.CenterId == centerId)
            .FirstOrDefaultAsync();

        return entity;
    }

    public async Task<List<StudentPaymentRowModel>> GroupPaymentModel(int id, int centerId)
    {
        var query = BuildBaseQuery();

        Group? group = await _context
            .Groups.Where(g => g.Id == id && g.CenterId == centerId)
            .FirstOrDefaultAsync();

        List<StudentPaymentRowModel> sycles = await _context
            .GroupStudentPaymentSycles.Where(gs => gs.GroupId == id)
            .Select(gs => new StudentPaymentRowModel
            {
                StudentId = gs.StudentId,
                StudentName = gs.Student!.FullName,
                StudentPayments = gs.StudentPayments.OrderByDescending(sp => sp.CreatedAt).ToList(),

                JoinedAt = gs.CreatedAt,
            })
            .ToListAsync();

        return sycles;
    }
}
