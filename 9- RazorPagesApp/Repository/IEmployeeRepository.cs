using _9__RazorPagesApp.Models;

namespace _9__RazorPagesApp.Repository;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();
    Employee GetById(int id);
}