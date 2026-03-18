using DevFreela.Entities;
using DevFreela.Models;
using DevFreela.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.Controllers;

[ApiController]
[Route("projects")]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;
    public ProjectsController(DevFreelaDbContext context)
    {
        _dbContext =  context;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var projects = _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .Where(p => p.IsDeleted == false)
            .ToList();
        
        var model = projects.Select(ProjectModel.FromEntity).ToList();

        return Ok(model);
    }
    
    [HttpGet("{projectId}")]
    public IActionResult GetByProjectId(Guid projectId)
    {
        var project = _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Developer)
            .SingleOrDefault(p => p.Id == projectId);
        
        if(project == null)
            return NotFound();
        
        var model = ProjectModel.FromEntity(project);
      
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post( CreateProjectInputModel model)
    {
        var entity = model.toEntity();
        _dbContext.Projects.Add(entity);
        _dbContext.SaveChanges();
        
        return CreatedAtAction(nameof(GetByProjectId), new { projectId = entity.Id }, model);
    }

    [HttpPut("{projectId}")]
    public IActionResult Put(Guid projectId, ProjectModel model)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);
        
        if(project == null)
            return NotFound();
        
        project.Update(model.Title, model.Description, model.TotalCost);
        
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return Ok(project);
    }

    [HttpDelete("{projectId}")]
    public IActionResult Delete(Guid projectId)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);
        
        if(project == null)
            return NotFound();
        
        project.SetAsDeleted();
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return Ok();
    }

    [HttpGet("{projectId}/comments")]
    public IActionResult GetComments(Guid projectId)
    {
        var project = _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);
        
        if(project == null)
            return NotFound();

        var comments = project.Comments
            .Where(c => c.IsDeleted == false)
            .Select(CreateCommentInputModel.FromEntity)
            .ToList();
        
        return Ok(comments);
    }

    [HttpPost("{projectId}/comments")]
    public IActionResult PostComments(Guid projectId, CreateCommentInputModel comment)
    {
       var project =  _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);
       if(project == null)
           return NotFound();
       
       var comments = new CommentsEntity(projectId, comment.UserId, comment.Content);
       
       _dbContext.Comments.Add(comments);
       _dbContext.SaveChanges();
       return Ok(comments);
    }
}