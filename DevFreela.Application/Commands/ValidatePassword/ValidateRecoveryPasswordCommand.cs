using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.RecoveryPassword;

public class ValidateRecoveryPasswordCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
    public int Code { get; set; }
}