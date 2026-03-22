using System.Reflection.Metadata;

namespace DevFreela.Domain.Entities;

public class CommentsEntity : BaseEntity
{
    public CommentsEntity(Guid projectId, Guid userId, string content) : base()
    {
        ProjectId = projectId;
        UserId = userId;
        Content = content;
        TotalLikes = 0;
    }

    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public ProjectEntity Project { get; set; }
    public UserEntity User { get; set; }
    public string Content{ get; set; }
    public List<byte[]> Images { get; set; }
    public List<CommentsEntity> Replies { get; set; }
    public int TotalLikes { get; set; }
    
}