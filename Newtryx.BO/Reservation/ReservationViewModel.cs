using System;
using System.ComponentModel.DataAnnotations;

namespace Newtryx.BO.Reservation
{
    public class ReservationViewModel
    {
        public long? Id { get; set; }
        public long ReservationStatusId { get; set; }

        [Required(ErrorMessage = "Start Date and Time is required")]
        [Display(Name = "Start Date and Time")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Description is required")]
        public string Description { get; set; }
    }
}
