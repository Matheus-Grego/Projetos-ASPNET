using DevFreela.Domain.Enums;

namespace DevFreela.Domain.Entities;

public class TechEntity : BaseEntity
{
    public TechEntity(string name, string description, TechCategory category) : base()
    {
        Name = name;
        Description = description;
        Category = category;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public TechCategory Category { get; set; }
    public List<UserTechEntity> UserTech { get; set; }

    public void Update(TechEntity tech)
    {
        Name = tech.Name;
        Description = tech.Description;
        Category = tech.Category;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}