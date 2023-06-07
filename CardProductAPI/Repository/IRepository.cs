using System.Linq.Expressions;
using CardProductAPI.Commons.Pagination;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetWithFilter(Expression<Func<T, bool>>[]? predicates);
    void AddData(T data, CancellationToken cancellationToken);
    IQueryable<T> GetAll(string includeProperty);

    Task<PagedResponse<List<T>>> GetAllPaginated(PaginationFilter paginationFilter, CancellationToken cancellationToken,
        string includeProperty);

    Task<Unit> Delete(long id, CancellationToken cancellationToken);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CardProductContext _context;

    public Repository(CardProductContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetWithFilter(Expression<Func<T, bool>>[]? predicates)
    {
        var query = _context.Set<T>().AsQueryable();

        if (predicates == null) return query;

        return predicates.Aggregate(query, (current, predicate) => current.Where(predicate));
    }

    public async void AddData(T data, CancellationToken cancellationToken)
    {
        await _context.AddAsync(data, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<T> GetAll(string includeProperty = "")
    {
        return _context.Set<T>().Include(includeProperty);
    }

    public async Task<PagedResponse<List<T>>> GetAllPaginated(PaginationFilter paginationFilter,
        CancellationToken cancellationToken, string includeProperty = "")
    {
        var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
        var query = _context.Set<T>()
            .AsNoTracking()
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);
           
        if (includeProperty != "")
        {
            query.Include(includeProperty);
        }
        var pagedData = await query.ToListAsync(cancellationToken);
        var totalRecords = await _context.Set<T>().CountAsync(cancellationToken);
        var pagedResponse = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = (totalRecords / (double)validFilter.PageSize);
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        pagedResponse.TotalPages = roundedTotalPages;
        pagedResponse.TotalRecords = totalRecords;
        return pagedResponse;
    }

    public async Task<Unit> Delete(long id, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity is not null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);  
        }

        return Unit.Value;
    }

}