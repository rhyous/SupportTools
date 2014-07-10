using System;
using System.Windows.Input;
using AspectMVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Business;
using Rhyous.ServiceManager.Model;
using Rhyous.ServiceManager.Singletons;

namespace Rhyous.ServiceManager.ViewModel
{
    [NotifyPropertyChangedClass]
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

        // Bind directly to the ServiceStore.Instance.Services object
        [NotifyPropertyChanged]
        public ServiceCollection Services
        {
            get { return ServiceStore.Instance.Services; }
            set { ServiceStore.Instance.Services = value; }
        }

        [NotifyPropertyChanged]
        public Service SelectedService { get; set; }

        [NotifyPropertyChanged]
        public ICommand StartStopServiceCommand { get; set; }

        [NotifyPropertyChanged]
        public ICommand ServiceCollectionCommand { get; set; }

        [NotifyPropertyChanged]
        public bool IsWorking { get; set; }

        #endregion

        #region Methods

        [BackgroundWorkerAspect]
        private void StartStopService(object inArgs)
        {
            IsWorking = true;
            var args = inArgs as Object[];
            if (args.Length == 2)
            {
                var service = args[0] as Service;
                var action = args[1] as String;

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
            var status = inStatus as String;
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
