using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkill;
using DevFreela.Application.Commands.Login;
using DevFreela.Application.Commands.RecoveryPassword;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DevFreela.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;
    public UserController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(InsertUserCommand command)
    {
        var result = await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return Ok(result);
    }

    [HttpPost("{userId}/skills")]
    public async Task<IActionResult> PostSkills(Guid userId, InsertUserSkillCommand userSkill)
    {
        userSkill.UserId = userId;
        var result = await _mediator.Send(userSkill);
        return NoContent();
    }

    [HttpPost("{userId}/profile-picture")]
    public IActionResult UpdateUserProfilePicture(Guid userId,IFormFile image)
    {
        var description = $"{image.FileName} => {image.Length} ";
        return Ok(description);
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpPost("password-recovery/request")]
    [AllowAnonymous]
    public async Task<IActionResult> RequestPasswordRecovery(RecoveryPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }
    [HttpPost("password-recovery/validate")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateRecoveryCode(ValidateRecoveryPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
        
    }
    [HttpPost("password-recovery/change")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);    
    }
}