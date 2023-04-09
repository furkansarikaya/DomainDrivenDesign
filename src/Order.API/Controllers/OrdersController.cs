using Microsoft.AspNetCore.Mvc;
using Order.Application.Dtos;
using Order.Application.Services;
using Order.Domain.OrderAggregate;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a OrderItem.
    /// </summary>
    /// <param name="order"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A newly created OrderItem</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(typeof(Response<CreatedOrderDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<CreatedOrderDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveOrder(OrderDto order, CancellationToken cancellationToken = default)
    {
        var response = await _orderService.SaveOrder(order, cancellationToken);

        return CreateActionResultInstance(response);
    }
}