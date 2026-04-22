using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Model
{
    internal class CustomerAccount
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
        public Account? Account { get; set; }

        public string OwnershipType { get; set; } = string.Empty;
        public DateTime OwnershipStartDate { get; set; }
    }
}
