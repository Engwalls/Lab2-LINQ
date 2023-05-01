using Lab2Test.Data;
using Lab2Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace Lab2Test.Controllers
{
    public class ViewModelController : Controller
    {
        private readonly Lab2DbContext _context;
        public ViewModelController(Lab2DbContext context)
        {
            _context = context;
        }


        // Uppgift 3 
        public async Task<IActionResult> GetStudentsNTeachers()
        {
            var students = await _context.Students.ToListAsync();
            var teachers = await _context.Teachers.ToListAsync();
            var courses = await _context.Courses.ToListAsync();
            var createlist = from s in students
                             join c in courses on s.StudentId equals c.FK_StudentId
                             join t in teachers on c.FK_TeacherId equals t.TeacherId
                             where c.CourseName == "Programming 1"
                             orderby c.CourseName
                             select new Lab2ViewModel
                             {
                                 StudFirstName = s.StudFullName,
                                 CourseName = c.CourseName,
                                 TeacherFirstName = t.TeacherFullName
                             };
                             return View(createlist);
        }


        //Uppgift 4
        public async Task<IActionResult> EditCourseName()
        {
            var courseList = await _context.Courses.ToListAsync();
            return View(courseList);
        }

        public async Task<IActionResult> EditCourseNameInputs(string courseName, string newCourseName)
        {
            courseName = courseName.Trim().ToLower();
            newCourseName = newCourseName.Trim();

            var coursesToUpdate = await _context.Courses
                                    .Where(c => c.CourseName.Trim().ToLower() == courseName)
                                    .ToListAsync();

            if (coursesToUpdate.Count == 0)
            {
                return View("No match");
            }
            else
            {
                foreach (var course in coursesToUpdate)
                {
                    course.CourseName = newCourseName;
                }

                await _context.SaveChangesAsync();
                return View(coursesToUpdate);
            }
        }


        // Uppgift 5 
        public async Task<IActionResult> EditTeacherName()
        {
            var teacherList = await _context.Teachers.Include(t => t.Courses).ToListAsync();
            return View(teacherList);
        }

        [HttpPost]
        public IActionResult EditTeacherName(int teacherId, string newTeacherName)
        {
            var teacher = _context.Teachers.Find(teacherId);
            if (teacher != null)
            {
                var names = newTeacherName.Split(" ");
                teacher.TeacherFirstName = names[0];
                teacher.TeacherLastName = names[1];
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Course");
        }


    }
}
