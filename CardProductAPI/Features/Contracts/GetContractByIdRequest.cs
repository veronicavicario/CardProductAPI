using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Contracts;

public class GetContractByIdRequest : IRequest<Contract>
{
    public long ContractId { get; set; }
}

public class GetContractsRequestHandler : IRequestHandler<GetContractByIdRequest, Contract>
{
    private readonly CardProductContext _context;

    public GetContractsRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<Contract> Handle(GetContractByIdRequest request, CancellationToken cancellationToken)
    {
        var contract = _context.Contract
            .Where(c => c.Id == request.ContractId)
            .AsNoTracking()
            .SingleOrDefault();

        if (contract is null) throw new CardProductException("Contract does not exists.");
       
        return Task.FromResult(contract);
    }
}