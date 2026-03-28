using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.Project.StartProject;

public class StartViewModelHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;

    public StartViewModelHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.ProjectId);

        if (project == null)
            return ResultViewModel.Failed("Project not found");

        if (project.DeveloperId != request.UserId)
        {
            return ResultViewModel.Failed("You dont have the permition to start this project");
        }
        
        project.Start();

        return ResultViewModel.Success();

    }
}