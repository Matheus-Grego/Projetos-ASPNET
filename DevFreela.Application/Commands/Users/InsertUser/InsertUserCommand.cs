using DevFreela.Application.Models;
using DevFreela.Domain.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Users.InsertUser;

public class InsertUserCommand : IRequest<ResultViewModel>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    
    public UserEntity ToEntity(string hash)
        => new (Username, Name, Email, BirthDate, hash, Role);
    
}