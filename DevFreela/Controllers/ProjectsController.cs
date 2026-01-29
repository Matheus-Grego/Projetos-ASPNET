using DevFreela.Models;
using DevFreela.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;
    public ProjectsController(DevFreelaDbContext context)
    {
        _dbContext =  context;
    }
    
    [HttpGet("{projectId}")]
    public IActionResult GetProject(Guid projectId)
    {

        // return _dbContext.Projects.SingleOrDefault(x => x.Id == projectId);
      
        return Ok(projectId);
    }

    [HttpPost]
    public IActionResult PostProject(ProjectModel projectModel)
    {
        
        return CreatedAtAction(nameof(GetProject), new { projectId = projectModel.Id }, projectModel);
    }

    [HttpPut("{projectId}")]
    public IActionResult PutProject(int projectId, ProjectModel projectModel)
    {
        return NoContent();
    }
    
    
}