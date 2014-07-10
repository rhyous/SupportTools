using System;
using System.Collections.Generic;

namespace Rhyous.ServiceManager.Model
{
    public enum StartupType
    {
        Unknown,
        AutomaticDelayed,
        Automatic,
        Manual,
        Disabled
    }

    public class StartupTypeStrings
    {
        #region Statics

        public static StartupTypeStrings Instance
        {
            get { return _Instance ?? (_Instance = new StartupTypeStrings()); }
            private set { _Instance = value; }
        } private static StartupTypeStrings _Instance;

        #endregion

        #region members

        /// <summary>
        /// Must match the order of the StartupType enum
        /// </summary>
        private List<String> _List = new List<string> { "Unknown", "Automatic (Delayed Start)", "Automatic", "Manual", "Disabled" };

        #endregion

        #region Constructor

        private StartupTypeStrings()
        {
            Unknown = _List[(int)StartupType.Unknown];
            AutomaticDelayed = _List[(int)StartupType.AutomaticDelayed];
            Automatic = _List[(int)StartupType.Automatic];
            Manual = _List[(int)StartupType.Manual];
            Disabled = _List[(int)StartupType.Disabled];
        }

        #endregion

        #region Properties

        public String Unknown { get; private set; }

        public String AutomaticDelayed { get; private set; }

        public String Automatic { get; private set; }

        public String Manual { get; private set; }

        public String Disabled { get; private set; }

        #endregion

        #region Methods

        public String GetStartupTypeString(StartupType inStartupType)
        {
            return _List[(int)inStartupType];
        }

        #endregion
    }
}
