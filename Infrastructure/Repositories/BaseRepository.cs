using System;
using Application.Results;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity>
    where TEntity : class
{
    protected AppDbContext _context;

    public BaseRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public virtual async Task<PagedResult<TEntity>> GetAllAsync(int page, int pageSize = 50)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        var totalCount = await query.CountAsync();

        var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<TEntity>(result, totalCount, page, pageSize);
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
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
