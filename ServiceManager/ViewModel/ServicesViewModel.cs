using System;
using System.Windows.Input;
using Rhyous.MVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Business;
using Rhyous.ServiceManager.Model;
using Rhyous.ServiceManager.Singletons;

namespace Rhyous.ServiceManager.ViewModel
{
    [NotifyPropertyChanged]
    class ServicesViewModel
    {
        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        public ServicesViewModel()
        {
            StartStopServiceCommand = new RelayCommand(StartStopService, StartStopServiceCanExecute);
            ServiceCollectionCommand = new RelayCommand(StartStopAllServices, f => ServiceCollectionCommandCanExecute());
        }

        #endregion

        #region Properties

        public ServiceCollection Services
        {
            get { return ServiceStore.Instance.Services; }
            set { ServiceStore.Instance.Services = value; }
        }

        public Service SelectedService { get; set; }

        public ICommand StartStopServiceCommand { get; set; }

        public ICommand ServiceCollectionCommand { get; set; }

        public bool IsWorking { get; set; }

        #endregion

        #region Methods

        [BackgroundWorkerAspect]
        private void StartStopService(object inArgs)
        {
            IsWorking = true;
            Object[] args = inArgs as Object[];
            if (args.Length == 2)
            {
                Service service = args[0] as Service;
                String action = args[1] as String;

                if (action == "Start")
                {
                    service.Start();
                    goto End;
                }
                if (action == "Stop")
                {
                    service.Stop();
                    goto End;
                }
                if (action == "Wait")
                    goto End;
            }
            throw new ArgumentException("Parameters are invalid.");
        End:
            OnCompleted();
        }

        private bool StartStopServiceCanExecute(object inArgs)
        {
            return !IsWorking;
        }

        [BackgroundWorkerAspect]
        private void StartStopAllServices(object inStatus)
        {
            IsWorking = true;
            String status = inStatus as String;
            switch (status)
            {
                case "Start":
                    Services.StartServices();
                    break;
                case "Restart":
                    Services.StopServices();
                    break;
                case "Stop":
                    Services.StopServices();
                    break;
                default:
                    return;
            }
            OnCompleted();
        }

        [RunOnGuiThreadAspect]
        public void OnCompleted()
        {
            IsWorking = false;
            CommandManager.InvalidateRequerySuggested();
        }

        public bool ServiceCollectionCommandCanExecute()
        {
            return !IsWorking;
        }

        #endregion
    }
}
