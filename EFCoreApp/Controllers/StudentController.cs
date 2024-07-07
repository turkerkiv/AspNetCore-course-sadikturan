using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class StudentController : Controller
    {
        readonly CourseContext _courseCtx;

        public StudentController(CourseContext courseContext)
        {
            _courseCtx = courseContext;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _courseCtx.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            _courseCtx.Students.Add(student);
            await _courseCtx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var std = await _courseCtx
            .Students
            .Include(s => s.CourseRegistirations)
            .ThenInclude(cr => cr.Course)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (std == null) return NotFound();

            return View(std);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _courseCtx.Update(student);
                    await _courseCtx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_courseCtx.Students.Any(s => s.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();

            var std = await _courseCtx.Students.FindAsync(id);

            if(std == null) return NotFound();

            return View(std);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var std = await _courseCtx.Students.FindAsync(id);
            if(std == null) return NotFound();

            _courseCtx.Students.Remove(std);
            await _courseCtx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}