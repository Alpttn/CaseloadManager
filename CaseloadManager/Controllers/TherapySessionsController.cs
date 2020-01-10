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
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Configuration;

namespace CaseloadManager.Controllers
{
    [Authorize]
    public class TherapySessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public TherapySessionsController(ApplicationDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }
        private readonly IConfiguration _config;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: TherapySessions
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var therapySessions = from t in _context.TherapySessions.Include(t => t.Client).Where(t => t.Client.UserId == user.Id)
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
            List<Client> clientsWithTherapySessions = new List<Client>();

            using SqlConnection conn = Connection;
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                var start = DateTime.Now.StartOfWeek();
                var end = DateTime.Now.EndOfWeek();
                cmd.CommandText = @"
                        SELECT *
                        FROM Clients c
                        Cross APPLY
                        (
                        SELECT * FROM TherapySessions ts
                        WHERE c.ClientId = ts.ClientId AND ts.Date >= @Start AND ts.Date <= @End AND c.StatusTypeId = 3
                        ) ct 
                        ";
                cmd.Parameters.Add(new SqlParameter("@Start", start));
                cmd.Parameters.Add(new SqlParameter("@End", end));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client
                    {
                        ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                        FirstInitial = reader.GetString(reader.GetOrdinal("FirstInitial")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Birthdate = reader.GetDateTime(reader.GetOrdinal("Birthdate")),
                        Diagnosis = reader.GetString(reader.GetOrdinal("Diagnosis")),
                        SessionsPerWeek = reader.GetInt32(reader.GetOrdinal("SessionsPerWeek")),
                        StatusTypeId = reader.GetInt32(reader.GetOrdinal("StatusTypeId")),
                        FacilityId = reader.GetInt32(reader.GetOrdinal("FacilityId")),
                        UserId = reader.GetString(reader.GetOrdinal("UserId"))
                    };

                    clientsWithTherapySessions.Add(client);
                }

                reader.Close();



                var user = await _userManager.GetUserAsync(HttpContext.User);
                var clients = _context.Clients
                    .Include(c => c.Facility)
                    .Include(c => c.User)
                    .Where(c => c.UserId == user.Id && c.StatusTypeId == 3);
                
                List<ClientAttendanceRowViewModel> FinalClientAttendanceList = new List<ClientAttendanceRowViewModel>();
                foreach (Client client in clients)
                {
                    var foundClient = clientsWithTherapySessions.Find(c => c.ClientId == client.ClientId);
                var clientRow = new ClientAttendanceRowViewModel();
                    if (foundClient == null)
                    {
                         clientRow.Client = new Client
                        {
                            ClientId = client.ClientId,
                            FirstInitial = client.FirstInitial,
                            LastName = client.LastName,
                            Birthdate = client.Birthdate,
                            Diagnosis = client.Diagnosis,
                            SessionsPerWeek = client.SessionsPerWeek,
                            StatusTypeId = client.StatusTypeId,
                            FacilityId = client.FacilityId,
                            UserId = client.UserId
                        };

                        clientRow.SessionsHad = 0;
                        //add each row to a list
                        FinalClientAttendanceList.Add(clientRow);
                    } else
                    {
                        clientRow.Client = new Client
                        {
                            ClientId = client.ClientId,
                            FirstInitial = client.FirstInitial,
                            LastName = client.LastName,
                            Birthdate = client.Birthdate,
                            Diagnosis = client.Diagnosis,
                            SessionsPerWeek = client.SessionsPerWeek,
                            StatusTypeId = client.StatusTypeId,
                            FacilityId = client.FacilityId,
                            UserId = client.UserId
                        };
                        List<Client> therapySessionsCount = new List<Client>();
                        foreach (var currentClient in clientsWithTherapySessions)
                        {
                            if (currentClient.ClientId == clientRow.Client.ClientId)
                            {
                                therapySessionsCount.Add(currentClient);
                            }
                        }
                        clientRow.SessionsHad = therapySessionsCount.Count();
                        FinalClientAttendanceList.Add(clientRow);
                    }
                }

                return View(FinalClientAttendanceList);
            }
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
