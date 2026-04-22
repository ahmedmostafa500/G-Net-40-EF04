using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF04.Model
{
    internal class Branch
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Manager? Manager { get; set; }

        public ICollection<Account> Accounts { get; set; }=new List<Account>();
    }
}
