using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Model
{
    internal class Transaction
    {
        [Key]
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public string AccountNumber { get; set; } = string.Empty;
        public Account? Account { get; set; }
    }
}
