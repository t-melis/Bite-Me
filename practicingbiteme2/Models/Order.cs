using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace practicingbiteme2.Models
{
    public enum Status
    {
        Pending,
        Delivered,
        Canceled
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        public Status Status { get; set; }

        public int DeliveryAddressID { get; set; }

        public virtual DeliveryAddress Address { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}