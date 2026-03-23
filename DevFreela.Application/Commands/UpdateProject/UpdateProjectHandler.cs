using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _dbContext;

    public UpdateProjectHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == request.IdProject);

        if (project == null)
            return ResultViewModel.Failed("Not found");

        project.Update(request.Title, request.Description, request.TotalCost);

        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}