namespace Order.Application.Dtos;

public record AddressDto
{
    public string Line { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
}