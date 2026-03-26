using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;

    public InsertUserHandler(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }
    
    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var hash = _authService.ComputeHash(request.Password);
        var entity = request.ToEntity(hash);
        _repository.Add(entity);
        
        return ResultViewModel.Success();
    }
}