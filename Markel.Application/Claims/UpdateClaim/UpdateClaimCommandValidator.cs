using FluentValidation;

namespace Markel.Application.Claims.UpdateClaim;

public class UpdateClaimCommandValidator : AbstractValidator<UpdateClaimCommand>
{
    public UpdateClaimCommandValidator()
    {
        RuleFor(x => x.UCR).NotEmpty();
        
        RuleFor(x => x.UCR).MaximumLength(20);
        
        RuleFor(x => x.AssuredName).NotEmpty();
        
        RuleFor(x => x.AssuredName).MaximumLength(100);
        
        RuleFor(x => x.ClaimTypeId).NotEqual(default(int))
            .WithMessage("Claim Type is required");
        
        RuleFor(x => x.CompanyId).NotEqual(default(int))
            .WithMessage("Company is required");

        RuleFor(x => x.LossDate).LessThanOrEqualTo(x => x.ClaimDate);
        
        RuleFor(x => x.ClaimDate).LessThanOrEqualTo(x => DateTime.Now);
    }
}