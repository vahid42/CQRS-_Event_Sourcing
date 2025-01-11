using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Api.CQRS.Commands;
using Order_Api.CQRS.Commands.CommandHandlers;
using Order_Api.CQRS.Queries;
using Order_Api.Dto;
using Order_Api.Entities;

namespace Order_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;

        public OrderController(IMapper mapper)
        {
            this.mapper = mapper;
        }


        [HttpGet("{orderId:guid}",Name= nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(Guid orderId, [FromServices] IQueryHandler<GetOrderByIdQuery, Order> queryHandler)
        {
            var result = await queryHandler.HandleAsync(new GetOrderByIdQuery(orderId));
          
            return Ok(value: mapper.Map<OrderForReturnDto>(result));

        }

        [HttpGet(Name = nameof(GetsOrder))]
        public async Task<IActionResult> GetsOrder([FromServices]  IQueryHandler<GetOrderQuery, IEnumerable<Order>> queryHandler)
        {
            var result = await queryHandler.HandleAsync(new GetOrderQuery());

            return Ok(value: mapper.Map<IEnumerable<OrderForReturnDto> >(result));

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrders([FromBody] OrderForCreateDto orderForCreateDto, [FromServices] ICommandHandler<CreateOrderCommand> commandHandler)
        {
            var order = mapper.Map<Order>(orderForCreateDto);
            order.Id = Guid.NewGuid();

            await commandHandler.HandleAsync(new CreateOrderCommand(order));
            return Ok(mapper.Map<OrderForReturnDto>(order));
        }

        [HttpPut("{orderId:guid}", Name = nameof(UpdateOrder))]
        public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] OrderForUpdateDto orderForUpdateDto, [FromServices] ICommandHandler<UpdateOrderCommand> commandHandler)
        {
            var order = mapper.Map<Order>(orderForUpdateDto);
            order.Id = orderId;

            await commandHandler.HandleAsync(new UpdateOrderCommand(order));
            return Ok(mapper.Map<OrderForReturnDto>(order));
        }


        [HttpDelete("{orderId:guid}", Name = nameof(DeleteOrder))]
        public async Task<IActionResult> DeleteOrder(Guid orderId, [FromServices] ICommandHandler<DeleteOrderCommand> commandHandler, [FromServices] IQueryHandler<GetOrderByIdQuery, Order> queryHandler)
        {
            var order = await queryHandler.HandleAsync(new GetOrderByIdQuery(orderId));
            await commandHandler.HandleAsync(new DeleteOrderCommand(order));
            return Ok();
        }
    }
}
