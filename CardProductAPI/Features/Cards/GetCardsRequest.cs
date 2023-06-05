using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Cards;

public class GetCardsRequest: IRequest<List<Card>>
{
}

public class GetRequestHandler : IRequestHandler<GetCardsRequest, List<Card>>
{
    private readonly CardProductContext _context;

    public GetRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<List<Card>> Handle(GetCardsRequest request, CancellationToken cancellationToken)
    {
        var cards = _context.Cards
            .Include(card => card.Contract)
            .AsNoTracking()
            .ToList();
        return Task.FromResult(cards);
    }
    
}