namespace DevFreela.Entities;

public class UserTechEntity : BaseEntity
{
    // public UserTechEntity(Guid userId, Guid techId) : base()
    // {
    //     UserId = userId;
    //     TechId = techId;
    // }
    //
    // public Guid UserId { get; set; }
    // public Guid ProjectId { get; set; }
    // public Guid TechId { get; set; }
    // public List<TechEntity> Techs { get; set; }
    // public UserEntity User { get; set; }
    
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