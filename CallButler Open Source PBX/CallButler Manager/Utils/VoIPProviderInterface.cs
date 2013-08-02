///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////



using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;

namespace CallButler.Manager.Utils
{
    class VoIPProviderInterface
    {
        static string[] lowerCaseWords = { "of", "the", "and", "or", "a", "an", "von" };

        // The following prefixes will cause their next character to be uppercased
        // Note: Keep the first character uppercase when defining these; all else must be in lowercase
        static string[] upperCasePrefixes = { "Mc", "O'" };

        // The following words will be always presented in the case they have here.
        static string[] fixedCaseWords = { "USA", "NATO", "MHz" };

        private const string didNumberLookupURL = "https://www.teliax.com/API/did_avail.php?APIKEY=testkey4823";
        private XmlDocument xmlDoc;

        public VoIPProviderInterface()
        {
            xmlDoc = new XmlDocument();
        }

        public string[] GetAvailableCitiesForState(string stateCode)
        {
            List<string> cityList = new List<string>();

            if (stateCode.Trim().Length > 0)
            {
                xmlDoc.Load(didNumberLookupURL + "&state=" + stateCode);

                XmlNode cityNodes = xmlDoc.DocumentElement.SelectSingleNode("response[@type=\"element\" and @id=\"city\"]");

                foreach (XmlNode cityNode in cityNodes.ChildNodes)
                {
                    if (!cityNode.InnerText.StartsWith("*"))
                        cityList.Add(ProperCase(cityNode.InnerText));
                }
            }

            return cityList.ToArray();
        }

        public string[] GetAvailableNumbersForCity(string stateCode, string cityName)
        {
            List<string> numberList = new List<string>();

            xmlDoc.Load(didNumberLookupURL + "&city=" + stateCode + cityName);

            XmlNode numberNodes = xmlDoc.DocumentElement.SelectSingleNode("response[@type=\"element\" and @id=\"numbers\"]");

            foreach (XmlNode numberNode in numberNodes.ChildNodes)
            {
                if (!numberNode.InnerText.StartsWith("*"))
                    numberList.Add(numberNode.InnerText);
            }

            return numberList.ToArray();
        }

        public string[] GetAvailableNumbersForAreaCode(string areaCode)
        {
            List<string> numberList = new List<string>();

            if (areaCode.Trim().Length > 0)
            {
                xmlDoc.Load(didNumberLookupURL + "&npa=" + areaCode);

                XmlNode numberNodes = xmlDoc.DocumentElement.SelectSingleNode("response[@type=\"element\" and @id=\"numbers\"]");

                foreach (XmlNode numberNode in numberNodes.ChildNodes)
                {
                    if (!numberNode.InnerText.StartsWith("*"))
                        numberList.Add(numberNode.InnerText);
                }
            }

            return numberList.ToArray();
        }

        public string[] GetAvailableTollFreeNumbers()
        {
            List<string> numberList = new List<string>();

            xmlDoc.Load(didNumberLookupURL + "&state=TF");

            XmlNode numberNodes = xmlDoc.DocumentElement.SelectSingleNode("response[@type=\"element\" and @id=\"numbers\"]");

            foreach (XmlNode numberNode in numberNodes.ChildNodes)
            {
                if (!numberNode.InnerText.StartsWith("*"))
                    numberList.Add(numberNode.InnerText);
            }

            return numberList.ToArray();
        }

        public static string ProperCase(string original)
        {

            if (original == null || original.Length == 0) return "";

            // Run the original through the massage word-by-word
            string result =
              Regex.Replace(original.ToLower(), @"\b(\w+)\b", new MatchEvaluator(HandleSingleWord));

            // Always uppercase the first character
            return Char.ToUpper(result[0]) + (result.Length > 1 ? result.Substring(1) : "");
        }

        private static string HandleSingleWord(Match m)
        {

            string word = m.Groups[1].Value;

            // Is this word defined as all-lowercase?
            foreach (string lcw in lowerCaseWords)
            {
                if (word == lcw)
                {
                    return word;
                }
            }

            // Is this word defined as a fixed-case word?
            foreach (string fcw in fixedCaseWords)
            {
                if (String.Compare(word, fcw, true) == 0)
                {
                    return fcw;
                }
            }

            // Ok, this is a normal word; uppercase the first letter
            if (word.Length == 1)
            {
                return Char.ToUpper(word[0]).ToString();
            }
            word = Char.ToUpper(word[0]) + word.Substring(1);

            // Check if this word starts with one of the uppercasing prefixes
            // Note: Only one of the uppercasing prefixes is applies
            foreach (string ucPrefix in upperCasePrefixes)
            {
                if (word.StartsWith(ucPrefix) && word.Length > ucPrefix.Length)
                {
                    return word.Substring(0, ucPrefix.Length) + Char.ToUpper(word[ucPrefix.Length]) + (word.Length > ucPrefix.Length + 1 ? word.Substring(ucPrefix.Length + 1) : "");
                }
            }
           return word;
        }


    }
}