using System.ComponentModel.DataAnnotations;

namespace EventApp.DTO;

public class LoginDto
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}