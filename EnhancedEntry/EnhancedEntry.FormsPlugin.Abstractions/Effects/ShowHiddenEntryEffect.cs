using Xamarin.Forms;

namespace LeoJHarris.FormsPlugin.Abstractions.Effects
{
    public class ShowHiddenEntryEffect : RoutingEffect
    {
        public string EntryText { get; set; }
        public ShowHiddenEntryEffect() : base("Xamarin.ShowHiddenEntryEffect") { }
    }
}