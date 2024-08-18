using _9__RazorPagesApp.Models;
using _9__RazorPagesApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _9__RazorPagesApp.Pages.Employees;

public class EditModel : PageModel
{
    public Employee Employee { get; set; } = null!;
    IEmployeeRepository _repo;

    public EditModel(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public IActionResult OnGet(int id)
    {
        if (_repo.GetById(id) == null) return RedirectToPage("/NotFound");
        Employee = _repo.GetById(id);
        return Page();
    }

    public IActionResult OnPost(Employee employee)
    {
        Employee emp = _repo.GetById(employee.Id);
        emp.Name = employee.Name;
        emp.Email = employee.Email;
        emp.Department = employee.Department;
        return RedirectToPage("/Employees/index");
    }
}