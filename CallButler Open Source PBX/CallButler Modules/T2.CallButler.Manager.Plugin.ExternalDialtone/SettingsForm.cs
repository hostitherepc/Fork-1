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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CallButler.Manager.Plugin;

namespace T2.CallButler.Manager.Plugin.ExternalDialtone
{
    public partial class SettingsForm : Form
    {
        private const string productID = "CB-M-ED-1";
        CallButlerManagementAddonModulePlugin plugin;

        public SettingsForm(CallButlerManagementAddonModulePlugin plugin)
        {
            this.plugin = plugin;
            InitializeComponent();

            lblLicenseInfo.Text = (string)plugin.ExchangeServicePluginData("GetLicenseInfo", null);
        }

        public string[] Passcodes
        {
            get
            {
                List<string> passcodes = new List<string>();

                foreach (Controls.ListBoxExItem lbItem in lbPasscodes.Items)
                {
                    passcodes.Add(lbItem.Text);
                }

                return passcodes.ToArray();
            }
            set
            {
                foreach(string passcode in value)
                {
                    Controls.ListBoxExItem lbItem = new Controls.ListBoxExItem();

                    lbItem.Text = passcode;
                    lbItem.Tag = passcode;
                    lbItem.Image = Properties.Resources.key1;

                    lbPasscodes.Items.Add(lbItem);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Controls.InputBoxResult result = global::Controls.InputBox.ShowDialog(this, "Please enter a new passcode.", "", null);

            if (result.DialogResult == DialogResult.OK)
            {
                Controls.ListBoxExItem lbItem = new Controls.ListBoxExItem();

                lbItem.Text = result.Value;
                lbItem.Tag = result.Value;
                lbItem.Image = Properties.Resources.key1;

                lbPasscodes.Items.Add(lbItem);
            }
        }

        private void lbPasscodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPasscodes.SelectedItems.Count > 0)
                btnRemove.Enabled = true;
            else
                btnRemove.Enabled = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbPasscodes.SelectedItems.Count > 0)
            {
                lbPasscodes.Items.Remove(lbPasscodes.SelectedItem);
            }
        }

        private void btnLicenseKey_Click(object sender, EventArgs e)
        {

        }
    }
}