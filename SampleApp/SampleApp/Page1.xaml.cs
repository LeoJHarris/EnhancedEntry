namespace SampleApp
{
    using System.Collections.Generic;

    using LeoJHarris.EnhancedEntry.Plugin.Abstractions;
    using LeoJHarris.EnhancedEntry.Plugin.Abstractions.Helpers;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            EnhancedEntry advancedEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                Placeholder = "Type email",
                FocusBorderColor = Color.Green,
                BackgroundColor = Color.Yellow,
                BorderWidth = 3,
                CornerRadius = 10,
                Behaviors =
                {
                    new EmailValidatorBehavior(),
                },
                Keyboard = Keyboard.Email,
                ReturnKeyType = ReturnKeyTypes.Done
            };

            EnhancedEntry entryPasswordConfirm = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                Placeholder = "Password confirm",
                IsPassword = true
            };

            EnhancedEntry passwordEntry = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                Placeholder = "Password",
                IsPassword = true,
                PasswordCompareValidation = new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    entryPasswordConfirm
                })

                {
                    ValidColor = Color.Orange,
                    InValidColor = Color.Red
                }

                ,
            };

            entryPasswordConfirm.PasswordCompareValidation =
                new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    passwordEntry
                })
                {
                    ValidColor = Color.Orange,
                    InValidColor = Color.Red
                };

            EnhancedEntry entry3 = new EnhancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
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

            if (entryPasswordConfirm.PasswordCompareValidation.IsValid && entryPasswordConfirm.PasswordCompareValidation.IsValid)
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