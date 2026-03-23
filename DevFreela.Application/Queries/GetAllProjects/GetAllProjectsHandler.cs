using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectModel>>>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetAllProjectsHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel<List<ProjectModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .Where(p => !p.IsDeleted && (request.Search == "" || p.Title.ToLower().Contains(request.Search.ToLower()) ||
                                         p.Description.ToLower().Contains(request.Search.ToLower())))
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var model = projects.Select(ProjectModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectModel>>.Success(model);
    }
}
