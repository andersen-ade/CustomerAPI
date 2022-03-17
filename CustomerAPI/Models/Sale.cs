using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public DateTime? CreationTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
