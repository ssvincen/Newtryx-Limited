using Dapper;
using Newtryx.BO;
using Newtryx.BO.Restaurant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public class Restaurant : IRestaurant
    {
        private readonly IConnectionManager connectionManager;
        public Restaurant(IConnectionManager _connectionManager)
        {
            connectionManager = _connectionManager;
        }

        public async Task<long> AddRestaurant(RestaurantViewModel restaurant)
        {
            var param = new DynamicParameters();
            param.Add("@name", dbType: DbType.Int64, value: restaurant.Name, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<long>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> DeleteRestaurant(long? restaurantId)
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: restaurantId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<RestaurantModel> GetRestaurantById(long? restaurantId)
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: restaurantId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<RestaurantModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurants()
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: "", direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<RestaurantModel>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> UpdateRestaurant(RestaurantViewModel restaurant)
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: restaurant.Id, direction: ParameterDirection.Input);
            param.Add("@name", dbType: DbType.Int64, value: restaurant.Name, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("device.pr_DeviceSearch", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }
    }
}
