using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practicingbiteme2.Models
{
    public class OrderProduct
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}