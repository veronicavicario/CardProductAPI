using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;

namespace CardProductAPI.Features.Contracts;

public record PostContractRequest(ContractDto ContractDto) : IRequest<Contract>;

public class PostContractRequestHandler : IRequestHandler<PostContractRequest, Contract>
{
    private readonly CardProductContext _context;

    public PostContractRequestHandler(CardProductContext context)
    {
        _context = context;
    }


    public Task<Contract> Handle(PostContractRequest request, CancellationToken cancellationToken)
    {
        var contract = CreateContract(request.ContractDto);
        _context.Contract.Add(contract);
        _context.SaveChanges();
        return Task.FromResult(contract);
    }

    private static Contract CreateContract(ContractDto contractDto)
    {
       var contract = new Contract();
       contract.ContractNumber = contractDto.ContractNumber;
       contract.Account = contractDto.Account;
       contract.UserId = contractDto.UserId;
       contract.Code = contractDto.Code;
       contract.Country = contractDto.Country;
       contract.State = contractDto.State;
       contract.Type = contractDto.Type;
       contract.CreatedAt = new DateTime();
       return contract;
    }
}