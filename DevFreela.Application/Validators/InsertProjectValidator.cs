using DevFreela.Application.Commands.Project.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
{
    public InsertProjectValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
                .WithMessage("Title is required")
            .MaximumLength(50)
            .WithMessage("Title cannot exceed 50 characters");
        
        RuleFor(p => p.TotalCost)
            .GreaterThanOrEqualTo(0)
                .WithMessage("Total cost cannot be negative");
    }
}