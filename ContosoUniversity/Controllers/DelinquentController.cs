using ContosoUniversity.Data;
using ContosoUniversity.Models;
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


        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null)
            {
                return View();
            }
            var delinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);
            if(delinquent == null)
            { 
                return NotFound();
            }
            return View(delinquent);


        }


        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, LastName, FirstMidName, RecentViolation")] Delinquent delinquent)
        {
            if(ModelState.IsValid)
            {
                _context.Delinquents.Add(delinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delinquent);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var delinquentToEdit = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delinquentToEdit == null)
            {
                return NotFound(); 
            }
            return View(delinquentToEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID, LastName, FirstMidName, RecentViolation")] Delinquent modelfieddelinquent)
        {  
            if(ModelState.IsValid) 
            { 
                if (modelfieddelinquent.ID == null)
                { 
                    return BadRequest();
                }
                _context.Delinquents.Update(modelfieddelinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            } 
            return View(modelfieddelinquent);
        }

        public IActionResult Edit() 
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);

            if (delinquent == null)
            {
                return NotFound();
            }

            return View(delinquent);
        }
     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delinquent = await _context.Delinquents.FindAsync(id); 
            _context.Delinquents.Remove(delinquent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
         public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);
            if (delinquent == null)
            {
                return NotFound();
            }


            var clonedDelinquents = new Delinquent
            {
                FirstMidName = delinquent.FirstMidName,
                LastName = delinquent.LastName,
                RecentViolation = delinquent.RecentViolation,
            };

            _context.Delinquents.Add(clonedDelinquents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


   
    }
}
