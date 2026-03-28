using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectModel>>
{
    private readonly IProjectRepository _repository;

    public GetProjectByIdHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResultViewModel<ProjectModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetDetailsById(request.Id);

        if (project == null)
            return ResultViewModel<ProjectModel>.Failed("Not found");

        var model = ProjectModel.FromEntity(project);

        return ResultViewModel<ProjectModel>.Success(model);
    }
}