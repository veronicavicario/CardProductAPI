using System.ComponentModel.DataAnnotations;
using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Models;

public class Contract
{
    public long Id { get; set; }
    
    [Required]
    public ContractType Type { get; set; }
    
    [Required]
    [MaxLength(16)]
    public string Code { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string ContractNumber { get; set; }
    
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public ContractState State { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string Country { get; set; }
    
    public string Account { get; set; }
    
}