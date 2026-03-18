using DevFreela.Models;
using DevFreela.Persistance;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetUsers(Guid userId)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);
        if (user == null)
            return NotFound();
        
        var model = UserModel.FromEntity(user);
        return Ok(model);
    }

    [HttpPost("{userId}/profile-picture")]
    public IActionResult UpdateUserProfilePicture(Guid userId,IFormFile image)
    {
        var description = $"{image.FileName} => {image.Length} ";
        return Ok(description);
    }
}