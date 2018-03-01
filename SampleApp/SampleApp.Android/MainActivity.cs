namespace SampleApp.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    [Activity(Label = "SampleApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //LeoJHarris.EnhancedEntry.Plugin.Droid.EnhancedEntryRenderer.Init(this);

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            // this.LoadApplication(new App());
        }
    }
}

