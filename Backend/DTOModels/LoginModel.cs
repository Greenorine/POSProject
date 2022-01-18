using System.ComponentModel.DataAnnotations;

namespace POSProject.Backend.DTOModels;

public class LoginModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}