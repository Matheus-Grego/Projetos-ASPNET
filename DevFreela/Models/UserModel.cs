using DevFreela.Entities;

namespace DevFreela.Models;

public class UserModel
{
    public UserModel(string username, string email, DateTime birthday, List<string> technologies) : base()
    {
        Username = username;
        Email = email;
        Birthday = birthday;
        Technologies = technologies;
    }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public DateTime Birthday { get; private set; }
    public List<string> Technologies { get; set; }

    public static UserModel FromEntity(UserEntity entity)
    {
        var technologies = entity.Technologies.Select(u => u.Technology.Description).ToList();
        return new UserModel(entity.Username, entity.Email, entity.BirthDate, technologies);
    }
}