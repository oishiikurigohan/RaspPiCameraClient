using Android.App;
using Firebase.Iid;
using WindowsAzure.Messaging;

namespace RaspPiCameraClient.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            // Azure Notification Hubに接続し、FirebaseのTokenを登録する
            NotificationHub hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString, this);
            hub.Register(FirebaseInstanceId.Instance.Token, null);

            // Template通知受信のためにJSON文字列でFCM用のテンプレートを登録する
            string templateBodyFCM = "{\"data\":{\"filename\":\"$(message)\"}}";
            hub.RegisterTemplate(FirebaseInstanceId.Instance.Token, "simpleFCMTemplate", templateBodyFCM, new string[0]);
        }
    }
}