using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BO
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string ReservationStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
