using System.Windows;
using Rhyous.ServiceManager.Singletons;
using Rhyous.ServiceManager.View;
using Rhyous.ServiceManager.ViewModel;

namespace Rhyous.ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static App Instance;

        protected override void OnStartup(StartupEventArgs e)
        {
            Instance = this;
            ColumnSettings.CreateInstanceFromXml();
            ServiceStore.CreateInstanceFromXml(ServiceStore.DEFAULT_FILE);

            base.OnStartup(e);
            var main = new MainWindow();
            main.ServicesView.DataContext = new ServicesViewModel();
            main.Show();
        }
    }
}
