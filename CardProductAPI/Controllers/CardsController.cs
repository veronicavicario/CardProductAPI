using CardProductAPI.Commons.Pagination;
using CardProductAPI.Commons.Validators;
using CardProductAPI.Features.Cards;
using CardProductAPI.Infrastructure.Dtos;
using CardProductAPI.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CardDto> _validator;

    public CardsController(IMediator mediator, IValidator<CardDto> validator) {
        _mediator = mediator;
        _validator = validator;
    }
    
    /**
     * Get all the cards related to the specified user id
     */
    [HttpGet("userId/{userId:long}")]
    public async Task<IActionResult> GetCardsByUserId(long userId)
    {
        var response =
            await _mediator.Send(new GetCardsByUserIdRequest(userId), HttpContext.RequestAborted);
        return Ok(response);
    }

    /**
     * Get all cards
     */
    [HttpGet]
    public async Task<IActionResult> GetCards([FromQuery] PaginationFilter filter)
    {
        var cardsPaginated = await _mediator.Send(new GetCardsRequest(filter), HttpContext.RequestAborted);
        return Ok(cardsPaginated);
    }

    /**
     * Create card associated to the specific contract
     */
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] PostCardRequest cardRequest)
    {
        await _validator.ValidateAsync(cardRequest.CardDto, options => options.ThrowOnFailures());
        var card = await _mediator.Send(cardRequest, HttpContext.RequestAborted);
        return Ok(card);
    }

    /**
     * Delete card by id
     */
    [HttpDelete("{cardId:long}")]
    public async Task<IActionResult> DeleteCard(long cardId)
    {
        await _mediator.Send(new DeleteCardRequest(cardId), HttpContext.RequestAborted);
        return NoContent();
    }
}