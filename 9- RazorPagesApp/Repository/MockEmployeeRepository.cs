using _9__RazorPagesApp.Models;

namespace _9__RazorPagesApp.Repository;

public class MockEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;
    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new Employee {Id = 1, Name = "Lorem ipsum1", Email = "test1@gmail.com", Photo = "1.jpg", Department = "Muhasebe"},
            new Employee {Id = 2, Name = "Lorem ipsum2", Email = "test2@gmail.com", Photo = "2.jpg", Department = "Muhasebe"},
            new Employee {Id = 3, Name = "Lorem ipsum3", Email = "test3@gmail.com", Photo = "3.jpg", Department = "Muhasebe"},
            new Employee {Id = 4, Name = "Lorem ipsum4", Email = "test4@gmail.com", Photo = "4.jpg", Department = "Muhasebe"},
            new Employee {Id = 5, Name = "Lorem ipsum5", Email = "test5@gmail.com", Photo = "5.jpg", Department = "Muhasebe"},
        };
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employeeList;
    }

    public Employee GetById(int id)
    {
        return _employeeList.FirstOrDefault(e => e.Id == id)!;
    }
}
