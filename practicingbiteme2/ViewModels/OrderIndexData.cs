using practicingbiteme2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practicingbiteme2.ViewModels
{
    public class OrderIndexData
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<DeliveryAddress> Addresses { get; set; }
    }
}