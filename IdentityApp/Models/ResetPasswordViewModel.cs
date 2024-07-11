using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models;

public class ResetPasswordViewModel
{
    public string? Id { get; set; }
    public string? Token { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }
}