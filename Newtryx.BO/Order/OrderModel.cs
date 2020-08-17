using System;

namespace Newtryx.BO
{
    public class OrderModel
    {
        public long Id { get; set; }
        public long ReservationId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
