using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class CreateViewModel
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords must be same!")]
    [DisplayName("Confirm password")]
    public string? ConfirmPassword { get; set; }
}