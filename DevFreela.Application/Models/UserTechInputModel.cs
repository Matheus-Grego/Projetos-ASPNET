using DevFreela.Domain.Entities;

namespace DevFreela.Application.Models;

public class UserTechInputModel
{
    public UserTechInputModel() { }

    public UserTechInputModel(Guid userId, Guid[] tech) : base()
    {
        UserId = userId;
        TechnologyID = tech;
    }
    public Guid UserId { get; set; }
    public Guid[] TechnologyID { get; set; }
}