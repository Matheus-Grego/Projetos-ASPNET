using DevFreela.Entities;

namespace DevFreela.Models;

public class UserModel
{
    public UserModel(string username, string email, DateTime birthday) : base()
    {
        Username = username;
        Email = email;
        Birthday = birthday;
    }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }

    public static UserModel FromEntity(UserEntity entity)
        => new UserModel(entity.Username, entity.Email, entity.BirthDate);
}