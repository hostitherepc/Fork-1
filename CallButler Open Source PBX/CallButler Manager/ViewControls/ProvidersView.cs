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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using WOSI.CallButler.Data;
using CallButler.Manager.Forms;

namespace CallButler.Manager.ViewControls
{
    public partial class ProvidersView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private System.Timers.Timer timer;

        public ProvidersView()
        {
            InitializeComponent();
            providerGrid.ReadOnly = false;
            SetColumnPriviledges();

            this.HelpRTFText = Properties.Resources.ProviderHelp;
        }

        private void SetColumnPriviledges()
        {
            foreach (DataGridViewColumn col in providerGrid.Columns)
            {
                if (col.HeaderText.Equals("Default") || col.HeaderText.Equals("Enabled"))
                {
                    col.ReadOnly = false;
                }
                else
                {
                    col.ReadOnly = true;
                }
            }
        }

        private System.Timers.Timer Timer
        {
            get
            {
                if (timer == null)
                {
                    timer = new System.Timers.Timer();
                    timer.Interval = 5000;
                    timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                }
                return timer;
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                lock (callButlerDataset)
                {
                    callButlerDataset.Providers.Merge(ManagementInterfaceClient.ManagementInterface.GetProviders(ManagementInterfaceClient.AuthInfo));
                }
            }
            catch { }
            finally
            {
                timer.Start();
            }
        }

        private void btnAddNewProvider_Click(object sender, EventArgs e)
        {
            AddNewProvider();
        }

        internal static DialogResult Add3rdPartyProvider()
        {
            return DialogResult.OK;
        }

        internal DialogResult AddNewProvider()
        {
            return AddNewProvider(ProviderType.None, true);
        }

        internal DialogResult AddNewProvider(ProviderType type, bool showInitialDialog)
        {
            DialogResult res = DialogResult.Cancel;
            CallButler.Manager.Plugin.CallButlerManagementProviderPlugin providerPlugin = null;
            
            /*if (showInitialDialog)
            {
                InitialProviderDialog dlg = new InitialProviderDialog();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    type = dlg.ProviderType;
                }
            }*/
            
                /*switch (type)
                {
                    case ProviderType.CallButler:
                        // Get our default CallButler provider plugin
                        providerPlugin = (CallButler.Manager.Plugin.CallButlerManagementProviderPlugin)PluginManager.GetPluginFromID(new Guid(Properties.Settings.Default.PreferredVoIPProviderPluginID));
                        break;*/

                    //case ProviderType.Other:
                        providerPlugin = (CallButler.Manager.Plugin.CallButlerManagementProviderPlugin)PluginManager.GetPluginFromID(new Guid(Properties.Settings.Default.DefaultVoIPProviderPluginID));
                        //break;
                //}

                if (providerPlugin != null)
                {
                    CallButler.Manager.Plugin.ProviderData providerData = providerPlugin.AddNewProvider();

                    if (providerData != null)
                    {
                        CallButlerDataset.ProvidersRow provider = CreateProviderRow(providerData, providerPlugin.ServicePluginID.ToString());
                        callButlerDataset.Providers.AddProvidersRow(provider);
                        SaveProviderRow(provider);
                        res = DialogResult.OK;
                    }
                }
                return res;
         }

        private int DefaultCount
        {
            get
            {
                DataRow[] rows = callButlerDataset.Providers.Select("IsDefault = true");
                return rows.Length;
            }
        }

        private void providerGrid_DeleteDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
         
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ProvidersView_ConfirmDelete), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteProvider((CallButlerDataset.ProvidersRow)e.DataRow);
            }
        }

        private void DeleteProvider(CallButlerDataset.ProvidersRow provider)
        {
            Guid providerID = provider.ProviderID;
            callButlerDataset.Providers.RemoveProvidersRow(provider);
            ManagementInterfaceClient.ManagementInterface.DeleteProvider(ManagementInterfaceClient.AuthInfo, providerID);
        }

        protected override void OnLoadData()
        {
            lock (callButlerDataset)
            {
                callButlerDataset.Providers.Merge(ManagementInterfaceClient.ManagementInterface.GetProviders(ManagementInterfaceClient.AuthInfo));
                callButlerDataset.AcceptChanges();
            }

            providerGrid.CellValueChanged += new DataGridViewCellEventHandler(providerGrid_CellValueChanged);
            providerGrid.CurrentCellDirtyStateChanged += new EventHandler(providerGrid_CurrentCellDirtyStateChanged);

            Thread regTimerThread = new Thread(new ThreadStart(StartRegStatusTimer));
            regTimerThread.Start();
        }

        protected override void OnSaveData()
        {
            Timer.Stop();
            Timer.Dispose();
        }

        private void StartRegStatusTimer()
        {
            Timer.Stop();
            Timer.Start();
        }

        private void providerGrid_EditDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            EditProvider((CallButlerDataset.ProvidersRow)e.DataRow);
        }

        private void EditProvider(CallButlerDataset.ProvidersRow provider)
        {
            // Get the provider editor plugin
            CallButler.Manager.Plugin.CallButlerManagementProviderPlugin providerPlugin = (CallButler.Manager.Plugin.CallButlerManagementProviderPlugin)PluginManager.GetPluginFromID(new Guid(Properties.Settings.Default.DefaultVoIPProviderPluginID));

            /*try
            {
                // Check to see if this provider uses a different plugin
                if(provider.PluginID != null && provider.PluginID.Length > 0)
                    providerPlugin = (CallButler.Manager.Plugin.CallButlerManagementProviderPlugin)PluginManager.GetPluginFromID(new Guid(provider.PluginID));
            }
            catch
            {
            }*/

            if (providerPlugin != null)
            {
                // Copy our provider row into our provider data
                CallButler.Manager.Plugin.ProviderData providerData = new CallButler.Manager.Plugin.ProviderData();

                if (provider.AuthPassword != null && provider.AuthPassword.Length > 0)
                    providerData.AuthPassword = WOSI.Utilities.CryptoUtils.Decrypt(provider.AuthPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);
                
                providerData.AuthUsername = provider.AuthUsername;
                providerData.DisplayName = provider.DisplayName;
                providerData.Domain = provider.Domain;
                providerData.EnableRegistration = provider.EnableRegistration;
                providerData.Expires = provider.Expires;
                providerData.ExtraData = provider.ExtraData;
                providerData.IsEnabled = provider.IsEnabled;
                providerData.Name = provider.Name;
                providerData.SIPProxy = provider.SIPProxy;
                providerData.SIPRegistrar = provider.SIPRegistrar;
                providerData.Username = provider.Username;
                providerData.SupressOutboundUsername = provider.SupressOutboundUsername;
                providerData.OutboundUsernamePrefix = provider.OutboundUsernamePrefix;

                // Edit the data
                providerData = providerPlugin.EditProvider(providerData);

                if (providerData != null)
                {
                    // Copy the data back into our provider row
                    if (providerData.AuthPassword != null && providerData.AuthPassword.Length > 0)
                        provider.AuthPassword = WOSI.Utilities.CryptoUtils.Encrypt(providerData.AuthPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                    provider.AuthUsername = providerData.AuthUsername;
                    provider.DisplayName = providerData.DisplayName;
                    provider.Domain = providerData.Domain;
                    provider.EnableRegistration = providerData.EnableRegistration;
                    provider.Expires = providerData.Expires;
                    provider.ExtraData = providerData.ExtraData;
                    provider.IsEnabled = providerData.IsEnabled;
                    provider.Name = providerData.Name;
                    provider.SIPProxy = providerData.SIPProxy;
                    provider.SIPRegistrar = providerData.SIPRegistrar;
                    provider.Username = providerData.Username;
                    provider.SupressOutboundUsername = providerData.SupressOutboundUsername;
                    provider.OutboundUsernamePrefix = providerData.OutboundUsernamePrefix;

                    SaveProviderRow(provider);
                    LoadData();
                }
            }
        }

        void providerGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (providerGrid.IsCurrentCellDirty)
            {
                providerGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
       }


        private void providerGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRowView rv = (DataRowView)providerGrid.Rows[e.RowIndex].DataBoundItem;
                DataRow row = rv.Row;

                bool IsChecked = (bool) providerGrid[e.ColumnIndex, e.RowIndex].Value;

                if (IsChecked)
                {
                    if (providerGrid.Columns[e.ColumnIndex].HeaderText.Equals("Default"))
                    {
                        foreach (DataGridViewRow dr in providerGrid.Rows)
                        {
                            if (dr.Index != e.RowIndex)
                            {
                                dr.Cells["IsDefault"].Value = false;
                            }
                        }
                    }

                }

                rv.EndEdit();

                if (!IsChecked)
                {
                    if (providerGrid.CurrentRow.Index == e.RowIndex && DefaultCount == 0)
                    {
                        providerGrid[e.ColumnIndex, e.RowIndex].Value = true;
                    }
                }

                SaveProviderRow((CallButlerDataset.ProvidersRow) row);
                providerGrid.InvalidateRow(e.RowIndex);
            }
        }

        private CallButlerDataset.ProvidersRow CreateProviderRow(CallButler.Manager.Plugin.ProviderData providerData, string pluginID)
        {
            CallButlerDataset.ProvidersRow provider = callButlerDataset.Providers.NewProvidersRow();

            if (DefaultCount == 0)
            {
                provider.IsDefault = true;
            }

            provider.CustomerID = Properties.Settings.Default.CustomerID;
            provider.ProviderID = Guid.NewGuid();
            provider.PluginID = pluginID;

            if (providerData.AuthPassword != null && providerData.AuthPassword.Length > 0)
                provider.AuthPassword = WOSI.Utilities.CryptoUtils.Encrypt(providerData.AuthPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

            provider.AuthUsername = providerData.AuthUsername;
            provider.DisplayName = providerData.DisplayName;
            provider.Domain = providerData.Domain;
            provider.EnableRegistration = providerData.EnableRegistration;
            provider.Expires = providerData.Expires;
            provider.IsEnabled = providerData.IsEnabled;
            provider.ExtraData = providerData.ExtraData;
            provider.Name = providerData.Name;
            provider.SIPProxy = providerData.SIPProxy;
            provider.SIPRegistrar = providerData.SIPRegistrar;
            provider.Username = providerData.Username;
            provider.SupressOutboundUsername = providerData.SupressOutboundUsername;
            provider.OutboundUsernamePrefix = providerData.OutboundUsernamePrefix;
            
            return provider;
        }

        private void SaveProviderRow( CallButlerDataset.ProvidersRow row )
        {
             CallButlerDataset.ProvidersDataTable providerDataTable = Utils.TableUtils<CallButlerDataset.ProvidersDataTable>.CreateTableFromRow(row);
             ManagementInterfaceClient.ManagementInterface.PersistProviders(ManagementInterfaceClient.AuthInfo, providerDataTable);
        }

        private void providerGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == usernameDataGridViewTextBoxColumn.Index && e.Value != System.DBNull.Value)
            {
                e.Value = WOSI.Utilities.StringUtils.FormatPhoneNumber((string)e.Value);
            }
        }

    }
}
