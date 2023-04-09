namespace Order.Domain.Enums;

/// <summary>
/// Order Status 
/// </summary>
public enum OrderStatus : int
{
    /// <summary>
    /// Pending
    /// </summary>
    Pending = 10,

    /// <summary>
    /// Shipped
    /// </summary>
    Shipped = 20,

    /// <summary>
    /// Failed
    /// </summary>
    Failed = 30,

    /// <summary>
    /// Success
    /// </summary>
    Success = 40
}