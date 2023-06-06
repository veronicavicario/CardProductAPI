using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Cards;

public record DeleteCardRequest(long CardId) : IRequest<Unit>;

public class DeleteRequestHandler : IRequestHandler<DeleteCardRequest, Unit>
{
    private readonly CardProductContext _context;

    public DeleteRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCardRequest request, CancellationToken cancellationToken)
    {
        await _context.Cards.Where(c => c.Id == request.CardId).ExecuteDeleteAsync(cancellationToken); 
        return Unit.Value;
    }
}