using System.ComponentModel.DataAnnotations;

namespace CardProductAPI.Models;

public class CreditLine
{
    public long Id { get; set; }
    
    public string Code { get; set; }
    
    [Required]
    public Contract Contract { get; set; }

    [Required]
    public long UserId { get; set; }
    
    [Required]
    public string State { get; set; }
   
    public double TakenAmount { get; set; }
    
    public double CurrentAmount { get; set; }
}