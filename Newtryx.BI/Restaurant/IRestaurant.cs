using Newtryx.BO;
using Newtryx.BO.Reservation;
using Newtryx.BO.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public interface IRestaurant
    {
        Task<RestaurantModel> GetRestaurantByName(string name);
        Task<RestaurantModel> GetRestaurantById(long? restaurantId);
        Task<IEnumerable<RestaurantModel>> GetRestaurants();
        Task<long> AddRestaurant(RestaurantViewModel restaurant);
        Task<bool> DeleteRestaurant(long? restaurantId);
        Task<bool> UpdateRestaurant(RestaurantViewModel restaurant);
    }
}
