using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Model
{
    internal class Account
    {
        [Key]
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
        public DateTime OpeningDate { get; set; }

        public string AccountStatus { get; set; } = string.Empty;

        public int BranchCode { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
