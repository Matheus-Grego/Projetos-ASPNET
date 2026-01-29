using DevFreela.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[ApiController]
[Route("api/projects")]
public class CommentsController : ControllerBase
{
   [HttpGet]
   public IActionResult Get(Guid projectId)
   {
      return NoContent();
   }

   [HttpPost]
   public IActionResult PostComment(CreateCommentInputModel comment)
   {
      return NoContent();
   }

   [HttpPut]
   public IActionResult EditComment(Guid commentId, CreateCommentInputModel newComment)
   {
      return NoContent();
   }
   
   [HttpDelete("{id}")]
   public IActionResult DeleteComment(Guid commentId)
   {
      return NoContent();
   }
   
   
}