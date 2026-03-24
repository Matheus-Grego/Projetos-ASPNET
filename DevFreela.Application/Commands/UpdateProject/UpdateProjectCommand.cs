using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public Guid IdProject { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal TotalCost { get; init; }
}