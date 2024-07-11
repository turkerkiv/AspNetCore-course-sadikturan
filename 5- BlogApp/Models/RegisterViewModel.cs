using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(10, ErrorMessage = "{0} field must be at least {2} characters long and maximum {1} characters long", MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords must be same")]
    [DisplayName("Confirm Password")]
    public string? ConfirmPassword { get; set; }

}