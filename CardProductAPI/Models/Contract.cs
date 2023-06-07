using CardProductAPI.Commons.Enums;

namespace CardProductAPI.Models;

public class Contract
{
    public long Id { get; set; }
    public ContractType Type { get; set; }
    public string Code { get; set; }
    public string ContractNumber { get; set; }
    public long UserId { get; set; }
    public ContractState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Country { get; set; }
    public string Account { get; set; }
}