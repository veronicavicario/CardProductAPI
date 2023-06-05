using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Models.Data;
using MediatR;

namespace CardProductAPI.Features.Contracts;

public class DeleteContractRequest : IRequest<long>
{
    public long ContractId { get; set; }
}

public class DeleteRequestHandler : IRequestHandler<DeleteContractRequest, long>
{
    private readonly CardProductContext _context;

    public DeleteRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public Task<long> Handle(DeleteContractRequest request, CancellationToken cancellationToken)
    {
        var contractToDelete = _context.Contract.Find(request.ContractId);
        if (contractToDelete is null)
            throw new CardProductException(string.Format("Invalid Contract ID = {0}", request.ContractId));

        _context.Contract.Remove(_context.Contract.Single(c => c.Id == request.ContractId));
        _context.SaveChangesAsync();

        return Task.FromResult(request.ContractId);
    }
}