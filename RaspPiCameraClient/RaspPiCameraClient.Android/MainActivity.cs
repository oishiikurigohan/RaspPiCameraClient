using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace RaspPiCameraClient.Droid
{
    [Activity(Label = "RaspPiCameraClient", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            if (Intent.Extras != null)
            {
                // MessagingCenterでリスナーにメッセージを送る
                var message = Intent.Extras.GetString(Constants.AndroidIntentExtraID);
                Xamarin.Forms.MessagingCenter.Send<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, Constants.MessageID, message);
            }
        }
    }
}

