using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAStest.Data;
using CAStest.Models;
using CAStest.Models.AgrementViewModel;
using CAStest.Models.ContractViewModel;
using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CAStest.Controllers
{
    public class Agreement : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public IHostingEnvironment hostingEnvironment;

        public Agreement(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            hostingEnvironment = env;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var contract = from c in _context.Contracts.ToList()
                select c;
            foreach (var a in contract)
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.IsInFavorites = await IsInFavorites(a);
                }
                else
                {
                    ViewBag.IsInFavorites = false;
                }
                
            }
            
            if (!String.IsNullOrEmpty(searchString))
            {
                contract = _context.Contracts.Where(s => s.ContractNumber.Contains(searchString)
                                                         || s.ContractNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Date":
                    contract = contract.OrderBy(s => s.DateOfOffer);
                    break;
                case "date_desc":
                    contract = contract.OrderBy(s => s.DateOfOffer);
                    break;
            }
            ApplicationUser us = await _userManager.GetUserAsync(this.User);
            List<Favorites> fav = _context.Favorites.Where(f => f.UserId == us.Id).ToList();
            List<Contract> con = new List<Contract>();
            foreach (var a in fav)
            {
                Contract ctr = _context.Contracts.FirstOrDefault(c => c.Id == a.ContractId);
                con.Add(ctr);
            }


            ViewBag.Fav = con;

            if (User.Claims.FirstOrDefault(c => c.Value == "Admin") != null)
            {
                ViewBag.Message = "Hello World";
                return View(contract);
            }

            
            return View(contract.Where(i => i.IsBlocked == false));
        }


        private async Task<bool> IsInFavorites(Contract contract)
        {
            ApplicationUser user = await _userManager.GetUserAsync(this.User);

            return 
                User.Identity.IsAuthenticated &&
                _context.Favorites.FirstOrDefault(f => f.UserId == user.Id && f.ContractId == contract.Id) != null;
        }


        public async Task<IActionResult> Favorites()
        {
            ApplicationUser user = await _userManager.GetUserAsync(this.User);
            IEnumerable<Contract> contracts = _context.Favorites.Include(f => f.Contract).Where(f => f.UserId == user.Id).Select(f => f.Contract);

            return View("Favorites", contracts);
        }

        
        [HttpPost]
        public async Task<JsonResult> AddToFavorites(int id)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(this.User);
                Contract contract =  _context.Contracts.FirstOrDefault(c => c.Id == id);
                
                if(!_context.Favorites.ToList().Exists(c => c.ContractId == id))
                {
                    _context.Favorites.Add(new Favorites()
                    {
                        User = user,
                        Contract = contract
                    });
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false
                });
            }
            return Json(new
            {
                success = true
            });
        }

        public string RemoveFromFavorites(string id)
        {
            Favorites favorite = _context.Favorites.FirstOrDefault(c => c.ContractId.ToString() == id);
            _context.Favorites.Remove(favorite);

            _context.SaveChanges();

            return JsonConvert.SerializeObject(true,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailsContractViewModel model = new DetailsContractViewModel();
            model.Contract = await _context.Contracts
                .Include(c => c.ContractFiles)
                .Include(c => c.ContractProperties).ThenInclude(c => c.Property)
                .Include(c => c.Acts).ThenInclude(a => a.ActFiles)
                .Include(c => c.Supplementaries).ThenInclude(s => s.SupplementaryFiles)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (model.Contract == null)
            {
                return NotFound();
            }
            model.Counterparties = _context.ContractCounterparties.Include(s => s.Counterparty).Where(c => c.ContractId == id).Select(s => s.Counterparty).ToList();

            return View(model);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            CreateContractViewModel model = new CreateContractViewModel();
            List<UsersCheckList> list = new List<UsersCheckList>();
            model.Properties = new SelectList(_context.Properties, "Id", "Name");
            model.Individuals = _context.Individuals.Where(i => i.IsBlocked == false).ToList();
            model.IndividualEntrepreneurs = _context.IndividualEntrepreneurs.Where(e => e.IsBlocked == false).ToList();
            model.Companies = _context.Companies.Where(c => c.IsBlocked == false).ToList();
            model.Countries = new SelectList(Country.GetCountryList());

            return View(model);
        }

        public FileResult GetFileStream(string file)
        {
            string fileName = file.Split('\\')[file.Split('\\').Count() - 1];
            FileStream stream = new FileStream(file, FileMode.Open);
            string fileType = "application/octet-stream";
            return File(stream, fileType, fileName);
        }

        public void UploadFiles(Contract contract, List<IFormFile> formFiles)
        {
            if (formFiles != null)
            {
                foreach(var file in formFiles)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/files/contracts/", Path.GetFileName(file.FileName));
                    file.CopyTo(new FileStream(fileName, FileMode.Create));
                    ContractFile contractFile = new ContractFile() { Location = fileName, Name = file.FileName, ContractId = contract.Id };
                    _context.CASFiles.Add(contractFile);
                    _context.SaveChanges();
                }
            }
        }

        public void UploadFiles(Act act, List<ActSuppFile> formFiles)
        {
            if (formFiles != null)
            {
                foreach (var element in formFiles)
                {
                    if(act.ActNumber == element.Number && element.File != null)
                    { 
                        var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/files/acts", Path.GetFileName(element.File.FileName));
                        element.File.CopyTo(new FileStream(fileName, FileMode.Create));
                        ActFile actFile = new ActFile() { Location = fileName, Name = element.File.FileName, ActId = act.Id };
                        _context.CASFiles.Add(actFile);
                        _context.SaveChanges();
                    }
                }
            }
        }
        public void UploadFiles(Supplementary supplementary, List<ActSuppFile> formFiles)
        {
            if (formFiles != null)
            {
                foreach (var element in formFiles)
                {
                    if (supplementary.SupplementaryNumber == element.Number && element.File != null)
                    {
                        var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/files/supplementaries", Path.GetFileName(element.File.FileName));
                        element.File.CopyTo(new FileStream(fileName, FileMode.Create));
                        SupplementaryFile suppFile = new SupplementaryFile() { Location = fileName, Name = element.File.FileName, SupplementaryId = supplementary.Id };
                        _context.CASFiles.Add(suppFile);
                        _context.SaveChanges();
                    }
                }
            }
        }
        public void UploadFiles(Supplementary supplementary, List<IFormFile> formFiles)
        {
            if (formFiles.Count() != 0)
            {
                foreach(var formFile in formFiles)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath + "/files/supplementaries", Path.GetFileName(formFile.FileName));
                    formFile.CopyTo(new FileStream(fileName, FileMode.Create));
                    SupplementaryFile suppFile = new SupplementaryFile() { Location = fileName, Name = formFile.FileName, SupplementaryId = supplementary.Id };
                    _context.CASFiles.Add(suppFile);
                    _context.SaveChanges();
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> Prolongate(string prolDate, string prolNumber, string contractId)
        {
            List<IFormFile> file = Request.Form.Files.ToList();
            Contract contract = _context.Contracts.FirstOrDefault(c => c.Id == Convert.ToInt32(contractId));
            if(contract.ContractTime < Convert.ToDateTime(prolDate))
            {
                Supplementary supp = new Supplementary()
                {
                    ContractId = Convert.ToInt32(contractId),
                    SupplementaryNumber = prolNumber
                };
                _context.Supplementaries.Add(supp);
                _context.SaveChanges();
                contract.ContractTime = Convert.ToDateTime(prolDate);
                _context.Contracts.Update(contract);
                _context.SaveChanges();
                UploadFiles(supp, file);

                
                return Json(new
                {
                    success = true
                });
            }
            return Json(new
            {
                success = false
            });
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContractViewModel model)
        {
            Contract contract = new Contract()
            {
                ContractNumber = model.ContractNumber,
                ContractTime = model.ContractTime,
                DateOfOffer = model.DateOfOffer,
                Country = model.Country,
                Autorolongation = model.CheckAutorolongation
            };
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            UploadFiles(contract, model.contractFiles);

            if(model.actNumbers != null)
            {
                foreach (var actNumber in model.actNumbers)
                {
                    if(actNumber != "$$")
                    {
                        Act act = new Act() { ActNumber = actNumber, ContractId = contract.Id };
                        _context.Acts.Add(act);
                        _context.SaveChanges();
                        UploadFiles(act, model.actFiles);
                    }
                }
            }
            if (model.supplementaryNumbers != null)
            {
                foreach (var suppNumber in model.supplementaryNumbers)
                {
                    if(suppNumber != "$$")
                    {
                        Supplementary supp = new Supplementary() { SupplementaryNumber = suppNumber, ContractId = contract.Id };
                        _context.Supplementaries.Add(supp);
                        _context.SaveChanges();
                        UploadFiles(supp, model.suppFiles);
                    }
                }
            }


            int contractId = contract.Id;
            if (model.propertyId != null && model.valueProperty != null && model.valueProperty.Any() && model.propertyId.Any())
            {
                for (int i = 0; i < model.valueProperty.Count; i++)
                {
                    Property test = _context.Properties.FirstOrDefault(u => u.Id == model.propertyId[i]);
                    Contract contractTest = _context.Contracts.FirstOrDefault(u => u.ContractNumber == contract.ContractNumber);
                    _context.ContractPropertieses.Add(new ContractProperties()
                    {
                        Value = model.valueProperty[i],
                        PropertyId = test.Id,
                        ContractId = contractTest.Id
                    });
                    _context.SaveChanges();
                }
            }
            if (model.CounterpartiesId != null)
            {
                for (int i = 0; i < model.CounterpartiesId.Count; i++)
                {
                    _context.ContractCounterparties.Add(new ContractCounterparty()
                    {
                        ContractId = _context.Contracts.FirstOrDefault(c => c.Id == contract.Id).Id,
                        CounterpartyId = model.CounterpartiesId[i]
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EditContractViewModel model = new EditContractViewModel()
            {
                Contract = await _context.Contracts
                .SingleOrDefaultAsync(m => m.Id == id),
                Counterparties = _context.ContractCounterparties.Include(s => s.Counterparty).Where(c => c.ContractId == id).Select(s => s.Counterparty).ToList(),
                Properties = new SelectList(_context.Properties, "Id", "Name"),
                ContractProperties = _context.ContractPropertieses.Include(c => c.Property).Where(c => c.ContractId == id).ToList()
            };
            model.Individuals = _context.Individuals.Where(i => i.IsBlocked == false).ToList();
            model.IndividualEntrepreneurs = _context.IndividualEntrepreneurs.Where(e => e.IsBlocked == false).ToList();
            model.Companies = _context.Companies.Where(c => c.IsBlocked == false).ToList();
            ViewData["Country"] = new SelectList(Country.GetCountryList());
            return View(model);
        }

        // POST: Contracts/Edit/5
        [HttpPost]
        public string DeleteCounterparty(string _counterpartyId, string _contractId)
        {
            if(_counterpartyId != null && _contractId != null)
            {
                ContractCounterparty contractCounterparty = _context.ContractCounterparties.FirstOrDefault(c => c.CounterpartyId.ToString() == _counterpartyId 
                && c.ContractId.ToString() == _contractId);
                _context.ContractCounterparties.Remove(contractCounterparty);
                _context.SaveChanges();
            }
            return JsonConvert.SerializeObject(true,
                new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditContractViewModel model)
        {
            if (id != model.Contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   

                    if (model.CounterpartiesId != null)
                    {
                        for (int i = 0; i < model.CounterpartiesId.Count; i++)
                        {
                            ContractCounterparty contractCounterparty = new ContractCounterparty()
                            {
                                ContractId = id,
                                CounterpartyId = model.CounterpartiesId[i]
                            };
                            if (!_context.ContractCounterparties.ToList().Exists(c => c.ContractId == contractCounterparty.ContractId 
                            && c.CounterpartyId == contractCounterparty.CounterpartyId))
                                {
                                    _context.ContractCounterparties.Add(contractCounterparty); 
                                    await _context.SaveChangesAsync();
                                }
                           
                        }
                    }
                    _context.Update(model.Contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(model.Contract.Id))
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
            return View(model.Contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }

        public IActionResult Close(int id)
        {
            Contract contract = _context.Contracts.FirstOrDefault(c => c.Id == id);
            contract.IsBlocked = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Open(int id)
        {
            Contract contract = _context.Contracts.FirstOrDefault(c => c.Id == id);
            contract.IsBlocked = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
