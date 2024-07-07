using Microsoft.AspNetCore.Mvc;
using basics.Models;

namespace basics.Controllers;

public class CourseController : Controller
{
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("CourseList", "Course");
        }

        Course course = Repository.GetById(id);
        return View(course);
    }

    public IActionResult CourseList()
    {
        return View(Repository.Courses);
    }
}
