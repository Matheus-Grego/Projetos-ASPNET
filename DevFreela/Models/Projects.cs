namespace DevFreela.Models;

public class Projects : BaseModel
{
    public Projects(string name, string description,Guid developerId, Guid clientId, List<Technologies> technologies, decimal stars)
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
    public List<Technologies> Technologies { get; set; }
    public decimal TotalCost { get; set; }
    public decimal Stars { get; set; }
    
}