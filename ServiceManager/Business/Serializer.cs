using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Rhyous.ServiceManager.Business
{
    public static class Serializer
    {
        private static Dictionary<string, bool> LockTable = new Dictionary<string, bool>();

        #region Serialization Functions
        /// <summary>
        /// This function writes the serialized XML to the file name passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="outFileName">The file name. It can be a full path.</param>
        public static void SerializeToXML<T>(T t, string outFileName, XmlSerializerNamespaces inNameSpaces = null) where T : class
        {
            if (IsLocked(outFileName))
            {
                WaitForUnlock(outFileName);
                if (IsLocked(outFileName))
                    return;
            }
            Lock(outFileName);
            if (null == t) return;

            if (!File.Exists(outFileName))
            {
                var dir = Path.GetDirectoryName(outFileName);
                if (!Directory.Exists(dir) && !string.IsNullOrWhiteSpace(dir))
                    Directory.CreateDirectory(dir);
            }

            var ns = inNameSpaces;
            if (ns == null)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = new StreamWriter(outFileName);
            serializer.Serialize(textWriter, t, ns);
            textWriter.Close();
            Unlock(outFileName);
        }

        /// <summary>
        /// This function returns the serialized XML as a string
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="outString">The string that will be passed the XML.</param>
        public static string SerializeToXML<T>(T t, XmlSerializerNamespaces inNameSpaces = null)
        {
            var ns = inNameSpaces;
            if (ns == null)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = new StringWriter();
            serializer.Serialize(textWriter, t, ns);
            return textWriter.ToString();
        }

        /// <summary>
        /// This function deserializes the XML file passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="inFilename">The file or full path to the file.</param>
        /// <returns>The object that was deserialized from xml.</returns>
        public static T DeserializeFromXML<T>(string inFilename)
        {
            // Wait 1 second if file doesn't exist, in case we are waiting on a
            // separate thread and beat it here.
            if (!File.Exists(inFilename))
                Thread.Sleep(1000);

            // File should exist by now.
            if (File.Exists(inFilename))
            {
                var deserializer = new XmlSerializer(typeof(T));
                TextReader textReader = new StreamReader(inFilename);
                var reader = new XmlTextReader(textReader);
                try { reader.Read(); }
                catch { }
                var retVal = (T)deserializer.Deserialize(reader);
                textReader.Close();
                return retVal;
            }
            throw new FileNotFoundException(inFilename);
        }

        /// <summary>
        /// This function deserializes the XML string passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="inString">The string containing the XML.</param>
        /// <returns>The object that was deserialized from xml.</returns>
        public static T DeserializeFromXML<T>(ref string inString)
        {
            var deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(inString);
            var retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }
        #endregion

        #region Lock functions

        private static void Lock(string inFileName)
        {
            if (LockTable.ContainsKey(inFileName))
            {
                LockTable[inFileName] = true;
            }
            else
            {
                LockTable.Add(inFileName, true);
            }
        }

        private static void Unlock(string inFileName)
        {
            if (LockTable.ContainsKey(inFileName))
            {
                LockTable[inFileName] = false;
            }
            else
            {
                LockTable.Add(inFileName, false);
            }
        }

        private static bool IsLocked(string inFileName)
        {
            if (LockTable.ContainsKey(inFileName))
            {
                return LockTable[inFileName];
            }
            return false;
        }

        private static void WaitForUnlock(string inFileName)
        {
            var i = 0;
            while (LockTable[inFileName])
            {
                Thread.Sleep(100);
                i++;
                if (i > 50)
                    break;
            }
        }

        #endregion
    }
}