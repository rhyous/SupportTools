using System.Windows;

namespace MessagePresenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            ArgsHandler.Instance.Args = e.Args;
            if (string.IsNullOrEmpty(ArgsHandler.Instance["html"]))
            {
                Shutdown();
                return;
            }

            MainWindow mw = new MainWindow();
            mw.Show();
        }

    }
}
