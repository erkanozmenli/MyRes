using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyRes.BuildingBlocks.Authentication;

namespace MyRes.NotificationService.Infrastructure.Notifications.WebSocket
{
    public sealed class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation(
                "Connected - Pod: {Pod}, ConnectionId: {ConnectionId}",
                Environment.MachineName,
                Context.ConnectionId);

            var httpContext = Context.GetHttpContext();
            var accessor = httpContext!.RequestServices.GetRequiredService<ICurrentIdentityAccessor>();

            if (accessor.Identity.UserId is Guid userId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation(
                "Disconnected - Pod: {Pod}, ConnectionId: {ConnectionId}",
                Environment.MachineName,
                Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
