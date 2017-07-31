using AutoMapper;
using mego.Core;
using mego.Core.Models;
using mego.Core.Models.Resourses;
using mego.Models;
using mego.Models.Resources;
using mego.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mego.Controllers
{

    [Route("/api/orders")]
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _repo;

        public OrdersController(IMapper mapper,
             IOrderRepository repo, IUnitOfWork uow)
        {
            _mapper = mapper;
            _repo = repo;
            _uow = uow;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody]SaveOrderResource orderResource)
        {
            var sd = HttpContext.Request.ToString();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<SaveOrderResource, Order>(orderResource);

            order.LastUpdate = DateTime.Now;

            _repo.Add(order);
            //_context.Orders.Add(order);

            await _uow.CompleteAsync();

            order = await _repo.GetOrder(order.Id);

            var result = _mapper.Map<Order, OrderResource>(order);

            return Ok(result);
        }


        [HttpPut("{id}")] //api/orders/{id}
        [Authorize]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody]SaveOrderResource orderResourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _repo.GetOrder(id);

            if (order == null)
                return NotFound();

            _mapper.Map(orderResourse, order);

            order.LastUpdate = DateTime.Now;

            await _uow.CompleteAsync();

            order = await _repo.GetOrder(order.Id);

            var result = _mapper.Map<Order, OrderResource>(order);

            return Ok(result);
        }


        [HttpDelete("{id}")]
     
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var ordr = await _repo.GetOrder(id, includeRelated: false);

            if (ordr == null)
                return NotFound();

            _repo.Remove(ordr);

            await _uow.CompleteAsync();

            return Ok(id);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {            
            var ordre = await _repo.GetOrder(id);

            if (ordre == null)
                return NotFound();

            var vr = _mapper.Map<Order, OrderResource>(ordre);

            return Ok(vr);
        }


        [HttpGet]
        public async Task<QueryResultRsource<OrderResource>> GetOrders(OrderQueryResource resource)
        {
            var filter = _mapper.Map<OrderQueryResource, OrderQuery>(resource);

            var queryResult = await _repo.GetOrders(filter);

            return _mapper.Map<QueryResult<Order>, QueryResultRsource<OrderResource>>(queryResult);
        }
    }
}
