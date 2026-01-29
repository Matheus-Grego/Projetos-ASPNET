using DevFreela.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(CreateUserInputModel userInput)
    {
        return Ok();
    }
    [HttpGet("{userId}")]
    public IActionResult GetUsers(int userId)
    {
        return Ok();
    }

    [HttpPost("{userId}/profile-picture")]
    public IActionResult UpdateUserProfilePicture(IFormFile image)
    {
        var description = $"{image.FileName} => {image.Length} ";
        return Ok(description);
    }
}