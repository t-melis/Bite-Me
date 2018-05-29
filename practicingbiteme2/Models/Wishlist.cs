using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practicingbiteme2.Models
{
    public class Wishlist
    {
        [Key]
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        private ICollection<Product> products;
        public ICollection<Product> Products
        {
            get
            {
                return products ?? (products = new List<Product>());
            }
            set
            {
                products = value;
            }
        }
    }
}