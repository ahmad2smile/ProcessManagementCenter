using System;

namespace Notifications.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public int MinerId { get; set; }
        public int ShiftId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AcknowledgedAt { get; set; }
        public DateTime? DismissedAt { get; set; }
        public NotificationStatus NotificationStatus { get; set; }

        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}