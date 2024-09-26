using FluentValidation;

namespace Markel.Application.Companies.AddCompany;

public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
{
    public AddCompanyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        
        RuleFor(x => x.Name).MaximumLength(200);
        
        RuleFor(x => x.Address1).MaximumLength(100);
        
        RuleFor(x => x.Address2).MaximumLength(100);
        
        RuleFor(x => x.Address3).MaximumLength(100);
        
        RuleFor(x => x.PostCode).MaximumLength(20);
        
        RuleFor(x => x.Country).MaximumLength(50);
    }
}