using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;

    public CompleteProjectHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetById(request.Id);

        if (project == null)
        {
            return ResultViewModel.Failed("Project not found");
        }

        project.Complete();
        await _repository.Update(project);

        return ResultViewModel.Success();
    }
}