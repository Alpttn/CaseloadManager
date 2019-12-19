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

namespace CaseloadManager.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public ClientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var clients = _context.Clients.Include(c => c.StatusType)
                .Include(c => c.User)
                .Include(c => c.Facility)
                .Where(c => c.UserId == user.Id);

            return View(await clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new ClientEditViewModel();

            viewModel.ClientAssessments = await _context.ClientAssessments
               .Include(c => c.Client)
               .ThenInclude(Client => Client.StatusType)
               .Include(c => c.Client)
               .ThenInclude(Client => Client.User)
               .Include(c => c.Client)
               .ThenInclude(Client => Client.Facility)
               .Include(c => c.Assessment)
               .Include(c => c.Client)
               .ThenInclude(Client => Client.Goals)
               .Include(c => c.Client)
               .ThenInclude(Client => Client.TherapySessions)
               .Where(c => c.ClientId == id).ToListAsync();

            viewModel.Client = await _context.Clients
                    .Include(c => c.StatusType)
                    .Include(c => c.User)
                    .Include(c => c.Facility)
                    .FirstOrDefaultAsync(c => c.ClientId == id);


            ViewBag.HasAssessment = "true";
            ViewBag.HasGoal = "true";

            if (viewModel.ClientAssessments.Count == 0)
            {
                ViewBag.HasAssessment = "false";
                viewModel.Client = await _context.Clients
                    .Include(c => c.StatusType)
                    .Include(c => c.User)
                    .Include(c => c.Facility)
                    .FirstOrDefaultAsync(c => c.ClientId == id);
            }

            return View(viewModel);
        }

        // GET: Clients/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name");
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");

            var viewModel = new ClientCreateViewModel()
            {
                Facilities = await _context.Facilities.Where(c => c.UserId == user.Id).ToListAsync()
            };

            ViewData["FacilityId"] = new SelectList(viewModel.Facilities, "FacilityId", "Name");
            return View(viewModel);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateViewModel viewModel)
        {
            var user = await GetCurrentUserAsync();
            ModelState.Remove("Client.User");
            ModelState.Remove("Client.UserId");
            viewModel.Client.User = user;
            viewModel.Client.UserId = user.Id;


            if (ModelState.IsValid)
            {
                //viewModel.Client.UserId = user.Id;
                _context.Add(viewModel.Client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", viewModel.Client.StatusTypeId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            viewModel.Facilities = await _context.Facilities.Where(c => c.UserId == user.Id).ToListAsync();
            ViewData["FacilityId"] = new SelectList(viewModel.Facilities, "FacilityId", "Name");

            return View(viewModel);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var viewModel = new ClientInformationEditViewModel()
            {
                Facilities = await _context.Facilities.Where(c => c.UserId == user.Id).ToListAsync()
            };
            viewModel.Client = await _context.Clients.FindAsync(id);

            if (viewModel.Client == null)
            {
                return NotFound();
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", viewModel.Client.StatusTypeId);
            ViewData["FacilityId"] = new SelectList(viewModel.Facilities, "FacilityId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", viewModel.Client.UserId);
            return View(viewModel);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ClientInformationEditViewModel viewModel)
        {
            if (id != viewModel.Client.ClientId)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            viewModel.Client.User = user;
            viewModel.Client.UserId = user.Id;
            viewModel.Client.ClientAssessmets = await _context.ClientAssessments.Where(c => c.ClientId == viewModel.Client.ClientId).ToListAsync();
            //ModelState.Remove("User");
            //ModelState.Remove("UserId");


            //if (ModelState.IsValid)
            //{
                try
                {
                    if (viewModel.Client.ClientAssessmets.Count() == 0 && viewModel.Client.StatusTypeId == 3)
                    {
                        TempData["ErrorMessage"] = "Client needs to be assessed before they can be eligible.";
                        ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", viewModel.Client.StatusTypeId);
                        return View(viewModel);
                    }
                    else
                    {
                        _context.Update(viewModel.Client);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(viewModel.Client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            //}
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", viewModel.Client.StatusTypeId);
            //ViewData["FacilityId"] = new SelectList(viewModel.Facilities, "FacilityId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", viewModel.Client.UserId);
                return RedirectToAction(nameof(Index));
            //return View(viewModel);
        }
        //Edit status method
        // GET: Clients/Discharge/5
        //public async Task<IActionResult> Discharge(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    var client = new Client();
        //    client = await _context.Clients.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }
        //    //ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);

        //    //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
        //    return View(client);
        //}

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Discharge(int id)
        {
            //if (ModelState.IsValid)
            //{
            var client = new Client();
            client = await _context.Clients.FindAsync(id);
            try
            {
                //ModelState.Remove("Client.UserId");
                if (client.StatusTypeId != 5)
                {
                    client.StatusTypeId = 5;
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.ClientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            //return View(client);
        }

        //GET: Clients/Discharge/5


        //public async Task<IActionResult> Discharge(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var dischargeStatus = await _context.Clients
        //        .FirstOrDefaultAsync(c => c.ClientId == id);
        //    if (dischargeStatus.StatusTypeId != 5)
        //    {
        //        var dischargeClient = dischargeStatus.StatusTypeId == 5;
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(dischargeStatus);
        //}

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.StatusType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
