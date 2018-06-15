using Android.App;
using Android.Content;
using Firebase.Messaging;

namespace RaspPiCameraClient.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            // 通知メッセージを取得
            var messageBody = message.Data["filename"];

            // PendingIntentに、通知をタップしたときに起動するActivity(+ 起動時に渡す値)を指定
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra(Constants.AndroidIntentExtraID, messageBody);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            // Notification.Builderで通知を作成
            var notificationBuilder = new Notification.Builder(this)
                        .SetContentTitle(Application.Context.PackageName)
                        .SetSmallIcon(Resource.Drawable.ic_launcher)
                        .SetContentText("What's New")
                        .SetAutoCancel(true)
                        .SetContentIntent(pendingIntent);

            // NotificationManagerで通知を送信
            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}