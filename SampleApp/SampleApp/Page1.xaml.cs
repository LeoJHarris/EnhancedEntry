using System.Linq;
using LeoJHarris.EnhancedEntry.Plugin.Abstractions.Effects;
using LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers;

namespace SampleApp
{
    using System.Collections.Generic;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using LeoJHarris.EnhancedEntry.Plugin.Abstractions;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1
    {
        public Page1()
        {
            EnhancedEntry advancedEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                LeftIcon = "account",
                Placeholder = "Type email",
                FocusBorderColor = Color.Green,
                BackgroundColor = Color.Yellow,
                BorderWidth = 3,
                HasShowAndHidePassword = true,
                CornerRadius = 10,
                Behaviors =
                {
                    new EmailValidatorBehavior()
                    {
                        InValidColor = Color.Red,
                        ValidColor = Color.Green
                    },
                },

                Keyboard = Keyboard.Email,
                ReturnKeyType = ReturnKeyTypes.Done
            };

            EnhancedEntry entryPasswordConfirm = new EnhancedEntry
            {
                BorderColor = Color.Red,
                LeftIcon = "password",
                Effects = { new ShowHiddenEntryEffect() },
                BorderWidth = 1,
                CornerRadius = 2,
                Placeholder = "Password confirm",
                IsPassword = true
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
                Behaviors = { new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    entryPasswordConfirm
                })
                {
                    ValidColor = Color.Green,
                    InValidColor = Color.Red,
                }
                }
            };

            entryPasswordConfirm.Behaviors.Add(
                new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    passwordEntry
                })
                {
                    ValidColor = Color.Green,
                    InValidColor = Color.Red
                });

            EnhancedEntry entry3 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                FontSize = 11,
                Placeholder = "Tap done in keyboard to execute some code in keyboardaction...",
                KeyBoardAction = new Command(
                    () =>
                    {
                        DisplayAlert("Tapped", "Action executed", "OK");
                    }),
            };

            EnhancedEntry jumpToEntry3 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                NextEntry = entry3,
                ReturnKeyType = ReturnKeyTypes.Next,
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
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
                Behaviors = { new MaxLengthValidator()
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
                Behaviors = { new MaxLengthValidator()
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
                    jumpToEntry3
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

                                            }
            };

            ScrollView scrollView = new ScrollView
            {
                Content = stack
            };

            Content = scrollView;

            InitializeComponent();
        }
    }
}