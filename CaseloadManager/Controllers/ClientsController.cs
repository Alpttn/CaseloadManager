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
               .Where(c => c.ClientId == id).ToListAsync();

            viewModel.Client = await _context.Clients
                    .Include(c => c.StatusType)
                    .Include(c => c.User)
                    .Include(c => c.Facility)
                    .FirstOrDefaultAsync(c => c.ClientId == id);

            ViewBag.HasAssessment = "true";

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

            var client = new Client();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            client = await _context.Clients.FindAsync(id);
            
            if (client == null)
            {
                return NotFound();
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);

            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,FirstInitial,LastName,Birthdate,Diagnosis,SessionsPerWeek,StatusTypeId,FacilityTypeId,UserId")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            client.User = user;
            client.UserId = user.Id;
            //ModelState.Remove("Client.User");
            //ModelState.Remove("Client.UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
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
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            return View(client);
        }
        //Edit status method
        // GET: Clients/Edit/5
        public async Task<IActionResult> EditClientStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);

            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClientStatus(int id, [Bind("ClientId,FirstInitial,LastName,Birthdate,Diagnosis,SessionsPerWeek,StatusTypeId,FacilityTypeId,UserId")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ModelState.Remove("Client.UserId");
                    _context.Update(client);
                    await _context.SaveChangesAsync();
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
            }
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "Name", client.StatusTypeId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", client.UserId);
            return View(client);
        }

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
