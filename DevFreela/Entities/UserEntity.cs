namespace DevFreela.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(string username, string fullname, string email, DateTime birthDate) : base()
    {
        Username = username;
        FullName = fullname;
        Email = email;
        BirthDate = birthDate;
        isActive = true;
        Projects = [];
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
    public List<ProjectEntity> Projects { get; set; }
    public List<UserTechEntity> Technologies { get; set; }
    
}