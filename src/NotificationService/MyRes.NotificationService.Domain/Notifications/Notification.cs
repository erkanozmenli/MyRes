namespace MyRes.NotificationService.Domain.Notifications
{
    public sealed class NotificationMessage
    {
        private NotificationMessage(
            Guid id,
            Guid tripId,
            Guid userId,
            NotificationType type,
            string message,
            DateTime createdAtUtc)
        {
            Id = id;
            TripId = tripId;
            UserId = userId;
            Type = type;
            Message = message;
            CreatedAtUtc = createdAtUtc;
        }


        public Guid Id { get; private set; }
        public Guid TripId { get; private set; }
        public Guid UserId { get; private set; }
        public NotificationType Type { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }


        public static NotificationMessage TripCompleted(Guid tripId, Guid userId)
        {
            return new NotificationMessage(
                Guid.NewGuid(),
                tripId,
                userId,
                NotificationType.TripCompleted,
                "Your trip transaction has been completed.",
                DateTime.UtcNow);
        }

        public static NotificationMessage TripFailed(Guid tripId, Guid userid, string message)
        {
            return new NotificationMessage(
                Guid.NewGuid(),
                tripId,
                userid,
                NotificationType.TripFailed,
                message,
                DateTime.UtcNow);
        }
    }
}
