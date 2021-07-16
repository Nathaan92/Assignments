using Microsoft.EntityFrameworkCore;
using Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donation.Data
{
    public class DonationSummaryContext : DbContext
    {
        public DonationSummaryContext(DbContextOptions<DonationSummaryContext> options) : base(options)
        {
        }
        public DbSet<DonationSummary> DonationSummaries { get; set; }
    }
}
