using System.ComponentModel.DataAnnotations;
using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Models;

public class Card
{
    public long Id { get; set; }
    public CardType Type { get; set; }
    public CardState State { get; set; }
    public string Token { get; set; }
    public long UserId { get; set; }
    public string Code { get; set; }
    public Contract Contract { get; set; }
    public DateOnly CreatedAt { get; set; }
    public string Country { get; set; }
}