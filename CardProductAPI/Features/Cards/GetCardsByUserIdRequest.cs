using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Cards;

public record GetCardsByUserIdRequest(long UserId) : IRequest<List<Card>>;
    
public class GetCardsRequestHandler : IRequestHandler<GetCardsByUserIdRequest, List<Card>>
{
    private readonly CardProductContext _context;

    public GetCardsRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> Handle(GetCardsByUserIdRequest byUserIdRequest, CancellationToken cancellationToken)
    {
        var cards = await _context.Cards
            .Where(c => c.UserId == byUserIdRequest.UserId)
            .Include(card => card.Contract)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return cards;
    }
}