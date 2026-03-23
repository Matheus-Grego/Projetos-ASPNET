using DevFreela.Domain.Enums;

namespace DevFreela.Domain.Entities;

public class ProjectEntity : BaseEntity
{
    protected ProjectEntity() {}
    public ProjectEntity(string title, string description, Guid developerId, Guid clientId, decimal totalCost) : base()
    {
        Title = title;
        DeveloperId = developerId;
        Description = description;
        ClientID = clientId;
        TotalCost = totalCost;
        Status = ProjectStatus.Created;
        StatusId = 0;
        Technologies = [];
        Comments = [];
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public Guid DeveloperId { get; set; }
    public Guid ClientID { get; set; }
    public UserEntity Client { get; set; }
    public UserEntity Developer { get; set; }
    public decimal TotalCost { get; set; }
    public int StatusId { get; set; }
    public ProjectStatus Status { get; set; }
    public List<TechEntity> Technologies { get; set; }
    public List<CommentsEntity> Comments { get; set; }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
    
    public void Start()
    {
        if (Status != ProjectStatus.Created)
        {
            throw new InvalidOperationException();
        }

        Status = ProjectStatus.OnGoing;
    }


    public void Complete()
    {
        if (Status != ProjectStatus.OnGoing)
        {
            throw new InvalidOperationException();
        }

        Status = ProjectStatus.Completed;
    }
}