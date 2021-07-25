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
//allow null values in columns defined bellow
#nullable enable
        public string? DonorsName { get; set; }
        public string? Email { get; set; }
//end null values in columns
#nullable disable
        public DateTime DonationDate { get; set; }
        public string DonationType { get; set; }
        public decimal DonationCashValue { get; set; }

    }
}
