**Enhanced Entry for Xamarin forms**

Enhanced entry for Xamarin.forms projects that extends the current xamarin.forms entry. Unofficial. 

• Must have set keyboard return button command or set focus to next entry when user taps the Input Method Editor i.e. Done/Next button on keyboard tapped => do something / set focus on the next specified entry. 

• Customize keyboard return button. 

• Rounded corners, border color border width and background colors. On and off focus border color property change 

• Left icons perfect for form entry with padding enabled.

• Email and compare entries behavour validator (basically compares a collection of entries

**PULL REQUESTS WELCOME IF YOUD LIKE TO CONTRIBUTE FUNCTIONAILITY OR ADD ADDITIONAL BEHAVOURS**

**Screenshots**

![iOS Screenshot](https://github.com/LeoJHarris/EnhancedEntry/blob/master/ios.jpg)
![Android Screenshot](https://github.com/LeoJHarris/EnhancedEntry/blob/master/thumb3.png)

**Setup**

Available on NuGet: https://www.nuget.org/packages/LeoJHarris.XForms.Plugin.EnhancedEntry NuGet Install into your PCL project and Client projects.

**Usage**

_In your Android_

            Xamarin.Forms.Init();
            LeoJHarris.EnhancedEntryEntry.Plugin.iOS.EnhancedEntryRenderer.Init();

You must do this AFTER you call Xamarin.Forms.Init();

_In your iOS_

            Xamarin.Forms.Forms.Init(); 
            LeoJHarris.EnhancedEntry.Plugin.iOS.EnhancedEntryRenderer.Init();
            LoadApplication(new App());

            new EnhancedEntry()
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
                EmailValidatorBehavior = new EmailValidatorBehavior()
            };
            
            new EnhancedEntry()
            {
                BorderWidth = 1,
                CornerRadius = 2,
                PasswordCompareValidation = new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    SomeEnhancedEntry,
                })
                {
                    ValidColor = Color.Orange,
                    InValidColor = Color.Red
                },
            };
            
**Bindable Properties**

You are able to set the `LeftIcon` which will place an icon to the left inside the entry, icons to be placed inside respective drawable folders and iOS in the resources files, set the `PaddingLeftIcon` for padding space between icon and entry text.

You are able to set the `BorderWidth` of the entry which must be set for respective bindables such as `CornerRadius`, `FocusBorderColor` etc.

You are able to set `FocusBorderColor` when the entry has focus, the `BorderColor` will be set when off focus.

You are able to set `BackgroundColor` of the entry.

You are able to set `LeftPadding`, `RightPadding` and `TopBottomPadding` which will set the desired padding between the entry text and the edge of the entry.

You are able to set the `ReturnKeyType` which will set the text displayed for the keyboard action button. Runtime exception will be thrown if unsupported on platoform.

You are able to set the `NextEntry` which is the entry that will be given focus if when keyboard action button invoked or if `GoToNextEntryOnLengthBehaviour` condition is satisfied.

You are able to set the `KeyBoardAction` which will take some given command parameter to exceute.

**Custom Behaviours** 

You are able to set the `PasswordCompareValidationBehavior` which will compare these entries with some condition checks, each entry should contain the collection `PasswordCompareValidation` of entries to compare (see examples above), you are also able to set the `ValidColor` and `InValidColor` of the entry for the given condition of the password when there is or int a match. PLEASE NOTE: Although these bindable properties are set per entry, they should all have the same values for all entries to check in the collection. You are able to set the `MinimumLength` of the passwords. Currently the password validator requires an uppercase, lowercase and a number, therefore the minimum length can be set.

You are able to set the `EmailValidatorBehavior` for entries that will be used for emails, the `EmailRegularExpression` can be overriden if desired.

You are able to set `GoToNextEntryOnLengthBehaviour` which will set focus to the `NextEntry` on the given `CharacterLength`, use the `MaxLengthValidator` i.e. MaxLength="1" as well to ensure that entry doesnt exceed the `CharacterLength`

**License**

Licensed under MIT, see license file
