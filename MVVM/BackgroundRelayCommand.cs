using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace AspectMVVM
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the 
    /// CanExecute method is 'true'.
    /// </summary>
    public class BackgroundRelayCommand : ICommand
    {
        #region Fields
        protected readonly Action<object> _Execute;
        protected readonly Predicate<object> _CanExecute;
        protected readonly BackgroundWorker _Worker;
        #endregion // Fields

        #region Constructors
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public BackgroundRelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public BackgroundRelayCommand(Action<object> inExecute, Predicate<object> inCanExecute)
        {
            if (inExecute == null)
                throw new ArgumentNullException("execute");
            _Execute = inExecute;
            _CanExecute = inCanExecute;

            if (_Worker == null)
            {
                _Worker = new BackgroundWorker();
                _Worker.DoWork += ExecuteAsync;
            }
        }
        #endregion // Constructors

        #region ICommand Members
        [DebuggerStepThrough]
        public bool CanExecute(object inParameter)
        {
            return _CanExecute == null ? true : _CanExecute(inParameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object inParameter)
        {
            _Worker.RunWorkerAsync(inParameter);
        }

        #endregion // ICommand Members

        #region BackgroundWorker methods

        void ExecuteAsync(object sender, DoWorkEventArgs inArgs)
        {
            _Execute(inArgs.Argument);
        }

        #endregion
    }
}