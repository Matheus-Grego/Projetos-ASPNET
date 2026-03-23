using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _dbContext;

    public CompleteProjectHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

        if (project == null)
        {
            return ResultViewModel.Failed("Project not found");
        }

        project.Complete();
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}