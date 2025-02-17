using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.Common.Models
{
    public class AddBookingViewModel
    {
        [Required(ErrorMessage = "Start city is required")]
        public string StartCity { get; set; }

        [Required(ErrorMessage = "Destination city is required")]
        public string DestinationCity { get; set; }

        [Required(ErrorMessage = "Seat number is required")]
        public string Seat { get; set; }



        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Departure time is required")]
        public string DepartureTimes { get; set; }

    }
}