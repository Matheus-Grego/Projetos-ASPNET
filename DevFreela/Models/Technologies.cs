using DevFreela.Enums;

namespace DevFreela.Models;

public class Technologies : BaseModel
{
   public string Name { get; set; }
   public string Description { get; set; }
   public TechCategory Category { get; set; }
}