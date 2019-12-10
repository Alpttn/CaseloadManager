using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaseloadManager.Data;
using CaseloadManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CaseloadManager.Models.FacilityViewModels;

namespace CaseloadManager.Controllers
{
    [Authorize]
    public class FacilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public FacilitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Facilities.Include(f => f.FacilityType).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        //Facilities/MyFacilities
        public async Task<IActionResult> MyFacilities()
        {
            var model = new MyFacilitiesViewModel();

            var user = await GetCurrentUserAsync();

            var myFacilities = await _context.Facilities
                                        .Include(p => p.User)
                                        .Where(p => p.UserId == user.Id).ToListAsync();
            model.Facilities = myFacilities;
            return View(model);
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities
                .Include(f => f.FacilityType)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FacilityId == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            ViewData["FacilityTypeId"] = new SelectList(_context.FacilityTypes, "FacilityTypeId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacilityId,Name,Address,FacilityTypeId,UserId")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacilityTypeId"] = new SelectList(_context.FacilityTypes, "FacilityTypeId", "Name", facility.FacilityTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", facility.UserId);
            return View(facility);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            ViewData["FacilityTypeId"] = new SelectList(_context.FacilityTypes, "FacilityTypeId", "Name", facility.FacilityTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", facility.UserId);
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacilityId,Name,Address,FacilityTypeId,UserId")] Facility facility)
        {
            if (id != facility.FacilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityExists(facility.FacilityId))
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
            ViewData["FacilityTypeId"] = new SelectList(_context.FacilityTypes, "FacilityTypeId", "Name", facility.FacilityTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", facility.UserId);
            return View(facility);
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities
                .Include(f => f.FacilityType)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FacilityId == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Any(e => e.FacilityId == id);
        }
    }
}
