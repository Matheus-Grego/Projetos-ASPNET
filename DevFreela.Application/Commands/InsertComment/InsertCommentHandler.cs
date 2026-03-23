using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    private readonly DevFreelaDbContext _dbContext;

    public InsertCommentHandler(DevFreelaDbContext context)
    {
        _dbContext = context;
    }
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == request.ProjectId);
        if (project == null)
            return ResultViewModel.Failed("Project not found");

        var comments = new CommentsEntity(request.ProjectId, request.UserId, request.Message);

        await _dbContext.Comments.AddAsync(comments);
        await _dbContext.SaveChangesAsync();
        return ResultViewModel.Success();
    }
}