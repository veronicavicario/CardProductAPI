using CardProductAPI.Features.Contracts;
using CardProductAPI.Infrastructure.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<ContractDto> _validator;

    public ContractsController(IMediator mediator, IValidator<ContractDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }
 
    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] PostContractRequest contractRequest)
    {
        await _validator.ValidateAsync(contractRequest.ContractDto, options => options.ThrowOnFailures());
        var contract = await _mediator.Send(contractRequest, HttpContext.RequestAborted);
        return Ok(contract);
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
        var response = await _mediator.Send(new GetContractByIdRequest(contractId), HttpContext.RequestAborted);
        return Ok(response);
    }
    
    /**
     * Delete contract by id
     */
    [HttpDelete("{contractId:long}")]
    public async Task<IActionResult> DeleteContract(long contractId)
    {
        await _mediator.Send(new DeleteContractRequest(contractId), HttpContext.RequestAborted);
        return NoContent();
    }
}