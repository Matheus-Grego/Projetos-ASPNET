namespace DevFreela.Models;

public class BaseModel
{
    public BaseModel()
    {
        Id = new Guid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}