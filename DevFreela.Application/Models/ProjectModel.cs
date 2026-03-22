using System.ComponentModel.DataAnnotations;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;

namespace DevFreela.Application.Models;

public class ProjectModel : BaseModel 
{
    public ProjectModel(string title, string description,Guid developerId, Guid clientId, List<TechEntity> technologies)
    {
        Title = title;
        Description = description;
        Technologies = technologies;
        ClientId = clientId;
        DeveloperId = developerId;
    }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid DeveloperId { get; set; }
    public Guid ClientId { get; set; }
    public List<TechEntity> Technologies { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Stars { get; set; }
    public ProjectStatus Status { get; set; }


    public static ProjectModel FromEntity(ProjectEntity project) 
        => new(project.Title,project.Description, project.DeveloperId, project.ClientID,project.Technologies);
    
    public void UpdateTechnologies(List<TechEntity> technologies)
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