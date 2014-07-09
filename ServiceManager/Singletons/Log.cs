using System;
using System.IO;
using System.Reflection;
using Common.Aspects;

namespace Rhyous.ServiceManager.Singletons
{
    [MethodTraceAspect(AttributeExclude = true)]
    public class Log
    {
        private int _LogId = -1;

        public static Log Instance = new Log();

        private Log()
        {
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FileName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            Extension = ".log";
        }

        public String FileName
        {
            get
            {
                if (_LogId > -1)
                    return _FileName + "." + _LogId + Extension;

                return FilePath + _FileName + Extension;
            }

            // The root file name, without the extension
            set
            {
                _FileName = value;
                while (File.Exists(FileName))
                    _LogId++;
            }
        } private String _FileName;

        public String FilePath { get; set; }

        public String Extension { get; set; }

        public static void WriteLine(String inLogMessage)
        {
            Instance.Write(inLogMessage + Environment.NewLine);
        }

        public void Write(String inMessage)
        {
            lock (Instance)
            {
                var logWriter = new StreamWriter(FileName, true);
                logWriter.Write(inMessage);
                logWriter.Close();
            }
        }
    }
}
