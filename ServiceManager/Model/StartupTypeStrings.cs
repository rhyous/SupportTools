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
        } private static StartupTypeStrings _Instance;

        #endregion

        #region members

        /// <summary>
        /// Must match the order of the StartupType enum
        /// </summary>
        private List<string> _List = new List<string> { "Unknown", "Automatic (Delayed Start)", "Automatic", "Manual", "Disabled" };

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

        public string Unknown { get; private set; }

        public string AutomaticDelayed { get; private set; }

        public string Automatic { get; private set; }

        public string Manual { get; private set; }

        public string Disabled { get; private set; }

        #endregion

        #region Methods

        public string GetStartupTypeString(StartupType inStartupType)
        {
            return _List[(int)inStartupType];
        }

        #endregion
    }
}
