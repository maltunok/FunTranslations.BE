using FunTranslations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FunTranslations.Infrastructure.Data;

public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity

{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken) => await _dbContext.Set<T>().ToListAsync();

    public async Task<IReadOnlyList<T>> ListAllAsync(int perPage, int page, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().Skip(perPage * (page - 1)).Take(perPage).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListByExpressionAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) =>
        await _dbContext.Set<T>().Where(filter).ToListAsync(cancellationToken);

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
