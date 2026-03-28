using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Project.InsertComment;

public class InsertCommentCommand : IRequest<ResultViewModel>
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
}