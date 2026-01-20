using DevFreela.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly FreelanceTotalCostModel _config;
    public ProjectsController(IOptions<FreelanceTotalCostModel> options)
    {
        _config = options.Value;
    }
    
    [HttpGet("{projectId}")]
    public IActionResult GetProject(int projectId)
    {
        throw new Exception();
        return Ok(projectId);
    }

    [HttpPost]
    public IActionResult PostProject(Projects project)
    {
        if (project.TotalCost > _config.maxCost || project.TotalCost < _config.minCost)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetProject), new { projectId = project.Id }, project);
    }

    [HttpPut("{projectId}")]
    public IActionResult PutProject(int projectId, Projects project)
    {
        return NoContent();
    }
}