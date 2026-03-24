using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

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
            return ResultViewModel.Failed("Not found");

        project.SetAsDeleted();
        await _repository.Update(project);

        return ResultViewModel.Success();
    }
}