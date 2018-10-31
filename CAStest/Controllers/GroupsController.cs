using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAStest.Data;
using CAStest.Models;
using CAStest.Models.GroupUserViewModel;

namespace CAStest.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            IndexUserGroupViewModel model = new IndexUserGroupViewModel();
            if (id == null)
            {
                return NotFound();
            }
            Group group =_context.Groups.Include(x=>x.Groups).FirstOrDefault(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            model.GroupName = group.GroupName;
            model.Id = group.Id;          
            //model.UserId = group.Groups?.Select(x => x.UserId).First().ToString();
            var us = _context.Users.Where(x => x.Id.Contains(x.UserName)).ToList();
            model.UserName = us.ToString();
            //model.Users = new SelectList(_context.Users.Select(z=>z.Fullname).ToList());
            model.Users = new MultiSelectList(_context.Users, "Id", "UserName");

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.Users = new MultiSelectList(_context.Users, "Id", "Fullname");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndexUserGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Group
                {
                    GroupName = model.GroupName,
                    Groups = model.UserId.Select(uId => new UnionUserGroup{UserId = uId}).ToList()
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Users = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName")] Group @group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.Id))
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
            return View(group);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
