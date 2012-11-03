using System.Windows;
using Rhyous.ServiceManager.Singletons;
using Rhyous.ServiceManager.View;
using Rhyous.ServiceManager.ViewModel;

namespace Rhyous.ServiceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;

        protected override void OnStartup(StartupEventArgs e)
        {
            Instance = this;
            ColumnSettings.CreateInstanceFromXml();
            ServiceStore.CreateInstanceFromXml();

            base.OnStartup(e);
            MainWindow main = new MainWindow();
#if DEBUG
            main.ServicesView.DataContext = new ServicesViewModel()
            {
                Services = new Rhyous.ServiceManager.Business.ExampleData().Services
            };
#else
            main.ServicesView.DataContext = new ServicesViewModel();
#endif
            main.Show();
        }
    }
}
