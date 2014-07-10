using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Windows;

namespace MessagePresenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            if (ArgsHandler.Instance["html"].StartsWith("http://") || ArgsHandler.Instance["html"].StartsWith("https://"))
            {
                WebBrowserMessage.Navigate(ArgsHandler.Instance["html"]);
            }
            else
            {
                WebBrowserMessage.NavigateToString(WebUtility.HtmlDecode(ArgsHandler.Instance["html"]));
            }

            if (!string.IsNullOrEmpty(ArgsHandler.Instance["title"]))
                Title = ArgsHandler.Instance["title"];

            if (string.IsNullOrEmpty(ArgsHandler.Instance["from"]))
                GroupBoxFrom.Visibility = Visibility.Collapsed;
            else
                TextBlockFrom.Text = ArgsHandler.Instance["from"];
        }

        private void MessageLoaded(object sender, RoutedEventArgs e)
        {
            var strTimeout = ArgsHandler.Instance["timeout"];
            if (string.IsNullOrEmpty(strTimeout))
                return;

            var timeout = int.Parse(strTimeout);
            if (timeout < 1)
                return;

            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(timeout);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ButtonDismiss.Content = string.Format("Dismiss (Automatic dismissal in {0} seconds.)", e.ProgressPercentage);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = (int)e.Argument; i > -1; i--)
            {
                Thread.Sleep(1000);
                (sender as BackgroundWorker).ReportProgress(i);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
