using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<Guid>>
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly IMediator _mediator;

    public InsertProjectHandler(DevFreelaDbContext context, IMediator mediator)
    {
        _dbContext = context;
        _mediator = mediator;
    }
    
    public async Task<ResultViewModel<Guid>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();
        
        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreated);

        return ResultViewModel<Guid>.Success(project.Id);
    }
}