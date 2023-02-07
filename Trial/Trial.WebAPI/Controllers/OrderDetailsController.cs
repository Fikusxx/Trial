using Microsoft.AspNetCore.Mvc;
using Trial.Core.Common.Filtering;
using Trial.Core.CQRS.OrderDetailsCQRS.Commands.Create;
using Trial.Core.CQRS.OrderDetailsCQRS.Commands.Delete;
using Trial.Core.CQRS.OrderDetailsCQRS.Commands.Update;
using Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetails;
using Trial.Core.CQRS.OrderDetailsCQRS.Queries.GetOrderDetailsList;
using Trial.Core.DTO.OrderDetails;
using Trial.Core.DTO.Product;

namespace Trial.WebAPI.Controllers;

public class OrderDetailsController : BaseController
{

    /// <summary>
    /// Returns an order details.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/orderdetails/1
    /// </remarks>
    /// <response code="200"> Returns an order details by id </response>
    /// <response code="404"> If order details is not found </response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<OrderDetailsDTO>> Get(int id)
    {
        var query = new GetOrderDetailsQuery() { Id = id };
        var orderDetailsDTO = await Mediator.Send(query);

        return Ok(orderDetailsDTO);
    }

    /// <summary>
    /// Returns list of all order details.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/orderdetails/
    /// Sample query: ?OrderId=1&ProductId=1
    /// </remarks>
    /// <response code="200"> Returns all orders details </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductDTO>>> GetAll([FromQuery] OrderDetailsFilter filter)
    {
        var query = new GetOrderDetailsListQuery() { OrderDetailsFilter = filter };
        var orderDetailsListDTO = await Mediator.Send(query);

        return Ok(orderDetailsListDTO);
    }

    /// <summary>
    /// Creates an order details.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/orderdetails/
    ///
    ///     POST
    ///     {
    ///        "OrderId" : 1,
    ///        "ProductId" : 1,
    ///        "Amount" : 100
    ///     }
    ///
    /// </remarks>
    /// <response code="201"> Returns newly created order </response>
    /// <response code="400"> If order details is null / requested amount is less than available </response>
    /// <response code="404"> If product / order is not found </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Create(CreateOrderDetailsDTO createOrderDetailsDTO)
    {
        var query = new CreateOrderDetailsCommand() { CreateOrderDetailsDTO = createOrderDetailsDTO };
        var orderDetailsDTO = await Mediator.Send(query);

        return CreatedAtAction(nameof(Get), routeValues: new { orderDetailsDTO.Id }, value: orderDetailsDTO);
    }

    /// <summary>
    /// Updates an order details.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/orderdetails/
    ///
    ///     PUT
    ///     {
    ///        "Id" : 1,
    ///        "OrderId" : 1,
    ///        "ProductId" : 1,
    ///        "Amount": 100
    ///     }
    ///
    /// </remarks>
    /// <response code="204"> If update is successful </response>
    /// <response code="400"> If order is null / requested amount is less than available </response>
    /// <response code="404"> If order / product is not found </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateOrderDetailsDTO updateOrderDetailsDTO)
    {
        var query = new UpdateOrderDetailsCommand() { UpdateOrderDetailsDTO = updateOrderDetailsDTO };
        await Mediator.Send(query);

        return NoContent();
    }

    /// <summary>
    /// Deletes an order details.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/orderdetails/1
    /// </remarks>
    /// <response code="204"> If delete is successful </response>
    /// <response code="404"> If orderdetails is not found </response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var query = new DeleteOrderDetailsCommand() { Id = id };
        await Mediator.Send(query);

        return NoContent();
    }
}
