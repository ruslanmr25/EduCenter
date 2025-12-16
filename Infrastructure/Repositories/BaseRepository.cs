using Application.Results;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected AppDbContext _context;

    public BaseRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    protected IQueryable<TEntity> BuildBaseQuery(
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = true
    )
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
        if (orderBy is not null)
        {
            query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        }
        else
        {
            query = query.OrderByDescending(e => e.CreatedAt);
        }

        query = query.Where(e => e.DeletedAt == null);

        return query;
    }

    protected async Task<PagedResult<TEntity>> GetPagedResult(
        IQueryable<TEntity> query,
        int page,
        int pageSize
    )
    {
        var totalCount = await query.CountAsync();

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<TEntity>(result, totalCount, page, pageSize);
    }

    public virtual async Task<PagedResult<TEntity>> GetAllAsync(
        int page,
        int pageSize = 50,
        Expression<Func<TEntity, object>>? orderBy = null,
        bool descending = true
    )
    {
        var query = BuildBaseQuery(orderBy, descending);

        return await GetPagedResult(query, page, pageSize);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var createdEntity = await _context.Set<TEntity>().AddAsync(entity);

        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }

    public virtual async Task<TEntity?> GetAsync(int id)
    {
        TEntity? entity = await _context.Set<TEntity>().FindAsync(id);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.DeletedAt = DateTime.UtcNow;

        _context.Update(entity);

        await _context.SaveChangesAsync();
    }
}
