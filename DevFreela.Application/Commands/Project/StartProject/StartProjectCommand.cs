using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.StartProject;

public class StartProjectCommand : IRequest<ResultViewModel>
{
    public StartProjectCommand(Guid projectId, Guid userId)
    {
        ProjectId = projectId;
        UserId = userId;
    }
    public Guid ProjectId { get; init; }
    public Guid UserId { get; init; }
}