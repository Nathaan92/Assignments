using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donation.Models;


namespace Donation.Data
{
    public class DbInitializer
    {
        public static void Initialize(DonationSummaryContext context)
        {
           
            context.Database.EnsureCreated();
            //Check if Db exists
            if (context.DonationSummaries.Any())
            {
                return; // Db has been seeded
            }
            //Seed Db
            var donationsummaries = new DonationSummary[]
            {
            new DonationSummary{DonorsName="John Jacob",Email="JJ@gmail.com",DonationDate=DateTime.Parse("2021-01-16"),DonationType="Cash", DonationCashValue=2000.00M},
            new DonationSummary{DonorsName="Anne Harkson",Email="Anne3566@hotmail.com",DonationDate=DateTime.Parse("2021-01-17"),DonationType="Time", DonationCashValue=350.00M},
            new DonationSummary{DonorsName="Billy Bob",Email="BBob@bbindustry.com",DonationDate=DateTime.Parse("2021-02-02"),DonationType="Cash", DonationCashValue=1500.00M},
            new DonationSummary{DonorsName="Susan Boil",Email="Susana@gmail.com",DonationDate=DateTime.Parse("2021-03-16"),DonationType="Cash", DonationCashValue=1500.00M},
            new DonationSummary{DonorsName="Rob Boil",Email="BigRob@Robselectric.com",DonationDate=DateTime.Parse("2021-03-16"),DonationType="Cash", DonationCashValue=1500.00M},
            new DonationSummary{DonorsName="Anne Harkson",Email="AnnHark@hotmail.com",DonationDate=DateTime.Parse("2021-04-10"),DonationType="Cash", DonationCashValue=600.00M},
            new DonationSummary{DonorsName="John Jacob",Email="JJ@gmail.com",DonationDate=DateTime.Parse("2021-04-20"),DonationType="Item", DonationCashValue=75.00M},
            new DonationSummary{DonorsName="Tim Smithers",Email="MrSmither@BigTech.com",DonationDate=DateTime.Parse("2021-05-28"),DonationType="Cash", DonationCashValue=300.00M},
            new DonationSummary{DonorsName="Tim Smithers",Email="MrSmither@BigTech.com",DonationDate=DateTime.Parse("2021-06-06"),DonationType="Time", DonationCashValue=1623.00M},
            new DonationSummary{DonorsName="John Jacob",Email="JJ@gmail.com",DonationDate=DateTime.Parse("2021-07-01"),DonationType="Item", DonationCashValue=1325.00M},
            new DonationSummary{DonorsName="Carson Scott",Email="Carson@carsoncars.com",DonationDate=DateTime.Parse("2021-07-10"),DonationType="Cash", DonationCashValue=5000.00M},
            new DonationSummary{DonorsName="Tom Tommy",Email="TT@gmail.com",DonationDate=DateTime.Parse("2021-07-14"),DonationType="Cash", DonationCashValue=100.00M},
            };
            foreach (DonationSummary s in donationsummaries)
            {
                context.DonationSummaries.Add(s);
            }
            context.SaveChanges();
        }
    }
}
