using DevFreela.Domain.Entities;

namespace DevFreela.Application.Models;

public class CreateCommentInputModel : BaseModel
{
    public CreateCommentInputModel(string content, Guid userId, Guid projectId)
    {
        Content = content;
        UserId = userId;
        ProjectId = projectId;
        Likes = 0;
    }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public int Likes { get; set; }

    public static CreateCommentInputModel FromEntity(CommentsEntity comment) 
        => new CreateCommentInputModel(comment.Content, comment.UserId, comment.ProjectId);
}
