using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.UpdateProject;

public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public Guid IdProject { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal TotalCost { get; init; }
}