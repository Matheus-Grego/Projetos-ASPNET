using DevFreela.Domain.Entities;

namespace DevFreela.Application.Models;

public class CreateProjectInputModel
{
    public string title { get; set; }
    public string description { get; set; }
    public Guid idClient { get; set; }
    public Guid idDeveloper { get; set; }
    public decimal totalCost { get; set; }

    public ProjectEntity toEntity() => new ProjectEntity(title, description,idDeveloper, idClient, totalCost);
}