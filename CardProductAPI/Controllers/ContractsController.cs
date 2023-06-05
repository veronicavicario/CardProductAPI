using CardProductAPI.Features.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ContractsController(IMediator mediator) =>
        _mediator = mediator;
 
    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] PostContractRequest contractRequest)
    {
        await _mediator.Send(contractRequest, HttpContext.RequestAborted);
        return Ok();
    }
    
    /**
     * Get all contracts
     */
    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        var response = await _mediator.Send(new GetContractsRequest(), HttpContext.RequestAborted);
        return Ok(response);
    }
    
    /**
     * Get all contracts
     */
    [HttpGet("{contractId:long}")]
    public async Task<IActionResult> GetContractById(long contractId)
    {
        var response = await _mediator.Send(new GetContractByIdRequest{ContractId = contractId}, HttpContext.RequestAborted);
        return Ok(response);
    }
    
    /**
     * Delete contract by id
     */
    [HttpDelete("{contractId:long}")]
    public async Task<IActionResult> DeleteContract(long contractId)
    {
        await _mediator.Send(new DeleteContractRequest { ContractId = contractId }, HttpContext.RequestAborted);
        return Ok();
    }
}