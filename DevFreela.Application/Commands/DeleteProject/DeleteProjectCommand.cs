using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<ResultViewModel>
{
    public DeleteProjectCommand(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
}