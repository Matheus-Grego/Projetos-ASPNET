using DevFreela.Enums;

namespace DevFreela.Entities;

public class TechEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TechCategory Category { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
    
    public List<UserTechEntity> UserTech { get; set; }
    public List<UserEntity> Users { get; set; }
}