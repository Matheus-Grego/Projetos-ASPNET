namespace DevFreela.Domain.Entities;

public class UserTechEntity : BaseEntity
{
    public UserTechEntity(Guid userId, Guid techId) : base()
    {
        UserId = userId;
        TechId = techId;
    }

    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }
    public Guid TechId { get; private set; }
    public TechEntity Technology { get; private set; }
}