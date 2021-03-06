﻿using System.Windows;

namespace MessagePresenter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            ArgsHandler.Instance.Args = e.Args;
            if (string.IsNullOrEmpty(ArgsHandler.Instance["html"]))
            {
                Shutdown();
                return;
            }

            var mw = new MainWindow();
            mw.Show();
        }

    }
}
