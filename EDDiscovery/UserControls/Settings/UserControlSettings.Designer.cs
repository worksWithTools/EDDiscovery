﻿/*
 * Copyright © 2016 - 2017 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */
namespace EDDiscovery.UserControls
{
    partial class UserControlSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlSettings));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonEditCommander = new ExtendedControls.ExtButton();
            this.btnDeleteCommander = new ExtendedControls.ExtButton();
            this.buttonAddCommander = new ExtendedControls.ExtButton();
            this.comboBoxCustomEssentialEntries = new ExtendedControls.ExtComboBox();
            this.comboBoxCustomHistoryLoadTime = new ExtendedControls.ExtComboBox();
            this.checkBoxOrderRowsInverted = new ExtendedControls.ExtCheckBox();
            this.checkBoxUTC = new ExtendedControls.ExtCheckBox();
            this.textBoxDefaultZoom = new ExtendedControls.NumberBoxDouble();
            this.radioButtonHistorySelection = new ExtendedControls.ExtRadioButton();
            this.radioButtonCentreHome = new ExtendedControls.ExtRadioButton();
            this.textBoxHomeSystem = new ExtendedControls.ExtTextBoxAutoComplete();
            this.panel_defaultmapcolor = new ExtendedControls.PanelNoTheme();
            this.comboBoxTheme = new ExtendedControls.ExtComboBox();
            this.button_edittheme = new ExtendedControls.ExtButton();
            this.buttonSaveTheme = new ExtendedControls.ExtButton();
            this.buttonExtSafeMode = new ExtendedControls.ExtButton();
            this.checkBoxPanelSortOrder = new ExtendedControls.ExtCheckBox();
            this.checkBoxKeepOnTop = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomResize = new ExtendedControls.ExtCheckBox();
            this.checkBoxMinimizeToNotifyIcon = new ExtendedControls.ExtCheckBox();
            this.comboBoxClickThruKey = new ExtendedControls.ExtComboBox();
            this.checkBoxUseNotifyIcon = new ExtendedControls.ExtCheckBox();
            this.buttonExtEDSMConfigureArea = new ExtendedControls.ExtButton();
            this.checkBoxCustomEDSMEDDBDownload = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomCopyToClipboard = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomMarkHiRes = new ExtendedControls.ExtCheckBox();
            this.checkBoxCustomRemoveOriginals = new ExtendedControls.ExtCheckBox();
            this.buttonExtScreenshot = new ExtendedControls.ExtButton();
            this.checkBoxCustomEnableScreenshots = new ExtendedControls.ExtCheckBox();
            this.extPanelScroll = new ExtendedControls.ExtPanelScroll();
            this.extScrollBar1 = new ExtendedControls.ExtScrollBar();
            this.groupBoxCommanders = new ExtendedControls.ExtGroupBox();
            this.dataViewScrollerCommanders = new ExtendedControls.ExtPanelDataGridViewScroll();
            this.vScrollBarCommanders = new ExtendedControls.ExtScrollBar();
            this.dataGridViewCommanders = new System.Windows.Forms.DataGridView();
            this.ColumnCommander = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EdsmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JournalDirCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotesCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxCustomHistoryLoad = new ExtendedControls.ExtGroupBox();
            this.labelHistoryEssItems = new System.Windows.Forms.Label();
            this.labelHistorySel = new System.Windows.Forms.Label();
            this.groupBox3dmap = new ExtendedControls.ExtGroupBox();
            this.labelMapCol = new System.Windows.Forms.Label();
            this.labelZoom = new System.Windows.Forms.Label();
            this.labelOpenOn = new System.Windows.Forms.Label();
            this.labelHome = new System.Windows.Forms.Label();
            this.groupBoxCustomLanguage = new ExtendedControls.ExtGroupBox();
            this.comboBoxCustomLanguage = new ExtendedControls.ExtComboBox();
            this.groupBoxTheme = new ExtendedControls.ExtGroupBox();
            this.groupBoxCustomSafeMode = new ExtendedControls.ExtGroupBox();
            this.labelSafeMode = new System.Windows.Forms.Label();
            this.groupBoxPopOuts = new ExtendedControls.ExtGroupBox();
            this.labelTKey = new System.Windows.Forms.Label();
            this.groupBoxCustomEDSM = new ExtendedControls.ExtGroupBox();
            this.groupBoxCustomScreenShots = new ExtendedControls.ExtGroupBox();
            this.extPanelScroll.SuspendLayout();
            this.groupBoxCommanders.SuspendLayout();
            this.dataViewScrollerCommanders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommanders)).BeginInit();
            this.groupBoxCustomHistoryLoad.SuspendLayout();
            this.groupBox3dmap.SuspendLayout();
            this.groupBoxCustomLanguage.SuspendLayout();
            this.groupBoxTheme.SuspendLayout();
            this.groupBoxCustomSafeMode.SuspendLayout();
            this.groupBoxPopOuts.SuspendLayout();
            this.groupBoxCustomEDSM.SuspendLayout();
            this.groupBoxCustomScreenShots.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // buttonEditCommander
            // 
            this.buttonEditCommander.Location = new System.Drawing.Point(552, 59);
            this.buttonEditCommander.Name = "buttonEditCommander";
            this.buttonEditCommander.Size = new System.Drawing.Size(90, 23);
            this.buttonEditCommander.TabIndex = 5;
            this.buttonEditCommander.Text = "Edit";
            this.toolTip.SetToolTip(this.buttonEditCommander, "Edit selected commander");
            this.buttonEditCommander.UseVisualStyleBackColor = true;
            this.buttonEditCommander.Click += new System.EventHandler(this.buttonEditCommander_Click);
            // 
            // btnDeleteCommander
            // 
            this.btnDeleteCommander.Location = new System.Drawing.Point(552, 99);
            this.btnDeleteCommander.Name = "btnDeleteCommander";
            this.btnDeleteCommander.Size = new System.Drawing.Size(90, 23);
            this.btnDeleteCommander.TabIndex = 3;
            this.btnDeleteCommander.Text = "Delete";
            this.toolTip.SetToolTip(this.btnDeleteCommander, "Delete selected commander");
            this.btnDeleteCommander.UseVisualStyleBackColor = true;
            this.btnDeleteCommander.Click += new System.EventHandler(this.btnDeleteCommander_Click);
            // 
            // buttonAddCommander
            // 
            this.buttonAddCommander.Location = new System.Drawing.Point(552, 19);
            this.buttonAddCommander.Name = "buttonAddCommander";
            this.buttonAddCommander.Size = new System.Drawing.Size(90, 23);
            this.buttonAddCommander.TabIndex = 0;
            this.buttonAddCommander.Text = "Add";
            this.toolTip.SetToolTip(this.buttonAddCommander, "Add a new commander");
            this.buttonAddCommander.UseVisualStyleBackColor = true;
            this.buttonAddCommander.Click += new System.EventHandler(this.buttonAddCommander_Click);
            // 
            // comboBoxCustomEssentialEntries
            // 
            this.comboBoxCustomEssentialEntries.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustomEssentialEntries.ButtonColorScaling = 0.5F;
            this.comboBoxCustomEssentialEntries.DataSource = null;
            this.comboBoxCustomEssentialEntries.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomEssentialEntries.DisplayMember = "";
            this.comboBoxCustomEssentialEntries.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustomEssentialEntries.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomEssentialEntries.Location = new System.Drawing.Point(169, 102);
            this.comboBoxCustomEssentialEntries.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustomEssentialEntries.Name = "comboBoxCustomEssentialEntries";
            this.comboBoxCustomEssentialEntries.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomEssentialEntries.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomEssentialEntries.SelectedIndex = -1;
            this.comboBoxCustomEssentialEntries.SelectedItem = null;
            this.comboBoxCustomEssentialEntries.SelectedValue = null;
            this.comboBoxCustomEssentialEntries.Size = new System.Drawing.Size(158, 21);
            this.comboBoxCustomEssentialEntries.TabIndex = 7;
            this.comboBoxCustomEssentialEntries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.comboBoxCustomEssentialEntries, "Select which items you consider essential to load older than the time above");
            this.comboBoxCustomEssentialEntries.ValueMember = "";
            // 
            // comboBoxCustomHistoryLoadTime
            // 
            this.comboBoxCustomHistoryLoadTime.BorderColor = System.Drawing.Color.White;
            this.comboBoxCustomHistoryLoadTime.ButtonColorScaling = 0.5F;
            this.comboBoxCustomHistoryLoadTime.DataSource = null;
            this.comboBoxCustomHistoryLoadTime.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomHistoryLoadTime.DisplayMember = "";
            this.comboBoxCustomHistoryLoadTime.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustomHistoryLoadTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomHistoryLoadTime.Location = new System.Drawing.Point(220, 69);
            this.comboBoxCustomHistoryLoadTime.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustomHistoryLoadTime.Name = "comboBoxCustomHistoryLoadTime";
            this.comboBoxCustomHistoryLoadTime.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomHistoryLoadTime.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomHistoryLoadTime.SelectedIndex = -1;
            this.comboBoxCustomHistoryLoadTime.SelectedItem = null;
            this.comboBoxCustomHistoryLoadTime.SelectedValue = null;
            this.comboBoxCustomHistoryLoadTime.Size = new System.Drawing.Size(107, 21);
            this.comboBoxCustomHistoryLoadTime.TabIndex = 7;
            this.comboBoxCustomHistoryLoadTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.comboBoxCustomHistoryLoadTime, "Reduce Memory use. Select either load all records, or load only essential items o" +
        "f records older than a set time before now");
            this.comboBoxCustomHistoryLoadTime.ValueMember = "";
            // 
            // checkBoxOrderRowsInverted
            // 
            this.checkBoxOrderRowsInverted.AutoSize = true;
            this.checkBoxOrderRowsInverted.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxOrderRowsInverted.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxOrderRowsInverted.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxOrderRowsInverted.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxOrderRowsInverted.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxOrderRowsInverted.Location = new System.Drawing.Point(10, 23);
            this.checkBoxOrderRowsInverted.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxOrderRowsInverted.Name = "checkBoxOrderRowsInverted";
            this.checkBoxOrderRowsInverted.Size = new System.Drawing.Size(196, 17);
            this.checkBoxOrderRowsInverted.TabIndex = 2;
            this.checkBoxOrderRowsInverted.Text = "Number Rows Lastest Entry Highest";
            this.checkBoxOrderRowsInverted.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxOrderRowsInverted, "Number oldest entry 1, latest entry highest");
            this.checkBoxOrderRowsInverted.UseVisualStyleBackColor = true;
            // 
            // checkBoxUTC
            // 
            this.checkBoxUTC.AutoSize = true;
            this.checkBoxUTC.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxUTC.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxUTC.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxUTC.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxUTC.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxUTC.Location = new System.Drawing.Point(10, 46);
            this.checkBoxUTC.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxUTC.Name = "checkBoxUTC";
            this.checkBoxUTC.Size = new System.Drawing.Size(209, 17);
            this.checkBoxUTC.TabIndex = 0;
            this.checkBoxUTC.Text = "Display Game time instead of local time";
            this.checkBoxUTC.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxUTC, "Display game time (UTC) instead of your local time");
            this.checkBoxUTC.UseVisualStyleBackColor = true;
            // 
            // textBoxDefaultZoom
            // 
            this.textBoxDefaultZoom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.textBoxDefaultZoom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.textBoxDefaultZoom.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxDefaultZoom.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxDefaultZoom.BorderColorScaling = 0.5F;
            this.textBoxDefaultZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDefaultZoom.ClearOnFirstChar = false;
            this.textBoxDefaultZoom.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxDefaultZoom.DelayBeforeNotification = 0;
            this.textBoxDefaultZoom.EndButtonEnable = true;
            this.textBoxDefaultZoom.EndButtonImage = global::EDDiscovery.Images.Controls.Dropdown;
            this.textBoxDefaultZoom.EndButtonVisible = false;
            this.textBoxDefaultZoom.Format = "0.#######";
            this.textBoxDefaultZoom.InErrorCondition = true;
            this.textBoxDefaultZoom.Location = new System.Drawing.Point(166, 88);
            this.textBoxDefaultZoom.Maximum = 300D;
            this.textBoxDefaultZoom.Minimum = 0.01D;
            this.textBoxDefaultZoom.Multiline = false;
            this.textBoxDefaultZoom.Name = "textBoxDefaultZoom";
            this.textBoxDefaultZoom.ReadOnly = false;
            this.textBoxDefaultZoom.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxDefaultZoom.SelectionLength = 0;
            this.textBoxDefaultZoom.SelectionStart = 0;
            this.textBoxDefaultZoom.Size = new System.Drawing.Size(51, 20);
            this.textBoxDefaultZoom.TabIndex = 6;
            this.textBoxDefaultZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip.SetToolTip(this.textBoxDefaultZoom, "Set the zoom level of the map. 1 is normal");
            this.textBoxDefaultZoom.Value = 0D;
            this.textBoxDefaultZoom.WordWrap = true;
            // 
            // radioButtonHistorySelection
            // 
            this.radioButtonHistorySelection.AutoSize = true;
            this.radioButtonHistorySelection.Location = new System.Drawing.Point(166, 67);
            this.radioButtonHistorySelection.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonHistorySelection.Name = "radioButtonHistorySelection";
            this.radioButtonHistorySelection.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonHistorySelection.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonHistorySelection.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonHistorySelection.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonHistorySelection.Size = new System.Drawing.Size(126, 17);
            this.radioButtonHistorySelection.TabIndex = 4;
            this.radioButtonHistorySelection.TabStop = true;
            this.radioButtonHistorySelection.Text = "History Grid Selection";
            this.toolTip.SetToolTip(this.radioButtonHistorySelection, "Select history entry as opening location");
            this.radioButtonHistorySelection.UseVisualStyleBackColor = true;
            // 
            // radioButtonCentreHome
            // 
            this.radioButtonCentreHome.AutoSize = true;
            this.radioButtonCentreHome.Location = new System.Drawing.Point(166, 46);
            this.radioButtonCentreHome.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.radioButtonCentreHome.Name = "radioButtonCentreHome";
            this.radioButtonCentreHome.RadioButtonColor = System.Drawing.Color.Gray;
            this.radioButtonCentreHome.RadioButtonInnerColor = System.Drawing.Color.White;
            this.radioButtonCentreHome.SelectedColor = System.Drawing.Color.DarkBlue;
            this.radioButtonCentreHome.SelectedColorRing = System.Drawing.Color.Black;
            this.radioButtonCentreHome.Size = new System.Drawing.Size(90, 17);
            this.radioButtonCentreHome.TabIndex = 3;
            this.radioButtonCentreHome.TabStop = true;
            this.radioButtonCentreHome.Text = "Home System";
            this.toolTip.SetToolTip(this.radioButtonCentreHome, "Select home system as opening location");
            this.radioButtonCentreHome.UseVisualStyleBackColor = true;
            // 
            // textBoxHomeSystem
            // 
            this.textBoxHomeSystem.AutoCompleteCommentMarker = null;
            this.textBoxHomeSystem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxHomeSystem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxHomeSystem.BackErrorColor = System.Drawing.Color.Red;
            this.textBoxHomeSystem.BorderColor = System.Drawing.Color.Transparent;
            this.textBoxHomeSystem.BorderColorScaling = 0.5F;
            this.textBoxHomeSystem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxHomeSystem.ClearOnFirstChar = false;
            this.textBoxHomeSystem.ControlBackground = System.Drawing.SystemColors.Control;
            this.textBoxHomeSystem.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.textBoxHomeSystem.DropDownBorderColor = System.Drawing.Color.Green;
            this.textBoxHomeSystem.DropDownMouseOverBackgroundColor = System.Drawing.Color.Red;
            this.textBoxHomeSystem.DropDownScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.textBoxHomeSystem.DropDownScrollBarColor = System.Drawing.Color.LightGray;
            this.textBoxHomeSystem.EndButtonEnable = false;
            this.textBoxHomeSystem.EndButtonImage = global::EDDiscovery.Images.Controls.Dropdown;
            this.textBoxHomeSystem.EndButtonVisible = false;
            this.textBoxHomeSystem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.textBoxHomeSystem.InErrorCondition = false;
            this.textBoxHomeSystem.Location = new System.Drawing.Point(166, 19);
            this.textBoxHomeSystem.Multiline = false;
            this.textBoxHomeSystem.Name = "textBoxHomeSystem";
            this.textBoxHomeSystem.ReadOnly = false;
            this.textBoxHomeSystem.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxHomeSystem.SelectionLength = 0;
            this.textBoxHomeSystem.SelectionStart = 0;
            this.textBoxHomeSystem.Size = new System.Drawing.Size(150, 20);
            this.textBoxHomeSystem.TabIndex = 0;
            this.textBoxHomeSystem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.toolTip.SetToolTip(this.textBoxHomeSystem, "Pick a home system");
            this.textBoxHomeSystem.WordWrap = true;
            this.textBoxHomeSystem.Leave += new System.EventHandler(this.textBoxHomeSystem_Leave);
            // 
            // panel_defaultmapcolor
            // 
            this.panel_defaultmapcolor.AccessibleDescription = "";
            this.panel_defaultmapcolor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_defaultmapcolor.Location = new System.Drawing.Point(166, 114);
            this.panel_defaultmapcolor.Name = "panel_defaultmapcolor";
            this.panel_defaultmapcolor.Size = new System.Drawing.Size(28, 20);
            this.panel_defaultmapcolor.TabIndex = 5;
            this.panel_defaultmapcolor.Tag = "";
            this.toolTip.SetToolTip(this.panel_defaultmapcolor, "New travel entries get this colour on the map");
            this.panel_defaultmapcolor.Click += new System.EventHandler(this.panel_defaultmapcolor_Click);
            // 
            // comboBoxTheme
            // 
            this.comboBoxTheme.BackColor = System.Drawing.Color.Gray;
            this.comboBoxTheme.BorderColor = System.Drawing.Color.Red;
            this.comboBoxTheme.ButtonColorScaling = 0.5F;
            this.comboBoxTheme.DataSource = null;
            this.comboBoxTheme.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxTheme.DisplayMember = "";
            this.comboBoxTheme.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxTheme.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxTheme.Location = new System.Drawing.Point(10, 19);
            this.comboBoxTheme.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxTheme.Name = "comboBoxTheme";
            this.comboBoxTheme.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxTheme.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxTheme.SelectedIndex = -1;
            this.comboBoxTheme.SelectedItem = null;
            this.comboBoxTheme.SelectedValue = null;
            this.comboBoxTheme.Size = new System.Drawing.Size(292, 21);
            this.comboBoxTheme.TabIndex = 0;
            this.comboBoxTheme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.comboBoxTheme, "Select the theme to use");
            this.comboBoxTheme.ValueMember = "";
            // 
            // button_edittheme
            // 
            this.button_edittheme.Location = new System.Drawing.Point(202, 48);
            this.button_edittheme.Name = "button_edittheme";
            this.button_edittheme.Size = new System.Drawing.Size(100, 23);
            this.button_edittheme.TabIndex = 10;
            this.button_edittheme.Text = "Edit Theme";
            this.toolTip.SetToolTip(this.button_edittheme, "Edit theme and change colours fonts");
            this.button_edittheme.UseVisualStyleBackColor = true;
            this.button_edittheme.Click += new System.EventHandler(this.button_edittheme_Click);
            // 
            // buttonSaveTheme
            // 
            this.buttonSaveTheme.Location = new System.Drawing.Point(9, 48);
            this.buttonSaveTheme.Name = "buttonSaveTheme";
            this.buttonSaveTheme.Size = new System.Drawing.Size(100, 23);
            this.buttonSaveTheme.TabIndex = 7;
            this.buttonSaveTheme.Text = "Save Theme";
            this.toolTip.SetToolTip(this.buttonSaveTheme, "Save theme to disk");
            this.buttonSaveTheme.UseVisualStyleBackColor = true;
            this.buttonSaveTheme.Click += new System.EventHandler(this.buttonSaveTheme_Click);
            // 
            // buttonExtSafeMode
            // 
            this.buttonExtSafeMode.Location = new System.Drawing.Point(13, 39);
            this.buttonExtSafeMode.Name = "buttonExtSafeMode";
            this.buttonExtSafeMode.Size = new System.Drawing.Size(201, 23);
            this.buttonExtSafeMode.TabIndex = 10;
            this.buttonExtSafeMode.Text = "Restart in Safe Mode";
            this.toolTip.SetToolTip(this.buttonExtSafeMode, "Safe Mode allows you to perform special operations, such as moving the databases," +
        " resetting the UI, resetting the action packs,  DLLs etc.");
            this.buttonExtSafeMode.UseVisualStyleBackColor = true;
            this.buttonExtSafeMode.Click += new System.EventHandler(this.buttonExtSafeMode_Click);
            // 
            // checkBoxPanelSortOrder
            // 
            this.checkBoxPanelSortOrder.AutoSize = true;
            this.checkBoxPanelSortOrder.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxPanelSortOrder.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxPanelSortOrder.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxPanelSortOrder.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxPanelSortOrder.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxPanelSortOrder.Location = new System.Drawing.Point(10, 159);
            this.checkBoxPanelSortOrder.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxPanelSortOrder.Name = "checkBoxPanelSortOrder";
            this.checkBoxPanelSortOrder.Size = new System.Drawing.Size(188, 17);
            this.checkBoxPanelSortOrder.TabIndex = 5;
            this.checkBoxPanelSortOrder.Text = "Panel List Sorted Alphanumerically";
            this.checkBoxPanelSortOrder.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxPanelSortOrder, "Panel lists sorted alphanumerically instead of ordered in groups. Note Requires R" +
        "estart");
            this.checkBoxPanelSortOrder.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeepOnTop
            // 
            this.checkBoxKeepOnTop.AutoSize = true;
            this.checkBoxKeepOnTop.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxKeepOnTop.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxKeepOnTop.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxKeepOnTop.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxKeepOnTop.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxKeepOnTop.Location = new System.Drawing.Point(10, 136);
            this.checkBoxKeepOnTop.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxKeepOnTop.Name = "checkBoxKeepOnTop";
            this.checkBoxKeepOnTop.Size = new System.Drawing.Size(88, 17);
            this.checkBoxKeepOnTop.TabIndex = 5;
            this.checkBoxKeepOnTop.Text = "Keep on Top";
            this.checkBoxKeepOnTop.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxKeepOnTop, "This window, and its children, top");
            this.checkBoxKeepOnTop.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustomResize
            // 
            this.checkBoxCustomResize.AutoSize = true;
            this.checkBoxCustomResize.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomResize.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomResize.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomResize.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomResize.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomResize.Location = new System.Drawing.Point(10, 113);
            this.checkBoxCustomResize.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomResize.Name = "checkBoxCustomResize";
            this.checkBoxCustomResize.Size = new System.Drawing.Size(186, 17);
            this.checkBoxCustomResize.TabIndex = 6;
            this.checkBoxCustomResize.Text = "Redraw the screen during resizing";
            this.checkBoxCustomResize.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomResize, "Check to allow EDD to redraw the screen during main window resize. Only disable i" +
        "f its too slow");
            this.checkBoxCustomResize.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinimizeToNotifyIcon
            // 
            this.checkBoxMinimizeToNotifyIcon.AutoSize = true;
            this.checkBoxMinimizeToNotifyIcon.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxMinimizeToNotifyIcon.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxMinimizeToNotifyIcon.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxMinimizeToNotifyIcon.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxMinimizeToNotifyIcon.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMinimizeToNotifyIcon.Location = new System.Drawing.Point(10, 90);
            this.checkBoxMinimizeToNotifyIcon.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxMinimizeToNotifyIcon.Name = "checkBoxMinimizeToNotifyIcon";
            this.checkBoxMinimizeToNotifyIcon.Size = new System.Drawing.Size(179, 17);
            this.checkBoxMinimizeToNotifyIcon.TabIndex = 6;
            this.checkBoxMinimizeToNotifyIcon.Text = "Minimize to notification area icon";
            this.checkBoxMinimizeToNotifyIcon.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxMinimizeToNotifyIcon, "Minimize the main window to the system notification area (system tray) icon.");
            this.checkBoxMinimizeToNotifyIcon.UseVisualStyleBackColor = true;
            // 
            // comboBoxClickThruKey
            // 
            this.comboBoxClickThruKey.BorderColor = System.Drawing.Color.White;
            this.comboBoxClickThruKey.ButtonColorScaling = 0.5F;
            this.comboBoxClickThruKey.DataSource = null;
            this.comboBoxClickThruKey.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxClickThruKey.DisplayMember = "";
            this.comboBoxClickThruKey.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxClickThruKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxClickThruKey.Location = new System.Drawing.Point(225, 19);
            this.comboBoxClickThruKey.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxClickThruKey.Name = "comboBoxClickThruKey";
            this.comboBoxClickThruKey.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxClickThruKey.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxClickThruKey.SelectedIndex = -1;
            this.comboBoxClickThruKey.SelectedItem = null;
            this.comboBoxClickThruKey.SelectedValue = null;
            this.comboBoxClickThruKey.Size = new System.Drawing.Size(91, 21);
            this.comboBoxClickThruKey.TabIndex = 6;
            this.comboBoxClickThruKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.comboBoxClickThruKey, global::EDDiscovery.Properties.Resources.Tooltip_TransparencyHotkey);
            this.comboBoxClickThruKey.ValueMember = "";
            // 
            // checkBoxUseNotifyIcon
            // 
            this.checkBoxUseNotifyIcon.AutoSize = true;
            this.checkBoxUseNotifyIcon.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxUseNotifyIcon.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxUseNotifyIcon.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxUseNotifyIcon.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxUseNotifyIcon.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxUseNotifyIcon.Location = new System.Drawing.Point(10, 67);
            this.checkBoxUseNotifyIcon.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxUseNotifyIcon.Name = "checkBoxUseNotifyIcon";
            this.checkBoxUseNotifyIcon.Size = new System.Drawing.Size(154, 17);
            this.checkBoxUseNotifyIcon.TabIndex = 5;
            this.checkBoxUseNotifyIcon.Text = "Show notification area icon";
            this.checkBoxUseNotifyIcon.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxUseNotifyIcon, "Show a system notification area (system tray) icon for EDDiscovery.");
            this.checkBoxUseNotifyIcon.UseVisualStyleBackColor = true;
            // 
            // buttonExtEDSMConfigureArea
            // 
            this.buttonExtEDSMConfigureArea.Location = new System.Drawing.Point(216, 19);
            this.buttonExtEDSMConfigureArea.Name = "buttonExtEDSMConfigureArea";
            this.buttonExtEDSMConfigureArea.Size = new System.Drawing.Size(100, 23);
            this.buttonExtEDSMConfigureArea.TabIndex = 10;
            this.buttonExtEDSMConfigureArea.Text = "Galaxy Select";
            this.toolTip.SetToolTip(this.buttonExtEDSMConfigureArea, "Configure what parts of the galaxy is stored in the databases");
            this.buttonExtEDSMConfigureArea.UseVisualStyleBackColor = true;
            this.buttonExtEDSMConfigureArea.Click += new System.EventHandler(this.buttonExtEDSMConfigureArea_Click);
            // 
            // checkBoxCustomEDSMEDDBDownload
            // 
            this.checkBoxCustomEDSMEDDBDownload.AutoSize = true;
            this.checkBoxCustomEDSMEDDBDownload.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomEDSMEDDBDownload.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomEDSMEDDBDownload.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomEDSMEDDBDownload.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomEDSMEDDBDownload.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomEDSMEDDBDownload.Location = new System.Drawing.Point(9, 19);
            this.checkBoxCustomEDSMEDDBDownload.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomEDSMEDDBDownload.Name = "checkBoxCustomEDSMEDDBDownload";
            this.checkBoxCustomEDSMEDDBDownload.Size = new System.Drawing.Size(158, 17);
            this.checkBoxCustomEDSMEDDBDownload.TabIndex = 5;
            this.checkBoxCustomEDSMEDDBDownload.Text = "Enable Star Data Download";
            this.checkBoxCustomEDSMEDDBDownload.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomEDSMEDDBDownload, "Click to enable downloading of stars from EDSM and system information from EDDB. " +
        " Will apply at next start.");
            this.checkBoxCustomEDSMEDDBDownload.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustomCopyToClipboard
            // 
            this.checkBoxCustomCopyToClipboard.AutoSize = true;
            this.checkBoxCustomCopyToClipboard.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomCopyToClipboard.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomCopyToClipboard.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomCopyToClipboard.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomCopyToClipboard.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomCopyToClipboard.Location = new System.Drawing.Point(194, 65);
            this.checkBoxCustomCopyToClipboard.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomCopyToClipboard.Name = "checkBoxCustomCopyToClipboard";
            this.checkBoxCustomCopyToClipboard.Size = new System.Drawing.Size(108, 17);
            this.checkBoxCustomCopyToClipboard.TabIndex = 5;
            this.checkBoxCustomCopyToClipboard.Text = "Copy to clipboard";
            this.checkBoxCustomCopyToClipboard.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomCopyToClipboard, "Auto copy the image to the clipboard");
            this.checkBoxCustomCopyToClipboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustomMarkHiRes
            // 
            this.checkBoxCustomMarkHiRes.AutoSize = true;
            this.checkBoxCustomMarkHiRes.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomMarkHiRes.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomMarkHiRes.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomMarkHiRes.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomMarkHiRes.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomMarkHiRes.Location = new System.Drawing.Point(9, 65);
            this.checkBoxCustomMarkHiRes.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomMarkHiRes.Name = "checkBoxCustomMarkHiRes";
            this.checkBoxCustomMarkHiRes.Size = new System.Drawing.Size(103, 17);
            this.checkBoxCustomMarkHiRes.TabIndex = 5;
            this.checkBoxCustomMarkHiRes.Text = "Mark HiRes files";
            this.checkBoxCustomMarkHiRes.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomMarkHiRes, "For Hi-Res files, mark them in the file name");
            this.checkBoxCustomMarkHiRes.UseVisualStyleBackColor = true;
            // 
            // checkBoxCustomRemoveOriginals
            // 
            this.checkBoxCustomRemoveOriginals.AutoSize = true;
            this.checkBoxCustomRemoveOriginals.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomRemoveOriginals.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomRemoveOriginals.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomRemoveOriginals.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomRemoveOriginals.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomRemoveOriginals.Location = new System.Drawing.Point(9, 42);
            this.checkBoxCustomRemoveOriginals.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomRemoveOriginals.Name = "checkBoxCustomRemoveOriginals";
            this.checkBoxCustomRemoveOriginals.Size = new System.Drawing.Size(109, 17);
            this.checkBoxCustomRemoveOriginals.TabIndex = 5;
            this.checkBoxCustomRemoveOriginals.Text = "Remove Originals";
            this.checkBoxCustomRemoveOriginals.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomRemoveOriginals, "After conversion, remove originals");
            this.checkBoxCustomRemoveOriginals.UseVisualStyleBackColor = true;
            // 
            // buttonExtScreenshot
            // 
            this.buttonExtScreenshot.Location = new System.Drawing.Point(202, 13);
            this.buttonExtScreenshot.Name = "buttonExtScreenshot";
            this.buttonExtScreenshot.Size = new System.Drawing.Size(100, 23);
            this.buttonExtScreenshot.TabIndex = 10;
            this.buttonExtScreenshot.Text = "Configure";
            this.toolTip.SetToolTip(this.buttonExtScreenshot, "Configure further screenshot options");
            this.buttonExtScreenshot.UseVisualStyleBackColor = true;
            this.buttonExtScreenshot.Click += new System.EventHandler(this.buttonExtScreenshot_Click);
            // 
            // checkBoxCustomEnableScreenshots
            // 
            this.checkBoxCustomEnableScreenshots.AutoSize = true;
            this.checkBoxCustomEnableScreenshots.CheckBoxColor = System.Drawing.Color.Gray;
            this.checkBoxCustomEnableScreenshots.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomEnableScreenshots.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomEnableScreenshots.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomEnableScreenshots.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomEnableScreenshots.Location = new System.Drawing.Point(9, 19);
            this.checkBoxCustomEnableScreenshots.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomEnableScreenshots.Name = "checkBoxCustomEnableScreenshots";
            this.checkBoxCustomEnableScreenshots.Size = new System.Drawing.Size(169, 17);
            this.checkBoxCustomEnableScreenshots.TabIndex = 5;
            this.checkBoxCustomEnableScreenshots.Text = "Enable screenshot conversion";
            this.checkBoxCustomEnableScreenshots.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomEnableScreenshots, "Screen shot conversion on/off");
            this.checkBoxCustomEnableScreenshots.UseVisualStyleBackColor = true;
            // 
            // extPanelScroll
            // 
            this.extPanelScroll.Controls.Add(this.extScrollBar1);
            this.extPanelScroll.Controls.Add(this.groupBoxCommanders);
            this.extPanelScroll.Controls.Add(this.groupBoxCustomHistoryLoad);
            this.extPanelScroll.Controls.Add(this.groupBox3dmap);
            this.extPanelScroll.Controls.Add(this.groupBoxCustomLanguage);
            this.extPanelScroll.Controls.Add(this.groupBoxTheme);
            this.extPanelScroll.Controls.Add(this.groupBoxCustomSafeMode);
            this.extPanelScroll.Controls.Add(this.groupBoxPopOuts);
            this.extPanelScroll.Controls.Add(this.groupBoxCustomEDSM);
            this.extPanelScroll.Controls.Add(this.groupBoxCustomScreenShots);
            this.extPanelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extPanelScroll.InternalScrollbar = true;
            this.extPanelScroll.Location = new System.Drawing.Point(0, 0);
            this.extPanelScroll.Name = "extPanelScroll";
            this.extPanelScroll.Size = new System.Drawing.Size(732, 659);
            this.extPanelScroll.TabIndex = 22;
            this.extPanelScroll.VerticalScrollBarDockRight = true;
            // 
            // extScrollBar1
            // 
            this.extScrollBar1.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.extScrollBar1.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.extScrollBar1.ArrowColorScaling = 0.5F;
            this.extScrollBar1.ArrowDownDrawAngle = 270F;
            this.extScrollBar1.ArrowUpDrawAngle = 90F;
            this.extScrollBar1.BorderColor = System.Drawing.Color.White;
            this.extScrollBar1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.extScrollBar1.HideScrollBar = true;
            this.extScrollBar1.LargeChange = 10;
            this.extScrollBar1.Location = new System.Drawing.Point(719, 0);
            this.extScrollBar1.Maximum = -6;
            this.extScrollBar1.Minimum = 0;
            this.extScrollBar1.MouseOverButtonColor = System.Drawing.Color.Green;
            this.extScrollBar1.MousePressedButtonColor = System.Drawing.Color.Red;
            this.extScrollBar1.Name = "extScrollBar1";
            this.extScrollBar1.Size = new System.Drawing.Size(13, 659);
            this.extScrollBar1.SliderColor = System.Drawing.Color.DarkGray;
            this.extScrollBar1.SmallChange = 1;
            this.extScrollBar1.TabIndex = 22;
            this.extScrollBar1.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.extScrollBar1.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.extScrollBar1.ThumbColorScaling = 0.5F;
            this.extScrollBar1.ThumbDrawAngle = 0F;
            this.extScrollBar1.Value = -6;
            this.extScrollBar1.ValueLimited = -6;
            // 
            // groupBoxCommanders
            // 
            this.groupBoxCommanders.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCommanders.BackColorScaling = 0.5F;
            this.groupBoxCommanders.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCommanders.BorderColorScaling = 0.5F;
            this.groupBoxCommanders.Controls.Add(this.buttonEditCommander);
            this.groupBoxCommanders.Controls.Add(this.dataViewScrollerCommanders);
            this.groupBoxCommanders.Controls.Add(this.btnDeleteCommander);
            this.groupBoxCommanders.Controls.Add(this.buttonAddCommander);
            this.groupBoxCommanders.FillClientAreaWithAlternateColor = false;
            this.groupBoxCommanders.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCommanders.Name = "groupBoxCommanders";
            this.groupBoxCommanders.Size = new System.Drawing.Size(672, 176);
            this.groupBoxCommanders.TabIndex = 15;
            this.groupBoxCommanders.TabStop = false;
            this.groupBoxCommanders.Text = "Commanders";
            this.groupBoxCommanders.TextPadding = 0;
            this.groupBoxCommanders.TextStartPosition = -1;
            // 
            // dataViewScrollerCommanders
            // 
            this.dataViewScrollerCommanders.Controls.Add(this.vScrollBarCommanders);
            this.dataViewScrollerCommanders.Controls.Add(this.dataGridViewCommanders);
            this.dataViewScrollerCommanders.InternalMargin = new System.Windows.Forms.Padding(0);
            this.dataViewScrollerCommanders.Location = new System.Drawing.Point(10, 19);
            this.dataViewScrollerCommanders.Name = "dataViewScrollerCommanders";
            this.dataViewScrollerCommanders.Size = new System.Drawing.Size(533, 150);
            this.dataViewScrollerCommanders.TabIndex = 4;
            this.dataViewScrollerCommanders.VerticalScrollBarDockRight = true;
            // 
            // vScrollBarCommanders
            // 
            this.vScrollBarCommanders.ArrowBorderColor = System.Drawing.Color.LightBlue;
            this.vScrollBarCommanders.ArrowButtonColor = System.Drawing.Color.LightGray;
            this.vScrollBarCommanders.ArrowColorScaling = 0.5F;
            this.vScrollBarCommanders.ArrowDownDrawAngle = 270F;
            this.vScrollBarCommanders.ArrowUpDrawAngle = 90F;
            this.vScrollBarCommanders.BorderColor = System.Drawing.Color.White;
            this.vScrollBarCommanders.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.vScrollBarCommanders.HideScrollBar = false;
            this.vScrollBarCommanders.LargeChange = 0;
            this.vScrollBarCommanders.Location = new System.Drawing.Point(520, 0);
            this.vScrollBarCommanders.Maximum = -1;
            this.vScrollBarCommanders.Minimum = 0;
            this.vScrollBarCommanders.MouseOverButtonColor = System.Drawing.Color.Green;
            this.vScrollBarCommanders.MousePressedButtonColor = System.Drawing.Color.Red;
            this.vScrollBarCommanders.Name = "vScrollBarCommanders";
            this.vScrollBarCommanders.Size = new System.Drawing.Size(13, 150);
            this.vScrollBarCommanders.SliderColor = System.Drawing.Color.DarkGray;
            this.vScrollBarCommanders.SmallChange = 1;
            this.vScrollBarCommanders.TabIndex = 3;
            this.vScrollBarCommanders.ThumbBorderColor = System.Drawing.Color.Yellow;
            this.vScrollBarCommanders.ThumbButtonColor = System.Drawing.Color.DarkBlue;
            this.vScrollBarCommanders.ThumbColorScaling = 0.5F;
            this.vScrollBarCommanders.ThumbDrawAngle = 0F;
            this.vScrollBarCommanders.Value = -1;
            this.vScrollBarCommanders.ValueLimited = -1;
            // 
            // dataGridViewCommanders
            // 
            this.dataGridViewCommanders.AllowUserToAddRows = false;
            this.dataGridViewCommanders.AllowUserToDeleteRows = false;
            this.dataGridViewCommanders.AllowUserToOrderColumns = true;
            this.dataGridViewCommanders.AllowUserToResizeRows = false;
            this.dataGridViewCommanders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCommanders.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewCommanders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCommanders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCommander,
            this.EdsmName,
            this.JournalDirCol,
            this.NotesCol});
            this.dataGridViewCommanders.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCommanders.MultiSelect = false;
            this.dataGridViewCommanders.Name = "dataGridViewCommanders";
            this.dataGridViewCommanders.ReadOnly = true;
            this.dataGridViewCommanders.RowHeadersVisible = false;
            this.dataGridViewCommanders.RowHeadersWidth = 20;
            this.dataGridViewCommanders.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewCommanders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCommanders.Size = new System.Drawing.Size(520, 150);
            this.dataGridViewCommanders.TabIndex = 2;
            // 
            // ColumnCommander
            // 
            this.ColumnCommander.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnCommander.DataPropertyName = "Name";
            this.ColumnCommander.FillWeight = 120F;
            this.ColumnCommander.HeaderText = "Commander";
            this.ColumnCommander.MinimumWidth = 50;
            this.ColumnCommander.Name = "ColumnCommander";
            this.ColumnCommander.ReadOnly = true;
            // 
            // EdsmName
            // 
            this.EdsmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EdsmName.DataPropertyName = "EdsmName";
            this.EdsmName.HeaderText = "EDSM Name";
            this.EdsmName.MinimumWidth = 50;
            this.EdsmName.Name = "EdsmName";
            this.EdsmName.ReadOnly = true;
            // 
            // JournalDirCol
            // 
            this.JournalDirCol.DataPropertyName = "JournalDir";
            this.JournalDirCol.FillWeight = 120F;
            this.JournalDirCol.HeaderText = "Journal Dir";
            this.JournalDirCol.MinimumWidth = 50;
            this.JournalDirCol.Name = "JournalDirCol";
            this.JournalDirCol.ReadOnly = true;
            // 
            // NotesCol
            // 
            this.NotesCol.DataPropertyName = "Info";
            this.NotesCol.FillWeight = 180F;
            this.NotesCol.HeaderText = "Notes";
            this.NotesCol.MinimumWidth = 50;
            this.NotesCol.Name = "NotesCol";
            this.NotesCol.ReadOnly = true;
            // 
            // groupBoxCustomHistoryLoad
            // 
            this.groupBoxCustomHistoryLoad.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCustomHistoryLoad.BackColorScaling = 0.5F;
            this.groupBoxCustomHistoryLoad.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCustomHistoryLoad.BorderColorScaling = 0.5F;
            this.groupBoxCustomHistoryLoad.Controls.Add(this.comboBoxCustomEssentialEntries);
            this.groupBoxCustomHistoryLoad.Controls.Add(this.comboBoxCustomHistoryLoadTime);
            this.groupBoxCustomHistoryLoad.Controls.Add(this.checkBoxOrderRowsInverted);
            this.groupBoxCustomHistoryLoad.Controls.Add(this.checkBoxUTC);
            this.groupBoxCustomHistoryLoad.Controls.Add(this.labelHistoryEssItems);
            this.groupBoxCustomHistoryLoad.Controls.Add(this.labelHistorySel);
            this.groupBoxCustomHistoryLoad.FillClientAreaWithAlternateColor = false;
            this.groupBoxCustomHistoryLoad.Location = new System.Drawing.Point(3, 181);
            this.groupBoxCustomHistoryLoad.Name = "groupBoxCustomHistoryLoad";
            this.groupBoxCustomHistoryLoad.Size = new System.Drawing.Size(333, 142);
            this.groupBoxCustomHistoryLoad.TabIndex = 21;
            this.groupBoxCustomHistoryLoad.TabStop = false;
            this.groupBoxCustomHistoryLoad.Text = "History/Memory Options";
            this.groupBoxCustomHistoryLoad.TextPadding = 0;
            this.groupBoxCustomHistoryLoad.TextStartPosition = -1;
            // 
            // labelHistoryEssItems
            // 
            this.labelHistoryEssItems.AutoSize = true;
            this.labelHistoryEssItems.Location = new System.Drawing.Point(10, 102);
            this.labelHistoryEssItems.Name = "labelHistoryEssItems";
            this.labelHistoryEssItems.Size = new System.Drawing.Size(103, 13);
            this.labelHistoryEssItems.TabIndex = 5;
            this.labelHistoryEssItems.Text = "Essential Entries Are";
            // 
            // labelHistorySel
            // 
            this.labelHistorySel.AutoSize = true;
            this.labelHistorySel.Location = new System.Drawing.Point(10, 69);
            this.labelHistorySel.Name = "labelHistorySel";
            this.labelHistorySel.Size = new System.Drawing.Size(137, 13);
            this.labelHistorySel.TabIndex = 5;
            this.labelHistorySel.Text = "Only Read Essential Entries";
            // 
            // groupBox3dmap
            // 
            this.groupBox3dmap.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBox3dmap.BackColorScaling = 0.5F;
            this.groupBox3dmap.BorderColor = System.Drawing.Color.LightGray;
            this.groupBox3dmap.BorderColorScaling = 0.5F;
            this.groupBox3dmap.Controls.Add(this.labelMapCol);
            this.groupBox3dmap.Controls.Add(this.textBoxDefaultZoom);
            this.groupBox3dmap.Controls.Add(this.labelZoom);
            this.groupBox3dmap.Controls.Add(this.radioButtonHistorySelection);
            this.groupBox3dmap.Controls.Add(this.radioButtonCentreHome);
            this.groupBox3dmap.Controls.Add(this.labelOpenOn);
            this.groupBox3dmap.Controls.Add(this.labelHome);
            this.groupBox3dmap.Controls.Add(this.textBoxHomeSystem);
            this.groupBox3dmap.Controls.Add(this.panel_defaultmapcolor);
            this.groupBox3dmap.FillClientAreaWithAlternateColor = false;
            this.groupBox3dmap.Location = new System.Drawing.Point(342, 181);
            this.groupBox3dmap.Name = "groupBox3dmap";
            this.groupBox3dmap.Size = new System.Drawing.Size(333, 142);
            this.groupBox3dmap.TabIndex = 17;
            this.groupBox3dmap.TabStop = false;
            this.groupBox3dmap.Text = "3D Map Settings";
            this.groupBox3dmap.TextPadding = 0;
            this.groupBox3dmap.TextStartPosition = -1;
            // 
            // labelMapCol
            // 
            this.labelMapCol.AutoSize = true;
            this.labelMapCol.Location = new System.Drawing.Point(10, 117);
            this.labelMapCol.Name = "labelMapCol";
            this.labelMapCol.Size = new System.Drawing.Size(92, 13);
            this.labelMapCol.TabIndex = 7;
            this.labelMapCol.Text = "Default Map Color";
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.Location = new System.Drawing.Point(10, 91);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(71, 13);
            this.labelZoom.TabIndex = 5;
            this.labelZoom.Text = "Default Zoom";
            // 
            // labelOpenOn
            // 
            this.labelOpenOn.AutoSize = true;
            this.labelOpenOn.Location = new System.Drawing.Point(10, 49);
            this.labelOpenOn.Name = "labelOpenOn";
            this.labelOpenOn.Size = new System.Drawing.Size(90, 13);
            this.labelOpenOn.TabIndex = 2;
            this.labelOpenOn.Text = "Open Centred On";
            // 
            // labelHome
            // 
            this.labelHome.AutoSize = true;
            this.labelHome.Location = new System.Drawing.Point(10, 22);
            this.labelHome.Name = "labelHome";
            this.labelHome.Size = new System.Drawing.Size(72, 13);
            this.labelHome.TabIndex = 1;
            this.labelHome.Text = "Home System";
            // 
            // groupBoxCustomLanguage
            // 
            this.groupBoxCustomLanguage.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCustomLanguage.BackColorScaling = 0.5F;
            this.groupBoxCustomLanguage.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCustomLanguage.BorderColorScaling = 0.5F;
            this.groupBoxCustomLanguage.Controls.Add(this.comboBoxCustomLanguage);
            this.groupBoxCustomLanguage.FillClientAreaWithAlternateColor = false;
            this.groupBoxCustomLanguage.Location = new System.Drawing.Point(3, 518);
            this.groupBoxCustomLanguage.Name = "groupBoxCustomLanguage";
            this.groupBoxCustomLanguage.Size = new System.Drawing.Size(333, 51);
            this.groupBoxCustomLanguage.TabIndex = 21;
            this.groupBoxCustomLanguage.TabStop = false;
            this.groupBoxCustomLanguage.Text = "Language";
            this.groupBoxCustomLanguage.TextPadding = 0;
            this.groupBoxCustomLanguage.TextStartPosition = -1;
            // 
            // comboBoxCustomLanguage
            // 
            this.comboBoxCustomLanguage.BackColor = System.Drawing.Color.Gray;
            this.comboBoxCustomLanguage.BorderColor = System.Drawing.Color.Red;
            this.comboBoxCustomLanguage.ButtonColorScaling = 0.5F;
            this.comboBoxCustomLanguage.DataSource = null;
            this.comboBoxCustomLanguage.DisableBackgroundDisabledShadingGradient = false;
            this.comboBoxCustomLanguage.DisplayMember = "";
            this.comboBoxCustomLanguage.DropDownBackgroundColor = System.Drawing.Color.Gray;
            this.comboBoxCustomLanguage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxCustomLanguage.Location = new System.Drawing.Point(10, 19);
            this.comboBoxCustomLanguage.MouseOverBackgroundColor = System.Drawing.Color.Silver;
            this.comboBoxCustomLanguage.Name = "comboBoxCustomLanguage";
            this.comboBoxCustomLanguage.ScrollBarButtonColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomLanguage.ScrollBarColor = System.Drawing.Color.LightGray;
            this.comboBoxCustomLanguage.SelectedIndex = -1;
            this.comboBoxCustomLanguage.SelectedItem = null;
            this.comboBoxCustomLanguage.SelectedValue = null;
            this.comboBoxCustomLanguage.Size = new System.Drawing.Size(209, 21);
            this.comboBoxCustomLanguage.TabIndex = 0;
            this.comboBoxCustomLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.comboBoxCustomLanguage.ValueMember = "";
            // 
            // groupBoxTheme
            // 
            this.groupBoxTheme.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxTheme.BackColorScaling = 0.5F;
            this.groupBoxTheme.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxTheme.BorderColorScaling = 0.5F;
            this.groupBoxTheme.Controls.Add(this.comboBoxTheme);
            this.groupBoxTheme.Controls.Add(this.button_edittheme);
            this.groupBoxTheme.Controls.Add(this.buttonSaveTheme);
            this.groupBoxTheme.FillClientAreaWithAlternateColor = false;
            this.groupBoxTheme.Location = new System.Drawing.Point(3, 327);
            this.groupBoxTheme.Name = "groupBoxTheme";
            this.groupBoxTheme.Size = new System.Drawing.Size(333, 82);
            this.groupBoxTheme.TabIndex = 18;
            this.groupBoxTheme.TabStop = false;
            this.groupBoxTheme.Text = "Theme";
            this.groupBoxTheme.TextPadding = 0;
            this.groupBoxTheme.TextStartPosition = -1;
            // 
            // groupBoxCustomSafeMode
            // 
            this.groupBoxCustomSafeMode.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCustomSafeMode.BackColorScaling = 0.5F;
            this.groupBoxCustomSafeMode.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCustomSafeMode.BorderColorScaling = 0.5F;
            this.groupBoxCustomSafeMode.Controls.Add(this.buttonExtSafeMode);
            this.groupBoxCustomSafeMode.Controls.Add(this.labelSafeMode);
            this.groupBoxCustomSafeMode.FillClientAreaWithAlternateColor = false;
            this.groupBoxCustomSafeMode.Location = new System.Drawing.Point(3, 575);
            this.groupBoxCustomSafeMode.Name = "groupBoxCustomSafeMode";
            this.groupBoxCustomSafeMode.Size = new System.Drawing.Size(333, 68);
            this.groupBoxCustomSafeMode.TabIndex = 21;
            this.groupBoxCustomSafeMode.TabStop = false;
            this.groupBoxCustomSafeMode.Text = "Move System DB / Reset UI etc";
            this.groupBoxCustomSafeMode.TextPadding = 0;
            this.groupBoxCustomSafeMode.TextStartPosition = -1;
            // 
            // labelSafeMode
            // 
            this.labelSafeMode.Location = new System.Drawing.Point(10, 19);
            this.labelSafeMode.Name = "labelSafeMode";
            this.labelSafeMode.Size = new System.Drawing.Size(202, 38);
            this.labelSafeMode.TabIndex = 5;
            this.labelSafeMode.Text = "Click this to perform special operations";
            // 
            // groupBoxPopOuts
            // 
            this.groupBoxPopOuts.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxPopOuts.BackColorScaling = 0.5F;
            this.groupBoxPopOuts.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxPopOuts.BorderColorScaling = 0.5F;
            this.groupBoxPopOuts.Controls.Add(this.checkBoxPanelSortOrder);
            this.groupBoxPopOuts.Controls.Add(this.checkBoxKeepOnTop);
            this.groupBoxPopOuts.Controls.Add(this.checkBoxCustomResize);
            this.groupBoxPopOuts.Controls.Add(this.checkBoxMinimizeToNotifyIcon);
            this.groupBoxPopOuts.Controls.Add(this.comboBoxClickThruKey);
            this.groupBoxPopOuts.Controls.Add(this.checkBoxUseNotifyIcon);
            this.groupBoxPopOuts.Controls.Add(this.labelTKey);
            this.groupBoxPopOuts.FillClientAreaWithAlternateColor = false;
            this.groupBoxPopOuts.Location = new System.Drawing.Point(342, 336);
            this.groupBoxPopOuts.Name = "groupBoxPopOuts";
            this.groupBoxPopOuts.Size = new System.Drawing.Size(333, 192);
            this.groupBoxPopOuts.TabIndex = 19;
            this.groupBoxPopOuts.TabStop = false;
            this.groupBoxPopOuts.Text = "Window Options";
            this.groupBoxPopOuts.TextPadding = 0;
            this.groupBoxPopOuts.TextStartPosition = -1;
            // 
            // labelTKey
            // 
            this.labelTKey.AutoSize = true;
            this.labelTKey.Location = new System.Drawing.Point(7, 21);
            this.labelTKey.Name = "labelTKey";
            this.labelTKey.Size = new System.Drawing.Size(178, 13);
            this.labelTKey.TabIndex = 5;
            this.labelTKey.Text = "Key to activate transparent windows";
            // 
            // groupBoxCustomEDSM
            // 
            this.groupBoxCustomEDSM.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCustomEDSM.BackColorScaling = 0.5F;
            this.groupBoxCustomEDSM.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCustomEDSM.BorderColorScaling = 0.5F;
            this.groupBoxCustomEDSM.Controls.Add(this.buttonExtEDSMConfigureArea);
            this.groupBoxCustomEDSM.Controls.Add(this.checkBoxCustomEDSMEDDBDownload);
            this.groupBoxCustomEDSM.FillClientAreaWithAlternateColor = false;
            this.groupBoxCustomEDSM.Location = new System.Drawing.Point(342, 540);
            this.groupBoxCustomEDSM.Name = "groupBoxCustomEDSM";
            this.groupBoxCustomEDSM.Size = new System.Drawing.Size(333, 60);
            this.groupBoxCustomEDSM.TabIndex = 21;
            this.groupBoxCustomEDSM.TabStop = false;
            this.groupBoxCustomEDSM.Text = "EDSM/EDDB Control";
            this.groupBoxCustomEDSM.TextPadding = 0;
            this.groupBoxCustomEDSM.TextStartPosition = -1;
            // 
            // groupBoxCustomScreenShots
            // 
            this.groupBoxCustomScreenShots.AlternateClientBackColor = System.Drawing.Color.Blue;
            this.groupBoxCustomScreenShots.BackColorScaling = 0.5F;
            this.groupBoxCustomScreenShots.BorderColor = System.Drawing.Color.LightGray;
            this.groupBoxCustomScreenShots.BorderColorScaling = 0.5F;
            this.groupBoxCustomScreenShots.Controls.Add(this.checkBoxCustomCopyToClipboard);
            this.groupBoxCustomScreenShots.Controls.Add(this.checkBoxCustomMarkHiRes);
            this.groupBoxCustomScreenShots.Controls.Add(this.checkBoxCustomRemoveOriginals);
            this.groupBoxCustomScreenShots.Controls.Add(this.buttonExtScreenshot);
            this.groupBoxCustomScreenShots.Controls.Add(this.checkBoxCustomEnableScreenshots);
            this.groupBoxCustomScreenShots.FillClientAreaWithAlternateColor = false;
            this.groupBoxCustomScreenShots.Location = new System.Drawing.Point(3, 412);
            this.groupBoxCustomScreenShots.Name = "groupBoxCustomScreenShots";
            this.groupBoxCustomScreenShots.Size = new System.Drawing.Size(333, 100);
            this.groupBoxCustomScreenShots.TabIndex = 20;
            this.groupBoxCustomScreenShots.TabStop = false;
            this.groupBoxCustomScreenShots.Text = "Screenshots";
            this.groupBoxCustomScreenShots.TextPadding = 0;
            this.groupBoxCustomScreenShots.TextStartPosition = -1;
            // 
            // UserControlSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extPanelScroll);
            this.Name = "UserControlSettings";
            this.Size = new System.Drawing.Size(732, 659);
            this.extPanelScroll.ResumeLayout(false);
            this.groupBoxCommanders.ResumeLayout(false);
            this.dataViewScrollerCommanders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommanders)).EndInit();
            this.groupBoxCustomHistoryLoad.ResumeLayout(false);
            this.groupBoxCustomHistoryLoad.PerformLayout();
            this.groupBox3dmap.ResumeLayout(false);
            this.groupBox3dmap.PerformLayout();
            this.groupBoxCustomLanguage.ResumeLayout(false);
            this.groupBoxTheme.ResumeLayout(false);
            this.groupBoxCustomSafeMode.ResumeLayout(false);
            this.groupBoxPopOuts.ResumeLayout(false);
            this.groupBoxPopOuts.PerformLayout();
            this.groupBoxCustomEDSM.ResumeLayout(false);
            this.groupBoxCustomEDSM.PerformLayout();
            this.groupBoxCustomScreenShots.ResumeLayout(false);
            this.groupBoxCustomScreenShots.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ExtendedControls.ExtGroupBox groupBoxCommanders;
        private ExtendedControls.ExtButton buttonAddCommander;
        private System.Windows.Forms.DataGridView dataGridViewCommanders;
        private ExtendedControls.ExtGroupBox groupBox3dmap;
        private ExtendedControls.NumberBoxDouble textBoxDefaultZoom;
        private System.Windows.Forms.Label labelZoom;
        private ExtendedControls.ExtRadioButton radioButtonHistorySelection;
        private ExtendedControls.ExtRadioButton radioButtonCentreHome;
        private System.Windows.Forms.Label labelOpenOn;
        private System.Windows.Forms.Label labelHome;
        private ExtendedControls.ExtTextBoxAutoComplete textBoxHomeSystem;
        private ExtendedControls.ExtComboBox comboBoxTheme;
        private System.Windows.Forms.ToolTip toolTip;
        private ExtendedControls.PanelNoTheme panel_defaultmapcolor;
        private ExtendedControls.ExtButton buttonSaveTheme;
        private System.Windows.Forms.Label labelMapCol;
        private ExtendedControls.ExtButton button_edittheme;
        private ExtendedControls.ExtGroupBox groupBoxTheme;
        private ExtendedControls.ExtCheckBox checkBoxOrderRowsInverted;
        private ExtendedControls.ExtCheckBox checkBoxKeepOnTop;
        private ExtendedControls.ExtButton btnDeleteCommander;
        private ExtendedControls.ExtCheckBox checkBoxUTC;
        private ExtendedControls.ExtPanelDataGridViewScroll dataViewScrollerCommanders;
        private ExtendedControls.ExtScrollBar vScrollBarCommanders;
        private ExtendedControls.ExtGroupBox groupBoxPopOuts;
        private ExtendedControls.ExtCheckBox checkBoxMinimizeToNotifyIcon;
        private ExtendedControls.ExtCheckBox checkBoxUseNotifyIcon;
        private ExtendedControls.ExtButton buttonEditCommander;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommander;
        private System.Windows.Forms.DataGridViewTextBoxColumn EdsmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JournalDirCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesCol;
        private ExtendedControls.ExtComboBox comboBoxClickThruKey;
        private System.Windows.Forms.Label labelTKey;
        private ExtendedControls.ExtGroupBox groupBoxCustomScreenShots;
        private ExtendedControls.ExtCheckBox checkBoxCustomMarkHiRes;
        private ExtendedControls.ExtCheckBox checkBoxCustomRemoveOriginals;
        private ExtendedControls.ExtButton buttonExtScreenshot;
        private ExtendedControls.ExtCheckBox checkBoxCustomEnableScreenshots;
        private ExtendedControls.ExtCheckBox checkBoxCustomCopyToClipboard;
        private ExtendedControls.ExtGroupBox groupBoxCustomEDSM;
        private ExtendedControls.ExtButton buttonExtEDSMConfigureArea;
        private ExtendedControls.ExtCheckBox checkBoxCustomEDSMEDDBDownload;
        private ExtendedControls.ExtGroupBox groupBoxCustomHistoryLoad;
        private ExtendedControls.ExtComboBox comboBoxCustomHistoryLoadTime;
        private System.Windows.Forms.Label labelHistorySel;
        private ExtendedControls.ExtGroupBox groupBoxCustomLanguage;
        private ExtendedControls.ExtComboBox comboBoxCustomLanguage;
        private ExtendedControls.ExtCheckBox checkBoxCustomResize;
        private ExtendedControls.ExtCheckBox checkBoxPanelSortOrder;
        private ExtendedControls.ExtGroupBox groupBoxCustomSafeMode;
        private ExtendedControls.ExtButton buttonExtSafeMode;
        private System.Windows.Forms.Label labelSafeMode;
        private ExtendedControls.ExtComboBox comboBoxCustomEssentialEntries;
        private System.Windows.Forms.Label labelHistoryEssItems;
        private ExtendedControls.ExtPanelScroll extPanelScroll;
        private ExtendedControls.ExtScrollBar extScrollBar1;
    }
}
