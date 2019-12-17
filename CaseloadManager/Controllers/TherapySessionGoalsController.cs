using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaseloadManager.Data;
using CaseloadManager.Models;

namespace CaseloadManager.Controllers
{
    public class TherapySessionGoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TherapySessionGoalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TherapySessionGoals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TherapySessionsGoals.Include(t => t.Goal).Include(t => t.TherapySession);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TherapySessionGoals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySessionGoal = await _context.TherapySessionsGoals
                .Include(t => t.Goal)
                .Include(t => t.TherapySession)
                .FirstOrDefaultAsync(m => m.TherapySessionGoalId == id);
            if (therapySessionGoal == null)
            {
                return NotFound();
            }

            return View(therapySessionGoal);
        }

        // GET: TherapySessionGoals/Create
        public IActionResult Create()
        {
            ViewData["GoalId"] = new SelectList(_context.Goals, "GoalId", "Description");
            ViewData["TherapySessionId"] = new SelectList(_context.TherapySessions, "TherapySessionId", "Note");
            return View();
        }

        // POST: TherapySessionGoals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TherapySessionGoalId,GoalId,TherapySessionId")] TherapySessionGoal therapySessionGoal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(therapySessionGoal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GoalId"] = new SelectList(_context.Goals, "GoalId", "Description", therapySessionGoal.GoalId);
            ViewData["TherapySessionId"] = new SelectList(_context.TherapySessions, "TherapySessionId", "Note", therapySessionGoal.TherapySessionId);
            return View(therapySessionGoal);
        }

        // GET: TherapySessionGoals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySessionGoal = await _context.TherapySessionsGoals.FindAsync(id);
            if (therapySessionGoal == null)
            {
                return NotFound();
            }
            ViewData["GoalId"] = new SelectList(_context.Goals, "GoalId", "Description", therapySessionGoal.GoalId);
            ViewData["TherapySessionId"] = new SelectList(_context.TherapySessions, "TherapySessionId", "Note", therapySessionGoal.TherapySessionId);
            return View(therapySessionGoal);
        }

        // POST: TherapySessionGoals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TherapySessionGoalId,GoalId,TherapySessionId")] TherapySessionGoal therapySessionGoal)
        {
            if (id != therapySessionGoal.TherapySessionGoalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(therapySessionGoal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TherapySessionGoalExists(therapySessionGoal.TherapySessionGoalId))
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
            ViewData["GoalId"] = new SelectList(_context.Goals, "GoalId", "Description", therapySessionGoal.GoalId);
            ViewData["TherapySessionId"] = new SelectList(_context.TherapySessions, "TherapySessionId", "Note", therapySessionGoal.TherapySessionId);
            return View(therapySessionGoal);
        }

        // GET: TherapySessionGoals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySessionGoal = await _context.TherapySessionsGoals
                .Include(t => t.Goal)
                .Include(t => t.TherapySession)
                .FirstOrDefaultAsync(m => m.TherapySessionGoalId == id);
            if (therapySessionGoal == null)
            {
                return NotFound();
            }

            return View(therapySessionGoal);
        }

        // POST: TherapySessionGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var therapySessionGoal = await _context.TherapySessionsGoals.FindAsync(id);
            _context.TherapySessionsGoals.Remove(therapySessionGoal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TherapySessionGoalExists(int id)
        {
            return _context.TherapySessionsGoals.Any(e => e.TherapySessionGoalId == id);
        }
    }
}
