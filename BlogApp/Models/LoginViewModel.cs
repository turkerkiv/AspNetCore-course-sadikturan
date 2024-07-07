using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "Max. 10 characters.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

}