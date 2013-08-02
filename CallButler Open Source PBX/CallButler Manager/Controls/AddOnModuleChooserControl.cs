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

namespace CallButler.Manager.Controls
{
    public partial class AddOnModuleChooserControl : UserControl
    {
        public AddOnModuleChooserControl()
        {
            InitializeComponent();
        }

        public void Load()
        {
            lbModules.Items.Clear();

            Guid[] moduleIDs = ManagementInterfaceClient.ManagementInterface.GetInstalledAddonModules();

            foreach (Guid module in moduleIDs)
            {
                global::Controls.ListBoxExItem lbItem = new global::Controls.ListBoxExItem();
                lbItem.Image = CallButler.Manager.Properties.Resources.gear_connection_24;
                lbItem.Text = "Unknown Addon Module";

                CallButler.Manager.Plugin.CallButlerManagementPlugin managementPlugin = PluginManager.GetPluginFromID(module);

                if (managementPlugin != null && managementPlugin is CallButler.Manager.Plugin.CallButlerManagementAddonModulePlugin && !managementPlugin.ShowInPluginView)
                {
                    lbItem.Text = managementPlugin.PluginName;
                    lbItem.Caption = managementPlugin.PluginDescription;

                    lbItem.Tag = module;

                    lbModules.Items.Add(lbItem);
                }
            }
        }

        public Guid SelectedAddOnModule
        {
            get
            {
                if (lbModules.SelectedItems.Count > 0)
                {
                    return (Guid)((global::Controls.ListBoxExItem)lbModules.SelectedItem).Tag;
                }

                return Guid.Empty;
            }
            set
            {
                foreach (global::Controls.ListBoxExItem item in lbModules.Items)
                {
                    if ((Guid)item.Tag == value)
                    {
                        lbModules.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void lbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbModules.SelectedItems.Count > 0)
                btnConfigureModule.Enabled = true;
            else
                btnConfigureModule.Enabled = false;
        }

        private void btnConfigureModule_Click(object sender, EventArgs e)
        {
            if (lbModules.SelectedItems.Count > 0)
            {
                Guid moduleID = (Guid)((global::Controls.ListBoxExItem)lbModules.SelectedItem).Tag;

                CallButler.Manager.Plugin.CallButlerManagementPlugin managementPlugin = PluginManager.GetPluginFromID(moduleID);

                if (managementPlugin != null && managementPlugin is CallButler.Manager.Plugin.CallButlerManagementAddonModulePlugin)
                {
                    // Load the plugin
                    managementPlugin.Load(new CallButler.Manager.Plugin.CallButlerManagementPluginContext(ManagementInterfaceClient.ManagementInterface, ManagementInterfaceClient.AuthInfo));

                    ((CallButler.Manager.Plugin.CallButlerManagementAddonModulePlugin)managementPlugin).OnShowSettingsDialog(this);

                    managementPlugin.Unload();
                }
            }
        }
    }
}
