using CardProductAPI.Infrastructure.Dtos;
using FluentValidation;

namespace CardProductAPI.Commons.Validators;

public class CardValidator : AbstractValidator<CardDto>  
{
    public CardValidator() 
    {
        RuleFor(c => c.ContractId).NotNull();
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.Token).Length(8, 8);
        RuleFor(c => c.Code).NotNull();
    }
}