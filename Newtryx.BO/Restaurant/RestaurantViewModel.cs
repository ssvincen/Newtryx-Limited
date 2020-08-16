using System.ComponentModel.DataAnnotations;

namespace Newtryx.BO
{
    public class RestaurantViewModel
    {
        public long? Id { get; set; }


        [Required(ErrorMessage = "Name of Restaurant is required")]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }
    }
}
