using Xamarin.Forms;

namespace SampleApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Page1();
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
