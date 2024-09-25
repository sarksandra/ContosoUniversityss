using ContosoUniversity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class DelinquentController : Controller
    {
        private readonly SchoolContext _context;
        public DelinquentController(SchoolContext context) 
        { 
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delinquents.ToListAsync());
        }


        public IActionResult Details() 
        { 
            return View();
        }
        public IActionResult Create() 
        {
            return View();
        }
        public IActionResult Edit() 
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
