using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectModel>>>
{
    private readonly IProjectRepository _repository;

    public GetAllProjectsHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResultViewModel<List<ProjectModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
       
            var projects = await _repository.GetAll();

            var model = projects.Select(ProjectModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectModel>>.Success(model);
       
    }
}
