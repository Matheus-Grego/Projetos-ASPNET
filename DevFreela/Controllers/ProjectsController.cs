using DevFreela.Domain.Entities;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.Controllers;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly IProjectService _projectService;
    public ProjectsController(DevFreelaDbContext context, IProjectService projectService)
    {
        _dbContext =  context;
        _projectService = projectService;
    }
    
    [HttpGet]
    public IActionResult Get(string search = "", int page = 0, int pageSize = 10)
    {
        var result = _projectService.GetAllProjects(search, page, pageSize);
        return Ok(result);
    }
    
    [HttpGet("{projectId}")]
    public IActionResult GetByProjectId(Guid projectId)
    {
        var result = _projectService.GetProjectById(projectId);
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post(CreateProjectInputModel model)
    {
        var result = _projectService.InsertProject(model);
        return CreatedAtAction(nameof(GetByProjectId), new { projectId = result.Data}, model);
    }

    [HttpPut("{projectId}")]
    public IActionResult Put(ProjectModel model)
    {
        var result = _projectService.UpdateProject(model);
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpDelete("{projectId}")]
    public IActionResult Delete(Guid projectId)
    {
        var result = _projectService.DeleteProject(projectId);
        
        if(!result.IsSuccessful)
            return BadRequest(result.Message);
        
        return NoContent();
    }

    [HttpGet("{projectId}/comments")]
    public IActionResult GetComments(Guid projectId)
    {
       var result =  _projectService.GetComments(projectId);
       
       if(!result.IsSuccessful)
           return BadRequest(result.Message);
       
       return Ok(result.Data);
    }

    [HttpPost("{projectId}/comments")]
    public IActionResult PostComments(CreateCommentInputModel comment)
    {

        var result = _projectService.InsertComments(comment);
        if (!result.IsSuccessful)
            return BadRequest(result.Message);

        return NoContent();
        
        
    }
}