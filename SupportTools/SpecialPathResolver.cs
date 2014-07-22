using Microsoft.Win32;

namespace SupportTools
{
    public class SpecialPathResolver
    {
        public static SpecialPathResolver Instance
        {
            get { return _Instance ?? (_Instance = new SpecialPathResolver()); }
        } private static SpecialPathResolver _Instance;

        private SpecialPathResolver()
        {
        }

        public string LDMainPath
        {
            get { return _LDMainPath ?? (_LDMainPath = GetLDMainPathFromReg()); }
        } private string _LDMainPath;

        /*
         *  This is not for environment variables, this is for special variables such as
         *  %DTMDIR% or %ldmain%, which both should resolve to the ManagementSuite directory.
         */
        public string ResolveCommandPathVars(string inCommand)
        {
            // Make it all lowercase cause I don't want to worry about case
            // when checking if the command path contains these variables.
            inCommand = inCommand.ToLower();

            // I choose %ldmain% because that is more current and recognized
            // terminology for the managemementsuite folder.
            if (inCommand.Contains("%ldmain%"))
            {
                return inCommand.Replace("%ldmain%", LDMainPath);
            }

            // %dtmdir% is what custom scripts uses so I added it too but it no longer
            // makes sense as dtmdir is old terminology.
            if (inCommand.Contains("%dtmdir%"))
            {
                return inCommand.Replace("%dtmdir%", LDMainPath);
            }
            return inCommand;
        }

        private string GetLDMainPathFromReg()
        {
            const string defaultFilePath = @"C:\Program Files\LANDesk\ManagementSuite\";

            // Try 32 bit registry
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            // Try 64 bit registry
            var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            key = localKey.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            return defaultFilePath;
        }

    }
}
