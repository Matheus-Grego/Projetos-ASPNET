using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Queries.Users.GetUserBySkill;

public class GetUserBySkillHandler : IRequestHandler<GetUsersBySkillQuery, ResultViewModel<List<UserModel>>>
{
    private readonly IUserRepository _repository;

    public GetUserBySkillHandler(DevFreelaDbContext context, IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<List<UserModel>>> Handle(GetUsersBySkillQuery request, CancellationToken cancellationToken)
    {
        var usersTech = await _repository.GetuserBySkillId(request.SkillId);

        if (usersTech == null)
            return ResultViewModel<List<UserModel>>.Failed("No User found with that skill");
        
        var users = usersTech.Select(x => UserModel.FromEntity(x.User)).ToList();
        return ResultViewModel<List<UserModel>>.Success(users);
    }
}