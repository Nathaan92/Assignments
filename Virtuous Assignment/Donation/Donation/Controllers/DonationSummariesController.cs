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
            var viewModel = new DonationMetricsViewModel();

            //call the database once to get all three summations
            var donationSummaries = _context.DonationSummaries
                .GroupBy(x => x.DonationType, (type, group) => new { Type = type, Count = group.Count() })
                .ToDictionary(x => x.Type, x => x.Count);

            //Query cash donations for count for views piechart
            //var cashPieChart = _context.DonationSummaries.Where(a => a.DonationType == "Cash").Count();
            viewModel.CashTotal = donationSummaries["Cash"];

            //Query time donations for count for views piechart
            //var timePieChart = _context.DonationSummaries.Where(a => a.DonationType == "Time").Count();
            viewModel.TimeTotal = donationSummaries["Time"];

            //Query item donations for count for views piechart
            //var itemPieChart = _context.DonationSummaries.Where(a => a.DonationType == "Item").Count();
            viewModel.ItemTotal = donationSummaries["Item"];

            //Query group by month, add total of donationcash value, to list. Year was included to add yearly charts.
            //var monthlyDonations = _context.DonationSummaries
            //    .Select(d => new { d.DonationDate.Year, d.DonationDate.Month, d.DonationCashValue })
            //    .GroupBy(x => new { x.Year, x.Month }, (key, group) => new { donationtotal = group.Sum(dt => dt.DonationCashValue) })
            //    .ToList();

            //set a start boundary to fetch giving
            var startDate = DateTime.Now.AddYears(-1);
            startDate = new DateTime(startDate.Year, startDate.Month, 1);

            //call the database and get the total giving for each month
            var monthlyDonations = _context.DonationSummaries
                .Where(x => x.DonationDate >= startDate)
                .GroupBy(x => new { x.DonationDate.Year, x.DonationDate.Month }, (date, group) => new { Year = date.Year, Month = date.Month, Total = group.Sum(x => x.DonationCashValue) })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();

            //cycle through to find and fill empty months along with data that does exist (leaves open the chance of no giving and aligns the chart)
            for (int i = 0; i < 12; i++)
            {
                viewModel.TimeAxis.Add(startDate.ToString("MM/yyyy"));
                var monthDonations = monthlyDonations.SingleOrDefault(x => x.Year == startDate.Year && x.Month == startDate.Month);
                if (monthDonations == null)
                {
                    viewModel.Datapoints.Add(0);
                }
                else
                {
                    viewModel.Datapoints.Add(monthDonations.Total);
                }
                startDate = startDate.AddMonths(1);
            }

            //Query sum of donationcashvalue to date
            var totalDonationsToDate = _context.DonationSummaries.Sum(a => a.DonationCashValue);
            viewModel.TotalDonationsToDate = totalDonationsToDate.ToString("C0"); //format the decimal as currency

            //Query max of donationcashvalue to date
            var largestDonationToDate = _context.DonationSummaries.Max(a => a.DonationCashValue);
            viewModel.LargestDonationToDate = largestDonationToDate.ToString("C0"); //format the decimal as currency

            return View(viewModel); //modelbind the view
        }
    }
}
