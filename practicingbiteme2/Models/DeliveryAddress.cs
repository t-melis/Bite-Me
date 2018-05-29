using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace practicingbiteme2.Models
{
    public class DeliveryAddress
    {
        public int DeliveryAddressID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City name is required")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Street name is required")]
        public string Street { get; set; }

        public string Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Post code is required")]
        public string PostCode { get; set; }

        public string ReceiverName { get; set; }

        public bool IsDeleted { get; set; }

        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}