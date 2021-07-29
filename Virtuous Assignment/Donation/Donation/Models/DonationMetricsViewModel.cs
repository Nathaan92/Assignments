using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donation.Models
{
    public class DonationMetricsViewModel
    {
        public DonationMetricsViewModel()
        {
            TimeAxis = new List<string>();
            Datapoints = new List<decimal>();
        }

        public int CashTotal { get; set; }

        public int TimeTotal { get; set; }

        public int ItemTotal { get; set; }

        public List<string> TimeAxis { get; set; }

        public List<decimal> Datapoints { get; set; }

        public string TotalDonationsToDate { get; set; }

        public string LargestDonationToDate { get; set; }
    }
}
