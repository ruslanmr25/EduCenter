using System;
using System.Linq.Expressions;
using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CenterRepository : BaseRepository<Center>
{
    public CenterRepository(AppDbContext appDbContext)
        : base(appDbContext) { }

    public override async Task<PagedResult<Center>> GetAllAsync(
        int page,
        int pageSize = 50,
        Expression<Func<Center, object>>? orderBy = null,
        bool descending = true
    )
    {
        IQueryable<Center> query = this.BuildBaseQuery(orderBy, descending);

        query = query.Include(c => c.CenterAdmin);

        return await GetPagedResult(query, page, pageSize);
    }
}
