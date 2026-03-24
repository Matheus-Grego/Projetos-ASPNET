using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<Guid>>
{
    private readonly IMediator _mediator;
    private readonly IProjectRepository _repository;

    public InsertProjectHandler(IMediator mediator, IProjectRepository repository)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<ResultViewModel<Guid>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();
        await _repository.Add(project);
        
        var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
        await _mediator.Publish(projectCreated);

        return ResultViewModel<Guid>.Success(project.Id);
    }
}