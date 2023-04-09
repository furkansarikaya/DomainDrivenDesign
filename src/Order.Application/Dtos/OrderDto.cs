namespace Order.Application.Dtos;

public record OrderDto
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
}