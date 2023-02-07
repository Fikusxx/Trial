using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trial.Core.CQRS.CustomerCQRS.Commands.Create;
using Trial.Core.CQRS.CustomerCQRS.Commands.Delete;
using Trial.Core.CQRS.CustomerCQRS.Commands.Update;
using Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomer;
using Trial.Core.CQRS.CustomerCQRS.Queries.GetCustomerList;
using Trial.Core.Domain;
using Trial.Core.DTO.Customer;

namespace Trial.WebAPI.Controllers;


public class CustomerController : BaseController
{

    /// <summary>
    /// Returns a customer.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/customer/1
    /// </remarks>
    /// <response code="200"> Returns a customer by id </response>
    /// <response code="404"> If customer is not found </response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CustomerDTO>> Get(int id)
    {
        var query = new GetCustomerQuery() { Id = id };
        var customerDTO = await Mediator.Send(query);

        return Ok(customerDTO);
    }

    /// <summary>
    /// Returns list of all customers.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/customer/
    /// </remarks>
    /// <response code="200"> Returns all customers </response>
    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAll()
    {
        var query = new GetCustomerListQuery();
        var customerDTOList = await Mediator.Send(query);

        return Ok(customerDTOList);
    }

    /// <summary>
    /// Creates a customer.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/customer/
    ///
    ///     POST
    ///     {
    ///        "FullName": "Customer Name",
    ///        "Phone": "+7 123 123 1234"
    ///     }
    ///
    /// </remarks>
    /// <response code="201"> Returns newly created customer </response>
    /// <response code="400"> If customer is null </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateCustomerDTO createCustomerDTO)
    {
        var query = new CreateCustomerCommand() { CreateCustomerDTO= createCustomerDTO };
        var customerDTO = await Mediator.Send(query);

        return CreatedAtAction(nameof(Get), routeValues: new { customerDTO.Id }, value: customerDTO);
    }

    /// <summary>
    /// Updates a customer.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/customer/
    ///
    ///     PUT
    ///     {
    ///        "Id" : 1
    ///        "FullName": "Customer Name",
    ///        "Phone": "+7 123 123 1234"
    ///     }
    ///
    /// </remarks>
    /// <response code="204"> If update is successful </response>
    /// <response code="400"> If customer is null </response>
    /// <response code="404"> If customer is not found </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateCustomerDTO updateCustomerDTO)
    {
        var query = new UpdateCustomerCommand() { UpdateCustomerDTO= updateCustomerDTO };
        await Mediator.Send(query);

        return NoContent();
    }


    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/customer/1
    /// </remarks>
    /// <response code="204"> If delete is successful </response>
    /// <response code="404"> If customer is not found </response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var query = new DeleteCustomerCommand() { Id = id };
        await Mediator.Send(query);

        return NoContent();
    }
}
