using FluentValidation;

namespace AlasimVar.Application.Features.Commands.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .EmailAddress().WithMessage("bad_request")
            .NotEmpty().WithMessage("bad_request")
            .NotNull().WithMessage("bad_request");
    }
    
}