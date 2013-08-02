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



namespace CallButler.Manager.ViewControls
{
    partial class TestDriveView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestDriveView));
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.trkSpeakerVolume = new global::Controls.TrackBarEx();
            this.lblStatus = new System.Windows.Forms.Label();
            this.imageButton12 = new global::Controls.ImageButton();
            this.imageButton11 = new global::Controls.ImageButton();
            this.imageButton10 = new global::Controls.ImageButton();
            this.imageButton9 = new global::Controls.ImageButton();
            this.imageButton8 = new global::Controls.ImageButton();
            this.imageButton7 = new global::Controls.ImageButton();
            this.imageButton6 = new global::Controls.ImageButton();
            this.imageButton5 = new global::Controls.ImageButton();
            this.imageButton4 = new global::Controls.ImageButton();
            this.imageButton3 = new global::Controls.ImageButton();
            this.imageButton2 = new global::Controls.ImageButton();
            this.imageButton1 = new global::Controls.ImageButton();
            this.lblNumbers = new System.Windows.Forms.Label();
            this.lblHook = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlPickup = new System.Windows.Forms.Panel();
            this.lblExtension = new global::Controls.SmoothLabel();
            this.txtCallerID = new System.Windows.Forms.TextBox();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.pnlPhone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPhone
            // 
            resources.ApplyResources(this.pnlPhone, "pnlPhone");
            this.pnlPhone.BackgroundImage = global::CallButler.Manager.Properties.Resources.Phone_back_down;
            this.pnlPhone.Controls.Add(this.trkSpeakerVolume);
            this.pnlPhone.Controls.Add(this.lblStatus);
            this.pnlPhone.Controls.Add(this.imageButton12);
            this.pnlPhone.Controls.Add(this.imageButton11);
            this.pnlPhone.Controls.Add(this.imageButton10);
            this.pnlPhone.Controls.Add(this.imageButton9);
            this.pnlPhone.Controls.Add(this.imageButton8);
            this.pnlPhone.Controls.Add(this.imageButton7);
            this.pnlPhone.Controls.Add(this.imageButton6);
            this.pnlPhone.Controls.Add(this.imageButton5);
            this.pnlPhone.Controls.Add(this.imageButton4);
            this.pnlPhone.Controls.Add(this.imageButton3);
            this.pnlPhone.Controls.Add(this.imageButton2);
            this.pnlPhone.Controls.Add(this.imageButton1);
            this.pnlPhone.Controls.Add(this.lblNumbers);
            this.pnlPhone.Controls.Add(this.lblHook);
            this.pnlPhone.Controls.Add(this.lblTime);
            this.pnlPhone.Controls.Add(this.pnlPickup);
            this.pnlPhone.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlPhone.Name = "pnlPhone";
            // 
            // trkSpeakerVolume
            // 
            this.trkSpeakerVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.trkSpeakerVolume, "trkSpeakerVolume");
            this.trkSpeakerVolume.MaxValue = 255;
            this.trkSpeakerVolume.MinValue = 0;
            this.trkSpeakerVolume.Name = "trkSpeakerVolume";
            this.trkSpeakerVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkSpeakerVolume.TrackerCursor = System.Windows.Forms.Cursors.Hand;
            this.trkSpeakerVolume.TrackerImage = ((System.Drawing.Image)(resources.GetObject("trkSpeakerVolume.TrackerImage")));
            this.trkSpeakerVolume.Value = 0;
            this.trkSpeakerVolume.Scrolling += new System.EventHandler(this.trkSpeakerVolume_ValueChanged);
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // imageButton12
            // 
            this.imageButton12.ButtonToggled = false;
            this.imageButton12.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton12, "imageButton12");
            this.imageButton12.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton12.MouseDownImage")));
            this.imageButton12.Name = "imageButton12";
            this.imageButton12.TabStop = false;
            this.imageButton12.Tag = "#";
            this.imageButton12.ToggleButton = false;
            this.imageButton12.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton11
            // 
            this.imageButton11.ButtonToggled = false;
            this.imageButton11.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton11, "imageButton11");
            this.imageButton11.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton11.MouseDownImage")));
            this.imageButton11.Name = "imageButton11";
            this.imageButton11.TabStop = false;
            this.imageButton11.Tag = "0";
            this.imageButton11.ToggleButton = false;
            this.imageButton11.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton10
            // 
            this.imageButton10.ButtonToggled = false;
            this.imageButton10.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton10, "imageButton10");
            this.imageButton10.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton10.MouseDownImage")));
            this.imageButton10.Name = "imageButton10";
            this.imageButton10.TabStop = false;
            this.imageButton10.Tag = "*";
            this.imageButton10.ToggleButton = false;
            this.imageButton10.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton9
            // 
            this.imageButton9.ButtonToggled = false;
            this.imageButton9.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton9, "imageButton9");
            this.imageButton9.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton9.MouseDownImage")));
            this.imageButton9.Name = "imageButton9";
            this.imageButton9.TabStop = false;
            this.imageButton9.Tag = "9";
            this.imageButton9.ToggleButton = false;
            this.imageButton9.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton8
            // 
            this.imageButton8.ButtonToggled = false;
            this.imageButton8.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton8, "imageButton8");
            this.imageButton8.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton8.MouseDownImage")));
            this.imageButton8.Name = "imageButton8";
            this.imageButton8.TabStop = false;
            this.imageButton8.Tag = "8";
            this.imageButton8.ToggleButton = false;
            this.imageButton8.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton7
            // 
            this.imageButton7.ButtonToggled = false;
            this.imageButton7.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton7, "imageButton7");
            this.imageButton7.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton7.MouseDownImage")));
            this.imageButton7.Name = "imageButton7";
            this.imageButton7.TabStop = false;
            this.imageButton7.Tag = "7";
            this.imageButton7.ToggleButton = false;
            this.imageButton7.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton6
            // 
            this.imageButton6.ButtonToggled = false;
            this.imageButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton6, "imageButton6");
            this.imageButton6.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton6.MouseDownImage")));
            this.imageButton6.Name = "imageButton6";
            this.imageButton6.TabStop = false;
            this.imageButton6.Tag = "6";
            this.imageButton6.ToggleButton = false;
            this.imageButton6.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton5
            // 
            this.imageButton5.ButtonToggled = false;
            this.imageButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton5, "imageButton5");
            this.imageButton5.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton5.MouseDownImage")));
            this.imageButton5.Name = "imageButton5";
            this.imageButton5.TabStop = false;
            this.imageButton5.Tag = "5";
            this.imageButton5.ToggleButton = false;
            this.imageButton5.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton4
            // 
            this.imageButton4.ButtonToggled = false;
            this.imageButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton4, "imageButton4");
            this.imageButton4.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton4.MouseDownImage")));
            this.imageButton4.Name = "imageButton4";
            this.imageButton4.TabStop = false;
            this.imageButton4.Tag = "4";
            this.imageButton4.ToggleButton = false;
            this.imageButton4.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton3
            // 
            this.imageButton3.ButtonToggled = false;
            this.imageButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton3, "imageButton3");
            this.imageButton3.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton3.MouseDownImage")));
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.TabStop = false;
            this.imageButton3.Tag = "3";
            this.imageButton3.ToggleButton = false;
            this.imageButton3.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton2
            // 
            this.imageButton2.ButtonToggled = false;
            this.imageButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton2, "imageButton2");
            this.imageButton2.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton2.MouseDownImage")));
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.TabStop = false;
            this.imageButton2.Tag = "2";
            this.imageButton2.ToggleButton = false;
            this.imageButton2.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.ButtonToggled = false;
            this.imageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.imageButton1, "imageButton1");
            this.imageButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("imageButton1.MouseDownImage")));
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.TabStop = false;
            this.imageButton1.Tag = "1";
            this.imageButton1.ToggleButton = false;
            this.imageButton1.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // lblNumbers
            // 
            resources.ApplyResources(this.lblNumbers, "lblNumbers");
            this.lblNumbers.Name = "lblNumbers";
            // 
            // lblHook
            // 
            resources.ApplyResources(this.lblHook, "lblHook");
            this.lblHook.Name = "lblHook";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // pnlPickup
            // 
            this.pnlPickup.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pnlPickup, "pnlPickup");
            this.pnlPickup.Name = "pnlPickup";
            this.pnlPickup.Click += new System.EventHandler(this.pnlPickup_Click);
            // 
            // lblExtension
            // 
            resources.ApplyResources(this.lblExtension, "lblExtension");
            this.lblExtension.AntiAliasText = false;
            this.lblExtension.BackColor = System.Drawing.Color.Transparent;
            this.lblExtension.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExtension.EnableHelp = true;
            this.lblExtension.Name = "lblExtension";
            // 
            // txtCallerID
            // 
            resources.ApplyResources(this.txtCallerID, "txtCallerID");
            this.txtCallerID.Name = "txtCallerID";
            // 
            // smoothLabel1
            // 
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // txtNumber
            // 
            resources.ApplyResources(this.txtNumber, "txtNumber");
            this.txtNumber.Name = "txtNumber";
            // 
            // tmrTime
            // 
            this.tmrTime.Enabled = true;
            this.tmrTime.Interval = 5000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // TestDriveView
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.txtCallerID);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.pnlPhone);
            this.DoubleBuffered = true;
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.gauge_48_shadow;
            this.Name = "TestDriveView";
            this.Load += new System.EventHandler(this.TestDriveView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TestDriveView_KeyDown);
            this.Controls.SetChildIndex(this.pnlPhone, 0);
            this.Controls.SetChildIndex(this.lblExtension, 0);
            this.Controls.SetChildIndex(this.txtCallerID, 0);
            this.Controls.SetChildIndex(this.smoothLabel1, 0);
            this.Controls.SetChildIndex(this.txtNumber, 0);
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.Panel pnlPickup;
        private global::Controls.SmoothLabel lblExtension;
        private System.Windows.Forms.TextBox txtCallerID;
        private global::Controls.SmoothLabel smoothLabel1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Label lblHook;
        private System.Windows.Forms.Label lblNumbers;
        private global::Controls.ImageButton imageButton1;
        private global::Controls.ImageButton imageButton10;
        private global::Controls.ImageButton imageButton9;
        private global::Controls.ImageButton imageButton8;
        private global::Controls.ImageButton imageButton7;
        private global::Controls.ImageButton imageButton6;
        private global::Controls.ImageButton imageButton5;
        private global::Controls.ImageButton imageButton4;
        private global::Controls.ImageButton imageButton3;
        private global::Controls.ImageButton imageButton2;
        private global::Controls.ImageButton imageButton12;
        private global::Controls.ImageButton imageButton11;
        private System.Windows.Forms.Label lblStatus;
        private global::Controls.TrackBarEx trkSpeakerVolume;


    }
}
