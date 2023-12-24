namespace DC.NotificationService.Models
{
    public class PushNotificationRes
    {
        public PushNotificationRes()
        {
            
        }

        public PushNotificationRes(string summary, int successCount = 0, int failureCount = 0)
        {
            summary = Summary;
            successCount = SuccessCount;
            failureCount = FailureCount;
        }
        public string Summary { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}
