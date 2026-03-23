using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _dbContext;

    public DeleteProjectHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == request.Id);

        if (project == null)
            return ResultViewModel.Failed("Not found");

        project.SetAsDeleted();
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}