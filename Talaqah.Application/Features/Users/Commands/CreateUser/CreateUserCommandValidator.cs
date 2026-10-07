using FluentValidation;


namespace Talaqah.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();
    }
}