namespace RaspPiCameraClient
{
    public static class Constants
    {
        public const string GenerateSasFunctionUri = "https://generatesasfunction.azurewebsites.net/api/SasToken?code=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx=";
        public const string BlobServiceEndpoint = "https://storagetest20180420.blob.core.windows.net/";
        public const string ContainerName = "container001";

        public const string ListenConnectionString = "Endpoint=sb://pushnotification20180508.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxx=";
        public const string NotificationHubName = "PushNotificationTest2";
        public const string MessageID = "NotificationFileName";
        public const string AndroidIntentExtraID = "AndroidIntentExtraID";
    }
}
