using Dapper;
using Newtryx.BO;
using Newtryx.BO.Reservation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public class Reservation : IReservation
    {
        private readonly IConnectionManager connectionManager;
        public Reservation(IConnectionManager _connectionManager)
        {
            connectionManager = _connectionManager;
        }

        public async Task<ReservationModel> GetReservationById(long? reservationId)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: reservationId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<ReservationModel>("dbo.spr_GetReservationById", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<ReservationModel>> GetReservations()
        {
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<ReservationModel>("dbo.spr_GetAllReservations", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<long> UpsertReservation(ReservationViewModel reservation)
        {
            var param = new DynamicParameters();
            param.Add("@id", dbType: DbType.Int64, value: reservation.Id, direction: ParameterDirection.Input);
            param.Add("@restaurantId", dbType: DbType.Int64, value: reservation.RestaurantId, direction: ParameterDirection.Input);
            param.Add("@reservationStatusId", dbType: DbType.Int64, value: reservation.ReservationStatusId, direction: ParameterDirection.Input);
            param.Add("@startDateTime", dbType: DbType.DateTime, value: reservation.StartDateTime, direction: ParameterDirection.Input);
            param.Add("@description", dbType: DbType.String, value: reservation.Description, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<long>("dbo.spr_UpsertReservation", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> DeleteReservation(long? reservationId)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: reservationId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("dbo.spr_DeleteReservation", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<ReservationViewModel> UpdateReservation(long? reservationId)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: reservationId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<ReservationViewModel>("dbo.spr_GetReservationByIdAndForeignKeysId", commandType: CommandType.StoredProcedure,
                    param: param);
            }
            
        }

        public async Task<IEnumerable<ReservationStatus>> GetReservationStatus()
        {
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<ReservationStatus>("dbo.spr_GetReservationStatus", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
