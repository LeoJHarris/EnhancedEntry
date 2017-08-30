using LeoJHarris.Control.Abstractions;
using LeoJHarris.Control.WindowsPhone;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(AdvancedEntry), typeof(AdvancedEntryRenderer))]
namespace LeoJHarris.Control.WindowsPhone
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
