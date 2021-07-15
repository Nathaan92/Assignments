using System;
using System.Collections.Generic;

#nullable disable

namespace Donate.Models
{
    public partial class DonationSummary
    {
        public int DonationId { get; set; }
        public DateTime DonationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DonationType { get; set; }
        public decimal DonationCashValue { get; set; }
    }
}
