using System.ComponentModel.DataAnnotations;
using DevFreela.Entities;
using Microsoft.Extensions.Options;

namespace DevFreela.Models;

public class CreateUserInputModel : BaseModel
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public UserEntity ToEntity()
        => new UserEntity (UserName, FullName, Email, BirthDate);
}