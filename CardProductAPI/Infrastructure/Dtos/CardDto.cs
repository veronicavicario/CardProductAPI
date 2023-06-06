using System.ComponentModel.DataAnnotations;
using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Infrastructure.Dtos;

public class CardDto
{
    public CardType Type { get; set; }

    public CardState State { get; set; }

    public string Token { get; set; }

    public long UserId { get; set; }

    public string Code { get; set; }

    public long ContractId { get; set; }
    
    public string Country { get; set; }
}