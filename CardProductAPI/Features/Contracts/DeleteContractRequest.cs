using CardProductAPI.Models.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CardProductAPI.Features.Contracts;

public record DeleteContractRequest(long ContractId) : IRequest<Unit>;

public class DeleteRequestHandler : IRequestHandler<DeleteContractRequest, Unit>
{
    private readonly CardProductContext _context;

    public DeleteRequestHandler(CardProductContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteContractRequest request, CancellationToken cancellationToken)
    {
        await _context.Contract.Where(c => c.Id == request.ContractId).ExecuteDeleteAsync(cancellationToken);
        return Unit.Value;
    }
}