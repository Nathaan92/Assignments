using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donation.Models
{
    public class DonationSummary
    {
        // model with nullable fields
        public int ID { get; set; }
#nullable enable
        public string? DonorsName { get; set; }
        public string? Email { get; set; }
#nullable disable
        public DateTime DonationDate { get; set; }
        public string DonationType { get; set; }
        public decimal DonationCashValue { get; set; }

    }
}
