namespace Notifications.Domain
{
    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string SiteName { get; set; }
        public string MinerName { get; set; }
        public string Link { get; set; }
    }
}