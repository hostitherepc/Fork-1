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
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Windows.Forms;

namespace WOSI.Utilities
{
	public class FilenameEventArgs : EventArgs
	{
		public string				Filename = "";

		public FilenameEventArgs(string filename)
		{
			Filename = filename;
		}
	}

	public delegate void FilenameEventHandler(object sender, FilenameEventArgs e);

	public class ApplicationSettings
	{
		private class RecentlyUsedFileMenuItem : MenuItem
		{
			public RecentlyUsedFileMenuItem(string text) : base(text)
			{
			}
		}

		private const string									asdlfka9098723 = "23987qlkwjhKA97K(*8kas9d8792835lasp";
		private const string									encryptPrefix = "-------092745";

		public static event FilenameEventHandler				RecentlyUsedFileMenuClicked;

		private static ApplicationSettingsDataset				appSettingsDataset = new ApplicationSettingsDataset();

		public static void LoadSettingsFromRegistry(string companyName, string applicationName, bool forCurrentUserOnly)
		{
			appSettingsDataset.Clear();

			RegistryKey regKey;

			if(forCurrentUserOnly)
			{
				regKey = Registry.CurrentUser.CreateSubKey("Software\\" + companyName + "\\" + applicationName);
			}
			else
			{
				regKey = Registry.LocalMachine.CreateSubKey("Software\\" + companyName + "\\" + applicationName);
			}

			string[] subKeys = regKey.GetSubKeyNames();

			for(int subKeyIndex = 0; subKeyIndex < subKeys.Length; subKeyIndex++)
			{
				RegistryKey subKey = regKey.OpenSubKey(subKeys[subKeyIndex]);
				string[] values = subKey.GetValueNames();

				for(int valueIndex = 0; valueIndex < values.Length; valueIndex++)
				{
					SetSetting(subKeys[subKeyIndex], values[valueIndex], Convert.ToString(subKey.GetValue(values[valueIndex])));
				}
			}

			// Load recently used files
			RegistryKey recentlyUsedFiles = regKey.CreateSubKey("9EF852E5-C9EF-4163-8D05-95EEBEFE999B-Recently Used Files");

			string[] files = (string[])recentlyUsedFiles.GetValue("Files");

			for(int index = 0; index < files.Length; index++)
			{
				appSettingsDataset.RecentlyUsedFiles.AddRecentlyUsedFilesRow(files[index]);
			}

			appSettingsDataset.AcceptChanges();
		}

		public static void SaveSettingsToRegistry(string companyName, string applicationName, bool forCurrentUserOnly)
		{
			RegistryKey regKey;

			if(forCurrentUserOnly)
			{
				regKey = Registry.CurrentUser.CreateSubKey("Software\\" + companyName + "\\" + applicationName);
			}
			else
			{
				regKey = Registry.LocalMachine.CreateSubKey("Software\\" + companyName + "\\" + applicationName);
			}

			for(int index = 0; index < appSettingsDataset.Setting.Count; index++)
			{
				ApplicationSettingsDataset.SettingRow settingRow = appSettingsDataset.Setting[index];

				RegistryKey groupKey = regKey.CreateSubKey(settingRow.SettingsGroupRow.GroupName);
				groupKey.SetValue(settingRow.Name, settingRow.Value);
			}

			// Save recently used files
			RegistryKey recentlyUsedFiles = regKey.CreateSubKey("9EF852E5-C9EF-4163-8D05-95EEBEFE999B-Recently Used Files");

			recentlyUsedFiles.SetValue("Files", GetRecentlyUsedFiles());
		}

		public static void LoadSettingsFromFile(bool forCurrentUserOnly)
		{
			LoadSettingsFromFile(Application.StartupPath, forCurrentUserOnly);
		}

		public static void LoadSettingsFromFile(string settingsDirectory, bool forCurrentUserOnly)
		{
			appSettingsDataset.Clear();
			string filename = settingsDirectory + "\\";

			if(forCurrentUserOnly)
			{
				filename += Environment.UserName + ".settings";
			}
			else
			{
				filename += "app.settings";
			}

			if(File.Exists(filename))
			{
				appSettingsDataset.ReadXml(filename, XmlReadMode.Auto);
				appSettingsDataset.AcceptChanges();
			}
		}

		public static void SaveSettingsToFile(bool forCurrentUserOnly)
		{
			SaveSettingsToFile(Application.StartupPath, forCurrentUserOnly);
		}

		public static void SaveSettingsToFile(string settingsDirectory, bool forCurrentUserOnly)
		{
			string filename = settingsDirectory + "\\";

			if(forCurrentUserOnly)
			{
				filename += Environment.UserName + ".settings";
			}
			else
			{
				filename += "app.settings";
			}

			appSettingsDataset.WriteXml(filename, XmlWriteMode.IgnoreSchema);
		}

		public static void ClearSettings()
		{
			appSettingsDataset.Clear();
		}


		public static int GetSetting(string settingGroup, string settingName, int defaultValue)
		{
			try
			{
				return Convert.ToInt32(GetSetting(settingGroup, settingName, defaultValue.ToString()));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static bool GetSetting(string settingGroup, string settingName, bool defaultValue)
		{
			try
			{
				return Convert.ToBoolean(GetSetting(settingGroup, settingName, defaultValue.ToString()));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static float GetSetting(string settingGroup, string settingName, float defaultValue)
		{
			try
			{
				return Convert.ToSingle(GetSetting(settingGroup, settingName, defaultValue.ToString()));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static double GetSetting(string settingGroup, string settingName, double defaultValue)
		{
			try
			{
				return Convert.ToDouble(GetSetting(settingGroup, settingName, defaultValue.ToString()));
			}
			catch
			{
				return defaultValue;
			}
		}

		public static string GetSetting(string settingGroup, string settingName, string defaultValue)
		{
			try
			{
				if(appSettingsDataset != null)
				{
					ApplicationSettingsDataset.SettingRow[] settingRows = (ApplicationSettingsDataset.SettingRow[])appSettingsDataset.Setting.Select("GroupName = '" + settingGroup + "' AND Name = '" + settingName + "'");

					if(settingRows.Length > 0)
					{
						string settingValue = settingRows[0].Value;

						// Decrypt the string
						if(settingValue.StartsWith(encryptPrefix))
						{
							settingValue = settingValue.Remove(0, encryptPrefix.Length);
							settingValue = CryptoUtils.Decrypt(settingValue, asdlfka9098723);
						}

						return settingValue;
					}
				}
			}
			catch(Exception)
			{
			}

			return defaultValue;
		}

		public static void SetSetting(string settingGroup, string settingName, int settingValue)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString());
		}

		public static void SetSetting(string settingGroup, string settingName, int settingValue, bool encrypt)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString(), encrypt);
		}

		public static void SetSetting(string settingGroup, string settingName, double settingValue)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString());
		}

		public static void SetSetting(string settingGroup, string settingName, double settingValue, bool encrypt)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString(), encrypt);
		}

		public static void SetSetting(string settingGroup, string settingName, bool settingValue)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString());
		}

		public static void SetSetting(string settingGroup, string settingName, bool settingValue, bool encrypt)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString(), encrypt);
		}

		public static void SetSetting(string settingGroup, string settingName, float settingValue)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString());
		}

		public static void SetSetting(string settingGroup, string settingName, float settingValue, bool encrypt)
		{
			SetSetting(settingGroup, settingName, settingValue.ToString(), encrypt);
		}

		public static void SetSetting(string settingGroup, string settingName, string settingValue)
		{
			SetSetting(settingGroup, settingName, settingValue, false);
		}

		public static void SetSetting(string settingGroup, string settingName, string settingValue, bool encrypt)
		{
			if(appSettingsDataset != null)
			{
				string sValue = settingValue;

				if(encrypt)
					sValue = encryptPrefix + CryptoUtils.Encrypt(sValue, asdlfka9098723);

				// Check to see if the group already exists
				ApplicationSettingsDataset.SettingsGroupRow settingsGroup = appSettingsDataset.SettingsGroup.FindByGroupName(settingGroup);

				if(settingsGroup == null)
				{
					settingsGroup = appSettingsDataset.SettingsGroup.AddSettingsGroupRow(settingGroup);
				}

				// Check to see if the value already exists
				ApplicationSettingsDataset.SettingRow[] settings = (ApplicationSettingsDataset.SettingRow[])appSettingsDataset.Setting.Select("GroupName = '" + settingGroup + "' AND Name = '" + settingName + "'");

				if(settings.Length > 0)
				{
					settings[0].Value = sValue;
				}
				else
				{
					appSettingsDataset.Setting.AddSettingRow(settingName, sValue, settingsGroup);
				}
			}
		}

		public static void AddRecentlyUsedFile(string filename, int maxCount)
		{
			AddRecentlyUsedFile(filename, maxCount, false);
		}

		public static void AddRecentlyUsedFile(string filename, int maxCount, bool encrypt)
		{
			if(encrypt)
				filename = encryptPrefix + CryptoUtils.Encrypt(filename, asdlfka9098723);

			// If the file already exists in the menu, delete it
			ApplicationSettingsDataset.RecentlyUsedFilesRow existingFilename = appSettingsDataset.RecentlyUsedFiles.FindByFilename(filename);

			if(existingFilename != null)
				appSettingsDataset.RecentlyUsedFiles.RemoveRecentlyUsedFilesRow(existingFilename);

			appSettingsDataset.RecentlyUsedFiles.AddRecentlyUsedFilesRow(filename);

			// Trim the recently used files list
			if(maxCount < appSettingsDataset.RecentlyUsedFiles.Count)
			{
				for(int index = 0; index < appSettingsDataset.RecentlyUsedFiles.Count - maxCount; index++)
				{
					appSettingsDataset.RecentlyUsedFiles.RemoveRecentlyUsedFilesRow(appSettingsDataset.RecentlyUsedFiles[0]);
				}
			}
		}

		public static void CreateRecentlyUsedFileSubmenu(MenuItem parentMenu, bool includeNumber)
		{
			for(int index = appSettingsDataset.RecentlyUsedFiles.Count - 1; index >= 0; index--)
			{
				string menuText = appSettingsDataset.RecentlyUsedFiles[index].Filename;

				if(includeNumber)
					menuText = "&" + (appSettingsDataset.RecentlyUsedFiles.Count - index) + " " + menuText;

				RecentlyUsedFileMenuItem menuItem = new RecentlyUsedFileMenuItem(menuText);

				menuItem.Tag = appSettingsDataset.RecentlyUsedFiles[index].Filename;
			
				menuItem.Click += new EventHandler(menuItem_Click);
			
				parentMenu.MenuItems.Add(menuItem);
			}
		}

		public void ClearRecentlyUsedFiles()
		{
			appSettingsDataset.RecentlyUsedFiles.Clear();
		}

		public static string[] GetRecentlyUsedFiles()
		{
			string[] fileNames = new string[appSettingsDataset.RecentlyUsedFiles.Count];

			for(int index = 0; index < appSettingsDataset.RecentlyUsedFiles.Count; index++)
			{
				fileNames[index] = appSettingsDataset.RecentlyUsedFiles[index].Filename;

				try
				{
					if(fileNames[index].StartsWith(encryptPrefix))
					{
						fileNames[index] = fileNames[index].Remove(0, encryptPrefix.Length);
						fileNames[index] = CryptoUtils.Decrypt(fileNames[index], asdlfka9098723);
					}
				}
				catch
				{
				}
			}

			return fileNames;
		}

		private static void menuItem_Click(object sender, EventArgs e)
		{
			if(RecentlyUsedFileMenuClicked != null)
			{
				RecentlyUsedFileMenuClicked(null, new FilenameEventArgs((string)((RecentlyUsedFileMenuItem)sender).Tag));
			}
		}
	}
}
