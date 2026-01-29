using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace DevFreela.Models;

public class CreateUserInputModel : BaseModel
{
    [Required]
    [MinLength(4)]
    public string Username { get; set; }
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}