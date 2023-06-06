using CardProductAPI.Commons.Pagination;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IValidator<ContractDto> _validator;
    private readonly IContractService _service;

    public ContractsController(IValidator<ContractDto> validator, IContractService service)
    {
        _validator = validator;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] ContractDto contractDto)
    {
        await _validator.ValidateAsync(contractDto, options => options.ThrowOnFailures());
        var contract = _service.CreateContract(contractDto, HttpContext.RequestAborted);
        return Ok(contract);
    }

    /**
     * Get all contracts
     */
    [HttpGet]
    public async Task<IActionResult> GetContracts([FromQuery] PaginationFilter filter)
    {
        var response = await _service.GetAllContracts(filter, HttpContext.RequestAborted);
        return Ok(response);
    }

    /**
     * Get all contracts
     */
    [HttpGet("{contractId:long}")]
    public async Task<IActionResult> GetContractById(long contractId)
    {
        var response = await _service.GetContractById(contractId, HttpContext.RequestAborted);
        return Ok(response);
    }

    /**
     * Delete contract by id
     */
    [HttpDelete("{contractId:long}")]
    public async Task<IActionResult> DeleteContract(long contractId)
    {
        await _service.DeleteContract(contractId, HttpContext.RequestAborted);
        return NoContent();
    }
}