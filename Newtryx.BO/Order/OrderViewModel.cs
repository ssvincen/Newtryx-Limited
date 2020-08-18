using System;
using System.ComponentModel.DataAnnotations;

namespace Newtryx.BO
{
    public class OrderViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Please select Reservation")]
        public long? ReservationId { get; set; }

        [Required(ErrorMessage = "Description of an Order is required")]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
