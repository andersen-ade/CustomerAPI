using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Available { get; set; }

        [ForeignKey("VendorId")]
        public int? VendorId { get; set; }
        public DateTime? CreationTime { get; set; }

        public Vendor Vendor { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
