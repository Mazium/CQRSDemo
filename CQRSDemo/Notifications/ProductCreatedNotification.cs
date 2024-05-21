using MediatR;

namespace CQRSDemo.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;
}
