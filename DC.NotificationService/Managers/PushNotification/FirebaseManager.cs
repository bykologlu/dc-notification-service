using DC.NotificationService.Extensions;
using DC.NotificationService.Interfaces;
using DC.NotificationService.Models;
using DC.NotificationService.Settings;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
namespace DC.NotificationService.Managers.PushNotification
{
    public class FirebaseManager : IPushNotificationService
    {
        public FirebaseManager(PushNotificationSettings settings)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(settings.FirebaseCredentialPath)
            });
        }
        public async Task<PushNotificationRes> SendNotificationAsync(PushNotificationMessage message)
        {
            List<BatchResponse> responses = new List<BatchResponse>();

            Notification notification = new Notification()
            {
                Title = message.Title,
                Body = message.Body
            };

            AndroidConfig androidConfig = new AndroidConfig()
            {
                Notification = new AndroidNotification()
                {
                    ClickAction = message.ClickAction,
                    ImageUrl = message.ImageUrl
                }
            };

            ApnsConfig apnsConfig = new ApnsConfig()
            {
                Aps = new Aps()
                {
                    Category = message.ClickAction,
                    MutableContent = true
                },
                FcmOptions = new ApnsFcmOptions()
                {
                    ImageUrl = message.ImageUrl
                }
            };

            WebpushConfig webpushConfig = new WebpushConfig()
            {
                FcmOptions = new WebpushFcmOptions()
                {
                    Link = message.ClickAction
                },
                Headers = new Dictionary<string, string>()
                {
                    { "image", message.ImageUrl ?? string.Empty }
                },
            };

            if (message.DeviceTokens.Count > 0)
            {
                foreach (var tokens in message.DeviceTokens.SplitList(500))
                {
                    MulticastMessage firebaseMessage = new MulticastMessage()
                    {
                        Notification = notification,
                        Data = message.Data,
                        Tokens = tokens,
                        Android = androidConfig,
                        Apns = apnsConfig,
                        Webpush = webpushConfig
                    };

                    BatchResponse response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(firebaseMessage);

                    responses.Add(response);
                }
            }
            
            if(message.Topics.Count > 0)
            {
                List<Message> firebaseMessages = new List<Message>();

                foreach (var topic in message.Topics)
                {
                    Message firebaseMessage = new Message()
                    {
                        Notification = notification,
                        Data = message.Data,
                        Topic = topic,
                        Android = androidConfig,
                        Apns = apnsConfig,
                        Webpush = webpushConfig
                    };

                    firebaseMessages.Add(firebaseMessage);
                }

                BatchResponse response = await FirebaseMessaging.DefaultInstance.SendEachAsync(firebaseMessages);

                responses.Add(response);
            }

            PushNotificationRes serviceResponse = new PushNotificationRes(string.Empty,
                responses.Sum(j => j.SuccessCount),
                responses.Sum(j => j.FailureCount));

            return serviceResponse;
        }
    }
}
