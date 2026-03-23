using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetCommentsByProject;

public class GetCommentsByProjectHandler : IRequestHandler<GetCommentsByProjectQuery, ResultViewModel<List<CreateCommentInputModel>>>
{
    private readonly DevFreelaDbContext _dbContext;

    public GetCommentsByProjectHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ResultViewModel<List<CreateCommentInputModel>>> Handle(GetCommentsByProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects.Include(x => x.Comments).SingleOrDefaultAsync(x => x.Id == request.ProjectId);

        if (project == null)
            return ResultViewModel<List<CreateCommentInputModel>>.Failed("Project Not found");

        var comments = project.Comments
            .Where(c => c.IsDeleted == false)
            .Select(CreateCommentInputModel.FromEntity).ToList();

        return ResultViewModel<List<CreateCommentInputModel>>.Success(comments);
    }
}