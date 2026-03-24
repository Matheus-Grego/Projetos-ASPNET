using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkill;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace DevFreela.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(DevFreelaDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    
    [HttpPost]
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
}