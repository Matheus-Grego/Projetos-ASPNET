using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetCommentsByProject;

public class GetCommentsByProjectQuery : IRequest<ResultViewModel<List<CreateCommentInputModel>>>
{
    public GetCommentsByProjectQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
    public Guid ProjectId { get; set; }
}