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
using System.IO;
using System.Xml.Serialization;

namespace SupportTools.ContextMenuXml
{
    public static class Serializer
    {
        #region Functions        
        public static void SerializeToXML<T>(T t, string inFilename)
        {
            var serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = new StreamWriter(inFilename);
            serializer.Serialize(textWriter, t);
            textWriter.Close();
        }

        public static T DeserializeFromXML<T>(string inFilename)
        {
            var deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(inFilename);
            var retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }

        public static T DeserializeFromXML<T>(string inSnippetOrFile, bool isString)
        {
            var deserializer = new XmlSerializer(typeof(T));
            var textReader = isString ? new StringReader(inSnippetOrFile) : (TextReader)new StreamReader(inSnippetOrFile);
            var retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }
        #endregion
    }
}
