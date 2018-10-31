using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAStest.Data;
using CAStest.Models;
using CAStest.Models.IndividualEntrepreneurViewModels;
using System.Globalization;
using System.Threading;

namespace CAStest.Controllers
{
    public class IndividualEntrepreneurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndividualEntrepreneurController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                var entrepreneuer = _context.IndividualEntrepreneurs.Include(i => i.Company).ToList();
                return View(entrepreneuer);
            }
            else
            {
                var individualEntrepreneurs = _context.IndividualEntrepreneurs.Where(e => e.IsBlocked == false).Include(i => i.Company).ToList();
                return View(individualEntrepreneurs);
            }
        }

        public async Task<IActionResult> Cards()
        {
            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                var entrepreneuer = _context.IndividualEntrepreneurs.Include(i => i.Company).ToList();
                return View(entrepreneuer);
            }
            else
            {
                var individualEntrepreneurs = _context.IndividualEntrepreneurs.Where(e => e.IsBlocked == false).Include(i => i.Company).ToList();
                return View(individualEntrepreneurs);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individualEntrepreneur = await _context.IndividualEntrepreneurs
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (individualEntrepreneur == null)
            {
                return NotFound();
            }
            DetailsViewModel model = new DetailsViewModel();
            model.IndividualEntrepreneur = individualEntrepreneur;
            model.Contacts = _context.Contacts.Include(c => c.ContactType).Where(c => c.CounterpartyId == id).ToList();
            return View(model);
        }
        
        public IActionResult Create()
        {
            var model = new CreateIEViewModel();
            model.Countries = new SelectList(Country.GetCountryList());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateIEViewModel model)
        {
            if (model != null)
            {
                IndividualEntrepreneur IE = new IndividualEntrepreneur()
                {
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Fullname = model.Fullname,
                    Inn = model.Inn,
                    PassportData = model.PassportData,
                    Country = model.Country
                };
                _context.Individuals.Add(IE);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individualEntrepreneur = await _context.IndividualEntrepreneurs.SingleOrDefaultAsync(m => m.Id == id);
            if (individualEntrepreneur == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(Country.GetCountryList());
            return View(individualEntrepreneur);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fullname,Inn,DateOfBirth,PassportData,Address,Country,Id")] IndividualEntrepreneur individualEntrepreneur)
        {
            if (id != individualEntrepreneur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(individualEntrepreneur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualEntrepreneurExists(individualEntrepreneur.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Discriminator", individualEntrepreneur.CompanyId);
            return View(individualEntrepreneur);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individualEntrepreneur = await _context.IndividualEntrepreneurs
                .Include(i => i.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (individualEntrepreneur == null)
            {
                return NotFound();
            }

            return View(individualEntrepreneur);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var individualEntrepreneur = await _context.IndividualEntrepreneurs.SingleOrDefaultAsync(m => m.Id == id);
            _context.IndividualEntrepreneurs.Remove(individualEntrepreneur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualEntrepreneurExists(int id)
        {
            return _context.IndividualEntrepreneurs.Any(e => e.Id == id);
        }

        public IActionResult Block(int id)
        {
            IndividualEntrepreneur individualEntrepreneur = _context.IndividualEntrepreneurs.FirstOrDefault(c => c.Id == id);
            individualEntrepreneur.IsBlocked = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UnBlock(int id)
        {
            IndividualEntrepreneur individualEntrepreneur = _context.IndividualEntrepreneurs.FirstOrDefault(c => c.Id == id);
            individualEntrepreneur.IsBlocked = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
