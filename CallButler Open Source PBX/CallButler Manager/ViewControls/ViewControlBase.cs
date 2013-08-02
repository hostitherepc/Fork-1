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

namespace CallButler.Manager.ViewControls
{
    [Localizable(true)]
    public partial class ViewControlBase : UserControl
    {
        private Image headerIcon = null;
        private string headerTitle = "";
        private string headerCaption = "";
        private HelpView helpView;
        private bool showHelpPanel = true;

        private bool enableHelp = true;

        public ViewControlBase()
        {
            InitializeComponent();

            helpView = new HelpView();
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();

            helpView.Dispose();
        }

        [DefaultValue(true)]
        public bool ShowHelpPanel
        {
            get
            {
                return showHelpPanel;
            }
            set
            {
                showHelpPanel = value;
                pnlHeader.Visible = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [Localizable(true)]
        public string HelpRTFText
        {
            get
            {
                return helpView.HelpRTFText;
            }
            set
            {
                helpView.HelpRTFText = value;
            }
        }

        [DefaultValue(true)]
        public bool EnableHelpIcon
        {
            get
            {
                return enableHelp;
            }
            set
            {
                enableHelp = value;
                btnHelp.Enabled = value;
            }
        }

        [TypeConverter(typeof(ImageConverter)), DefaultValue(typeof(Image), null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image HeaderIcon
        {
            get
            {
                return headerIcon;
            }
            set
            {
                headerIcon = value;
            }
        }

        [Localizable(true)]
        public string HeaderTitle
        {
            get
            {
                return headerTitle;
            }
            set
            {
                headerTitle = value;
            }
        }

        [Localizable(true)]
        public string HeaderCaption
        {
            get
            {
                return headerCaption;
            }
            set
            {
                headerCaption = value;
            }
        }

        public bool LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                OnLoadData();
                Cursor.Current = Cursors.Default;

                return true;
            }
            catch(Exception e)
            {
                RemotingExceptionManager.ProcessException(e);
            }

            Cursor.Current = Cursors.Default;
            return false;
        }

        public bool SaveData()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                OnSaveData();
                Cursor.Current = Cursors.Default;

                return true;
            }
            catch (Exception e)
            {
                RemotingExceptionManager.ProcessException(e);
            }

            Cursor.Current = Cursors.Default;
            return false;
        }

        protected virtual void OnLoadData()
        {
        }

        protected virtual void OnSaveData()
        {
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            helpView.Parent = this.Parent;
            helpView.Size = this.Parent.Size;
            helpView.Left = 0;
            helpView.Top = 0;

            helpView.Show();
            helpView.BringToFront();
        }

        private void ViewControlBase_SizeChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && helpView.Visible)
            {
                helpView.Size = this.Parent.Size;
                helpView.Left = 0;
                helpView.Top = 0;
            }
        }
    }
}
