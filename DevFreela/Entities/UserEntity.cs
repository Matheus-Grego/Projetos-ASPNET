namespace DevFreela.Entities;

public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int TelephoneNumber { get; set; }
    public string Password { get; set; }
    public decimal Rating { get; set; }
    public bool isActive { get; set; }
    public DateTime BirthDate { get; set; }
    
}