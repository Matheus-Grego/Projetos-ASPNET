using DevFreela.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("skills")]
public class SkillsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(TechnologyModel technologyModel)
    {
        return NoContent();
    }

    [HttpPut]
    public IActionResult Put(TechnologyModel technologyModel)
    {
        return NoContent();
    }
}