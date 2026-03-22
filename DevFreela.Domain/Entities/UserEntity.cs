namespace DevFreela.Domain.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(string username, string fullName, string email, DateTime birthDate) : base()
    {
        Username = username;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        isActive = true;
        ProjectsAsDeveloper = [];
        ProjectsAsClient = [];
        Technologies = [];
        Rating = decimal.Zero;
    }

    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int TelephoneNumber { get; set; }
    public decimal Rating { get; set; }
    public bool isActive { get; set; }
    public DateTime BirthDate { get; set; }
    public List<ProjectEntity> ProjectsAsDeveloper { get; set; }
    public List<ProjectEntity> ProjectsAsClient { get; set; }
    public List<UserTechEntity> Technologies { get; set; }
    
}