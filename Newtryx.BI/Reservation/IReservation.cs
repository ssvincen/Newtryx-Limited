using Newtryx.BO;
using Newtryx.BO.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public interface IReservation
    {
        Task<ReservationModel> GetReservationById(long? reservationId);
        Task<IEnumerable<ReservationModel>> GetReservations();
        Task<long> AddReservation(ReservationViewModel reservation);
        Task<bool> DeleteReservation(long? reservationId);
        Task<bool> UpdateReservation(ReservationViewModel reservation);
    }
}
