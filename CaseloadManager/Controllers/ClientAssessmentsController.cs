using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaseloadManager.Data;
using CaseloadManager.Models;
using Microsoft.AspNetCore.Identity;

namespace CaseloadManager.Controllers
{
    public class ClientAssessmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ClientAssessmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ClientAssessments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientAssessments.Include(c => c.Assessment).Include(c => c.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientAssessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAssessment = await _context.ClientAssessments
                .Include(c => c.Assessment)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ClientAssessmentId == id);
            if (clientAssessment == null)
            {
                return NotFound();
            }

            return View(clientAssessment);
        }

        // GET: ClientAssessments/Create
        public IActionResult Create(string clientId)
        {

            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "TestName");
            ViewData["ClientId"] = clientId;
            return View();
        }

        // POST: ClientAssessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientAssessmentId,StandarizedScore,DateAdministered,ClientId,AssessmentId")] ClientAssessment clientAssessment)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(clientAssessment);
                await _context.SaveChangesAsync();
                
                return Redirect($"/Clients/Details/{clientAssessment.ClientId}");
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "TestName", clientAssessment.AssessmentId);
            ViewData["ClientId"] = clientAssessment.ClientId;
            return RedirectToRoute("/Clients/Details", new { clientId = clientAssessment.ClientId });
        }

        // GET: ClientAssessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAssessment = await _context.ClientAssessments.FindAsync(id);
            if (clientAssessment == null)
            {
                return NotFound();
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "TestName", clientAssessment.AssessmentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstInitial", clientAssessment.ClientId);
            return View(clientAssessment);
        }

        // POST: ClientAssessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientAssessmentId,StandarizedScore,DateAdministered,ClientId,AssessmentId")] ClientAssessment clientAssessment)
        {
            if (id != clientAssessment.ClientAssessmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientAssessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientAssessmentExists(clientAssessment.ClientAssessmentId))
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
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "TestName", clientAssessment.AssessmentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstInitial", clientAssessment.ClientId);
            return View(clientAssessment);
        }

        // GET: ClientAssessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientAssessment = await _context.ClientAssessments
                .Include(c => c.Assessment)
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ClientAssessmentId == id);
            if (clientAssessment == null)
            {
                return NotFound();
            }

            return View(clientAssessment);
        }

        // POST: ClientAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientAssessment = await _context.ClientAssessments.FindAsync(id);
            _context.ClientAssessments.Remove(clientAssessment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientAssessmentExists(int id)
        {
            return _context.ClientAssessments.Any(e => e.ClientAssessmentId == id);
        }
    }
}
