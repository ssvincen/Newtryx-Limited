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

        public async Task<long> UpsertRestaurant(RestaurantViewModel restaurant)
        {
            var param = new DynamicParameters();
            param.Add("@id", dbType: DbType.Int64, value: restaurant.Id, direction: ParameterDirection.Input);
            param.Add("@name", dbType: DbType.String, value: restaurant.Name, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<long>("dbo.spr_UpsertRestaurant", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<bool> DeleteRestaurant(long? restaurantId)
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: restaurantId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<bool>("dbo.spr_DeleteRestaurant", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<RestaurantModel> GetRestaurantById(long? restaurantId)
        {
            var param = new DynamicParameters();
            param.Add("@restaurantId", dbType: DbType.Int64, value: restaurantId, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<RestaurantModel>("dbo.spr_GetRestaurantById", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<RestaurantModel> GetRestaurantByName(string name)
        {
            var param = new DynamicParameters();
            param.Add("@name", dbType: DbType.String, value: name, direction: ParameterDirection.Input);
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryFirstOrDefaultAsync<RestaurantModel>("dbo.spr_GetRestaurantByName", commandType: CommandType.StoredProcedure,
                    param: param);
            }
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurants()
        {
            using (var db = connectionManager.NewtryxConnection())
            {
                return await db.QueryAsync<RestaurantModel>("dbo.spr_GetAllRestaurants", commandType: CommandType.StoredProcedure);
            }
        }

    }
}
