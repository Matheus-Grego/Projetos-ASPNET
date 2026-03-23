using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistance;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("skills")]
public class SkillsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;
    public SkillsController(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var skills = _dbContext.Techs.ToList();
        var model = skills.Select(x => TechnologyModel.FromEntity(x)).ToList();
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Post(TechnologyModel technologyModel)
    {

        var entity = TechnologyModel.ToEntity(technologyModel);
        _dbContext.Techs.Add(entity);
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpPut("{skillId}")]
    public IActionResult Put(Guid skillId, TechnologyModel technologyModel)
    {
        var skill = _dbContext.Techs.SingleOrDefault(t => t.Id == skillId);
        if (skill == null)
            return NotFound();
        var entity = TechnologyModel.ToEntity(technologyModel);
        skill.Update(entity);
        _dbContext.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    public IActionResult Delete(Guid skillId, TechnologyModel technologyModel)
    {
        var skill = _dbContext.Techs.SingleOrDefault(t => t.Id == skillId);

        if (skill == null)
            return NotFound();
        
        skill.Delete();
        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpGet("{skillId}/users")]
    public IActionResult GetUsersBySkill(Guid skillId)
    {
        var skill = _dbContext.UserTechs.Where(x => x.TechId == skillId).ToList();

        if (skill == null)
            return NotFound();
        
        var users = skill.Select(x => UserModel.FromEntity(x.User)).ToList();
        return Ok(users);
    }
}