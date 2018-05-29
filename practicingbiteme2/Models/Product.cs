using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practicingbiteme2.Models
{
    public enum Category
    {
        Cookie,
        Cake,
        Pastry
    }

    public class Product
    {
        public int ProductID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string ImageURL { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        private ICollection<Wishlist> wishlists;
        public virtual ICollection<Wishlist> Wishlists
        {
            get
            {
                return wishlists ?? (wishlists = new List<Wishlist>());
            }
            set
            {
                wishlists = value;
            }
        }

        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}