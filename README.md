**Advanced Entry for Xamarin forms**

Customize the keyboard's ImeOptions, give your entry a command or assign the next entry you want focus, give your entry rounded corners with border color and background color or customize your entry to allows left icons with padding perfect for forms

![](https://github.com/LeoJHarris/AdvancedEntry/blob/master/ios.jpg) ![](https://github.com/LeoJHarris/AdvancedEntry/blob/master/android.png)

**Setup**

Available on NuGet: https://www.nuget.org/packages/LeoJHarris.AdvancedEntry NuGet Install into your PCL project and Client projects.

**Usage**

_In your Android_

Xamarin.Forms.Init();
LeoJHarris.Control.Android.AdvancedEntryRenderer.Init(this);
You must do this AFTER you call Xamarin.Forms.Init();

_In your iOS_

`Xamarin.Forms.Forms.Init(); `
`LeoJHarris.Control.iOS.AdvancedEntryRenderer.Init(); `
`LoadApplication(new App());`

`new AdvancedEntry()
            {
                KeyBoardAction = LoginCommand,
                ReturnKeyType = ReturnKeyTypes.Search,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
                ,CornerRadius = 5,
                BorderWidth = 1,
                BorderColor = Color.Blue,
                Placeholder = "email",
                LeftIcon = "email",
                CustomBackgroundXML = "roundedcornerentry"
            };`

IMPORTANT: Due to android and iOS different methods required for setting background for then for iOS use the bindable properties that specify color, borderradius, borderwidth etc however for android put your background xml file that will be a drawable into your drawable folder and also you will need to put a layout folder in resources that has the colors if you do not already and place color xml document if your follow my example:

**Bindable Properties**

To be added.

Final Builds

**License**

Licensed under MIT, see license file

