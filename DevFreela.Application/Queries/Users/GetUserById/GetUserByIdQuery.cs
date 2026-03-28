using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.Users.GetUserById;

public class GetUserByIdQuery : IRequest<ResultViewModel<UserModel>>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<UserTechInputModel> Tecnologies { get; set; }
    public List<ProjectModel> Projects { get; set; }
}