using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<ResultViewModel<List<UserModel>>>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}