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
using System.Configuration;
using System.Xml;
using System.IO;

namespace WOSI.Utilities
{
    public class WOSISettingsProvider : SettingsProvider
    {
        public override string ApplicationName
        {
            get
            {
                return System.Windows.Forms.Application.ProductName;
            }
            set
            {
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(this.ApplicationName, config);
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

            string settingsFile = string.Format("{0}\\{1}.settings", Utilities.FileUtils.GetApplicationRelativePath(""), ApplicationName);

            XmlDocument xmlDoc = null;

            if (File.Exists(settingsFile))
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(settingsFile);
            }

            foreach (SettingsProperty setting in collection)
            {
                SettingsPropertyValue value = new SettingsPropertyValue(setting);
                value.IsDirty = false;

                if (xmlDoc != null)
                {
                    // Find our setting
                    XmlNode settingNode = xmlDoc.DocumentElement.SelectSingleNode(String.Format("Setting[@Name=\"{0}\"]", value.Name));

                    if (settingNode != null)
                        value.SerializedValue = settingNode.InnerText;
                    else
                        value.SerializedValue = setting.DefaultValue;
                }
                else
                {
                    value.SerializedValue = setting.DefaultValue;
                }

                values.Add(value);
            }
            
            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            XmlTextWriter writer = new XmlTextWriter(string.Format("{0}\\{1}.settings", Utilities.FileUtils.GetApplicationRelativePath(""), ApplicationName), Encoding.UTF8);

            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");

            foreach (SettingsPropertyValue propval in collection)
            {
                if (propval.SerializedValue != null)
                {
                    writer.WriteStartElement("Setting");

                    writer.WriteAttributeString("Name", propval.Name);

                    //writer.WriteRaw(propval.SerializedValue.ToString());
                    writer.WriteValue(propval.SerializedValue);

                    writer.WriteEndElement();

                    propval.IsDirty = false;
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();
        }
    }
}
