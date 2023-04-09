using MassTransit;
using Order.Domain.Core;
using Order.Domain.Enums;

namespace Order.Domain.OrderAggregate;
public class Order : Entity, IAggregateRoot
{
    public DateTime CreatedDate { get; private set; }

    public Address Address { get; private set; }

    public string BuyerId { get; private set; }

    private readonly List<OrderItem> _orderItems;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public int Status { get; private set; }

    public string FailMessage { get; private set; }

    public Order()
    {
    }

    public Order(string buyerId, Address address)
    {
        Id = NewId.NextGuid();
        _orderItems = new List<OrderItem>();
        CreatedDate = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
        Status = (int)OrderStatus.Pending;
        FailMessage = string.Empty;
    }

    public void AddOrderItem(int productId, int count, decimal price)
    {
        var existProduct = _orderItems.Any(x => x.ProductId == productId);

        if (!existProduct)
        {
            var newOrderItem = new OrderItem(productId, count, price);

            _orderItems.Add(newOrderItem);
        }
    }

    public void SetFailMessage(string failMessage)
    {
        FailMessage = failMessage;
    }

    public void SetStatus(OrderStatus orderStatus)
    {
        Status = (int)orderStatus;
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}