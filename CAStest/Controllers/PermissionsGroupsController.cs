using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAStest.Data;
using CAStest.Models.ViewModels;

namespace CAStest.Controllers
{
    public class PermissionsGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionsGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PermissionsGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PermissionsGroups.Include(p => p.Contract);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PermissionsGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionsGroup = await _context.PermissionsGroups
                .Include(p => p.Contract)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permissionsGroup == null)
            {
                return NotFound();
            }

            return View(permissionsGroup);
        }

        // GET: PermissionsGroups/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id");
            return View();
        }

        // POST: PermissionsGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PermissionId,ContractId")] PermissionsGroup permissionsGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permissionsGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", permissionsGroup.ContractId);
            return View(permissionsGroup);
        }

        // GET: PermissionsGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionsGroup = await _context.PermissionsGroups.SingleOrDefaultAsync(m => m.Id == id);
            if (permissionsGroup == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", permissionsGroup.ContractId);
            return View(permissionsGroup);
        }

        // POST: PermissionsGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PermissionId,ContractId")] PermissionsGroup permissionsGroup)
        {
            if (id != permissionsGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionsGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionsGroupExists(permissionsGroup.Id))
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
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Id", permissionsGroup.ContractId);
            return View(permissionsGroup);
        }

        // GET: PermissionsGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permissionsGroup = await _context.PermissionsGroups
                .Include(p => p.Contract)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permissionsGroup == null)
            {
                return NotFound();
            }

            return View(permissionsGroup);
        }

        // POST: PermissionsGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permissionsGroup = await _context.PermissionsGroups.SingleOrDefaultAsync(m => m.Id == id);
            _context.PermissionsGroups.Remove(permissionsGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionsGroupExists(int id)
        {
            return _context.PermissionsGroups.Any(e => e.Id == id);
        }
    }
}
