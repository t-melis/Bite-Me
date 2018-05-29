using practicingbiteme2.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace practicingbiteme2.ViewModels
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        public string Venue { get; set; }

        public string Details { get; set; }

        public string CustomerID { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}