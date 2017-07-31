using AdvancedEntry.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using AdvancedEntry.FormsPlugin.WindowsPhone;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(AdvancedEntry.FormsPlugin.Abstractions.AdvancedEntryControl), typeof(AdvancedEntryRenderer))]
namespace AdvancedEntry.FormsPlugin.WindowsPhone
{
    /// <summary>
    /// AdvancedEntry Renderer
    /// </summary>
    public class AdvancedEntryRenderer //: TRender (replace with renderer type
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
    }
}
