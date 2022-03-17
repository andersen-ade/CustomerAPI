using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime? CreationTime { get; set; }

        //This is a navigation property (or you can call it a nested property)
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
