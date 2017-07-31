using mego.Core.Models;
using mego.Extensions;
using mego.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mego.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MegoDbContext _context;

        public OrderRepository(MegoDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Order> GetOrder(int id, bool inRelated = true)
        {
            if (!inRelated)
                return await _context.Orders.FindAsync(id);

            else
                return await _context.Orders
                   .Include(v => v.Features)
                   .ThenInclude(vf => vf.Feature)
                   .Include(c => c.Model)
                   .ThenInclude(x => x.Make)
                   .SingleOrDefaultAsync(s => s.Id == id);
        }



        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Remove(Order order)
        {
            _context.Remove(order);
        }

        public async Task<QueryResult<Order>> GetOrders(OrderQuery queryObject)
        {
            var result = new QueryResult<Order>();

            var query = _context.Orders
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .AsQueryable();

            if (queryObject.MakeId.HasValue)
                query = query.Where(q => q.Model.MakeId == queryObject.MakeId.Value);

            if (queryObject.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObject.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Order, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };


            query = query.Ordering(queryObject, columnsMap);                     
            
            result.TotalItems = await query.CountAsync();

            query = query.Paging(queryObject);

            result.Items =  await query.ToListAsync();

            return result;
        }

        private IQueryable<Order> Ordering(OrderQuery queryObject, IQueryable<Order> query, Dictionary<string, Expression<Func<Order, object>>> columnsMap)
        {
            if (queryObject.IsSort)
                return query = query.OrderBy(columnsMap[queryObject.SortBy]);

            else
                return query.OrderByDescending(columnsMap[queryObject.SortBy]);
        }

        
    }
}
