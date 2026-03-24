using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserModel>>
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(DevFreelaDbContext context, IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResultViewModel<UserModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserDetailsById(request.Id);
        
        if (user == null)
            return ResultViewModel<UserModel>.Failed("User not found");
        
        var model = UserModel.FromEntity(user);
        return ResultViewModel<UserModel>.Success(model);
    }
}