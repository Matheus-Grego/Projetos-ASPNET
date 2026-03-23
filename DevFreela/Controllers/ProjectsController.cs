using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetCommentsByProject;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(string search = "", int page = 1)
    {
        var result =  _mediator.Send(new GetAllProjectsQuery(search, page));
        return Ok(result);
    }
    
    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(projectId));
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(InsertProjectCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }
        return CreatedAtAction(nameof(GetByProjectId), new { projectId = result.Data}, command);
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> Put(UpdateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> Delete(Guid projectId)
    {
        var result = await _mediator.Send(new DeleteProjectCommand(projectId));
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpGet("{projectId}/comments")]
    public async Task<IActionResult> GetComments(Guid projectId)
    {
       var result =  await _mediator.Send(new GetCommentsByProjectQuery(projectId));
       
       if(!result.IsSuccessful)
           return BadRequest(result.Message);
       
       return Ok(result.Data);
    }

    [HttpPost("{projectId}/comments")]
    public async Task<IActionResult> PostComments(InsertCommentCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return BadRequest(result.Message);

        return NoContent();
        
        
    }
}