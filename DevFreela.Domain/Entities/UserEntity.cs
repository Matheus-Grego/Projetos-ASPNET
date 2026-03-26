namespace DevFreela.Domain.Entities;

public class UserEntity : BaseEntity
{
    public UserEntity(string username, string fullName, string email, DateTime birthDate, string password, string role) : base()
    {
        Username = username;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        isActive = true;
        Password = password;
        Role = role;
        ProjectsAsDeveloper = [];
        ProjectsAsClient = [];
        Technologies = [];
        Rating = decimal.Zero;
    }

    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int TelephoneNumber { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public decimal Rating { get; set; }
    public bool isActive { get; set; }
    public DateTime BirthDate { get; set; }
    public List<ProjectEntity> ProjectsAsDeveloper { get; set; }
    public List<ProjectEntity> ProjectsAsClient { get; set; }
    public List<UserTechEntity> Technologies { get; set; }
    
    public void UpdatePassword(string newPassword)
    {
        Password = newPassword;
    }
    
}