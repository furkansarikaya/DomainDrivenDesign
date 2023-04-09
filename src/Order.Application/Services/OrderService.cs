using Microsoft.AspNetCore.Http;
using Order.Application.Dtos;
using Order.Domain.OrderAggregate;
using Order.Infrastructure;

namespace Order.Application.Services;

public class OrderService : IOrderService
{
    #region Fields

    private readonly OrderDbContext _context;

    #endregion

    #region Ctor

    public OrderService(OrderDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    public async Task<Shared.Dtos.Response<CreatedOrderDto>> SaveOrder(OrderDto order,
        CancellationToken cancellationToken = default)
    {
        Address newAddress = new(order.Address.Province, order.Address.District, order.Address.Street,
            order.Address.ZipCode, order.Address.Line);

        Domain.OrderAggregate.Order newOrder = new(order.BuyerId, newAddress);

        order.OrderItems.ForEach(x => { newOrder.AddOrderItem(x.ProductId, x.Count, x.Price); });

        await _context.Orders.AddAsync(newOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Shared.Dtos.Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id },
            StatusCodes.Status201Created);
    }

    #endregion
}