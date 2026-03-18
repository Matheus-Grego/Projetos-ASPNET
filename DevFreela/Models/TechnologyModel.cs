using DevFreela.Entities;
using DevFreela.Enums;

namespace DevFreela.Models;

public class TechnologyModel : BaseModel
{
   public TechnologyModel(string name, string description, TechCategory category) : base()
   {
      Name = name;
      Description = description;
      Category = category;
   }

   public string Name { get; set; }
   public string Description { get; set; }
   public TechCategory Category { get; set; }
   
   public void UpdateTechCategory(Guid TechnologyId, TechCategory category)
   {
      Category = category;
   }
   
   public static TechnologyModel FromEntity(TechEntity tech) 
      => new TechnologyModel(tech.Name, tech.Description, tech.Category);
   
   public static TechEntity ToEntity(TechnologyModel tech)
      => new TechEntity(tech.Name, tech.Description, tech.Category);
}