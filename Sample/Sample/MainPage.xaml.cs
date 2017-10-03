using LeoJHarris.Control.Abstractions;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            AdvancedEntry AdvancedEntry = new AdvancedEntry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Advancedentry",
                BorderColor = Color.Red,

                BorderWidth =1
                
            };

            TESTVIEW2.Content = AdvancedEntry;

            TESTVIEW3.Content = new Entry
                ()
            {
                Placeholder = "Standard entry"
            };
        }
        public Command LoginCommand => new Command(async () =>
        {
            await DisplayAlert("alert", "Tapped", "Ok");
        });
    }
}

