using DevFreela.Application.Models;
using DevFreela.Infrastructure.Cache;
using MediatR;

namespace DevFreela.Application.Commands.Users.ValidatePassword;

public class ValidatePasswordHandler : IRequestHandler<ValidateRecoveryPasswordCommand, ResultViewModel>
{
    private readonly IRecoveryPasswordCache _cache;
    
    public ValidatePasswordHandler(IRecoveryPasswordCache cache)
    {
        _cache = cache;
    }
    public async Task<ResultViewModel> Handle(ValidateRecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
       var cacheKey = $"RecoveryCode:{request.Email}";
       var code =  _cache.GetCode(cacheKey);
       if (code == null || code != request.Code.ToString())
       {
           return ResultViewModel.Failed("Invalido");
       }
       
       return ResultViewModel.Success();

    }
}