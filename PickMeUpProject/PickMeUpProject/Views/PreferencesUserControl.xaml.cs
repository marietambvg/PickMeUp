using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PickMeUpProject.Views
{
    public sealed partial class PreferencesUserControl : UserControl
    {
        public PreferencesUserControl()
        {
            this.InitializeComponent();
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("Remember"))
                Remember.IsOn = (bool)ApplicationData.Current.RoamingSettings.Values["Remember"];

        }
        public void OnToggled(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.RoamingSettings.Values["Remember"] = Remember.IsOn;
        }
    }
}
