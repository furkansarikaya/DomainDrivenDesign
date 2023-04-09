using Order.Application.Dtos;
using Order.Domain.OrderAggregate;
using Shared.Dtos;

namespace Order.Application.Services;

public interface IOrderService
{
    Task<Response<CreatedOrderDto>> SaveOrder(OrderDto order, CancellationToken cancellationToken = default);
}