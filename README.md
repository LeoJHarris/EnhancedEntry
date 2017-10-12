**Advanced Entry for Xamarin forms**

Advanced entry for Xamarin.forms projects that extends the current xamarin.forms entry. Unofficial. 

• Must have set keyboard return button command or set focus to next entry when user taps the Input Method Editor i.e. Done/Next button on keyboard tapped => do something / set focus on the next specified entry. 

• Customize keyboard return button. 

• Rounded corners, border color border width and background colors. On and off focus border color property change 

• Left icons perfect for form entry with padding enabled.

**Screenshots**


![](https://github.com/LeoJHarris/AdvancedEntry/blob/master/ios.jpg) ![](https://github.com/LeoJHarris/AdvancedEntry/blob/master/android.png)

**Setup**

Available on NuGet: https://www.nuget.org/packages/LeoJHarris.AdvancedEntry NuGet Install into your PCL project and Client projects.

**Usage**

_In your Android_

Xamarin.Forms.Init();
LeoJHarris.AdvancedEntry.Plugin.iOS.AdvancedEntryRenderer.Init();;
You must do this AFTER you call Xamarin.Forms.Init();

_In your iOS_

`Xamarin.Forms.Forms.Init(); `
`LeoJHarris.AdvancedEntry.Plugin.iOS.AdvancedEntryRenderer.Init(); `
`LoadApplication(new App());`

`new AdvancedEntry()
            {
                KeyBoardAction = LoginCommand,
                FocusBorderColor = Color.Yellow,
                BorderColor = Color.Red,
                LeftPadding = 10,
                RightPadding = 20,
                TopBottomPadding = 20,
                BorderWidth = 2,
                BackgroundColor = Color.Pink,
                LeftIcon = "email",
            };`
            
**Bindable Properties**

To be added.

Final Builds

**License**

Licensed under MIT, see license file

