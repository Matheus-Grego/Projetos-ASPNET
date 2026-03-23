using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectModel>>
{
    public GetProjectByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}