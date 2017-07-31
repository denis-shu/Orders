using System.Threading.Tasks;
using mego.Models;
using System.Collections.Generic;
using mego.Core.Models;

namespace mego.Persistence
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(int id, bool includeRelated = true);
        Task<QueryResult<Order>> GetOrders(OrderQuery x);
        void Add(Order order);
        void Remove(Order order);
    }
}