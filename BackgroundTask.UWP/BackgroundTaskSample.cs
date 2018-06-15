using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace RaspPiCameraClientBackgroundTask.UWP
{
    public sealed class BackgroundTaskSample : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 受信したRaw Notification
            RawNotification notification = (RawNotification)taskInstance.TriggerDetails;

            // トーストのテンプレートを選択
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            // トーストに表示するテキストを指定
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode("What's New"));

            // トーストに表示する画像を指定
            XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "https://2.bp.blogspot.com/-Euo8ySieZ1s/VlAYy90a2oI/AAAAAAAA02Y/r3AmTznIjOo/s800/allergy_kuri.png");

            // トーストをクリックした場合、パラメータ付きでアプリを起動する
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("launch", notification.Content);

            // トースト表示
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}