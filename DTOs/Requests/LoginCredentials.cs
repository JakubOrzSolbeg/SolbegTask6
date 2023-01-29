using System.ComponentModel.DataAnnotations;

namespace DTOs.Requests;

public class LoginCredentials
{
    [Required]
    [MinLength(3)]
    public string Login { get; set; } = null!;
    [Required] 
    [MinLength(3)] 
    public string Password { get; set; } = null!;
}