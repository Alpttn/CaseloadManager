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
using Microsoft.AspNetCore.Authorization;
using CaseloadManager.Utilities;
using Z.EntityFramework.Plus;

namespace CaseloadManager.Controllers
{
    [Authorize]
    public class TherapySessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public TherapySessionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TherapySessions
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var therapySessions = from t in _context.TherapySessions.Include(t => t.Client)
                                  select t;
            switch (sortOrder)
            {
                case "name_desc":
                    therapySessions = therapySessions.OrderByDescending(t => t.ClientId);
                    break;
                case "Date":
                    therapySessions = therapySessions.OrderBy(t => t.Date);
                    break;
                case "date_desc":
                    therapySessions = therapySessions.OrderByDescending(t => t.Date);
                    break;
                default:
                    therapySessions = therapySessions.OrderBy(t => t.ClientId);
                    break;
            }
            return View(await therapySessions.ToListAsync());
        }
        // Index Attendance method

        public async Task<IActionResult> Attendance(string sortOrder)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //var currentDate = System.DateTime.UtcNow;
            //var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            //var weekOfYear = cal.GetWeekOfYear(currentDate,
            //              System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var start = DateTime.Now.StartOfWeek();
            var end = DateTime.Now.EndOfWeek();
            //var tsclients = _context.TherapySessions
            //    .Include(t => t.Client)
            //    .ThenInclude(client => client.User)
            //    .Include(t => t.Client)
            //    .ThenInclude(c => c.Facility)
            //            .Where(t => t.Date >= start && t.Date <= end && t.Client.UserId == user.Id)
            //            .ToList();

            var clients = _context.Clients
                .IncludeOptimized(Client => Client.TherapySessions.Where(t => t.Date >= start && t.Date <= end))
                .Include(c => c.User)
                .Include(c => c.Facility)
                .Where(c => c.UserId == user.Id);

            //var currentWeek = GetWeekOfYear()

            //foreach (Client c in clients)
            //{
            //    foreach (TherapySession ts in c.TherapySessions)
            //    {
            //        if (ts )
            //    }
            //}



            return View(clients.ToList());
        }



        // GET: TherapySessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySession = await _context.TherapySessions
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.TherapySessionId == id);
            if (therapySession == null)
            {
                return NotFound();
            }

            return View(therapySession);
        }

        // GET: TherapySessions/Create
        public async Task<IActionResult> Create(int clientId)
        {
            ViewData["ClientId"] = clientId;
            ViewData["GoalId"] = new SelectList(_context.Goals.Where(g => g.ClientId == clientId), "GoalId", "Description", clientId);
            var therapySession = await _context.TherapySessions
                .Include(t => t.Client)
                .ThenInclude(Client => Client.Goals)
                .FirstOrDefaultAsync(t => t.TherapySessionId == clientId);
            return View();
        }

        // POST: TherapySessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TherapySessionId,Date,Note,ClientId")] TherapySession therapySession)
        {
            //ModelState.Remove("ClientId");
            if (ModelState.IsValid)
            {
                _context.Add(therapySession);
                await _context.SaveChangesAsync();
                return Redirect($"/Clients/Details/{therapySession.ClientId}");
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstInitial", therapySession.ClientId);
            ViewData["GoalId"] = new SelectList(_context.Goals, "GoalId", "Description");
            return View(therapySession);
        }

        // GET: TherapySessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySession = await _context.TherapySessions.FindAsync(id);
            if (therapySession == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstInitial", therapySession.ClientId);
            return View(therapySession);
        }

        // POST: TherapySessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TherapySessionId,Date,Note,ClientId")] TherapySession therapySession)
        {
            if (id != therapySession.TherapySessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(therapySession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TherapySessionExists(therapySession.TherapySessionId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "FirstInitial", therapySession.ClientId);
            return View(therapySession);
        }

        // GET: TherapySessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapySession = await _context.TherapySessions
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.TherapySessionId == id);
            if (therapySession == null)
            {
                return NotFound();
            }

            return View(therapySession);
        }

        // POST: TherapySessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var therapySession = await _context.TherapySessions.FindAsync(id);
            _context.TherapySessions.Remove(therapySession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TherapySessionExists(int id)
        {
            return _context.TherapySessions.Any(e => e.TherapySessionId == id);
        }
    }
}
