/* 
 * Copyright (c) Rhyous, Jared A. Barneck
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * 
 *    * Redistributions of source code must retain the above copyright notice, this list of 
 *      conditions and the following disclaimer.
 *      
 *    * Redistributions in binary form must reproduce the above copyright notice, this list
 *      of conditions and the following disclaimer in the documentation and/or other materials
 *      provided with the distribution.
 *    
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY 
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR 
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using AspectMVVM;
using Rhyous.Agent.Ping.Business;
using Rhyous.Agent.Ping.Model;

namespace Rhyous.Agent.Ping.ViewModel
{
    [NotifyPropertyChangedClass]
    public class LDPingViewModel
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public LDPingViewModel()
        {
            if (App.Args.Length == 1)
            {
                IPAddress = App.Args[0];
                Ping();
            }


            var npc = this as INotifyPropertyChangedWithMethod;
            if (npc != null)
                npc.PropertyChanged += OnIPAddressChanged;
        }
        #endregion

        #region Properties

        [NotifyPropertyChanged]
        public LDPing LDPing { get; set; }

        [NotifyPropertyChanged]
        public string IPAddress { get; set; }

        [NotifyPropertyChanged]
        public string ConnectionResult { get; set; }

        public bool CanPing
        {
            get { return !string.IsNullOrEmpty(IPAddress) && !IsPinging; }
        }

        [NotifyPropertyChanged("CanPing", "PingCommand", "IsPinging")]
        public bool IsPinging { get; set; }

        public ICommand PingCommand
        {
            get { return _PingCommand ?? (_PingCommand = new RelayCommand(param => Ping(), param => CanPing)); }
        } private RelayCommand _PingCommand;
        #endregion

        #region Functions
        /// <summary>
        /// This is the Ping method that launches an LDPing as a BackgroundWorker process, using 
        /// PingWorker which is an object that inherits BackgroundWorker and adds one property called
        /// PingSucceeded.
        /// </summary>
        private void Ping()
        {
            IsPinging = true;
            ClearPingResults();

            var worker = new PingWorker();
            worker.DoWork += worker_DoPing;
            worker.RunWorkerCompleted += worker_PingCompleted;
            worker.RunWorkerAsync();
        }

        void worker_DoPing(object sender, DoWorkEventArgs e)
        {
            var timer = new Stopwatch();
            timer.Start();

            // Now lets ping
            var worker = sender as PingWorker;
            if (worker == null) return;
            var tmpLDPing = LDPingAction.AgentPing(IPAddress);
            worker.PingSucceeded = tmpLDPing != null;
            timer.Stop();

            // Lets make sure the GUI spinner has at least a little spin
            var i = 1200 - (int)timer.ElapsedMilliseconds;
            if (i > 0)
                Thread.Sleep(i);
            LDPing = tmpLDPing;
        }

        void worker_PingCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsPinging = false;
            var worker = sender as PingWorker;
            if (worker == null) return;
            ConnectionResult = worker.PingSucceeded ? "Ping Successful." : "Ping failed.";

            // This makes the UI refresh against the RelayCommand.CanExecute() function.
            // Essentially, this makes the button enable when pinging is done.
            CommandManager.InvalidateRequerySuggested();
        }

        void OnIPAddressChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IPAddress)
                ClearPingResults();
        }

        private void ClearPingResults()
        {
            LDPing = null;
            ConnectionResult = string.Empty;

            var me = this as INotifyPropertyChangedWithMethod;
            if (me == null) return;
            me.OnPropertyChanged("CanPing");
            me.OnPropertyChanged("PingCommand");
        }
        #endregion
    }
}
