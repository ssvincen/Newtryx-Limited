using System;
using System.ComponentModel.DataAnnotations;

namespace Newtryx.BO.Reservation
{
    public class ReservationViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Please select Restaurant")]
        [Display(Name = "Restaurant Name")]
        public long RestaurantId { get; set; }

        [Display(Name = "ReservationStatus")]
        public long ReservationStatusId { get; set; }

        [Required(ErrorMessage = "Start Date and Time is required")]
        [Display(Name = "Start Date and Time")]
        [DataType(DataType.DateTime)]

        public DateTime StartDateTime { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
