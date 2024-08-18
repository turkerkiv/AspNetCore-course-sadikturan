using _9__RazorPagesApp.Models;
using _9__RazorPagesApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _9__RazorPagesApp.Pages.Employees;

public class IndexModel : PageModel
{
    private readonly IEmployeeRepository _repo;
    public IEnumerable<Employee> EmployeeList = new List<Employee>();

    public IndexModel(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public void OnGet()
    {
        EmployeeList = _repo.GetAll();
    }
}
