using DevFreela.Application.Models;
using DevFreela.Domain.Repositories;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Cache;
using DevFreela.Infrastructure.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.RecoveryPassword;

public class RecoveryPasswordHandler : IRequestHandler<RecoveryPasswordCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;
    private readonly IRecoveryPasswordCache _cache;
    private readonly IEmailService _emailService;
    
    public RecoveryPasswordHandler(IUserRepository repository, IEmailService emailService, IRecoveryPasswordCache cache)
    {
        _repository = repository;
        _cache = cache;
        _emailService = emailService;
    }
    public async Task<ResultViewModel> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.ExistsByEmail(request.Email);
        if (!user)
        {
            return ResultViewModel.Failed("Not found");
        }

        var code = new Random().Next(10000, 99999);
        var cacheKey = $"RecoveryCode:{request.Email}";
        _cache.SetCode(cacheKey, code);
        
        await _emailService.SendAsync(request.Email, "Change Password", $"Your verify code is {code}");
        return ResultViewModel.Success();
        
    }

    
}