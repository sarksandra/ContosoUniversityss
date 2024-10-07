using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ContosoUniversity.Controllers
{
    
    public class DepartmentController : Controller
    {
        private readonly SchoolContext _context;
        public DepartmentController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);

            return View(await schoolContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * From Departments where DepartmentID = {0}";
            var departmentToSee = await _context.Departments
                .FromSqlRaw(query, id)
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (departmentToSee == null)
            {
                return NotFound();
            }
            return View(departmentToSee);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,RowVersion,InstructorID,DepartmentDog")]Department department)
        {
            if(ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewData["InstructorID "] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var DepartmentToEdit = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (DepartmentToEdit == null)
            {
                return NotFound();
            }
            return View(DepartmentToEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Budget,StartDate,RowVersion,InstructorID,DepartmentDog")] Department modifiedDepartment)
        {
            if (ModelState.IsValid)
            {
                if (modifiedDepartment.DepartmentID == null)
                {
                    return BadRequest();
                }
                _context.Departments.Update(modifiedDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedDepartment);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var department = await _context.Departments 
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (department == null) 
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        


    }
}
