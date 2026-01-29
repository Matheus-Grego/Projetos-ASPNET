namespace DevFreela.Models;

public class CreateCommentInputModel : BaseModel
{
    public CreateCommentInputModel(string message, Guid userId, Guid projectId)
    {
        Message = message;
        UserId = userId;
        ProjectId = projectId;
        Likes = 0;
    }
    public string Message { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public int Likes { get; set; }
}