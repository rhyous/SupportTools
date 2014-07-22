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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SupportTools
{
    public static class PromptHandler
    {
        public static string CheckForPrompt(string inParameter)
        {
            if (string.IsNullOrEmpty(inParameter))
                return string.Empty;

            if (inParameter.ToLower().Contains("%prompt"))
            {
                // Make sure that %prompt:Name of value to get% is replaced regardless of whether it is upper case
                // or lower case.  We do this by remove %Promp% and in any case and replacing
                // it with %prompt% all lowercase, without affecting the case of other parameters,
                // because some commands have case sensitive parameters.
                var allPrompts = new List<string>();
                var matchposition = new List<int>();


                // Create a new Regex object and define the regular expression.
                var r = new Regex("%prompt:([^%]*%)");
                // Use the Matches method to find all matches in the input string.
                var mc = r.Matches(inParameter);
                // Loop through the match collection to retrieve all 
                // matches and positions.
                for (var i = 0; i < mc.Count; i++)
                {
                    // Add the match string to the string array.   
                    allPrompts.Add(mc[i].Value.Substring("%prompt:".Length, mc[i].Length - "%prompt:".Length - 1));
                    // Record the character position where the match was found.
                    matchposition.Add(mc[i].Index);

                }
                var myForm = new DataPrompt(allPrompts);
                myForm.ShowDialog();
                var tempStrList = myForm.GetMessageText();

                if (tempStrList != null)
                {
                    if (tempStrList.Any(string.IsNullOrEmpty))
                    {
                        return "Cancel action!";
                    }
                    //Replace each prompt in the parameter with the userinput value.
                    var szNewParameter = inParameter;
                    for (var i = 0; i < allPrompts.Count; i++)
                    {
                        szNewParameter = szNewParameter.Replace(mc[i].Value, "\"" + HttpUtility.HtmlEncode(tempStrList[i]) + "\"");
                    }
                    return szNewParameter;
                }
            }
            return inParameter;
        }
    }
}
