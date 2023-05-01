using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2Test.Data;
using Lab2Test.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Lab2Test.Controllers
{
    public class CourseController : Controller
    {
        private readonly Lab2DbContext _context;

        public CourseController(Lab2DbContext context)
        {
            _context = context;
        }

        //Uppgift 1
        [HttpGet]
        public IActionResult GetTeacherForProgrammingCourse(string CourseName)
        {
            var teacherinprogramming = _context.Teachers
                .Join(_context.Courses,
                teacher => teacher.TeacherId,
                course => course.FK_TeacherId,
                (teacher, course) => new
                {
                    Teacher = teacher,
                    CourseName = course.CourseName
                })
                    .Where(tc => tc.CourseName == "Programming 1")
                    .Select(tc => tc.Teacher);
            foreach (var item in teacherinprogramming)
            {
                return View(teacherinprogramming);
            }
            return View();
        }

        //Uppgift 2
        public IActionResult StudentsWithTeachers()
        {
            var courses = from c in _context.Courses
                          join s in _context.Students on c.FK_StudentId equals s.StudentId
                          join t in _context.Teachers on c.FK_TeacherId equals t.TeacherId
                          join cl in _context.Classes on c.FK_ClassId equals cl.ClassId
                          select new Course
                          {
                              CourseId = c.CourseId,
                              CourseName = c.CourseName,
                              Students = s,
                              Teachers = t,
                              Classes = cl
                          };

            return View(courses.ToList());
        }





        // GET: Course
        public async Task<IActionResult> Index()
        {
            var lab2DbContext = _context.Courses.Include(c => c.Classes).Include(c => c.Students).Include(c => c.Teachers);
            return View(await lab2DbContext.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Classes)
                .Include(c => c.Students)
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudFullName");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFullName");
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,FK_StudentId,FK_TeacherId,FK_ClassId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudFullName");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFullName");
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudFirstName");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName");
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,FK_StudentId,FK_TeacherId,FK_ClassId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudFirstName");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName");
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Classes)
                .Include(c => c.Students)
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'Lab2DbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
