using System.Linq.Expressions;
using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Commons.Pagination;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Services;

public interface IContractService
{
     Task<PagedResponse<List<Contract>>> GetAllContracts(PaginationFilter paginationFilter, CancellationToken cancellationToken);
     Contract CreateContract(ContractDto contractDto, CancellationToken cancellationToken);
     Task<Contract> GetContractById(long contractId, CancellationToken cancellationToken);
     Task DeleteContract(long contractId, CancellationToken cancellationToken);

}

public class ContractService : IContractService
{
     private readonly IRepository<Contract> _repository;

     public ContractService(IRepository<Contract> repository)
     {
          _repository = repository;
     }
     
     public async Task<PagedResponse<List<Contract>>> GetAllContracts(PaginationFilter paginationFilter, CancellationToken cancellationToken)
     {
         return await _repository.GetAllPaginated(paginationFilter, cancellationToken, "");
     }

     public Contract CreateContract(ContractDto contractDto, CancellationToken cancellationToken)
     {
          var contract = CreateContract(contractDto); 
          _repository.AddData(contract, cancellationToken);
          return contract;  
     }

     public async Task<Contract> GetContractById(long contractId, CancellationToken cancellationToken)
     {
          Expression<Func<Contract, bool>>[]? expression = new Expression<Func<Contract, bool>>[1];
          expression[0] = x => x.Id == contractId;


          var contract = await _repository.GetWithFilter(expression).SingleOrDefaultAsync(cancellationToken) 
                                    ?? throw new NotFoundException("Contract does not exists.");
          return contract;

     }

     public async Task DeleteContract(long contractId, CancellationToken cancellationToken)
     {
          await _repository.Delete(contractId, cancellationToken);
     }

     private static Contract CreateContract(ContractDto contractDto)
     {
          return new Contract
          {
               ContractNumber = contractDto.ContractNumber,
               Account = contractDto.Account,
               UserId = contractDto.UserId,
               Code = contractDto.Code,
               Country = contractDto.Country,
               State = contractDto.State,
               Type = contractDto.Type,
               CreatedAt = new DateTime()
          };
     }
}