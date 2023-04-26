using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Data;
using MobileRecharge.Models;

namespace MobileRecharge.Controllers
{
    [Authorize]
    public class MobilePlanModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePlanModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePlanModels
        public async Task<IActionResult> Index()
        {
            return _context.MobilePlanModel != null ?
                        View(await _context.MobilePlanModel.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.MobilePlanModel'  is null.");
        }

        // GET: MobilePlanModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MobilePlanModel == null)
            {
                return NotFound();
            }

            var mobilePlanModel = await _context.MobilePlanModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobilePlanModel == null)
            {
                return NotFound();
            }

            return View(mobilePlanModel);
        }

        // GET: MobilePlanModels/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MobilePlanModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanName,ServiceProvider,MonthsOfValidity,AmountToBePaid,DataPlanCovered")] MobilePlanModel mobilePlanModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePlanModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mobilePlanModel);
        }

        // GET: MobilePlanModels/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MobilePlanModel == null)
            {
                return NotFound();
            }

            var mobilePlanModel = await _context.MobilePlanModel.FindAsync(id);
            if (mobilePlanModel == null)
            {
                return NotFound();
            }
            return View(mobilePlanModel);
        }

        // POST: MobilePlanModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanName,ServiceProvider,MonthsOfValidity,AmountToBePaid,DataPlanCovered")] MobilePlanModel mobilePlanModel)
        {
            if (id != mobilePlanModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilePlanModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilePlanModelExists(mobilePlanModel.Id))
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
            return View(mobilePlanModel);
        }

        // GET: MobilePlanModels/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MobilePlanModel == null)
            {
                return NotFound();
            }

            var mobilePlanModel = await _context.MobilePlanModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobilePlanModel == null)
            {
                return NotFound();
            }

            return View(mobilePlanModel);
        }

        // POST: MobilePlanModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MobilePlanModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MobilePlanModel'  is null.");
            }
            var mobilePlanModel = await _context.MobilePlanModel.FindAsync(id);
            if (mobilePlanModel != null)
            {
                _context.MobilePlanModel.Remove(mobilePlanModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Recharge(int planId)
        {
            return View();
        }

        private bool MobilePlanModelExists(int id)
        {
            return (_context.MobilePlanModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
