using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;

namespace CardProductAPI.Features.Cards;

public record PostCardRequest(CardDto CardDto) : IRequest<Card>;

public class PostCardRequestHandler : IRequestHandler<PostCardRequest, Card>
{
    private readonly CardProductContext _context;

    public PostCardRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<Card> Handle(PostCardRequest request, CancellationToken cancellationToken)
    {
        var contract = _context.Contract.FirstOrDefault(c => c.Id == request.CardDto.ContractId)
            ?? throw new CardProductException("Contract does not exist and it's required to create a card");
        
        var card = CreateCard(request.CardDto, contract);
        await _context.Cards.AddAsync(card, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return card;
    }

    private Card CreateCard(CardDto cardDto, Contract contract)
    {
        var card = new Card();
        card.Type = cardDto.Type;
        card.State = cardDto.State;
        card.Token = cardDto.Token;
        card.UserId = cardDto.UserId;
        card.Code = cardDto.Code;
        card.Contract = contract;
        card.CreatedAt = new DateOnly();
        card.Country = cardDto.Country;
        return card;
    }
}