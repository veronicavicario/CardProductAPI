using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Contracts;

public record GetContractByIdRequest(long ContractId) : IRequest<Contract>;

public class GetContractsRequestHandler : IRequestHandler<GetContractByIdRequest, Contract>
{
    private readonly CardProductContext _context;

    public GetContractsRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<Contract> Handle(GetContractByIdRequest request, CancellationToken cancellationToken)
    {
        var contract = await _context.Contract
            .Where(c => c.Id == request.ContractId)
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new CardProductException("Contract does not exists.");
        return contract;
    }
}