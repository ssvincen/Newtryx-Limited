using Newtryx.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Newtryx.BI
{
    public interface IOrder
    {
        Task<OrderModel> GetOrderById(long? orderId);
        Task<IEnumerable<OrderModel>> GetOrders();
        Task<long> AddOrder(OrderViewModel order);
        Task<bool> DeleteOrder(long? orderId);
        Task<bool> UpdateOrder(OrderViewModel order);
    }
}
