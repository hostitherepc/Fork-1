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
using System.Windows.Forms;
using System.Xml;

namespace WOSI.Utilities
{
    public class StringUtils
    {

        public static string GetProperSortText(SortOrder order)
        {
            string text = String.Empty;

            switch (order)
            {
                case (SortOrder.Ascending):
                    text += " ASC";
                    break;
                case (SortOrder.Descending):
                    text += " DESC";
                    break;
                case (SortOrder.None):
                    text = "";
                    break;
            }

            return text;
        }

        public static bool IsWellFormedXml(string inputString)
        {
            XmlTextReader xmlReader = new XmlTextReader(new System.IO.StringReader(inputString));

            try
            {
                while(xmlReader.Read())
                {
                }

                xmlReader.Close();
                return true;
            }
            catch
            {
            }

            xmlReader.Close();
            return false;
        }

        public static string XmlEncodeString(string inputString)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement xmlElement = xmlDoc.CreateElement("Str");

            xmlElement.InnerText = inputString;

            return xmlElement.InnerXml;
        }

        public static string[] SplitQuotedString(string stringToSplit, char token)
        {
            List<string> valueList = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool inQuotes = false;
            char quoteChar = '\0';

            foreach (Char character in stringToSplit)
            {
                if (character == '"' || character == '<')
                {
                    inQuotes = !inQuotes;
                    quoteChar = character;
                }
                else if (inQuotes && character == '>' && quoteChar == '<')
                {
                    inQuotes = false;
                }

                if (!inQuotes && character == token)
                {
                    valueList.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                else
                    sb.Append(character);
            }

            if (sb.Length > 0)
                valueList.Add(sb.ToString());

            return valueList.ToArray();
        }

        public static string[] SplitQuotedString(string stringToSplit, char startQuote, char endQuote, char token)
        {
            List<string> valueList = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool inQuotes = false;
            char quoteChar = '\0';

            foreach (Char character in stringToSplit)
            {
                if (character == startQuote)
                {
                    inQuotes = !inQuotes;
                    quoteChar = character;
                }
                else if (inQuotes && character == endQuote && quoteChar == startQuote)
                {
                    inQuotes = false;
                }

                if (!inQuotes && character == token)
                {
                    valueList.Add(sb.ToString());
                    sb = new StringBuilder();
                }
                else
                    sb.Append(character);
            }

            if (sb.Length > 0)
                valueList.Add(sb.ToString());

            return valueList.ToArray();
        }

        public static string GetAreaCode(string inputString)
        {
            //string outputNumber = "";
            string numberString = "";

            // Get number count
            int numberCount = 0;

            foreach (Char numberChar in inputString)
            {
                if (Char.IsNumber(numberChar))
                {
                    numberCount++;
                    numberString += numberChar.ToString();
                }
            }

            if (Regex.IsMatch(inputString, @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)"))
            {
                return "";
            }
            else if (numberCount == 7)
            {
                return "";
            }
            else if (numberCount == 10)
            {
                return numberString.Substring(0, 3);
            }
            else if (numberCount == 11)
            {
                return numberString.Substring(1, 3);
            }

            return "";
        }

        public static string FormatPhoneNumber(string inputString)
        {
            string outputNumber = "";
            string numberString = "";

            // Remove any spaces
            //inputString = inputString.Replace(" ", "");

            // Get number count
            int numberCount = 0;

            foreach (Char numberChar in inputString)
            {
                if (Char.IsNumber(numberChar))
                {
                    numberCount++;
                    numberString += numberChar.ToString();
                }
            }

            if (Regex.IsMatch(inputString, @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)"))
            {
                outputNumber = inputString;
            }
            else if (numberCount == 7)
            {
                outputNumber = numberString.Substring(0, 3) + "-" + numberString.Substring(3, 4);
            }
            else if (numberCount == 10)
            {
                outputNumber = "(" + numberString.Substring(0, 3) + ") " + numberString.Substring(3, 3) + "-" + numberString.Substring(6, 4);
            }
            else if (numberCount == 11)
            {
                outputNumber = numberString.Substring(0, 1) + " (" + numberString.Substring(1, 3) + ") " + numberString.Substring(4, 3) + "-" + numberString.Substring(7, 4);
            }
            else
                outputNumber = inputString;

            return outputNumber;
        }

        public static string CleanTelephoneNumber(string inputString)
        {
            return Regex.Replace(inputString, @"[\(\)\-\.\s]", "");
        }

        public static string ConvertStringToPhoneNumberString(string inputString)
        {
            string outputStr = "";

            foreach (char strChar in inputString.ToUpper())
            {
                switch (strChar)
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        outputStr += strChar.ToString();
                        break;
                    case 'A':
                    case 'B':
                    case 'C':
                        outputStr += '2';
                        break;
                    case 'D':
                    case 'E':
                    case 'F':
                        outputStr += '3';
                        break;
                    case 'G':
                    case 'H':
                    case 'I':
                        outputStr += '4';
                        break;
                    case 'J':
                    case 'K':
                    case 'L':
                        outputStr += '5';
                        break;
                    case 'M':
                    case 'N':
                    case 'O':
                        outputStr += '6';
                        break;
                    case 'P':
                    case 'Q':
                    case 'R':
                    case 'S':
                        outputStr += '7';
                        break;
                    case 'T':
                    case 'U':
                    case 'V':
                        outputStr += '8';
                        break;
                    case 'W':
                    case 'X':
                    case 'Y':
                    case 'Z':
                        outputStr += '9';
                        break;
                    case '0':
                        outputStr += '0';
                        break;
                    case '*':
                        outputStr += '*';
                        break;
                    case '#':
                        outputStr += '#';
                        break;
                    case '+':
                        outputStr += '+';
                        break;
                }
            }

            return outputStr;
        }
    }
}
