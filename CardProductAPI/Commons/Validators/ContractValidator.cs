using CardProductAPI.Infrastructure.Dtos;
using FluentValidation;

namespace CardProductAPI.Commons.Validators;

public class ContractValidator: AbstractValidator<ContractDto>  
{
    public ContractValidator() 
    {
        RuleFor(c => c.Code).NotNull();
        RuleFor(c => c.Account).Length(4,8);
        RuleFor(c => c.UserId).NotNull().GreaterThan(0);
    }
}