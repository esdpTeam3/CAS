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
using System.Globalization;
using System.Threading;

using Microsoft.AspNetCore.Identity;
using CAStest.Models.CompanyViewModel;

namespace CAStest.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Company
        public  IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var company = from c in _context.Companies.ToList()
                select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                company = _context.Companies.Where(s => s.CompanyName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    company = company.OrderBy(s => s.CompanyName);
                    break;
                
            }

            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                return View(company);
            }

            return View(company.Where(i => i.IsBlocked == false));
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            var model = new CreateCompanyViewModel();
            model.Countries = new SelectList(Country.GetCountryList());
            return View(model);
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCompanyViewModel model)
        {
            Company company = new Company();
            if (model!= null)
            {
                company = new Company()
                {
                    CompanyName = model.CompanyName,
                    ShortName = model.ShortName,
                    CompanyINN = model.CompanyINN,
                    CheckingAccount = model.CheckingAccount,
                    IndividualAddress = model.IndividualAddress,
                    LegalAddress = model.LegalAddress,
                    BIC = model.BIC,
                    Country = model.Country,
                    MailAddress = model.MailAddress
                };
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(Country.GetCountryList());
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyName,ShortName,CompanyINN,IndividualAddress,LegalAddress,MailAddress,BIC,CheckingAccount,Id, Country")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details" , new { id = id });
            }
            return View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        public IActionResult Cards(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var company = from c in _context.Companies.ToList()
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                company = _context.Companies.Where(s => s.CompanyName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    company = company.OrderBy(s => s.CompanyName);
                    break;

            }

            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                return View(company);
            }

            return View(company.Where(i => i.IsBlocked == false));
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
        
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckINN(int inn)
        {
            Company company = _context.Companies.FirstOrDefault(c => c.CompanyINN == inn);
            return Json(company == null);
        }

        public IActionResult Block(int id)
        {
            Company company = _context.Companies.FirstOrDefault(c => c.Id == id);
            company.IsBlocked = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UnBlock(int id)
        {
            Company company = _context.Companies.FirstOrDefault(c => c.Id == id);
            company.IsBlocked = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
