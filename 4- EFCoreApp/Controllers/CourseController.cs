using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class CourseController : Controller
    {
        readonly CourseContext _courseCtx;

        public CourseController(CourseContext courseContext)
        {
            _courseCtx = courseContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseCtx.Courses.ToListAsync();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid) return View(course);

            _courseCtx.Courses.Add(course);
            await _courseCtx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit
        (int? id)
        {
            if (id == null) return NotFound();

            var course = await _courseCtx
            .Courses
            .Include(c => c.CourseRegistirations)
            .ThenInclude(cr => cr.Student)
            .FirstOrDefaultAsync(c => c.Id == id);
            
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Course course)
        {
            if (id != course.Id) return NotFound();

            if (!ModelState.IsValid) return View(course);

            //try catch could be added to prevent updating data which is deleted already
            _courseCtx.Update(course);
            await _courseCtx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var course = await _courseCtx.Courses.FindAsync(id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var course = await _courseCtx.Courses.FindAsync(id);

            if (course == null) return NotFound();

            _courseCtx.Remove(course);
            await _courseCtx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}