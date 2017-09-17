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
                KeyBoardAction = LoginCommand,
                ReturnKeyType = ReturnKeyTypes.Search,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            ,
                CornerRadius = 5,
                BorderWidth = 1,
                BorderColor = Color.Blue,
                Placeholder = "Advancedentry",
                LeftIcon = "email",
                CustomBackgroundXML = "roundedcornerentry"
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

