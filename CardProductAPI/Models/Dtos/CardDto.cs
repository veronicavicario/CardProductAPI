using System.ComponentModel.DataAnnotations;
using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Models.Dtos;

public class CardDto
{
    public long Id { get; set; }
    
    [Required]
    public CardType Type { get; set; }
    
    [Required]
    public CardState State  { get; set; }
    
    public string Token  { get; set; }
    
    [Required]
    public long UserId  { get; set; }
    
    [Required]
    public string Code { get; set; }
    
    public long ContractId { get; set; }
    
    public DateOnly CreatedAt { get; set; } 
    
    [Required]
    public string Country { get; set; }
}