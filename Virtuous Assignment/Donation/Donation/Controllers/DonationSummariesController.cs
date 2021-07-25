using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Donation.Data;
using Donation.Models;

namespace Donation.Controllers
{
    public class DonationSummariesController : Controller
    {
        private readonly DonationSummaryContext _context;

        public DonationSummariesController(DonationSummaryContext context)
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Create([Bind("ID,DonorsName,Email,DonationDate,DonationType,DonationCashValue")] DonationSummary donationSummary)
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,DonorsName,Email,DonationDate,DonationType,DonationCashValue")] DonationSummary donationSummary)
        {
            if (id != donationSummary.ID)
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
                    if (!DonationSummaryExists(donationSummary.ID))
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
                .FirstOrDefaultAsync(m => m.ID == id);
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
            return _context.DonationSummaries.Any(e => e.ID == id);
        }
        public IActionResult DonationMetrics()
        {
            //Query cash donations for count for views piechart
            //Undeclared type variable = Donation Summaries table where doantion type is == to cash, count each instance
            var cashPieChart = _context.DonationSummaries.Where(a => a.DonationType == "Cash").Count();
            ViewBag.cashPieChart = cashPieChart;

            //Query time donations for count for views piechart
            //Undeclared type variable = Donation Summaries table where doantion type is == to time, count each instance
            var timePieChart = _context.DonationSummaries.Where(a => a.DonationType == "Time").Count();
            ViewBag.timePieChart = timePieChart;

            //Query item donations for count for views piechart
            //Undeclared type variable = Donation Summaries table where doantion type is == to count, count each instance
            var itemPieChart = _context.DonationSummaries.Where(a => a.DonationType == "Item").Count();
            ViewBag.itemPieChart = itemPieChart;

            //Query group by month, add total of donationcash value, to list. Year was included to add yearly charts.
            //undeclared type variable = Doantion Summaries table select donation date by year, month and donation cash value, group by year and month, sum donation cash value, add to list
            var monthlyDonations = _context.DonationSummaries.Select(d => new { d.DonationDate.Year, d.DonationDate.Month, d.DonationCashValue }).GroupBy(x => new { x.Year, x.Month }, (key, group) => new { donationtotal = group.Sum(dt => dt.DonationCashValue) }).ToList();

            //List to recieve decimal values from foreach loop
            List<Decimal> donationSummaryList = new List<Decimal>();
            //Foreach loop to convert from anonymous type list to decimal type values
            //foreach variable in monthlyDonations List add doantion total to donationsummaries list
            foreach (var monthlyDonation in monthlyDonations)
            {
                donationSummaryList.Add(monthlyDonation.donationtotal);
            }
            //Serialization of decimal list to send to views script chart
            ViewBag.donationSummaryList = Newtonsoft.Json.JsonConvert.SerializeObject(donationSummaryList);

            //Query sum of donationcashvalue to date
            //undeclared type variable = Donation Summaries table sum of doantion cash value column
            var totalDonationsToDate = _context.DonationSummaries.Sum(a => a.DonationCashValue);
            ViewBag.totalDonationsToDate = totalDonationsToDate;

            //Query max of donationcashvalue to date
            //undeclared varialbe = Donation Summaries largest value in doantion cash value table 
            var largestDonationToDate = _context.DonationSummaries.Max(a => a.DonationCashValue);
            ViewBag.largestDonationToDate = largestDonationToDate;

            return View();
        }
    }
}
