using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserModel>>>
{
    private readonly IUserRepository _repository;

    public GetAllUsersHandler(DevFreelaDbContext context, IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<List<UserModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAll();
        var model =  users.Select(UserModel.FromEntity).ToList();
        return ResultViewModel<List<UserModel>>.Success(model);
    }
}