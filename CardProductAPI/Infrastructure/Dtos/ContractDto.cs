using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Infrastructure.Dtos;

public class ContractDto
{
    public ContractType Type { get; set; }
    
    public string Code { get; set; }
    
    public string ContractNumber { get; set; }
    
    public long UserId { get; set; }
    
    public ContractState State { get; set; }
    
    public string Country { get; set; }
    
    public string Account { get; set; }
}