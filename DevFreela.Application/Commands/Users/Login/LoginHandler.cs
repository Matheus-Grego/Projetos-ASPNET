using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.Users.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginViewModel>>
{
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;
    
    public LoginHandler(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }
    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hash = _authService.ComputeHash(request.Password);
            var result = await _repository.Login(request.Email, hash);
            if (result == null)
            {
                return ResultViewModel<LoginViewModel>.Failed("Erro de login");
            }

            var token = _authService.GenerateToken(result.Email, result.Role);
            var model = new LoginViewModel(token);

            return ResultViewModel<LoginViewModel>.Success(model);
        }
        catch (Exception ex)
        {
            return ResultViewModel<LoginViewModel>.Failed(ex.Message);
        }
    }
}