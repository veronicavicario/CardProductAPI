using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Cards;

public class GetCardsByUserIdRequest : IRequest<List<Card>>
{
    public long UserId { get; set; }
}

public class GetCardsRequestHandler : IRequestHandler<GetCardsByUserIdRequest, List<Card>>
{
    private readonly CardProductContext _context;

    public GetCardsRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<List<Card>> Handle(GetCardsByUserIdRequest byUserIdRequest, CancellationToken cancellationToken)
    {
        var cards = _context.Cards
            .Where(c => c.UserId == byUserIdRequest.UserId)
            .Include(card => card.Contract)
            .AsNoTracking()
            .ToList();
        return Task.FromResult(cards);
    }
}