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
                return await db.QueryFirstOrDefaultAsync<ReservationModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<ReservationModel>> GetReservations()
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: "", direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<ReservationModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<long> AddReservation(ReservationViewModel reservation)
        {
            var param = new DynamicParameters();
            param.Add("@reservationStatusId", dbType: DbType.Int64, value: reservation.ReservationStatusId, direction: ParameterDirection.Input);
            param.Add("@startDateTime", dbType: DbType.DateTime, value: reservation.StartDateTime, direction: ParameterDirection.Input);
            param.Add("@description", dbType: DbType.String, value: reservation.Description, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<long>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> DeleteReservation(long? reservationId)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: reservationId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> UpdateReservation(ReservationViewModel reservation)
        {
            var param = new DynamicParameters();
            param.Add("@id", dbType: DbType.Int64, value: reservation.Id, direction: ParameterDirection.Input);
            param.Add("@reservationStatusId", dbType: DbType.Int64, value: reservation.ReservationStatusId, direction: ParameterDirection.Input);
            param.Add("@startDateTime", dbType: DbType.DateTime, value: reservation.StartDateTime, direction: ParameterDirection.Input);
            param.Add("@description", dbType: DbType.String, value: reservation.Description, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }
    }
}
