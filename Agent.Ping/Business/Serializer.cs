﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Rhyous.Agent.Ping.Business
{
    [ExcludeFromCodeCoverage]
    public static class Serializer
    {
        #region Functions

        /// <summary>
        /// This function writes the serialized XML to the file name passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="outFilename">The file name. It can be a full path.</param>
        /// <param name="inOmitXmlDeclaration"></param>
        /// <param name="inNameSpaces"></param>
        /// <param name="inEncoding"></param>
        public static void SerializeToXml<T>(T t, string outFilename, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null)
        {
            MakeDirectoryPath(outFilename);
            var ns = inNameSpaces;
            if (ns == null)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            var textWriter = (TextWriter)new StreamWriter(outFilename);
            if (inEncoding != null && inEncoding.Equals(Encoding.UTF8))
                textWriter = new Utf8StreamWriter(outFilename);
            var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { OmitXmlDeclaration = inOmitXmlDeclaration });
            serializer.Serialize(xmlWriter, t, ns);
            textWriter.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outFilename"></param>
        private static void MakeDirectoryPath(string outFilename)
        {
            var dir = Path.GetDirectoryName(outFilename);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// This function returns the serialized XML as a string
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="inOmitXmlDeclaration"></param>
        /// <param name="inNameSpaces"></param>
        /// <param name="inEncoding"></param>
        public static string SerializeToXml<T>(T t, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null)
        {
            var ns = inNameSpaces;
            if (ns == null)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            var textWriter = (TextWriter)new StringWriter();
            if (inEncoding != null && inEncoding.Equals(Encoding.UTF8))
                textWriter = new Utf8StringWriter();
            var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { OmitXmlDeclaration = inOmitXmlDeclaration });
            serializer.Serialize(xmlWriter, t, ns);
            return textWriter.ToString();
        }

        /// <summary>
        /// This function deserializes the XML file passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="inFilename">The file or full path to the file.</param>
        /// <returns>The object that was deserialized from xml.</returns>
        public static T DeserializeFromXml<T>(String inFilename)
        {
            if (string.IsNullOrWhiteSpace(inFilename))
            {
                return default(T);
            }
            // Wait 1 second if file doesn't exist, in case we are waiting on a
            // separate thread and beat it here.
            if (!File.Exists(inFilename))
                Thread.Sleep(1000);

            // File should exist by now.
            if (File.Exists(inFilename))
            {
                var deserializer = new XmlSerializer(typeof(T));
                var textReader = (TextReader)new StreamReader(inFilename);
                var reader = new XmlTextReader(textReader);
                reader.Read();
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
        public static T DeserializeFromXml<T>(ref string inString)
        {
            if (string.IsNullOrWhiteSpace(inString))
            {
                return default(T);
            }
            var deserializer = new XmlSerializer(typeof(T));
            var textReader = (TextReader)new StringReader(inString);
            var retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }
        #endregion

        #region UTF8StringWriter
        public sealed class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

        public sealed class Utf8StreamWriter : StreamWriter
        {
            public Utf8StreamWriter(string file)
                : base(file)
            {
            }

            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
        #endregion
    }
}