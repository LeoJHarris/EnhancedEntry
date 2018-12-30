**Enhanced Entry for Xamarin Forms**

[![Build Status](https://www.bitrise.io/app/7f1dafa3432c4b0f/status.svg?token=q5DIlKQd3GcOeNAipGvxKQ&branch=master)](https://www.bitrise.io/app/7f1dafa3432c4b0f)

**Setup**

Available on NuGet: https://www.nuget.org/packages/LeoJHarris.XForms.Plugin.EnhancedEntry NuGet Install into your Xamarin.Forms project including client project i.e. .Android, .iOS etc.

**Gif Demo**

![EnhancedEntry Gif](https://github.com/LeoJHarris/EnhancedEntry/blob/master/assets/android_gif.gif)

**Sample Code**

https://github.com/LeoJHarris/EnhancedEntry/blob/master/SampleApp/SampleApp/Page1.xaml.cs

**Usage**

You must do this AFTER you call Xamarin.Forms.Init();

_In your Android_

```csharp
 Xamarin.Forms.Init();
 LeoJHarris.FormsPlugin.Droid.EnhancedEntryRenderer.Init(this);
```
          
_In your iOS_

```csharp
 Xamarin.Forms.Forms.Init(); 
 LeoJHarris.FormsPlugin.iOS.EnhancedEntryRenderer.Init();
 LoadApplication(new App());
```
    
**XAML**

First add the xmlns namespace:

```csharp
 xmlns:enhancedEntry="clr-namespace:LeoJHarris.FormsPlugin.Abstractions;assembly=LeoJHarris.FormsPlugin.Abstractions"
```

Then add the xaml (or just use the code behind)
    
**Bindable Properties**

* `LeftIcon`

Places an icon to the left inside the entry, icons to be placed inside respective drawable folders and iOS in the resources files, set the `PaddingLeftIcon` for padding space between icon and entry text.

* BorderWidth

Must be set for respective bindables such as `CornerRadius`, `FocusBorderColor` etc.

* `FocusBorderColor`

When the entry has focus otherwise the `BorderColor` will be set when off focus.

* `BackgroundColor`

* `LeftPadding, TopBottomPadding and RightPadding`

Desired padding between the Entry text and the edge of the Entry.
 
* `ReturnKeyType`

Displayed for the keyboard action button. Run time exception will be thrown if unsupported on platform.

* `NextEntry`

Entry that will be given focus when keyboard action button pressed and keyboard `ReturnKeyType` is type - Next, otherwise if `GoToNextEntryOnLengthBehaviour` has been added and condition is satisfied.

* `KeyBoardAction`

Given command parameter to execute when keyboard action button pressed and `ReturnKeyType` is type - Done.

**Custom Behaviours** 

* `PasswordCompareValidationBehavior`

Compare entries given some condition checks. Each entry should contain in the collection `PasswordCompareValidation` the entries to compare (see examples above), additional bindable properties include `ValidColor` and `InValidColor` that apply if there is or isn't a match between the collection of Entries. 

PLEASE NOTE: Although these bindable properties are set per entry, they should all have the same values for all entries to check in the collection. You are able to set the `MinimumLength` of the passwords. Currently the password validation requires an uppercase, lowercase and a number, therefore the minimum length can be set.

* `EmailValidatorBehavior`

Used for emails. The `EmailRegularExpression` bindable property can be overridden if desired.

* `GoToNextEntryOnLengthBehaviour`

Sets focus to the `NextEntry` on the given `CharacterLength`. In addition required to set `MaxLengthValidator` i.e. MaxLength="1" to ensure that the entry doesn't exceed the `CharacterLength`

* `ShowHiddenEntryEffect`

Toggles between masked and unmasked of a entry with type password. Use `Effects = { new ShowHiddenEntryEffect() }`

* `MaskedBehavior`

Applies a mask to the Entry. Bindable properties InValidColor and ValidColor can be set to update the color for of the drawable image within the Entry. Please set `LeftIcon` to enable this. Refer to docs. 

**License**

Licensed under MIT, see license file
