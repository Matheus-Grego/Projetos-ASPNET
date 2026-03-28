using DevFreela.Application.Commands.Users.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class InsertUserValidator : AbstractValidator<InsertUserCommand>
{
    public InsertUserValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Invalid email address");
        
        RuleFor(u => u.BirthDate)
            .LessThanOrEqualTo(DateTime.Today.AddYears(-18))
                .WithMessage("You need to be at least 18 years old");
    }
}