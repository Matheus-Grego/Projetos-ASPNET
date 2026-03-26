using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Cache;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;
    private readonly IRecoveryPasswordCache _cache;
    
    public ChangePasswordHandler(IUserRepository repository, IRecoveryPasswordCache cache)
    {
        _repository = repository;
        _cache = cache;
    }
    public async Task<ResultViewModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"RecoveryCode:{request.Email}";
        var code =  _cache.GetCode(cacheKey);
        if (code == null || code != request.Code.ToString())
        {
            return ResultViewModel.Failed("Invalido");
        }
        
        var user = await _repository.GetUserByEmail(request.Email);

        if (user == null)
        {
            return ResultViewModel.Failed("User not found");
        }
        
        user.UpdatePassword(request.NewPassword);

        return ResultViewModel.Success();
    }
}