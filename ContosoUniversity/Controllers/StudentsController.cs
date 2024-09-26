using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        /*
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber
            )
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null) 
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["currentFilter"] = searchString;

            var students = from student in _context.Students
                           select student;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(student => 
                student.LastName.Contains(searchString) || 
                student.FirstMidName.Contains(searchString));
            }
            switch (sortOrder) 
            {
                case "name_desc":
                    students = students.OrderByDescending(student => student.LastName);
                    break;
                case "firstname_desc":
                    students = students.OrderByDescending(student => student.FirstMidName);
                    break;
                case "Date":
                    students = students.OrderBy(student => student.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(student => student.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(student => student.LastName);
                    break;
            }

            int pageSize = 3;
            return View(await _context.Students.ToListAsync());
        }
        */

        
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var student = await _context.Students 
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null) 
            { 
                return NotFound();
            }
            return View(student); 
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToEdit = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentToEdit == null)
            {
                return NotFound();
            }
            return View(studentToEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student modifiedStudent)
        {
            if (ModelState.IsValid)
            {
                if (modifiedStudent.ID == null)
                {
                    return BadRequest();
                }
                _context.Students.Update(modifiedStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedStudent);
        }


        [HttpPost]
        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clonedStudent = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clonedStudent == null)
            {
                return NotFound();
            }
            int lastID = _context.Students.OrderBy(u => u.ID).Last().ID;
            lastID++;
            var selectedStudent = new Student();
            selectedStudent.FirstMidName = clonedStudent.FirstMidName;
            selectedStudent.LastName = clonedStudent.LastName;
            selectedStudent.EnrollmentDate = clonedStudent.EnrollmentDate;
            _context.Students.Add(selectedStudent);
            await _context.SaveChangesAsync(true);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) //kui id on tühi/null, siis õpilast ei leita
            {
                return NotFound();
            }

            var student = await _context.Students // tehakse õpilase objekt andmebaasis oleva id järgi
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null) //kui student objekt on tühi/null, siis ka õpilast ei leita
            {
                return NotFound();
            }

            return View(student);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id); 
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
