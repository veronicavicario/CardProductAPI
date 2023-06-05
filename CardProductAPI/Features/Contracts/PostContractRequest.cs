using CardProductAPI.Models;
using CardProductAPI.Models.Data;
using MediatR;

namespace CardProductAPI.Features.Contracts;

public class PostContractRequest : IRequest<Contract>
{
    public Contract Contract { get; set; }
}

public class PostContractRequestHandler : IRequestHandler<PostContractRequest, Contract>
{
    private readonly CardProductContext _context;

    public PostContractRequestHandler(CardProductContext context)
    {
        _context = context;
    }


    public Task<Contract> Handle(PostContractRequest request, CancellationToken cancellationToken)
    {
        _context.Contract.Add(request.Contract);
        _context.SaveChanges();
        return Task.FromResult(request.Contract);
    }
}