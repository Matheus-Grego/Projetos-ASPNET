using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.DeleteProject;

public class DeleteProjectCommand : IRequest<ResultViewModel>
{
    public DeleteProjectCommand(Guid projectid, Guid userId)
    {
        Id = projectid;
        UserId = userId;
    }
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
}