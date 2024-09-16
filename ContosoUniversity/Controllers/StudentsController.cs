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
        /// <summary>
        /// Asünkroonne Index GET meetod.
        /// Kuvab kasutajale kõik õpilased andmebaasist.
        /// </summary>
        /// <returns>Tagastab kasutajale Index vaate koos kõigi õpilastega</returns>
        // get all for index, retreive all students
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

        /// <summary>
        /// Mitteasünkroonne GET meetod mis kuvab vaate uue õpilase andmete sisestuseks.
        /// </summary>
        /// <returns>Tagastab vaate kasutajale.</returns>
        // Create get, haarab vaatest andmed, mida create meetod vajab.
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
            //var existingStudent = Details(id);
            //return View(existingStudent);
            var clonedStudent = await _context.Students // tehakse õpilase objekt andmebaasis oleva id järgi
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

        /// <summary>
        /// Asünkroonne Delete GET meetod. 
        /// Leiab andmebaasist päringus oleva id järgi õpilase
        /// ning tagastab vaate koos selle õpilase infoga.
        /// </summary>
        /// <param name="id">Otsitava õpilase ID</param>
        /// <returns>Tagastab kasutajale vaate, koos õpilase andmetega</returns>
        //Delete GET meetod, otsib andmebaasist kaasaantud id järgi õpilast.
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
        /// <summary>
        /// Asünkroonne DeleteConfirmed meetod.
        /// Kustutab kaasaantud ID alusel ära õpilase andmebaasist ning tagastab kasutaja Index vaatesse.
        /// </summary>
        /// <param name="id">Kustutatava õpilase ID</param>
        /// <returns>Kustutab õpilase andmed andmebaasist ära ning tagastab kasutajale Index vaate</returns>
        //Delete POST meetod, teostab andmebaasis vajaliku muudatuse. ehk kustutab andme ära
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id); //otsime andmebaasist õpilast id järgi ja paneme ta "student" nimelisse muutujasse.

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
