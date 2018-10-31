using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAStest.Data;
using CAStest.Models;
using CAStest.Models.IndividualsViewModel;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CAStest.Controllers
{
    public class IndividualsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndividualsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Individuals
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var individual = from c in _context.Individuals.Where(i=> i.Position != null).ToList()
                             select c;
            if (!String.IsNullOrEmpty(searchString))  
            {
                individual = _context.Individuals.Where(s => s.Fullname.Contains(searchString)
                                                        || s.Country.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    individual = individual.OrderBy(s => s.Fullname);
                    break;

            }

            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                return View(individual);
            }
            return View(individual.Where(i => i.IsBlocked == false));
            
        }

        public async Task<IActionResult> Cards(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var individual = from c in _context.Individuals.ToList()
                             select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                individual = _context.Individuals.Where(s => s.Fullname.Contains(searchString)
                                                        || s.Country.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    individual = individual.OrderBy(s => s.Fullname);
                    break;

            }

            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                return View(individual);
            }
            return View("Cards", individual);

        }

        // GET: Individuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individuals
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (individual == null)
            {
                return NotFound();
            }
            DetailsViewModel model = new DetailsViewModel();
            model.Individual = individual;
            model.Contacts = _context.Contacts.Include(c => c.ContactType).Where(c => c.CounterpartyId == id).ToList();
            return View(model);
            
        }

        // GET: Individuals/Create
        public IActionResult Create()
        {
            var model = new CreateIndividualViewModel();
            model.Companies = new SelectList(_context.Companies.ToList(), "Id", "CompanyName");
            model.Countries = new SelectList(Country.GetCountryList());
            return View(model);
        }

        // POST: Individuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateIndividualViewModel model)
        {
            if (model != null)
            {
                Individual ind = new Individual()
                {
                    Address = model.Address,
                    CompanyId = model.CompanyId,
                    DateOfBirth = model.DateOfBirth,
                    Fullname = model.Fullname,
                    Inn = model.Inn,
                    PassportData = model.PassportData,
                    Position = model.Position,
                    Country = model.Country
                };
                _context.Individuals.Add(ind);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Individuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individuals.SingleOrDefaultAsync(m => m.Id == id);
            if (individual == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies.ToList(), "Id", "CompanyName", individual.CompanyId);
            ViewData["Country"] = new SelectList(Country.GetCountryList());

            return View(individual);
        }

        // POST: Individuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fullname,Inn,DateOfBirth,PassportData,Address,Position,CompanyId,Id, Country")] Individual individual)
        {
            if (id != individual.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(individual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualExists(individual.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = id });
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Discriminator", individual.CompanyId);
            return View(individual);
        }

        // GET: Individuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individuals
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (individual == null)
            {
                return NotFound();
            }

            return View(individual);
        }

        // POST: Individuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var individual = await _context.Individuals.SingleOrDefaultAsync(m => m.Id == id);
            _context.Individuals.Remove(individual);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualExists(int id)
        {
            return _context.Individuals.Any(e => e.Id == id);
        }

        public IActionResult Block(int id)
        {
            Individual individual = _context.Individuals.FirstOrDefault(c => c.Id == id);
            individual.IsBlocked = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UnBlock(int id)
        {
            Individual individual = _context.Individuals.FirstOrDefault(c => c.Id == id);
            individual.IsBlocked = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckINN(int inn)
        {
            Individual individual = _context.Individuals.FirstOrDefault(u => u.Inn == inn);
            return Json(individual == null);
        }
    }
}
