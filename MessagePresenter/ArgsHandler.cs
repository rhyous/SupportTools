using System;
using System.Collections.Generic;

namespace MessagePresenter
{
    public class ArgsHandler
    {
        public static ArgsHandler Instance
        {
            get { return _Instance ?? (_Instance = new ArgsHandler()); }
        } private static ArgsHandler _Instance;

        private ArgsHandler()
        {
        }

        public string[] Args
        {
            get { return _Args; }
            set
            {
                _Args = value;
                ParseArgs(_Args);
            }
        } private string[] _Args;

        private Dictionary<string, string> PropertyValueArgs
        {
            get { return _PropertyValueArgs ?? (_PropertyValueArgs = new Dictionary<string, string>()); }
        } private static Dictionary<string, string> _PropertyValueArgs;

        public void ParseArgs(string[] inArgs)
        {
            foreach (string arg in inArgs)
            {
                if (arg.StartsWith("html=", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyValueArgs.Add("html", arg.Substring("html=".Length));
                }

                if (arg.StartsWith("from=", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyValueArgs.Add("from", arg.Substring("from=".Length));
                }

                if (arg.StartsWith("timeout=", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyValueArgs.Add("timeout", arg.Substring("timeout=".Length));
                }

                // shortened version of timeout
                if (arg.StartsWith("t=", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyValueArgs.Add("timeout", arg.Substring("t=".Length));
                }

                if (arg.StartsWith("title=", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyValueArgs.Add("title", arg.Substring("title=".Length));
                }
            }
        }

        public string this[string arg]
        {
            get
            {
                string argValue;
                PropertyValueArgs.TryGetValue(arg, out argValue);
                return argValue;
            }
        }
    }
}
