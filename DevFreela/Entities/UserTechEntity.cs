namespace DevFreela.Entities;

public class UserTechEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid TechId { get; set; }
    public TechEntity Tech { get; set; }
    public UserEntity User { get; set; }
}