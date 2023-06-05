using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Models.Data;
using MediatR;

namespace CardProductAPI.Features.Cards;

public class DeleteCardRequest : IRequest<long>
{
    public long CardId { get; set; }
}

public class DeleteRequestHandler : IRequestHandler<DeleteCardRequest, long>
{
    private readonly CardProductContext _context;

    public DeleteRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<long> Handle(DeleteCardRequest request, CancellationToken cancellationToken)
    {
        var cardToDelete = _context.Cards.Find(request.CardId);
        if (cardToDelete is null)
            throw new CardProductException(string.Format("Invalid Card ID = {0}", request.CardId));

        _context.Cards.Remove(_context.Cards.Single(c => c.Id == request.CardId));
        _context.SaveChangesAsync();

        return Task.FromResult(request.CardId);
    }
}