using DevFreela.Application.Commands.Project.CompleteProject;
using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Application.Commands.Project.InsertComment;
using DevFreela.Application.Commands.Project.InsertProject;
using DevFreela.Application.Commands.Project.StartProject;
using DevFreela.Application.Commands.Project.UpdateProject;
using DevFreela.Application.Queries.Projects.GetAllProjects;
using DevFreela.Application.Queries.Projects.GetCommentsByProject;
using DevFreela.Application.Queries.Projects.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("projects")]
[Authorize]

public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize (Roles = "Freelancer, Client")]
    public async Task<IActionResult> Get(string search = "", int page = 1)
    {
        var result =  await _mediator.Send(new GetAllProjectsQuery(search, page));
        return Ok(result);
    }
    
    [HttpGet("{projectId}")]
    [Authorize (Roles = "Freelancer, Client")]
    public async Task<IActionResult> GetByProjectId(Guid projectId)
    {
        var result = await _mediator.Send(new GetProjectByIdQuery(projectId));
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return Ok(result);
    }

    [HttpPost]
    [Authorize (Roles = "Freelancer")]
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
    [Authorize (Roles = "Freelancer")]
    public async Task<IActionResult> Put(UpdateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpDelete("{projectId}")]
    [Authorize (Roles = "Freelancer")]

    public async Task<IActionResult> Delete(DeleteProjectCommand command)
    {
        var result = await _mediator.Send(command);
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpPut("{projectId}/complete")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> Complete(Guid projectId, CompleteProjectCommand command)
    {
        var result = await _mediator.Send(command);
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();

    }
    
    [HttpPut("{projectId}/start")]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> Start(Guid projectId, StartProjectCommand command)
    {
        var result = await _mediator.Send(command);
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
    [Authorize (Roles = "Freelancer, Client")]

    public async Task<IActionResult> PostComments(InsertCommentCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return BadRequest(result.Message);

        return NoContent();
    }
}