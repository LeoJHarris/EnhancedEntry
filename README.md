# AdvancedEntry
Advanced Entry for Xamarin forms

Customize the keyboard's ImeOptions and either give your entry a command or assign the next entry you want focus. 
Customize your entry to allow rounded corners with border color and background color
Customize your entry to allows left icons perfect for forms

Setup

Available on NuGet: https://www.nuget.org/packages/LeoJHarris.AdvancedEntry NuGet
Install into your PCL project and Client projects.

Usage

In your Android.

Xamarin.Forms.Init();//platform specific init
LeoJHarris.Control.Android.AdvancedEntryRenderer.Init(this);
You must do this AFTER you call Xamarin.Forms.Init();

In your iOS:

Xamarin.Forms.Forms.Init();
LeoJHarris.Control.iOS.AdvancedEntryRenderer.Init();
LoadApplication(new App());

new AdvancedEntry()
{
                KeyBoardAction = LoginCommand,
                ReturnKeyType = ReturnKeyTypes.Search,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                CornerRadius = 5,
                BorderWidth = 1,
                BorderColor = Color.Blue,
                Placeholder = "email",
                LeftIcon = "email",
                CustomBackgroundXML = "roundedcornerentry"
};

Regarding setting background color, border color and radius this is done seperately per platform
so for iOS use the bindable properties that specify color etc however for 
android put your background xml file into your drawable folder
and also you will need to put a layout folder in resources if not already exists
and place color xml document if your follow my example: 

Sample xml for android background image:

<?xml version="1.0" encoding="UTF-8"?>
<selector xmlns:android="http://schemas.android.com/apk/res/android">
  <item android:state_focused="true" >
    <shape android:shape="rectangle">
      <gradient
          android:startColor="@color/entry_background"
          android:endColor="@color/entry_background"
          android:angle="270" />
      <stroke
          android:width="1dp"
          android:color="@color/entry_border" />
      <corners
          android:radius="3dp" />
      <padding
        android:left="16dp"
        android:right="10dp"
        android:top="10dp"
        android:bottom="10dp" />
    </shape>
  </item>
  <item>
    <shape android:shape="rectangle">
      <gradient
          android:startColor="@color/entry_background"
          android:endColor="@color/entry_background"
          android:angle="270" />
      <stroke
          android:width="1dp"
          android:color="#21a8e1" />
      <corners
          android:radius="3dp" />
      <padding
        android:left="16dp"
        android:right="10dp"
        android:top="10dp"
        android:bottom="10dp" />
    </shape>
  </item>
</selector>

Bindable Properties

To be added.

Final Builds

License

Licensed under MIT, see license file
