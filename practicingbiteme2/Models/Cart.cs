using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practicingbiteme2.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        private ICollection<CartProduct> cartProducts;
        public virtual ICollection<CartProduct> CartProducts
        {
            get
            {
                return cartProducts ?? (cartProducts = new List<CartProduct>());
            }
            set
            {
                cartProducts = value;
            }
        }
    }
}