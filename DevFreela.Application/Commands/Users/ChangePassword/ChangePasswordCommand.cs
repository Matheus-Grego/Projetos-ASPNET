using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.ChangePassword;

public class ChangePasswordCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
    public int Code { get; set; }
    public string NewPassword { get; set; }
}