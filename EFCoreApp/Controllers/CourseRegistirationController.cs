using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class CourseRegistirationController : Controller
    {
        readonly CourseContext _courseCtx;
        public CourseRegistirationController(CourseContext courseContext)
        {
            _courseCtx = courseContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var CourseRegistirations = await _courseCtx.
            CourseRegistirations
            .Include(c => c.Student)
            .Include(c => c.Course)
            .ToListAsync();
            return View(CourseRegistirations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(await _courseCtx.Students.ToListAsync(), "Id", "NameSurname");
            ViewBag.Courses = new SelectList(await _courseCtx.Courses.ToListAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseRegistiration crsRgstr)
        {
            crsRgstr.RegistirationDate = DateTime.UtcNow;
            _courseCtx.CourseRegistirations.Add(crsRgstr);
            await _courseCtx.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}