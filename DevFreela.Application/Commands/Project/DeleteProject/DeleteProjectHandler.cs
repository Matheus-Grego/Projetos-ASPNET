using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.Project.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.Id);

        if (project == null)
            return ResultViewModel.Failed("project not found");

        if (project.DeveloperId != request.UserId)
        {
            return ResultViewModel.Failed("you don't have permission to delete this project");
        }

        project.SetAsDeleted();
        await _repository.Update(project);

        return ResultViewModel.Success();
    }
}