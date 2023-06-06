using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Contracts;

public record GetContractsRequest() : IRequest<List<Contract>>;

public class GetRequestHandler : IRequestHandler<GetContractsRequest, List<Contract>>
{
    private readonly CardProductContext _context;

    public GetRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<List<Contract>> Handle(GetContractsRequest request, CancellationToken cancellationToken)
    {
        var contracts = _context.Contract
            .AsNoTracking()
            .ToList();
        return Task.FromResult(contracts);
    }
    
}