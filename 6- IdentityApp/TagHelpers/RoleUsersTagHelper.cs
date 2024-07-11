using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.TagHelpers;

[HtmlTargetElement("td", Attributes = "asp-role-users")]
public class RoleUsersTagHelper : TagHelper
{
    readonly RoleManager<AppRole> _roleManager;
    readonly UserManager<AppUser> _userManager;

    public RoleUsersTagHelper(RoleManager<AppRole> rm, UserManager<AppUser> um)
    {
        _roleManager = rm;
        _userManager = um;
    }

    [HtmlAttributeName("asp-role-users")]
    public string Id { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var role = await _roleManager.FindByIdAsync(Id);
        var users = await _userManager.GetUsersInRoleAsync(role!.Name!);
        var userNames = users.Select(u => u.FullName).ToList();

        output.Content.SetContent(userNames.Count == 0 ? "No user" : string.Join(", ", userNames));
    }
}