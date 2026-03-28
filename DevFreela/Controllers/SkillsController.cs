using DevFreela.Application.Commands.Skills.DeleteSkill;
using DevFreela.Application.Commands.Skills.InsertSkill;
using DevFreela.Application.Commands.Skills.UpdateSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.Skills.GetSkills;
using DevFreela.Application.Queries.Users.GetUserBySkill;
using DevFreela.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("skills")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SkillsController(DevFreelaDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetSkillsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertSkillCommand command)
    {
        var result = await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{skillId}")]
    public async Task<IActionResult> Put(Guid skillId, UpdateSkillCommand command)
    {
        command.Id = skillId;
        var result = await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    public async Task<IActionResult> Delete(Guid skillId)
    {
        var result = await _mediator.Send(new DeleteSkillCommand(skillId));
        return NoContent();
    }

    [HttpGet("{skillId}/users")]
    public async Task<IActionResult> GetUsersBySkill(Guid skillId)
    {
        var result = await _mediator.Send(new GetUsersBySkillQuery(skillId));
        return Ok(result);
    }
}