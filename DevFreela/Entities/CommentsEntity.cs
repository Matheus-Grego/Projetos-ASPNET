using System.Reflection.Metadata;

namespace DevFreela.Entities;

public class CommentsEntity : BaseEntity
{
    public CommentsEntity(Guid projectId, Guid userId, string title) : base()
    {
        ProjectId = projectId;
        UserId = userId;
        Title = title;
        TotalLikes = 0;
    }

    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    
    public ProjectEntity Project { get; set; }
    public UserEntity User { get; set; }
    public string Title{ get; set; }
    public string Body { get; set; }
    public List<byte[]> Images { get; set; }
    public List<CommentsEntity> Replies { get; set; }
    public int TotalLikes { get; set; }
    
}