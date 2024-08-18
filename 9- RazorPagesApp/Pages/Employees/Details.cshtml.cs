using _9__RazorPagesApp.Models;
using _9__RazorPagesApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _9__RazorPagesApp.Pages.Employees;

public class DetailsModel : PageModel
{
    public Employee Employee { get; set; } = default!;
    private readonly IEmployeeRepository _repo;

    public DetailsModel(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public IActionResult OnGet(int id)
    {
        if (_repo.GetAll().Any(e => e.Id == id))
        {
            Employee = _repo.GetById(id);
            return Page();
        }
        else
            return RedirectToPage("/NotFound");
    }
}
