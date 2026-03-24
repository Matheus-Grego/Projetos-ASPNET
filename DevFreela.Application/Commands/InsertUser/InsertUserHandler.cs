using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Persistance;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public InsertUserHandler(DevFreelaDbContext context, IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();
        _repository.Add(entity);
        
        return ResultViewModel.Success();
    }
}