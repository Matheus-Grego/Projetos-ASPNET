using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Project.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _Repository;

    public UpdateProjectHandler(IProjectRepository repository)
    {
        _Repository = repository;
    }
    
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _Repository.GetById(request.IdProject);

        if (project == null)
            return ResultViewModel.Failed("Not found");

        project.Update(request.Title, request.Description, request.TotalCost);

        await _Repository.Update(project);

        return ResultViewModel.Success();
    }
}