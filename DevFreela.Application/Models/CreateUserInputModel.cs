using System.ComponentModel.DataAnnotations;
using DevFreela.Domain.Entities;
using Microsoft.Extensions.Options;

namespace DevFreela.Application.Models;

public class CreateUserInputModel : BaseModel
{
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public UserEntity ToEntity()
        => new UserEntity (UserName, FullName, Email, BirthDate);
}