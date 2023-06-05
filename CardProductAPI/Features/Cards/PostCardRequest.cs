using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using CardProductAPI.Models.Dtos;
using MediatR;

namespace CardProductAPI.Features.Cards;

public class PostCardRequest : IRequest<Card>
{
    public CardDto CardDto { get; set; }
}

public class PostCardRequestHandler : IRequestHandler<PostCardRequest, Card>
{
    private readonly CardProductContext _context;

    public PostCardRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<Card> Handle(PostCardRequest request, CancellationToken cancellationToken)
    {
        var contract = _context.Contract.FirstOrDefault(c => c.Id == request.CardDto.ContractId);

        if (contract is null)
            throw new CardProductException("Contract does not exist and it's required to create a card");

        var cardDto = request.CardDto;

        var card = CreateCard(cardDto, contract);

        _context.Cards.Add(card);
        _context.SaveChanges();

        return Task.FromResult(card);
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
        card.CreatedAt = cardDto.CreatedAt;
        card.Country = cardDto.Country;
        return card;
    }
}