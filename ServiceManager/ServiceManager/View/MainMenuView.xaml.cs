﻿using Microsoft.Win32;
using Rhyous.ServiceManager.Business;
using Rhyous.ServiceManager.Model;
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

        private void OpenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "XML |*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                ServiceStore.Instance.Services = Serializer.DeserializeFromXML<ServiceCollection>(dlg.FileName);
            }
        }
    }
}
