using System.Linq.Expressions;
using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Commons.Pagination;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Services;

public interface ICardService
{
     Task<PagedResponse<List<Card>>> GetAllCards(PaginationFilter paginationFilter, CancellationToken cancellationToken);
     Task<List<Card>> GetAllCardsByContractId(long contractId, CancellationToken cancellationToken);
     Card CreateCard(CardDto cardDto, CancellationToken cancellationToken);
     Task<Card> GetCardById(long cardId, CancellationToken cancellationToken);
     Task DeleteCard(long cardId, CancellationToken cancellationToken);

}

public class CardService : ICardService
{
     private readonly IRepository<Card> _repository;
     private readonly IContractService _contractService;

     public CardService(IRepository<Card> repository, IContractService contractService)
     {
          _repository = repository;
          _contractService = contractService;
     }
     
     public async Task<PagedResponse<List<Card>>> GetAllCards(PaginationFilter paginationFilter, CancellationToken cancellationToken)
     {
         return await _repository.GetAllPaginated(paginationFilter, cancellationToken, "Contract");
     }

     public async Task<List<Card>> GetAllCardsByContractId(long contractId, CancellationToken cancellationToken)
     {
          var expression = new Expression<Func<Card, bool>>[1];
          expression[0] = x => x.Contract.Id == contractId;
        
          return await _repository.GetWithFilter(expression).Include(card => card.Contract).ToListAsync(cancellationToken);
     }
     
     public Card CreateCard(CardDto cardDto, CancellationToken cancellationToken)
     {
          var contract = _contractService.GetContractById(cardDto.ContractId, cancellationToken);
          var card = CreateAndGetCard(cardDto, contract.Result); 
          _repository.AddData(card, cancellationToken);
          return card;  
     }

     public async Task<Card> GetCardById(long cardId, CancellationToken cancellationToken)
     {
          var expression = new Expression<Func<Card, bool>>[1];
          expression[0] = x => x.Id == cardId;
          
          var card = await _repository.GetWithFilter(expression).SingleOrDefaultAsync(cancellationToken) 
                                    ?? throw new NotFoundException("Card does not exists.");
          return card;
     }

     public async Task DeleteCard(long cardId, CancellationToken cancellationToken)
     {
          await _repository.Delete(cardId, cancellationToken);
     }

     private static Card CreateAndGetCard(CardDto cardDto, Contract contract)
     {
         return new Card
          {
               Type = cardDto.Type,
               State = cardDto.State,
               Token = cardDto.Token,
               UserId = cardDto.UserId,
               Code = cardDto.Code,
               Contract = contract,
               CreatedAt = new DateOnly(),
               Country = cardDto.Country
          };
     }
}