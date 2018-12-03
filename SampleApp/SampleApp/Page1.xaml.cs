using LeoJHarris.FormsPlugin.Abstractions;
using LeoJHarris.FormsPlugin.Abstractions.Effects;
using LeoJHarris.FormsPlugin.Abstractions.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1
    {
        public Page1()
        {
            InitializeComponent();

            EnhancedEntry entry3 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                FontSize = 11,
                ReturnKeyType = ReturnKeyTypes.Done,
                Placeholder = "Show dialog on tap",
                KeyBoardAction = new Command(() => DisplayAlert("Passwords match!", "Both passwords match", "OK"))
            };

            EnhancedEntry entryPasswordConfirm = new EnhancedEntry
            {
                BorderColor = Color.Red,
                LeftIcon = "password",
                Effects = { new ShowHiddenEntryEffect() },
                BorderWidth = 1,
                CornerRadius = 2,
                Placeholder = "Password confirm",
                IsPassword = true,
                NextEntry = entry3,
                ReturnKeyType = ReturnKeyTypes.Next,
            };

            EnhancedEntry passwordEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                LeftIcon = "password",
                BorderWidth = 1,
                CornerRadius = 2,
                Placeholder = "Password",
                Effects = { new ShowHiddenEntryEffect() },
                IsPassword = true,
                Behaviors = { new PasswordCompareValidationBehavior(new List<Entry>
                    {
                        entryPasswordConfirm
                    })
                    {
                        ValidColor = Color.Green,
                        InValidColor = Color.Red
                    }
                },
                NextEntry = entryPasswordConfirm,
                ReturnKeyType = ReturnKeyTypes.Done
            };

            EnhancedEntry advancedEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                LeftIcon = "account",
                Placeholder = "Type email",
                FocusBorderColor = Color.Green,
                BackgroundColor = Color.Yellow,
                BorderWidth = 3,
                CornerRadius = 10,
                Behaviors =
                {
                    new EmailValidatorBehavior
                    {
                        InValidColor = Color.Red,
                        ValidColor = Color.Green
                    }
                },
                NextEntry = passwordEntry,
                Keyboard = Keyboard.Email,
                ReturnKeyType = ReturnKeyTypes.Next
            };


            entryPasswordConfirm.Behaviors.Add(new PasswordCompareValidationBehavior(new List<Entry> { passwordEntry })
            {
                ValidColor = Color.Green,
                InValidColor = Color.Red
            });

            EnhancedEntry maskEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                LeftIcon = "account",
                CornerRadius = 2,
                ReturnKeyType = ReturnKeyTypes.Done,
                Keyboard = Keyboard.Numeric,
                Behaviors =
                {
                    new MaskedBehavior
                    {
                        Mask="XXXX-XXXX-XXXX-XXXX",
                        ValidColor = Color.Green,
                    }
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                EntryHeight = 80
            };

            EnhancedEntry jumpToEntry3 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                ReturnKeyType = ReturnKeyTypes.Done,
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            EnhancedEntry jumpToEntry2 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                NextEntry = entry3,
                ReturnKeyType = ReturnKeyTypes.Next,
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                Behaviors = { new MaxLengthValidator
                    {
                        MaxLength = 2
                    },
                    new GoToNextEntryOnLengthBehaviour(jumpToEntry3)
                    {
                        CharacterLength = 2
                    }
                }
            };

            EnhancedEntry jumpToEntry1 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                NextEntry = entry3,
                ReturnKeyType = ReturnKeyTypes.Next,
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                Behaviors = { new MaxLengthValidator
                    {
                    MaxLength = 2
                },
                      new GoToNextEntryOnLengthBehaviour(jumpToEntry2)
                    {
                        CharacterLength = 2
                    }
                }
            };

            StackLayout stackNextEntries = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 0),
                Children =
                {
                    jumpToEntry1,
                    jumpToEntry2,
                    jumpToEntry3,

                }
            };

            if (((PasswordCompareValidationBehavior)passwordEntry.Behaviors
                       .FirstOrDefault(behavior => behavior.GetType() ==
                                                typeof(PasswordCompareValidationBehavior))).IsValid &&
                ((PasswordCompareValidationBehavior)entryPasswordConfirm.Behaviors
                       .FirstOrDefault(behavior => behavior.GetType() ==
                                                typeof(PasswordCompareValidationBehavior))).IsValid)
            {
                this.DisplayAlert("Passwords match!", "Both passwords match", "OK");
            }

            StackLayout stack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Spacing = 10,
                Margin = 20,
                Children =
                {
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = advancedEntry
                    },
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = passwordEntry
                    },
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = entryPasswordConfirm
                    },
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = entry3
                    },
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = stackNextEntries
                    },
                    new ContentView
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Content = maskEntry
                    }
                }
            };

            CV.Content = stack;
        }
    }
}
