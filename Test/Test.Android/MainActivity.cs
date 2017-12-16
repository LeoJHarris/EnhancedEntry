namespace Test.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Test.Droid.Resources;

    [Activity(Label = "Test", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            LeoJHarris.EnhancedEntry.Plugin.Droid.EnhancedEntryRenderer.Init(this);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

