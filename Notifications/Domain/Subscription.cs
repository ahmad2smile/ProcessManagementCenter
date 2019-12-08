namespace Notifications.Domain
{
    public class Subscription
    {
        public int Id { get; set; }
        public int MineSiteId { get; set; }
        public string DeviceId { get; set; }
        public string PushEndpoint { get; set; }
        public string PushP256Dh { get; set; }
        public string PushAuth { get; set; }
    }
}
