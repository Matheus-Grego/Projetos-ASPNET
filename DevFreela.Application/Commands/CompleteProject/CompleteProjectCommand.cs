using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectCommand : IRequest<ResultViewModel>
{
    public CompleteProjectCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
}