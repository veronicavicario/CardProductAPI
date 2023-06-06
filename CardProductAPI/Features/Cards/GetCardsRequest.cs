using CardProductAPI.Commons.Pagination;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Cards;

public record GetCardsRequest(PaginationFilter PaginationFilter) : IRequest<PagedResponse<List<Card>>>;

public class GetRequestHandler : IRequestHandler<GetCardsRequest, PagedResponse<List<Card>>>
{
    private readonly CardProductContext _context;

    public GetRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<List<Card>>>Handle(GetCardsRequest request, CancellationToken cancellationToken)
    {
        var filter = request.PaginationFilter;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
       
        
        var pagedData = await _context.Cards
            .AsNoTracking()
            .Include(card => card.Contract)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync(cancellationToken);
        var totalRecords = await _context.Cards.CountAsync();
        var pagedResponse = new PagedResponse<List<Card>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
        var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        pagedResponse.TotalPages = roundedTotalPages;
        pagedResponse.TotalRecords = totalRecords;
        return await Task.FromResult(pagedResponse);
    }

}