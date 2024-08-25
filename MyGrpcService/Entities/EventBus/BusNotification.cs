// Services/DiscountPublisher.cs
public interface IDiscountPublisher
{
    Task PublishDiscountNotificationAsync(string productName, decimal discountAmount);
}

public class DiscountPublisher : IDiscountPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DiscountPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishDiscountNotificationAsync(string productName, decimal discountAmount)
    {
        var notification = new DiscountNotification
        {
            ProductName = productName,
            DiscountAmount = discountAmount
        };

        await _publishEndpoint.Publish<IDiscountNotification>(notification);
    }
}
