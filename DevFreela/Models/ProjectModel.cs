using System.ComponentModel.DataAnnotations;
using DevFreela.Enums;

namespace DevFreela.Models;

public class ProjectModel : BaseModel 
{
    public ProjectModel(string name, string description,Guid developerId, Guid clientId, List<TechnologyModel> technologies, decimal stars)
    {
        Name = name;
        Description = description;
        Technologies = technologies;
        Stars = stars;
        ClientId = clientId;
        DeveloperId = developerId;
    }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DeveloperId { get; set; }
    public Guid ClientId { get; set; }
    public List<TechnologyModel> Technologies { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Stars { get; set; }
    public ProjectStatus Status { get; set; }

    public void UpdateTechnologies(List<TechnologyModel> technologies)
    {
        if (technologies == null || !technologies.Any())
            throw new ArgumentException("At least one technology is required");
        
        Technologies = technologies;
    }
    public void UpdateTotalCost(decimal totalCost)
    {
        TotalCost = totalCost;
    }
    
    
}