using CardProductAPI.Commons.Exceptions;
using CardProductAPI.Features.Cards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardsController(IMediator mediator) =>
        _mediator = mediator;

    /**
     * Get all the cards related to the specified user id
     */
    [HttpGet("userId/{userId:long}")]
    public async Task<IActionResult> GetCardsByUserId(long userId)
    {
        var response =
            await _mediator.Send(new GetCardsByUserIdRequest { UserId = userId }, HttpContext.RequestAborted);
        return Ok(response);
    }

    /**
     * Get all cards
     */
    [HttpGet]
    public async Task<IActionResult> GetCards()
    {
        var response = await _mediator.Send(new GetCardsRequest(), HttpContext.RequestAborted);
        return Ok(response);
    }

    /**
     * Create card associated to the specific contract
     */
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] PostCardRequest cardRequest)
    {
        var card = await _mediator.Send(cardRequest, HttpContext.RequestAborted);
        return Ok(Task.FromResult(card));
    }

    /**
     * Delete card by id
     */
    [HttpDelete("{cardId:long}")]
    public async Task<IActionResult> DeleteCard(long cardId)
    {
        await _mediator.Send(new DeleteCardRequest { CardId = cardId }, HttpContext.RequestAborted);
        return Ok();
    }
}