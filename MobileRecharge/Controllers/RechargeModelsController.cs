using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Data;
using MobileRecharge.Models;

namespace MobileRecharge.Controllers
{
	[Authorize]
	public class RechargeModelsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public RechargeModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: RechargeModels
		public async Task<IActionResult> Index()
		{
			if (_context.RechargeModel == null)
			{
				return Problem("Entity set 'ApplicationDbContext.RechargeModel'  is null.");
			}
			List<RechargeModel> model;
			if (User.IsInRole("admin"))
			{
				model = await _context.RechargeModel.ToListAsync();
			}
			else
			{
				model = await _context.RechargeModel.Where(model => model.UserId == _userManager.GetUserId(User)).ToListAsync();
			}
			model.ForEach(model =>
			{
				model.MobilePlan = _context.MobilePlanModel.Find(model.PlanId);
			});
			return View(model);
		}

		// GET: RechargeModels/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.RechargeModel == null)
			{
				return NotFound();
			}

			var rechargeModel = await _context.RechargeModel
				.FirstOrDefaultAsync(m => m.Id == id);
			if (rechargeModel == null)
			{
				return NotFound();
			}
			rechargeModel.MobilePlan = _context.MobilePlanModel.Find(rechargeModel.PlanId);
			return View(rechargeModel);
		}

		// GET: RechargeModels/Recharge/id
		public IActionResult Recharge(int id)
		{
			return View(new RechargeModel()
			{
				PlanId = id,
			});
		}

		// GET: RechargeModels/Payment?id=&phoneNumber=
		public IActionResult Payment(int id, string phoneNumber)
		{
			return View(new RechargeModel()
			{
				PlanId = id,
				PhoneNumber = phoneNumber,
				MobilePlan = _context.MobilePlanModel.Find(id)
			});
		}

		// POST: RechargeModels/Payment/id
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Payment(int id, [Bind("PlanId", "PhoneNumber")] RechargeModel rechargeModel)
		{
			ModelState.Remove("UserId");
			ModelState.Remove("User");
			ModelState.Remove("MobilePlan");
			ModelState.Remove("Date");
			if (ModelState.IsValid)
			{
				rechargeModel.UserId = _userManager.GetUserId(User);
				rechargeModel.Date = DateTime.Now;
				_context.Add(rechargeModel);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(rechargeModel);
		}

		// GET: RechargeModels/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.RechargeModel == null)
			{
				return NotFound();
			}

			var rechargeModel = await _context.RechargeModel.FindAsync(id);
			if (rechargeModel == null)
			{
				return NotFound();
			}
			rechargeModel.MobilePlan = _context.MobilePlanModel.Find(rechargeModel.PlanId);
			return View(rechargeModel);
		}

		// POST: RechargeModels/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber")] RechargeModel rechargeModel)
		{
			if (id != rechargeModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(rechargeModel);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RechargeModelExists(rechargeModel.Id))
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
			rechargeModel.MobilePlan = _context.MobilePlanModel.Find(rechargeModel.PlanId);
			return View(rechargeModel);
		}

		// GET: RechargeModels/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.RechargeModel == null)
			{
				return NotFound();
			}

			var rechargeModel = await _context.RechargeModel
				.FirstOrDefaultAsync(m => m.Id == id);
			if (rechargeModel == null)
			{
				return NotFound();
			}
			rechargeModel.MobilePlan = _context.MobilePlanModel.Find(rechargeModel.PlanId);
			return View(rechargeModel);
		}

		// POST: RechargeModels/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.RechargeModel == null)
			{
				return Problem("Entity set 'ApplicationDbContext.RechargeModel'  is null.");
			}
			var rechargeModel = await _context.RechargeModel.FindAsync(id);
			if (rechargeModel != null)
			{
				_context.RechargeModel.Remove(rechargeModel);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool RechargeModelExists(int id)
		{
			return (_context.RechargeModel?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
