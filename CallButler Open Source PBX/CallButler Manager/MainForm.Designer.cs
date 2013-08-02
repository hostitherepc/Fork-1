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

namespace CallButler.Manager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblLoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPoweredBy = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectOtherComputer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnectThisComputer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpertModeDiv = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExpertMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTour = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExtensions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReceptionist = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCallFlow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCallPersonalization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestDrive = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPBX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVoIPProviders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCallHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowCommonTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGerman = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPurchase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangeEdition = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGetModules = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlRightOuter = new System.Windows.Forms.Panel();
            this.pnlContent = new global::Controls.RoundedCornerPanel();
            this.pnlExpired = new System.Windows.Forms.Panel();
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.btnRegister = new global::Controls.LinkButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlLeftOuter = new System.Windows.Forms.Panel();
            this.pnlCommonTasks = new global::Controls.RoundedCornerPanel();
            this.btnCloseCommonTasks = new System.Windows.Forms.PictureBox();
            this.grpCallHistory = new global::Controls.GroupBoxEx();
            this.btnManageGeneralSettings = new global::Controls.LinkButton();
            this.btnAddonModules = new global::Controls.LinkButton();
            this.btnAddCallPersonalization = new global::Controls.LinkButton();
            this.btnAddNewDepartment = new global::Controls.LinkButton();
            this.btnNewExtension = new global::Controls.LinkButton();
            this.btnGetPhoneNumber = new global::Controls.LinkButton();
            this.pnlNewVersion = new System.Windows.Forms.Panel();
            this.pnlDownloadNewVersion = new global::Controls.RoundedCornerPanel();
            this.btnDownloadNewVersion = new global::Controls.LinkButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.btnHideUpdateNotification = new global::Controls.LinkButton();
            this.pnlHeaderButtonContainer = new global::Controls.GradientPanel();
            this.divExtensions = new System.Windows.Forms.PictureBox();
            this.btnExtensions = new global::Controls.LinkButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnReceptionist = new global::Controls.LinkButton();
            this.divCallFlow = new System.Windows.Forms.PictureBox();
            this.btnScriptSchedule = new global::Controls.LinkButton();
            this.btnCallFlow = new global::Controls.LinkButton();
            this.divCallPersonalization = new System.Windows.Forms.PictureBox();
            this.btnCallPersonalization = new global::Controls.LinkButton();
            this.divTestDrive = new System.Windows.Forms.PictureBox();
            this.btnTestDrive = new global::Controls.LinkButton();
            this.divPBX = new System.Windows.Forms.PictureBox();
            this.btnPBX = new global::Controls.LinkButton();
            this.divProviders = new System.Windows.Forms.PictureBox();
            this.btnProviders = new global::Controls.LinkButton();
            this.divPlugins = new System.Windows.Forms.PictureBox();
            this.btnPlugins = new global::Controls.LinkButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnBackToSummary = new global::Controls.LinkButton();
            this.picHeaderIcon = new System.Windows.Forms.PictureBox();
            this.picCallButlerLogo = new System.Windows.Forms.PictureBox();
            this.lblHeaderCaption = new global::Controls.SmoothLabel();
            this.lblViewTitle = new global::Controls.SmoothLabel();
            this.openSettingsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlRightOuter.SuspendLayout();
            this.pnlExpired.SuspendLayout();
            this.roundedCornerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLeftOuter.SuspendLayout();
            this.pnlCommonTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCloseCommonTasks)).BeginInit();
            this.grpCallHistory.SuspendLayout();
            this.pnlNewVersion.SuspendLayout();
            this.pnlDownloadNewVersion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.pnlHeaderButtonContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divExtensions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divCallFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divCallPersonalization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divTestDrive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divPBX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divProviders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divPlugins)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCallButlerLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLoadStatus,
            this.lblConnectionStatus,
            this.lblPoweredBy});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // lblLoadStatus
            // 
            this.lblLoadStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblLoadStatus.Name = "lblLoadStatus";
            resources.ApplyResources(this.lblLoadStatus, "lblLoadStatus");
            this.lblLoadStatus.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            resources.ApplyResources(this.lblConnectionStatus, "lblConnectionStatus");
            // 
            // lblPoweredBy
            // 
            this.lblPoweredBy.ActiveLinkColor = System.Drawing.Color.CornflowerBlue;
            resources.ApplyResources(this.lblPoweredBy, "lblPoweredBy");
            this.lblPoweredBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblPoweredBy.IsLink = true;
            this.lblPoweredBy.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblPoweredBy.LinkColor = System.Drawing.Color.RoyalBlue;
            this.lblPoweredBy.Name = "lblPoweredBy";
            this.lblPoweredBy.Spring = true;
            this.lblPoweredBy.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.lblPoweredBy.Click += new System.EventHandler(this.lblPoweredBy_Click);
            // 
            // mnuMain
            // 
            this.mnuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.mnuMain, "mnuMain");
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuEdit,
            this.mnuView,
            this.mnuHelp});
            this.mnuMain.Name = "mnuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogin,
            this.mnuConnect,
            this.toolStripSeparator5,
            this.mnuImport,
            this.toolStripSeparator4,
            this.mnuClose});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // mnuLogin
            // 
            this.mnuLogin.Name = "mnuLogin";
            resources.ApplyResources(this.mnuLogin, "mnuLogin");
            this.mnuLogin.Click += new System.EventHandler(this.mnuLogin_Click);
            // 
            // mnuConnect
            // 
            this.mnuConnect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnectOtherComputer,
            this.mnuConnectThisComputer});
            this.mnuConnect.Image = global::CallButler.Manager.Properties.Resources.connection_16;
            this.mnuConnect.Name = "mnuConnect";
            resources.ApplyResources(this.mnuConnect, "mnuConnect");
            // 
            // mnuConnectOtherComputer
            // 
            this.mnuConnectOtherComputer.Name = "mnuConnectOtherComputer";
            resources.ApplyResources(this.mnuConnectOtherComputer, "mnuConnectOtherComputer");
            this.mnuConnectOtherComputer.Click += new System.EventHandler(this.mnuConnectOtherComputer_Click);
            // 
            // mnuConnectThisComputer
            // 
            this.mnuConnectThisComputer.Name = "mnuConnectThisComputer";
            resources.ApplyResources(this.mnuConnectThisComputer, "mnuConnectThisComputer");
            this.mnuConnectThisComputer.Click += new System.EventHandler(this.mnuConnectThisComputer_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // mnuImport
            // 
            this.mnuImport.Image = global::CallButler.Manager.Properties.Resources.import_16;
            this.mnuImport.Name = "mnuImport";
            resources.ApplyResources(this.mnuImport, "mnuImport");
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            resources.ApplyResources(this.mnuClose, "mnuClose");
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.mnuExpertModeDiv,
            this.mnuExpertMode});
            this.mnuEdit.Name = "mnuEdit";
            resources.ApplyResources(this.mnuEdit, "mnuEdit");
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = global::CallButler.Manager.Properties.Resources.nut_and_bolt_16;
            this.mnuSettings.Name = "mnuSettings";
            resources.ApplyResources(this.mnuSettings, "mnuSettings");
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // mnuExpertModeDiv
            // 
            this.mnuExpertModeDiv.Name = "mnuExpertModeDiv";
            resources.ApplyResources(this.mnuExpertModeDiv, "mnuExpertModeDiv");
            // 
            // mnuExpertMode
            // 
            this.mnuExpertMode.CheckOnClick = true;
            this.mnuExpertMode.Name = "mnuExpertMode";
            resources.ApplyResources(this.mnuExpertMode, "mnuExpertMode");
            this.mnuExpertMode.CheckedChanged += new System.EventHandler(this.mnuExpertMode_CheckedChanged);
            this.mnuExpertMode.Click += new System.EventHandler(this.mnuExpertMode_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuickStart,
            this.mnuTour,
            this.toolStripSeparator3,
            this.mnuExtensions,
            this.mnuReceptionist,
            this.mnuScriptSchedule,
            this.mnuCallFlow,
            this.mnuCallPersonalization,
            this.mnuTestDrive,
            this.mnuPBX,
            this.mnuVoIPProviders,
            this.mnuCallHistory,
            this.mnuPlugins,
            this.toolStripSeparator2,
            this.mnuShowCommonTasks,
            this.mnuLanguage});
            this.mnuView.Name = "mnuView";
            resources.ApplyResources(this.mnuView, "mnuView");
            // 
            // mnuQuickStart
            // 
            this.mnuQuickStart.Name = "mnuQuickStart";
            resources.ApplyResources(this.mnuQuickStart, "mnuQuickStart");
            this.mnuQuickStart.Click += new System.EventHandler(this.mnuQuickStart_Click);
            // 
            // mnuTour
            // 
            this.mnuTour.Name = "mnuTour";
            resources.ApplyResources(this.mnuTour, "mnuTour");
            this.mnuTour.Click += new System.EventHandler(this.mnuTour_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // mnuExtensions
            // 
            this.mnuExtensions.Name = "mnuExtensions";
            resources.ApplyResources(this.mnuExtensions, "mnuExtensions");
            this.mnuExtensions.Click += new System.EventHandler(this.btnExtensions_Click);
            // 
            // mnuReceptionist
            // 
            this.mnuReceptionist.Name = "mnuReceptionist";
            resources.ApplyResources(this.mnuReceptionist, "mnuReceptionist");
            this.mnuReceptionist.Click += new System.EventHandler(this.btnReceptionist_Click);
            // 
            // mnuScriptSchedule
            // 
            this.mnuScriptSchedule.Name = "mnuScriptSchedule";
            resources.ApplyResources(this.mnuScriptSchedule, "mnuScriptSchedule");
            this.mnuScriptSchedule.Click += new System.EventHandler(this.btnScriptSchedule_Click);
            // 
            // mnuCallFlow
            // 
            this.mnuCallFlow.Name = "mnuCallFlow";
            resources.ApplyResources(this.mnuCallFlow, "mnuCallFlow");
            this.mnuCallFlow.Click += new System.EventHandler(this.btnCallFlow_Click);
            // 
            // mnuCallPersonalization
            // 
            this.mnuCallPersonalization.Name = "mnuCallPersonalization";
            resources.ApplyResources(this.mnuCallPersonalization, "mnuCallPersonalization");
            this.mnuCallPersonalization.Click += new System.EventHandler(this.btnPersonalGreetings_Click);
            // 
            // mnuTestDrive
            // 
            this.mnuTestDrive.Name = "mnuTestDrive";
            resources.ApplyResources(this.mnuTestDrive, "mnuTestDrive");
            this.mnuTestDrive.Click += new System.EventHandler(this.btnTestDrive_Click);
            // 
            // mnuPBX
            // 
            this.mnuPBX.Name = "mnuPBX";
            resources.ApplyResources(this.mnuPBX, "mnuPBX");
            this.mnuPBX.Click += new System.EventHandler(this.btnPBX_Click);
            // 
            // mnuVoIPProviders
            // 
            this.mnuVoIPProviders.Name = "mnuVoIPProviders";
            resources.ApplyResources(this.mnuVoIPProviders, "mnuVoIPProviders");
            this.mnuVoIPProviders.Click += new System.EventHandler(this.btnProviders_Click);
            // 
            // mnuCallHistory
            // 
            this.mnuCallHistory.Name = "mnuCallHistory";
            resources.ApplyResources(this.mnuCallHistory, "mnuCallHistory");
            this.mnuCallHistory.Click += new System.EventHandler(this.mnuCallHistory_Click);
            // 
            // mnuPlugins
            // 
            this.mnuPlugins.Name = "mnuPlugins";
            resources.ApplyResources(this.mnuPlugins, "mnuPlugins");
            this.mnuPlugins.Click += new System.EventHandler(this.btnPlugins_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // mnuShowCommonTasks
            // 
            this.mnuShowCommonTasks.CheckOnClick = true;
            this.mnuShowCommonTasks.Name = "mnuShowCommonTasks";
            resources.ApplyResources(this.mnuShowCommonTasks, "mnuShowCommonTasks");
            this.mnuShowCommonTasks.Click += new System.EventHandler(this.mnuShowCommonTasks_Click);
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEnglish,
            this.mnuGerman});
            this.mnuLanguage.Name = "mnuLanguage";
            resources.ApplyResources(this.mnuLanguage, "mnuLanguage");
            // 
            // mnuEnglish
            // 
            this.mnuEnglish.Name = "mnuEnglish";
            resources.ApplyResources(this.mnuEnglish, "mnuEnglish");
            this.mnuEnglish.Tag = "en";
            this.mnuEnglish.Click += new System.EventHandler(this.mnuLanguage_Click);
            // 
            // mnuGerman
            // 
            this.mnuGerman.Name = "mnuGerman";
            resources.ApplyResources(this.mnuGerman, "mnuGerman");
            this.mnuGerman.Tag = "de";
            this.mnuGerman.Click += new System.EventHandler(this.mnuLanguage_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUserManual,
            this.toolStripSeparator6,
            this.mnuPurchase,
            this.mnuRegister,
            this.mnuChangeEdition,
            this.toolStripSeparator1,
            this.mnuSupport,
            this.toolStripMenuItem1,
            this.mnuGetModules,
            this.checkForUpdatesToolStripMenuItem,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuUserManual
            // 
            this.mnuUserManual.Image = global::CallButler.Manager.Properties.Resources.help_16;
            this.mnuUserManual.Name = "mnuUserManual";
            resources.ApplyResources(this.mnuUserManual, "mnuUserManual");
            this.mnuUserManual.Click += new System.EventHandler(this.mnuUserManual_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // mnuPurchase
            // 
            this.mnuPurchase.Image = global::CallButler.Manager.Properties.Resources.shoppingcart_full_16;
            this.mnuPurchase.Name = "mnuPurchase";
            resources.ApplyResources(this.mnuPurchase, "mnuPurchase");
            this.mnuPurchase.Click += new System.EventHandler(this.mnuPurchase_Click);
            // 
            // mnuRegister
            // 
            this.mnuRegister.Image = global::CallButler.Manager.Properties.Resources.keys_16;
            this.mnuRegister.Name = "mnuRegister";
            resources.ApplyResources(this.mnuRegister, "mnuRegister");
            this.mnuRegister.Click += new System.EventHandler(this.mnuRegister_Click);
            // 
            // mnuChangeEdition
            // 
            this.mnuChangeEdition.Name = "mnuChangeEdition";
            resources.ApplyResources(this.mnuChangeEdition, "mnuChangeEdition");
            this.mnuChangeEdition.Click += new System.EventHandler(this.mnuChangeEdition_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // mnuSupport
            // 
            this.mnuSupport.Name = "mnuSupport";
            resources.ApplyResources(this.mnuSupport, "mnuSupport");
            this.mnuSupport.Click += new System.EventHandler(this.mnuSupport_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mnuGetModules
            // 
            this.mnuGetModules.Name = "mnuGetModules";
            resources.ApplyResources(this.mnuGetModules, "mnuGetModules");
            this.mnuGetModules.Click += new System.EventHandler(this.btnAddonModules_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Image = global::CallButler.Manager.Properties.Resources.download_16;
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::CallButler.Manager.Properties.Resources.about_16;
            this.mnuAbout.Name = "mnuAbout";
            resources.ApplyResources(this.mnuAbout, "mnuAbout");
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlMainContent.Controls.Add(this.pnlRightOuter);
            this.pnlMainContent.Controls.Add(this.pnlLeftOuter);
            this.pnlMainContent.Controls.Add(this.pnlHeaderButtonContainer);
            this.pnlMainContent.Controls.Add(this.pnlExpired);
            this.pnlMainContent.Controls.Add(this.pnlNewVersion);
            resources.ApplyResources(this.pnlMainContent, "pnlMainContent");
            this.pnlMainContent.Name = "pnlMainContent";
            // 
            // pnlRightOuter
            // 
            this.pnlRightOuter.Controls.Add(this.pnlContent);
            resources.ApplyResources(this.pnlRightOuter, "pnlRightOuter");
            this.pnlRightOuter.Name = "pnlRightOuter";
            // 
            // pnlContent
            // 
            this.pnlContent.BorderSize = 1F;
            this.pnlContent.CornerRadius = 10;
            this.pnlContent.DisplayShadow = false;
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.ForeColor = System.Drawing.Color.Silver;
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.PanelColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.ShadowColor = System.Drawing.Color.Gray;
            this.pnlContent.ShadowOffset = 5;
            // 
            // pnlExpired
            // 
            this.pnlExpired.Controls.Add(this.roundedCornerPanel1);
            resources.ApplyResources(this.pnlExpired, "pnlExpired");
            this.pnlExpired.Name = "pnlExpired";
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.BackColor = System.Drawing.Color.White;
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.btnRegister);
            this.roundedCornerPanel1.Controls.Add(this.label2);
            this.roundedCornerPanel1.Controls.Add(this.pictureBox1);
            this.roundedCornerPanel1.CornerRadius = 5;
            this.roundedCornerPanel1.DisplayShadow = false;
            resources.ApplyResources(this.roundedCornerPanel1, "roundedCornerPanel1");
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.Silver;
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.LightGoldenrodYellow;
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.Gray;
            this.roundedCornerPanel1.ShadowOffset = 5;
            // 
            // btnRegister
            // 
            this.btnRegister.AntiAliasText = false;
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.BackColor = System.Drawing.Color.Transparent;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRegister.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegister.UnderlineOnHover = true;
            this.btnRegister.Click += new System.EventHandler(this.mnuRegister_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Name = "label2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::CallButler.Manager.Properties.Resources.information;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pnlLeftOuter
            // 
            this.pnlLeftOuter.Controls.Add(this.pnlCommonTasks);
            resources.ApplyResources(this.pnlLeftOuter, "pnlLeftOuter");
            this.pnlLeftOuter.Name = "pnlLeftOuter";
            // 
            // pnlCommonTasks
            // 
            this.pnlCommonTasks.BorderSize = 1F;
            this.pnlCommonTasks.Controls.Add(this.btnCloseCommonTasks);
            this.pnlCommonTasks.Controls.Add(this.grpCallHistory);
            this.pnlCommonTasks.CornerRadius = 10;
            this.pnlCommonTasks.DisplayShadow = false;
            resources.ApplyResources(this.pnlCommonTasks, "pnlCommonTasks");
            this.pnlCommonTasks.ForeColor = System.Drawing.Color.Silver;
            this.pnlCommonTasks.Name = "pnlCommonTasks";
            this.pnlCommonTasks.PanelColor = System.Drawing.Color.WhiteSmoke;
            this.pnlCommonTasks.ShadowColor = System.Drawing.Color.Gray;
            this.pnlCommonTasks.ShadowOffset = 5;
            // 
            // btnCloseCommonTasks
            // 
            resources.ApplyResources(this.btnCloseCommonTasks, "btnCloseCommonTasks");
            this.btnCloseCommonTasks.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCloseCommonTasks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseCommonTasks.Image = global::CallButler.Manager.Properties.Resources.view_previous_16;
            this.btnCloseCommonTasks.Name = "btnCloseCommonTasks";
            this.btnCloseCommonTasks.TabStop = false;
            this.btnCloseCommonTasks.Click += new System.EventHandler(this.btnCloseCommonTasks_Click);
            // 
            // grpCallHistory
            // 
            this.grpCallHistory.AntiAliasText = false;
            this.grpCallHistory.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpCallHistory.Controls.Add(this.btnManageGeneralSettings);
            this.grpCallHistory.Controls.Add(this.btnAddonModules);
            this.grpCallHistory.Controls.Add(this.btnAddCallPersonalization);
            this.grpCallHistory.Controls.Add(this.btnAddNewDepartment);
            this.grpCallHistory.Controls.Add(this.btnNewExtension);
            this.grpCallHistory.Controls.Add(this.btnGetPhoneNumber);
            this.grpCallHistory.CornerRadius = 10;
            this.grpCallHistory.DividerAbove = false;
            resources.ApplyResources(this.grpCallHistory, "grpCallHistory");
            this.grpCallHistory.DrawLeftDivider = false;
            this.grpCallHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.grpCallHistory.HeaderColor = System.Drawing.Color.Silver;
            this.grpCallHistory.Name = "grpCallHistory";
            // 
            // btnManageGeneralSettings
            // 
            this.btnManageGeneralSettings.AntiAliasText = false;
            resources.ApplyResources(this.btnManageGeneralSettings, "btnManageGeneralSettings");
            this.btnManageGeneralSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageGeneralSettings.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnManageGeneralSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageGeneralSettings.LinkImage = global::CallButler.Manager.Properties.Resources.nut_and_bolt_24;
            this.btnManageGeneralSettings.Name = "btnManageGeneralSettings";
            this.btnManageGeneralSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageGeneralSettings.UnderlineOnHover = true;
            this.btnManageGeneralSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // btnAddonModules
            // 
            this.btnAddonModules.AntiAliasText = false;
            resources.ApplyResources(this.btnAddonModules, "btnAddonModules");
            this.btnAddonModules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddonModules.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddonModules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddonModules.LinkImage = global::CallButler.Manager.Properties.Resources.gear_connection_24;
            this.btnAddonModules.Name = "btnAddonModules";
            this.btnAddonModules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddonModules.UnderlineOnHover = true;
            this.btnAddonModules.Click += new System.EventHandler(this.btnAddonModules_Click);
            // 
            // btnAddCallPersonalization
            // 
            this.btnAddCallPersonalization.AntiAliasText = false;
            resources.ApplyResources(this.btnAddCallPersonalization, "btnAddCallPersonalization");
            this.btnAddCallPersonalization.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCallPersonalization.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddCallPersonalization.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCallPersonalization.LinkImage = global::CallButler.Manager.Properties.Resources.toolbox_24;
            this.btnAddCallPersonalization.Name = "btnAddCallPersonalization";
            this.btnAddCallPersonalization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCallPersonalization.UnderlineOnHover = true;
            this.btnAddCallPersonalization.Click += new System.EventHandler(this.btnAddPersonalExtension_Click);
            // 
            // btnAddNewDepartment
            // 
            this.btnAddNewDepartment.AntiAliasText = false;
            resources.ApplyResources(this.btnAddNewDepartment, "btnAddNewDepartment");
            this.btnAddNewDepartment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewDepartment.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNewDepartment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewDepartment.LinkImage = global::CallButler.Manager.Properties.Resources.office_24;
            this.btnAddNewDepartment.Name = "btnAddNewDepartment";
            this.btnAddNewDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewDepartment.UnderlineOnHover = true;
            this.btnAddNewDepartment.Click += new System.EventHandler(this.btnAddNewDepartment_Click);
            // 
            // btnNewExtension
            // 
            this.btnNewExtension.AntiAliasText = false;
            resources.ApplyResources(this.btnNewExtension, "btnNewExtension");
            this.btnNewExtension.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewExtension.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnNewExtension.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewExtension.LinkImage = global::CallButler.Manager.Properties.Resources.user1_telephone_24;
            this.btnNewExtension.Name = "btnNewExtension";
            this.btnNewExtension.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewExtension.UnderlineOnHover = true;
            this.btnNewExtension.Click += new System.EventHandler(this.btnNewExtension_Click);
            // 
            // btnGetPhoneNumber
            // 
            this.btnGetPhoneNumber.AntiAliasText = false;
            resources.ApplyResources(this.btnGetPhoneNumber, "btnGetPhoneNumber");
            this.btnGetPhoneNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetPhoneNumber.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnGetPhoneNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetPhoneNumber.LinkImage = global::CallButler.Manager.Properties.Resources.telephone_24;
            this.btnGetPhoneNumber.Name = "btnGetPhoneNumber";
            this.btnGetPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetPhoneNumber.UnderlineOnHover = true;
            this.btnGetPhoneNumber.Click += new System.EventHandler(this.btnGetPhoneNumber_Click);
            // 
            // pnlNewVersion
            // 
            this.pnlNewVersion.Controls.Add(this.pnlDownloadNewVersion);
            resources.ApplyResources(this.pnlNewVersion, "pnlNewVersion");
            this.pnlNewVersion.Name = "pnlNewVersion";
            // 
            // pnlDownloadNewVersion
            // 
            this.pnlDownloadNewVersion.BackColor = System.Drawing.Color.White;
            this.pnlDownloadNewVersion.BorderSize = 1F;
            this.pnlDownloadNewVersion.Controls.Add(this.btnDownloadNewVersion);
            this.pnlDownloadNewVersion.Controls.Add(this.label1);
            this.pnlDownloadNewVersion.Controls.Add(this.pictureBox6);
            this.pnlDownloadNewVersion.Controls.Add(this.btnHideUpdateNotification);
            this.pnlDownloadNewVersion.CornerRadius = 5;
            this.pnlDownloadNewVersion.DisplayShadow = false;
            resources.ApplyResources(this.pnlDownloadNewVersion, "pnlDownloadNewVersion");
            this.pnlDownloadNewVersion.ForeColor = System.Drawing.Color.Silver;
            this.pnlDownloadNewVersion.Name = "pnlDownloadNewVersion";
            this.pnlDownloadNewVersion.PanelColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pnlDownloadNewVersion.ShadowColor = System.Drawing.Color.Gray;
            this.pnlDownloadNewVersion.ShadowOffset = 5;
            // 
            // btnDownloadNewVersion
            // 
            this.btnDownloadNewVersion.AntiAliasText = false;
            resources.ApplyResources(this.btnDownloadNewVersion, "btnDownloadNewVersion");
            this.btnDownloadNewVersion.BackColor = System.Drawing.Color.Transparent;
            this.btnDownloadNewVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadNewVersion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDownloadNewVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownloadNewVersion.Name = "btnDownloadNewVersion";
            this.btnDownloadNewVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownloadNewVersion.UnderlineOnHover = true;
            this.btnDownloadNewVersion.Click += new System.EventHandler(this.btnDownloadNewVersion_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label1.Name = "label1";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.Image = global::CallButler.Manager.Properties.Resources.information;
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // btnHideUpdateNotification
            // 
            this.btnHideUpdateNotification.AntiAliasText = false;
            resources.ApplyResources(this.btnHideUpdateNotification, "btnHideUpdateNotification");
            this.btnHideUpdateNotification.BackColor = System.Drawing.Color.Transparent;
            this.btnHideUpdateNotification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHideUpdateNotification.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnHideUpdateNotification.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHideUpdateNotification.Name = "btnHideUpdateNotification";
            this.btnHideUpdateNotification.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHideUpdateNotification.UnderlineOnHover = true;
            this.btnHideUpdateNotification.Click += new System.EventHandler(this.btnHideUpdateNotification_Click);
            // 
            // pnlHeaderButtonContainer
            // 
            this.pnlHeaderButtonContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.pnlHeaderButtonContainer.BorderWidth = 1F;
            this.pnlHeaderButtonContainer.Controls.Add(this.divExtensions);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnExtensions);
            this.pnlHeaderButtonContainer.Controls.Add(this.pictureBox2);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnReceptionist);
            this.pnlHeaderButtonContainer.Controls.Add(this.divCallFlow);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnScriptSchedule);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnCallFlow);
            this.pnlHeaderButtonContainer.Controls.Add(this.divCallPersonalization);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnCallPersonalization);
            this.pnlHeaderButtonContainer.Controls.Add(this.divTestDrive);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnTestDrive);
            this.pnlHeaderButtonContainer.Controls.Add(this.divPBX);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnPBX);
            this.pnlHeaderButtonContainer.Controls.Add(this.divProviders);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnProviders);
            this.pnlHeaderButtonContainer.Controls.Add(this.divPlugins);
            this.pnlHeaderButtonContainer.Controls.Add(this.btnPlugins);
            resources.ApplyResources(this.pnlHeaderButtonContainer, "pnlHeaderButtonContainer");
            this.pnlHeaderButtonContainer.DrawBorder = true;
            this.pnlHeaderButtonContainer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.pnlHeaderButtonContainer.GradientAngle = 90F;
            this.pnlHeaderButtonContainer.Name = "pnlHeaderButtonContainer";
            // 
            // divExtensions
            // 
            this.divExtensions.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divExtensions, "divExtensions");
            this.divExtensions.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divExtensions.Name = "divExtensions";
            this.divExtensions.TabStop = false;
            // 
            // btnExtensions
            // 
            this.btnExtensions.AntiAliasText = false;
            resources.ApplyResources(this.btnExtensions, "btnExtensions");
            this.btnExtensions.BackColor = System.Drawing.Color.Transparent;
            this.btnExtensions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExtensions.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnExtensions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExtensions.LinkImage = global::CallButler.Manager.Properties.Resources.user1_telephone_24;
            this.btnExtensions.Name = "btnExtensions";
            this.btnExtensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExtensions.UnderlineOnHover = true;
            this.btnExtensions.Click += new System.EventHandler(this.btnExtensions_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // btnReceptionist
            // 
            this.btnReceptionist.AntiAliasText = false;
            resources.ApplyResources(this.btnReceptionist, "btnReceptionist");
            this.btnReceptionist.BackColor = System.Drawing.Color.Transparent;
            this.btnReceptionist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReceptionist.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnReceptionist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceptionist.LinkImage = ((System.Drawing.Image)(resources.GetObject("btnReceptionist.LinkImage")));
            this.btnReceptionist.Name = "btnReceptionist";
            this.btnReceptionist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceptionist.UnderlineOnHover = true;
            this.btnReceptionist.Click += new System.EventHandler(this.btnReceptionist_Click);
            // 
            // divCallFlow
            // 
            this.divCallFlow.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divCallFlow, "divCallFlow");
            this.divCallFlow.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divCallFlow.Name = "divCallFlow";
            this.divCallFlow.TabStop = false;
            // 
            // btnScriptSchedule
            // 
            this.btnScriptSchedule.AntiAliasText = false;
            resources.ApplyResources(this.btnScriptSchedule, "btnScriptSchedule");
            this.btnScriptSchedule.BackColor = System.Drawing.Color.Transparent;
            this.btnScriptSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScriptSchedule.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnScriptSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptSchedule.LinkImage = global::CallButler.Manager.Properties.Resources.date_time_24;
            this.btnScriptSchedule.Name = "btnScriptSchedule";
            this.btnScriptSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptSchedule.UnderlineOnHover = true;
            this.btnScriptSchedule.Click += new System.EventHandler(this.btnScriptSchedule_Click);
            // 
            // btnCallFlow
            // 
            this.btnCallFlow.AntiAliasText = false;
            resources.ApplyResources(this.btnCallFlow, "btnCallFlow");
            this.btnCallFlow.BackColor = System.Drawing.Color.Transparent;
            this.btnCallFlow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCallFlow.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCallFlow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallFlow.LinkImage = global::CallButler.Manager.Properties.Resources.branch_24;
            this.btnCallFlow.Name = "btnCallFlow";
            this.btnCallFlow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallFlow.UnderlineOnHover = true;
            this.btnCallFlow.Click += new System.EventHandler(this.btnCallFlow_Click);
            // 
            // divCallPersonalization
            // 
            this.divCallPersonalization.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divCallPersonalization, "divCallPersonalization");
            this.divCallPersonalization.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divCallPersonalization.Name = "divCallPersonalization";
            this.divCallPersonalization.TabStop = false;
            // 
            // btnCallPersonalization
            // 
            this.btnCallPersonalization.AntiAliasText = false;
            resources.ApplyResources(this.btnCallPersonalization, "btnCallPersonalization");
            this.btnCallPersonalization.BackColor = System.Drawing.Color.Transparent;
            this.btnCallPersonalization.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCallPersonalization.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCallPersonalization.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallPersonalization.LinkImage = global::CallButler.Manager.Properties.Resources.toolbox_24;
            this.btnCallPersonalization.Name = "btnCallPersonalization";
            this.btnCallPersonalization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallPersonalization.UnderlineOnHover = true;
            this.btnCallPersonalization.Click += new System.EventHandler(this.btnPersonalGreetings_Click);
            // 
            // divTestDrive
            // 
            this.divTestDrive.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divTestDrive, "divTestDrive");
            this.divTestDrive.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divTestDrive.Name = "divTestDrive";
            this.divTestDrive.TabStop = false;
            // 
            // btnTestDrive
            // 
            this.btnTestDrive.AntiAliasText = false;
            resources.ApplyResources(this.btnTestDrive, "btnTestDrive");
            this.btnTestDrive.BackColor = System.Drawing.Color.Transparent;
            this.btnTestDrive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestDrive.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnTestDrive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestDrive.LinkImage = global::CallButler.Manager.Properties.Resources.gauge_24;
            this.btnTestDrive.Name = "btnTestDrive";
            this.btnTestDrive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestDrive.UnderlineOnHover = true;
            this.btnTestDrive.Click += new System.EventHandler(this.btnTestDrive_Click);
            // 
            // divPBX
            // 
            this.divPBX.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divPBX, "divPBX");
            this.divPBX.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divPBX.Name = "divPBX";
            this.divPBX.TabStop = false;
            // 
            // btnPBX
            // 
            this.btnPBX.AntiAliasText = false;
            resources.ApplyResources(this.btnPBX, "btnPBX");
            this.btnPBX.BackColor = System.Drawing.Color.Transparent;
            this.btnPBX.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPBX.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnPBX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPBX.LinkImage = global::CallButler.Manager.Properties.Resources.node_24;
            this.btnPBX.Name = "btnPBX";
            this.btnPBX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPBX.UnderlineOnHover = true;
            this.btnPBX.Click += new System.EventHandler(this.btnPBX_Click);
            // 
            // divProviders
            // 
            this.divProviders.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divProviders, "divProviders");
            this.divProviders.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divProviders.Name = "divProviders";
            this.divProviders.TabStop = false;
            // 
            // btnProviders
            // 
            this.btnProviders.AntiAliasText = false;
            resources.ApplyResources(this.btnProviders, "btnProviders");
            this.btnProviders.BackColor = System.Drawing.Color.Transparent;
            this.btnProviders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProviders.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnProviders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProviders.LinkImage = global::CallButler.Manager.Properties.Resources.provider_connection_24;
            this.btnProviders.Name = "btnProviders";
            this.btnProviders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProviders.UnderlineOnHover = true;
            this.btnProviders.Click += new System.EventHandler(this.btnProviders_Click);
            // 
            // divPlugins
            // 
            this.divPlugins.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.divPlugins, "divPlugins");
            this.divPlugins.Image = global::CallButler.Manager.Properties.Resources.divider_line;
            this.divPlugins.Name = "divPlugins";
            this.divPlugins.TabStop = false;
            // 
            // btnPlugins
            // 
            this.btnPlugins.AntiAliasText = false;
            resources.ApplyResources(this.btnPlugins, "btnPlugins");
            this.btnPlugins.BackColor = System.Drawing.Color.Transparent;
            this.btnPlugins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlugins.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnPlugins.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlugins.LinkImage = global::CallButler.Manager.Properties.Resources.gear_connection_24;
            this.btnPlugins.Name = "btnPlugins";
            this.btnPlugins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlugins.UnderlineOnHover = true;
            this.btnPlugins.Click += new System.EventHandler(this.btnPlugins_Click);
            // 
            // pnlHeader
            // 
            resources.ApplyResources(this.pnlHeader, "pnlHeader");
            this.pnlHeader.Controls.Add(this.btnBackToSummary);
            this.pnlHeader.Controls.Add(this.picHeaderIcon);
            this.pnlHeader.Controls.Add(this.picCallButlerLogo);
            this.pnlHeader.Controls.Add(this.lblHeaderCaption);
            this.pnlHeader.Controls.Add(this.lblViewTitle);
            this.pnlHeader.Name = "pnlHeader";
            // 
            // btnBackToSummary
            // 
            resources.ApplyResources(this.btnBackToSummary, "btnBackToSummary");
            this.btnBackToSummary.AntiAliasText = false;
            this.btnBackToSummary.BackColor = System.Drawing.Color.Transparent;
            this.btnBackToSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToSummary.ForeColor = System.Drawing.Color.White;
            this.btnBackToSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackToSummary.LinkImage = global::CallButler.Manager.Properties.Resources.back_up;
            this.btnBackToSummary.MouseDownImage = global::CallButler.Manager.Properties.Resources.back_down;
            this.btnBackToSummary.Name = "btnBackToSummary";
            this.btnBackToSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackToSummary.UnderlineOnHover = true;
            this.btnBackToSummary.Click += new System.EventHandler(this.btnBackToSummary_Click);
            // 
            // picHeaderIcon
            // 
            resources.ApplyResources(this.picHeaderIcon, "picHeaderIcon");
            this.picHeaderIcon.BackColor = System.Drawing.Color.Transparent;
            this.picHeaderIcon.Name = "picHeaderIcon";
            this.picHeaderIcon.TabStop = false;
            // 
            // picCallButlerLogo
            // 
            this.picCallButlerLogo.BackColor = System.Drawing.Color.Transparent;
            this.picCallButlerLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCallButlerLogo.Image = global::CallButler.Manager.Properties.Resources.logo;
            resources.ApplyResources(this.picCallButlerLogo, "picCallButlerLogo");
            this.picCallButlerLogo.Name = "picCallButlerLogo";
            this.picCallButlerLogo.TabStop = false;
            this.picCallButlerLogo.Click += new System.EventHandler(this.picCallButlerLogo_Click);
            // 
            // lblHeaderCaption
            // 
            resources.ApplyResources(this.lblHeaderCaption, "lblHeaderCaption");
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.White;
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            // 
            // lblViewTitle
            // 
            resources.ApplyResources(this.lblViewTitle, "lblViewTitle");
            this.lblViewTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblViewTitle.ForeColor = System.Drawing.Color.White;
            this.lblViewTitle.Name = "lblViewTitle";
            // 
            // openSettingsFileDialog
            // 
            resources.ApplyResources(this.openSettingsFileDialog, "openSettingsFileDialog");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlMainContent.ResumeLayout(false);
            this.pnlRightOuter.ResumeLayout(false);
            this.pnlExpired.ResumeLayout(false);
            this.roundedCornerPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLeftOuter.ResumeLayout(false);
            this.pnlCommonTasks.ResumeLayout(false);
            this.pnlCommonTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCloseCommonTasks)).EndInit();
            this.grpCallHistory.ResumeLayout(false);
            this.pnlNewVersion.ResumeLayout(false);
            this.pnlDownloadNewVersion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.pnlHeaderButtonContainer.ResumeLayout(false);
            this.pnlHeaderButtonContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divExtensions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divCallFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divCallPersonalization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divTestDrive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divPBX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divProviders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divPlugins)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeaderIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCallButlerLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlRightOuter;
        private System.Windows.Forms.Panel pnlLeftOuter;
        private global::Controls.RoundedCornerPanel pnlCommonTasks;
        private global::Controls.GradientPanel pnlHeaderButtonContainer;
        private System.Windows.Forms.PictureBox picCallButlerLogo;
        private global::Controls.SmoothLabel lblViewTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private global::Controls.SmoothLabel lblHeaderCaption;
        private System.Windows.Forms.PictureBox picHeaderIcon;
        private System.Windows.Forms.ToolStripStatusLabel lblLoadStatus;
        private global::Controls.LinkButton btnProviders;
        private System.Windows.Forms.PictureBox divProviders;
        private System.Windows.Forms.PictureBox divTestDrive;
        private global::Controls.LinkButton btnTestDrive;
        private System.Windows.Forms.PictureBox divCallFlow;
        private global::Controls.LinkButton btnCallFlow;
        private System.Windows.Forms.PictureBox divExtensions;
        private global::Controls.LinkButton btnExtensions;
        private global::Controls.LinkButton btnBackToSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuConnect;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectOtherComputer;
        private System.Windows.Forms.ToolStripMenuItem mnuConnectThisComputer;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripStatusLabel lblConnectionStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.PictureBox divCallPersonalization;
        private global::Controls.LinkButton btnCallPersonalization;
        private global::Controls.GroupBoxEx grpCallHistory;
        private global::Controls.LinkButton btnNewExtension;
        private System.Windows.Forms.PictureBox btnCloseCommonTasks;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuShowCommonTasks;
        private global::Controls.LinkButton btnAddCallPersonalization;
        private global::Controls.LinkButton btnAddNewDepartment;
        private global::Controls.LinkButton btnManageGeneralSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuRegister;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripSeparator mnuExpertModeDiv;
        private System.Windows.Forms.ToolStripMenuItem mnuExpertMode;
        private global::Controls.LinkButton btnScriptSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuExtensions;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuCallFlow;
        private System.Windows.Forms.ToolStripMenuItem mnuCallPersonalization;
        private System.Windows.Forms.ToolStripMenuItem mnuTestDrive;
        private System.Windows.Forms.ToolStripMenuItem mnuVoIPProviders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuCallHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private global::Controls.RoundedCornerPanel pnlContent;
        private global::Controls.RoundedCornerPanel pnlDownloadNewVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private global::Controls.LinkButton btnDownloadNewVersion;
        private global::Controls.LinkButton btnHideUpdateNotification;
        private System.Windows.Forms.Panel pnlNewVersion;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.ToolStripMenuItem mnuSupport;
        private System.Windows.Forms.PictureBox divPlugins;
        private global::Controls.LinkButton btnPlugins;
        private System.Windows.Forms.ToolStripMenuItem mnuPlugins;
        private System.Windows.Forms.ToolStripMenuItem mnuPurchase;
        private System.Windows.Forms.ToolStripMenuItem mnuLogin;
        private System.Windows.Forms.ToolStripStatusLabel lblPoweredBy;
        private global::Controls.LinkButton btnGetPhoneNumber;
        private System.Windows.Forms.ToolStripMenuItem mnuTour;
        private System.Windows.Forms.Panel pnlExpired;
        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private global::Controls.LinkButton btnRegister;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem mnuChangeEdition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.OpenFileDialog openSettingsFileDialog;
        private global::Controls.LinkButton btnPBX;
        private System.Windows.Forms.PictureBox divPBX;
        private global::Controls.LinkButton btnReceptionist;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem mnuUserManual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuReceptionist;
        private System.Windows.Forms.ToolStripMenuItem mnuPBX;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuEnglish;
        private System.Windows.Forms.ToolStripMenuItem mnuGerman;
        private global::Controls.LinkButton btnAddonModules;
        private System.Windows.Forms.ToolStripMenuItem mnuGetModules;




    }
}

