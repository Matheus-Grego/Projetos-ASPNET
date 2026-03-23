using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectModel>>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetProjectByIdHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel<ProjectModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .SingleOrDefaultAsync(p => p.Id == request.Id);

        if (project == null)
            return ResultViewModel<ProjectModel>.Failed("Not found");

        var model = ProjectModel.FromEntity(project);

        return ResultViewModel<ProjectModel>.Success(model);
    }
}