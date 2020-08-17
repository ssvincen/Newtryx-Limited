using Dapper;
using Newtryx.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public class Order: IOrder
    {
        private readonly IConnectionManager connectionManager;
        public Order(IConnectionManager _connectionManager)
        {
            connectionManager = _connectionManager;
        }

        

        public async Task<OrderModel> GetOrderById(long? orderId)
        {
            var param = new DynamicParameters();
            param.Add("@orderId", dbType: DbType.Int64, value: orderId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<OrderModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            var param = new DynamicParameters();
            param.Add("@SearchString", dbType: DbType.String, value: "", direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<OrderModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<long> AddOrder(OrderViewModel order)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: order.ReservationId, direction: ParameterDirection.Input);
            param.Add("@description", dbType: DbType.String, value: order.Description, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<long>("dbo.spr_AddOrder", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> DeleteOrder(long? orderId)
        {
            var param = new DynamicParameters();
            param.Add("@orderId", dbType: DbType.Int64, value: orderId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> UpdateOrder(OrderViewModel order)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: order.ReservationId, direction: ParameterDirection.Input);
            param.Add("@description", dbType: DbType.String, value: order.Description, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<OrderModel>> GetOrderByReservationId(long? reservationId)
        {
            var param = new DynamicParameters();
            param.Add("@reservationId", dbType: DbType.Int64, value: reservationId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<OrderModel>("dbo.spr_GetOrderByReservationId", 
                    commandType: CommandType.StoredProcedure, param: param);
            }
            
        }
    }
}
