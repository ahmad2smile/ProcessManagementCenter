using System;

namespace ProcessManagementCenter.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int MinerId { get; set; }
        public string? MinerName { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
