using CardProductAPI.Commons.Pagination;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IValidator<CardDto> _validator;

    public CardsController(ICardService cardService, IValidator<CardDto> validator) {
        _cardService = cardService;
        _validator = validator;
    }
    
    /**
     * Get all the cards related to the specified contract id
     */
    [HttpGet("contract/{contractId:long}")]
    public async Task<IActionResult> GetCardsContractId(long contractId)
    {
        var cardsPaginated =
            await _cardService.GetAllCardsByContractId(contractId, HttpContext.RequestAborted);
        return Ok(cardsPaginated);
    }

    /**
     * Get all cards
     */
    [HttpGet]
    public async Task<IActionResult> GetCards([FromQuery] PaginationFilter filter)
    {
        var cardsPaginated = await _cardService.GetAllCards(filter, HttpContext.RequestAborted);
        return Ok(cardsPaginated);
    }

    /**
     * Create card associated to the specific contract
     */
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
    {
        await _validator.ValidateAsync(cardDto, options => options.ThrowOnFailures());
        var card = _cardService.CreateCard(cardDto, HttpContext.RequestAborted);
        return Ok(card);
    }

    /**
     * Delete card by id
     */
    [HttpDelete("{cardId:long}")]
    public async Task<IActionResult> DeleteCard(long cardId)
    {
        await _cardService.DeleteCard(cardId, HttpContext.RequestAborted);
        return NoContent();
    }
}