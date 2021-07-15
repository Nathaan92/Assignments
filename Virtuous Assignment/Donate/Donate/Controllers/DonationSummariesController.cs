using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Donate.Models;

namespace Donate.Controllers
{
    public class DonationSummariesController : Controller
    {
        private readonly DonationsDbContext _context;

        public DonationSummariesController(DonationsDbContext context)
        {
            _context = context;
        }

        // GET: DonationSummaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonationSummaries.ToListAsync());
        }

        // GET: DonationSummaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSummary = await _context.DonationSummaries
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donationSummary == null)
            {
                return NotFound();
            }

            return View(donationSummary);
        }

        // GET: DonationSummaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonationSummaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationId,DonationDate,FirstName,LastName,Email,DonationType,DonationCashValue")] DonationSummary donationSummary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donationSummary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donationSummary);
        }

        // GET: DonationSummaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSummary = await _context.DonationSummaries.FindAsync(id);
            if (donationSummary == null)
            {
                return NotFound();
            }
            return View(donationSummary);
        }

        // POST: DonationSummaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationId,DonationDate,FirstName,LastName,Email,DonationType,DonationCashValue")] DonationSummary donationSummary)
        {
            if (id != donationSummary.DonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationSummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationSummaryExists(donationSummary.DonationId))
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
            return View(donationSummary);
        }

        // GET: DonationSummaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSummary = await _context.DonationSummaries
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (donationSummary == null)
            {
                return NotFound();
            }

            return View(donationSummary);
        }

        // POST: DonationSummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationSummary = await _context.DonationSummaries.FindAsync(id);
            _context.DonationSummaries.Remove(donationSummary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationSummaryExists(int id)
        {
            return _context.DonationSummaries.Any(e => e.DonationId == id);
        }
    }
}
