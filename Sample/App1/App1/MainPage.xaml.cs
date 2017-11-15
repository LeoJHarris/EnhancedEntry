using LeoJHarris.AdvancedEntry.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoJHarris.AdvancedEntry.Plugin.Abstractions.Helpers;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            AdvancedEntry advancedEntry = new AdvancedEntry
            {
                BorderColor = Color.Red,
                FocusBorderColor = Color.Green,
                BackgroundColor = Color.Yellow,
                BorderWidth = 1,
                CornerRadius = 2,
                EmailValidatorBehavior = new EmailValidatorBehavior()
            };

            AdvancedEntry advancedEntry3 = new AdvancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                GoToNextEntryOnLengthBehaviour = new GoToNextEntryOnLengthBehaviour(advancedEntry)
                {
                    CharacterLength = 2
                }
            };

            AdvancedEntry advancedEntry2 = new AdvancedEntry
            {
                BorderColor = Color.Red,
                BorderWidth = 1,
                CornerRadius = 2,
                PasswordCompareValidation = new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    advancedEntry3
                })
                {
                    ValidColor = Color.Orange,
                    InValidColor = Color.Red
                },
            };


            advancedEntry3.PasswordCompareValidation =
                new PasswordCompareValidationBehavior(new List<Entry>()
                {
                    advancedEntry2
                })
                {
                    ValidColor = Color.Orange,
                    InValidColor = Color.Red
                };



            CVAdvancedEntry1.Content = advancedEntry;
            CVAdvancedEntry2.Content = advancedEntry2;
            CVAdvancedEntry3.Content = advancedEntry3;
        }
    }
}
