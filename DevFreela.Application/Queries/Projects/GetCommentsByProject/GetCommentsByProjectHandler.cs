using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Queries.Projects.GetCommentsByProject;

public class GetCommentsByProjectHandler : IRequestHandler<GetCommentsByProjectQuery, ResultViewModel<List<CreateCommentInputModel>>>
{
    private readonly IProjectRepository _repository;

    public GetCommentsByProjectHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<List<CreateCommentInputModel>>> Handle(GetCommentsByProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetDetailsById(request.ProjectId);

        if (project == null)
            return ResultViewModel<List<CreateCommentInputModel>>.Failed("Project Not found");

        var comments = project.Comments
            .Where(c => c.IsDeleted == false)
            .Select(CreateCommentInputModel.FromEntity).ToList();

        return ResultViewModel<List<CreateCommentInputModel>>.Success(comments);
    }
}