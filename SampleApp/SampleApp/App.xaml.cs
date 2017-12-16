namespace SampleApp
{
    using System.Collections.Generic;

    using LeoJHarris.EnhancedEntry.Plugin.Abstractions;
    using LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers;

    using Xamarin.Forms;

    public partial class App
    {
        public App()
        {
            InitializeComponent();

            this.MainPage = new Page1();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
