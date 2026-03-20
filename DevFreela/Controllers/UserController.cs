using DevFreela.Entities;
using DevFreela.Models;
using DevFreela.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;
    public UserController(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _dbContext.Users.ToList();
        var model = users.Select(UserModel.FromEntity).ToList();
        return Ok(model);
    }
    
    [HttpPost]
    public IActionResult CreateUser(CreateUserInputModel userInput)
    {
        _dbContext.Users.Add(userInput.ToEntity());
        _dbContext.SaveChanges();
        
        var user = _dbContext.Users.SingleOrDefault(u => u.Email == userInput.Email);
        if (user == null)
            return NotFound();
        
        var model = UserModel.FromEntity(user);
        
        return Ok(model);
    }
    [HttpGet("{userId}")]
    public IActionResult GetUserById(Guid userId)
    {
        var user = _dbContext.Users
            .Include(t => t.Technologies)
            .ThenInclude(x => x.Technology)
            .SingleOrDefault(u => u.Id == userId);
        
        if (user == null)
            return NotFound();
        
        var model = UserModel.FromEntity(user);
        return Ok(model);
    }

    [HttpPost("{userId}/skills")]
    public IActionResult PostSkills(Guid userId, UserTechInputModel userSkill)
    {
        var userSkills = userSkill.TechnologyID.Select(s => new UserTechEntity(userId, s)).ToList();

        _dbContext.UserTechs.AddRange(userSkills);
        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpPost("{userId}/profile-picture")]
    public IActionResult UpdateUserProfilePicture(Guid userId,IFormFile image)
    {
        var description = $"{image.FileName} => {image.Length} ";
        return Ok(description);
    }
}