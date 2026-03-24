using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    private readonly IProjectRepository _repository;
    public InsertCommentHandler(DevFreelaDbContext context, IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.Exists(request.ProjectId);
        if (!project)
            return ResultViewModel.Failed("Project not found");

        var comments = new CommentsEntity(request.ProjectId, request.UserId, request.Message);
        _repository.Addcomment(comments);
        return ResultViewModel.Success();
    }
}