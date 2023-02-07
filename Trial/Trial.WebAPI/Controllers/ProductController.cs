using Microsoft.AspNetCore.Mvc;
using Trial.Core.Common.Filtering;
using Trial.Core.CQRS.ProductCQRS.Commands.Create;
using Trial.Core.CQRS.ProductCQRS.Commands.Delete;
using Trial.Core.CQRS.ProductCQRS.Commands.Update;
using Trial.Core.CQRS.ProductCQRS.Queries.GetProduct;
using Trial.Core.CQRS.ProductCQRS.Queries.GetProductList;
using Trial.Core.DTO.Product;

namespace Trial.WebAPI.Controllers;


public class ProductController : BaseController
{
    /// <summary>
    /// Returns a product.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/product/1
    /// </remarks>
    /// <response code="200"> Returns a product by id </response>
    /// <response code="404"> If product is not found </response>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var query = new GetProductQuery() { Id = id };
        var productDTO = await Mediator.Send(query);

        return Ok(productDTO);
    }

    /// <summary>
    /// Returns list of all products.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/product/
    /// Sample query: ?productType=1&isAvailable=true&minPrice=0&maxPrice=100
    /// </remarks>
    /// <response code="200"> Returns all products </response>
    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAll([FromQuery] ProductFilter filter)
    {
        var query = new GetProductListQuery() { ProductFilter = filter};
        var productListDTO = await Mediator.Send(query);

        return Ok(productListDTO);
    }

    /// <summary>
    /// Creates a product.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/product/
    ///
    ///     POST
    ///     {
    ///        "ProductTypeId" : 1,
    ///        "Description": "Product Info",
    ///        "Price": 100,
    ///        "AvailableAmount" : 100
    ///     }
    ///
    /// </remarks>
    /// <response code="201"> Returns newly created product </response>
    /// <response code="400"> If product is null </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateProductDTO createProductDTO)
    {
        var query = new CreateProductCommand() { CreateProductDTO = createProductDTO };
        var productDTO = await Mediator.Send(query);

        return CreatedAtAction(nameof(Get), routeValues: new { productDTO.Id }, value: productDTO);
    }

    /// <summary>
    /// Updates a product.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/product/
    ///
    ///     PUT
    ///     {
    ///        "Id" : 1,
    ///        "ProductTypeId" : 1,
    ///        "Description": "Product Info",
    ///        "Price": 100,
    ///        "AvailableAmount" : 100
    ///     }
    ///
    /// </remarks>
    /// <response code="204"> If update is successful </response>
    /// <response code="400"> If product is null </response>
    /// <response code="404"> If product type is not found </response>
    /// <response code="404"> If product is not found </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateProductDTO updateProductDTO)
    {
        var query = new UpdateProductCommand() { UpdateProductDTO = updateProductDTO };
        await Mediator.Send(query);

        return NoContent();
    }


    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <remarks>
    /// Sample request: https://localhost:5000/api/product/1
    /// </remarks>
    /// <response code="204"> If delete is successful </response>
    /// <response code="404"> If product is not found </response>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var query = new DeleteProductCommand() { Id = id };
        await Mediator.Send(query);

        return NoContent();
    }
}
