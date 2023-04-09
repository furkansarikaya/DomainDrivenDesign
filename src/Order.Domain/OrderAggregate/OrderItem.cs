using MassTransit;
using Order.Domain.Core;

namespace Order.Domain.OrderAggregate;

public class OrderItem : Entity
{
    public int ProductId { get; private set; }
    public int Count { get; private set; }
    public Decimal Price { get; private set; }

    public OrderItem()
    {
    }

    public OrderItem(int productId, int count, decimal price)
    {
        Id = NewId.NextGuid();
        ProductId = productId;
        Count = count;
        Price = price;
    }

    public void UpdateOrderItem(int count, decimal price)
    {
        Count = count;
        Price = price;
    }
}