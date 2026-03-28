using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.RecoveryPassword;

public class RecoveryPasswordCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
}