using DevFreela.Enums;
using DevFreela.Models;

namespace DevFreela.Entities;

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
    public ProjectEntity Project { get; set; }
    public List<UserTechEntity> UserTech { get; set; }
    public List<UserEntity> Users { get; set; }

    public void Update(TechnologyModel technologyModel)
    {
        Name = technologyModel.Name;
        Description = technologyModel.Description;
        Category = technologyModel.Category;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}