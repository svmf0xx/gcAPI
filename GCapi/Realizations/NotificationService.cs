using gcapi.Interfaces.Services;
using System.Net.Http.Headers;
using System.Text;

namespace gcapi.Realizations
{
    public class NotificationService : INotificationService
    {
        private static readonly string ServerKey = "BAp0xDvifSFHsEVRahozy8u-X05dIvoqyHalzLXuHaA2CL1S8B1PVyPeaHo2n9ToR5PPZi_yBPXrNdsXpnbVzwk";
        private static readonly string FcmUrl = "https://fcm.googleapis.com/fcm/send";

        public async Task SendToTopicAsync(string topic, string title, string body)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("key", "=" + ServerKey);

                var payload = new
                {
                    to = $"esj3d0QqSZOpk2MGcXE5DH:APA91bF9ggln8HxCFYSrWZkH_rO9W_OPQ4cOrxkUy9E6K8fvWo_hDWP6tkmJdQCWgwUt8tZ0aJNAFL7YKuxqflrAgpFX6H8KSgOd_cE9pxQd3aGpXCY6X2U",
                    notification = new
                    {
                        title = title,
                        body = body
                    },
                    data = new
                    {
                        click_action = "FLUTTER_NOTIFICATION_CLICK"
                    }
                };

                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(FcmUrl, content);

                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("FCM Response: " + result);
            }
        }
    }
}
