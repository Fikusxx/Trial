using Microsoft.AspNetCore.Mvc;
using Trial.Core.Common.Filtering;
using Trial.Core.CQRS.OrderCQRS.Commands.CreateOrderCommand;
using Trial.Core.CQRS.OrderCQRS.Commands.DeleteOrderCommand;
using Trial.Core.CQRS.OrderCQRS.Commands.UpdateOrderCommand;
using Trial.Core.CQRS.OrderCQRS.Queries.GetOrder;
using Trial.Core.CQRS.OrderCQRS.Queries.GetOrderList;
using Trial.Core.DTO.Order;
using Trial.Core.DTO.Product;

namespace Trial.WebAPI.Controllers;

public class OrderController : BaseController
{
    /// <summary>
    /// Returns an order.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/order/1
    /// </remarks>
    /// <response code="200"> Returns an order by id </response>
    /// <response code="404"> If order is not found </response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<OrderDTO>> Get(int id)
    {
        var query = new GetOrderQuery() { Id = id };
        var orderDTO = await Mediator.Send(query);

        return Ok(orderDTO);
    }

    /// <summary>
    /// Returns list of all orders.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/order/
    /// Sample query: ?CustomerId=1&From=2023-01-09T10:00:00&To=2023-01-16T10:00:00
    /// </remarks>
    /// <response code="200"> Returns all orders </response>
    /// <response code="404"> If customer is not found </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProductDTO>>> GetAll([FromQuery] OrderFilter filter)
    {
        var query = new GetOrderListQuery() { OrderFilter = filter };
        var orderListDTO = await Mediator.Send(query);

        return Ok(orderListDTO);
    }

    /// <summary>
    /// Creates an order.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/order/
    ///
    ///     POST
    ///     {
    ///        "CustomerId" : 1,
    ///        "CreatedAt" : "2023-01-01T10:00:00"
    ///     }
    ///
    /// </remarks>
    /// <response code="201"> Returns newly created order </response>
    /// <response code="400"> If order is null </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateOrderDTO createOrderDTO)
    {
        var query = new CreateOrderCommand() { CreateOrderDTO = createOrderDTO };
        var orderDTO = await Mediator.Send(query);

        return CreatedAtAction(nameof(Get), routeValues: new { orderDTO.Id }, value: orderDTO);
    }

    /// <summary>
    /// Updates an order.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/order/
    ///
    ///     PUT
    ///     {
    ///        "Id" : 1,
    ///        "CustomerId" : 1,
    ///        "CreatedAt": 123
    ///     }
    ///
    /// </remarks>
    /// <response code="204"> If update is successful </response>
    /// <response code="400"> If order is null </response>
    /// <response code="404"> If customer is not found </response>
    /// <response code="404"> If order is not found </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateOrderDTO updateOrderDTO)
    {
        var query = new UpdateOrderCommand() { UpdateOrderDTO = updateOrderDTO };
        await Mediator.Send(query);

        return NoContent();
    }


    /// <summary>
    /// Deletes an order.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/order/1
    /// </remarks>
    /// <response code="204"> If delete is successful </response>
    /// <response code="404"> If order is not found </response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var query = new DeleteOrderCommand() { Id = id };
        await Mediator.Send(query);

        return NoContent();
    }
}
