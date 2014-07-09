using Microsoft.Win32;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Singletons;

namespace Rhyous.ServiceManager.View
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void ExitClick(object sender, System.Windows.RoutedEventArgs e)
        {
            App.Instance.Shutdown(0);
        }

        [ExceptionAspect]
        private void OpenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { DefaultExt = ".xml", Filter = "XML |*.xml" };

            // Show open file dialog box
            var result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                ServiceStore.CreateInstanceFromXml(dlg.FileName);
            }
        }
    }
}
