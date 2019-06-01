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
    partial class UserControlScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlScan));
            this.checkBoxCustomHideFullMats = new ExtendedControls.ExtCheckBox();
            this.extCheckBoxDisplaySystemAlways = new ExtendedControls.ExtCheckBox();
            this.chkShowOverlays = new ExtendedControls.ExtCheckBox();
            this.extCheckBoxStar = new ExtendedControls.ExtCheckBox();
            this.checkBoxMaterials = new ExtendedControls.ExtCheckBox();
            this.checkBoxMaterialsRare = new ExtendedControls.ExtCheckBox();
            this.checkBoxEDSM = new ExtendedControls.ExtCheckBox();
            this.checkBoxMoons = new ExtendedControls.ExtCheckBox();
            this.buttonSize = new ExtendedControls.ExtButton();
            this.extButtonHighValue = new ExtendedControls.ExtButton();
            this.buttonExtExcel = new ExtendedControls.ExtButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.rollUpPanelTop = new ExtendedControls.ExtPanelRollUp();
            this.panelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.panelStars = new EDDiscovery.UserControls.ScanDisplayUserControl();
            this.rollUpPanelTop.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxCustomHideFullMats
            // 
            this.checkBoxCustomHideFullMats.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxCustomHideFullMats.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxCustomHideFullMats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBoxCustomHideFullMats.CheckBoxColor = System.Drawing.Color.White;
            this.checkBoxCustomHideFullMats.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxCustomHideFullMats.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxCustomHideFullMats.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxCustomHideFullMats.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxCustomHideFullMats.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxCustomHideFullMats.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxCustomHideFullMats.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkBoxCustomHideFullMats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkBoxCustomHideFullMats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxCustomHideFullMats.Image = global::EDDiscovery.Icons.Controls.Scan_HideFullMaterials;
            this.checkBoxCustomHideFullMats.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxCustomHideFullMats.ImageIndeterminate = null;
            this.checkBoxCustomHideFullMats.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxCustomHideFullMats.ImageUnchecked = null;
            this.checkBoxCustomHideFullMats.Location = new System.Drawing.Point(92, 1);
            this.checkBoxCustomHideFullMats.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.checkBoxCustomHideFullMats.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxCustomHideFullMats.Name = "checkBoxCustomHideFullMats";
            this.checkBoxCustomHideFullMats.Size = new System.Drawing.Size(24, 24);
            this.checkBoxCustomHideFullMats.TabIndex = 31;
            this.checkBoxCustomHideFullMats.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxCustomHideFullMats, "Show/Hide materials which have reached their storage limit");
            this.checkBoxCustomHideFullMats.UseVisualStyleBackColor = false;
            this.checkBoxCustomHideFullMats.CheckedChanged += new System.EventHandler(this.checkBoxCustomHideFullMats_CheckedChanged);
            // 
            // extCheckBoxDisplaySystemAlways
            // 
            this.extCheckBoxDisplaySystemAlways.Appearance = System.Windows.Forms.Appearance.Button;
            this.extCheckBoxDisplaySystemAlways.BackColor = System.Drawing.Color.Transparent;
            this.extCheckBoxDisplaySystemAlways.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extCheckBoxDisplaySystemAlways.CheckBoxColor = System.Drawing.Color.White;
            this.extCheckBoxDisplaySystemAlways.CheckBoxDisabledScaling = 0.5F;
            this.extCheckBoxDisplaySystemAlways.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extCheckBoxDisplaySystemAlways.CheckColor = System.Drawing.Color.DarkBlue;
            this.extCheckBoxDisplaySystemAlways.Cursor = System.Windows.Forms.Cursors.Default;
            this.extCheckBoxDisplaySystemAlways.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.extCheckBoxDisplaySystemAlways.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.extCheckBoxDisplaySystemAlways.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.extCheckBoxDisplaySystemAlways.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.extCheckBoxDisplaySystemAlways.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extCheckBoxDisplaySystemAlways.Image = global::EDDiscovery.Icons.Controls.Scan_DisplaySystemAlways;
            this.extCheckBoxDisplaySystemAlways.ImageButtonDisabledScaling = 0.5F;
            this.extCheckBoxDisplaySystemAlways.ImageIndeterminate = null;
            this.extCheckBoxDisplaySystemAlways.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extCheckBoxDisplaySystemAlways.ImageUnchecked = null;
            this.extCheckBoxDisplaySystemAlways.Location = new System.Drawing.Point(256, 1);
            this.extCheckBoxDisplaySystemAlways.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.extCheckBoxDisplaySystemAlways.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extCheckBoxDisplaySystemAlways.Name = "extCheckBoxDisplaySystemAlways";
            this.extCheckBoxDisplaySystemAlways.Size = new System.Drawing.Size(24, 24);
            this.extCheckBoxDisplaySystemAlways.TabIndex = 31;
            this.extCheckBoxDisplaySystemAlways.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.extCheckBoxDisplaySystemAlways, "Show system and value in main display");
            this.extCheckBoxDisplaySystemAlways.UseVisualStyleBackColor = false;
            this.extCheckBoxDisplaySystemAlways.CheckedChanged += new System.EventHandler(this.extCheckBoxDisplaySystemAlways_CheckedChanged);
            // 
            // chkShowOverlays
            // 
            this.chkShowOverlays.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkShowOverlays.BackColor = System.Drawing.Color.Transparent;
            this.chkShowOverlays.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chkShowOverlays.CheckBoxColor = System.Drawing.Color.White;
            this.chkShowOverlays.CheckBoxDisabledScaling = 0.5F;
            this.chkShowOverlays.CheckBoxInnerColor = System.Drawing.Color.White;
            this.chkShowOverlays.CheckColor = System.Drawing.Color.DarkBlue;
            this.chkShowOverlays.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkShowOverlays.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.chkShowOverlays.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.chkShowOverlays.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.chkShowOverlays.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.chkShowOverlays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowOverlays.Image = global::EDDiscovery.Icons.Controls.Scan_ShowOverlays;
            this.chkShowOverlays.ImageButtonDisabledScaling = 0.5F;
            this.chkShowOverlays.ImageIndeterminate = null;
            this.chkShowOverlays.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chkShowOverlays.ImageUnchecked = null;
            this.chkShowOverlays.Location = new System.Drawing.Point(228, 1);
            this.chkShowOverlays.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.chkShowOverlays.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.chkShowOverlays.Name = "chkShowOverlays";
            this.chkShowOverlays.Size = new System.Drawing.Size(24, 24);
            this.chkShowOverlays.TabIndex = 31;
            this.chkShowOverlays.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.chkShowOverlays, "Show/Hide body status icons");
            this.chkShowOverlays.UseVisualStyleBackColor = false;
            this.chkShowOverlays.CheckedChanged += new System.EventHandler(this.chkShowOverlays_CheckedChanged);
            // 
            // extCheckBoxStar
            // 
            this.extCheckBoxStar.Appearance = System.Windows.Forms.Appearance.Button;
            this.extCheckBoxStar.BackColor = System.Drawing.Color.Transparent;
            this.extCheckBoxStar.Image = global::EDDiscovery.Icons.Controls.Scan_ShowAnotherSystem;
            this.extCheckBoxStar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extCheckBoxStar.CheckBoxColor = System.Drawing.Color.White;
            this.extCheckBoxStar.CheckBoxDisabledScaling = 0.5F;
            this.extCheckBoxStar.CheckBoxInnerColor = System.Drawing.Color.White;
            this.extCheckBoxStar.CheckColor = System.Drawing.Color.DarkBlue;
            this.extCheckBoxStar.Cursor = System.Windows.Forms.Cursors.Default;
            this.extCheckBoxStar.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.extCheckBoxStar.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.extCheckBoxStar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.extCheckBoxStar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.extCheckBoxStar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.extCheckBoxStar.ImageButtonDisabledScaling = 0.5F;
            this.extCheckBoxStar.ImageIndeterminate = null;
            this.extCheckBoxStar.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.extCheckBoxStar.ImageUnchecked = null;
            this.extCheckBoxStar.Location = new System.Drawing.Point(0, 1);
            this.extCheckBoxStar.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.extCheckBoxStar.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.extCheckBoxStar.Name = "extCheckBoxStar";
            this.extCheckBoxStar.Size = new System.Drawing.Size(24, 24);
            this.extCheckBoxStar.TabIndex = 2;
            this.extCheckBoxStar.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.extCheckBoxStar, "Select another system to view");
            this.extCheckBoxStar.UseVisualStyleBackColor = false;
            this.extCheckBoxStar.Click += new System.EventHandler(this.extCheckBoxStar_Click);
            // 
            // checkBoxMaterials
            // 
            this.checkBoxMaterials.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMaterials.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMaterials.Image = global::EDDiscovery.Icons.Controls.Scan_ShowAllMaterials;
            this.checkBoxMaterials.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMaterials.CheckBoxColor = System.Drawing.Color.White;
            this.checkBoxMaterials.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxMaterials.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxMaterials.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxMaterials.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxMaterials.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxMaterials.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxMaterials.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkBoxMaterials.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkBoxMaterials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxMaterials.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxMaterials.ImageIndeterminate = null;
            this.checkBoxMaterials.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMaterials.ImageUnchecked = null;
            this.checkBoxMaterials.Location = new System.Drawing.Point(36, 1);
            this.checkBoxMaterials.Margin = new System.Windows.Forms.Padding(8, 1, 4, 1);
            this.checkBoxMaterials.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxMaterials.Name = "checkBoxMaterials";
            this.checkBoxMaterials.Size = new System.Drawing.Size(24, 24);
            this.checkBoxMaterials.TabIndex = 2;
            this.checkBoxMaterials.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxMaterials, "Show/Hide Materials");
            this.checkBoxMaterials.UseVisualStyleBackColor = false;
            this.checkBoxMaterials.CheckedChanged += new System.EventHandler(this.checkBoxMaterials_CheckedChanged);
            // 
            // checkBoxMaterialsRare
            // 
            this.checkBoxMaterialsRare.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMaterialsRare.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMaterialsRare.Image = global::EDDiscovery.Icons.Controls.Scan_ShowRareMaterials;
            this.checkBoxMaterialsRare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMaterialsRare.CheckBoxColor = System.Drawing.Color.White;
            this.checkBoxMaterialsRare.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxMaterialsRare.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxMaterialsRare.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxMaterialsRare.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxMaterialsRare.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxMaterialsRare.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxMaterialsRare.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkBoxMaterialsRare.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkBoxMaterialsRare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxMaterialsRare.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxMaterialsRare.ImageIndeterminate = null;
            this.checkBoxMaterialsRare.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMaterialsRare.ImageUnchecked = null;
            this.checkBoxMaterialsRare.Location = new System.Drawing.Point(64, 1);
            this.checkBoxMaterialsRare.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.checkBoxMaterialsRare.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxMaterialsRare.Name = "checkBoxMaterialsRare";
            this.checkBoxMaterialsRare.Size = new System.Drawing.Size(24, 24);
            this.checkBoxMaterialsRare.TabIndex = 2;
            this.checkBoxMaterialsRare.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxMaterialsRare, "Show rare materials only");
            this.checkBoxMaterialsRare.UseVisualStyleBackColor = false;
            this.checkBoxMaterialsRare.CheckedChanged += new System.EventHandler(this.checkBoxMaterialsRare_CheckedChanged);
            // 
            // checkBoxEDSM
            // 
            this.checkBoxEDSM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxEDSM.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEDSM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.checkBoxEDSM.CheckBoxColor = System.Drawing.Color.White;
            this.checkBoxEDSM.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxEDSM.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxEDSM.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxEDSM.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxEDSM.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxEDSM.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxEDSM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkBoxEDSM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkBoxEDSM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxEDSM.Image = global::EDDiscovery.Icons.Controls.Scan_FetchEDSMBodies;
            this.checkBoxEDSM.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxEDSM.ImageIndeterminate = null;
            this.checkBoxEDSM.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxEDSM.ImageUnchecked = null;
            this.checkBoxEDSM.Location = new System.Drawing.Point(200, 1);
            this.checkBoxEDSM.Margin = new System.Windows.Forms.Padding(8, 1, 4, 1);
            this.checkBoxEDSM.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxEDSM.Name = "checkBoxEDSM";
            this.checkBoxEDSM.Size = new System.Drawing.Size(24, 24);
            this.checkBoxEDSM.TabIndex = 3;
            this.checkBoxEDSM.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxEDSM, "Show/Hide Body data from EDSM");
            this.checkBoxEDSM.UseVisualStyleBackColor = false;
            this.checkBoxEDSM.CheckedChanged += new System.EventHandler(this.checkBoxEDSM_CheckedChanged);
            // 
            // checkBoxMoons
            // 
            this.checkBoxMoons.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMoons.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMoons.Image = global::EDDiscovery.Icons.Controls.Scan_ShowMoons;
            this.checkBoxMoons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMoons.CheckBoxColor = System.Drawing.Color.White;
            this.checkBoxMoons.CheckBoxDisabledScaling = 0.5F;
            this.checkBoxMoons.CheckBoxInnerColor = System.Drawing.Color.White;
            this.checkBoxMoons.CheckColor = System.Drawing.Color.DarkBlue;
            this.checkBoxMoons.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxMoons.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxMoons.FlatAppearance.CheckedBackColor = System.Drawing.Color.Green;
            this.checkBoxMoons.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.checkBoxMoons.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.checkBoxMoons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxMoons.ImageButtonDisabledScaling = 0.5F;
            this.checkBoxMoons.ImageIndeterminate = null;
            this.checkBoxMoons.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBoxMoons.ImageUnchecked = null;
            this.checkBoxMoons.Location = new System.Drawing.Point(128, 1);
            this.checkBoxMoons.Margin = new System.Windows.Forms.Padding(8, 1, 4, 1);
            this.checkBoxMoons.MouseOverColor = System.Drawing.Color.CornflowerBlue;
            this.checkBoxMoons.Name = "checkBoxMoons";
            this.checkBoxMoons.Size = new System.Drawing.Size(24, 24);
            this.checkBoxMoons.TabIndex = 2;
            this.checkBoxMoons.TickBoxReductionRatio = 0.75F;
            this.toolTip.SetToolTip(this.checkBoxMoons, "Show/Hide Moons");
            this.checkBoxMoons.UseVisualStyleBackColor = false;
            this.checkBoxMoons.CheckedChanged += new System.EventHandler(this.checkBoxMoons_CheckedChanged);
            // 
            // buttonSize
            // 
            this.buttonSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSize.Image = global::EDDiscovery.Icons.Controls.Scan_SizeLarge;
            this.buttonSize.Location = new System.Drawing.Point(164, 1);
            this.buttonSize.Margin = new System.Windows.Forms.Padding(8, 1, 4, 1);
            this.buttonSize.Name = "buttonSize";
            this.buttonSize.Size = new System.Drawing.Size(24, 24);
            this.buttonSize.TabIndex = 29;
            this.toolTip.SetToolTip(this.buttonSize, "Select image size");
            this.buttonSize.UseVisualStyleBackColor = true;
            this.buttonSize.Click += new System.EventHandler(this.buttonSize_Click);
            // 
            // extButtonHighValue
            // 
            this.extButtonHighValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extButtonHighValue.Image = global::EDDiscovery.Icons.Controls.Scan_Bodies_HighValue;
            this.extButtonHighValue.Location = new System.Drawing.Point(284, 1);
            this.extButtonHighValue.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.extButtonHighValue.Name = "extButtonHighValue";
            this.extButtonHighValue.Size = new System.Drawing.Size(24, 24);
            this.extButtonHighValue.TabIndex = 29;
            this.toolTip.SetToolTip(this.extButtonHighValue, "Set High Value Limit");
            this.extButtonHighValue.UseVisualStyleBackColor = true;
            this.extButtonHighValue.Click += new System.EventHandler(this.extButtonHighValue_Click);
            // 
            // buttonExtExcel
            // 
            this.buttonExtExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExtExcel.Image = global::EDDiscovery.Icons.Controls.Scan_ExportToExcel;
            this.buttonExtExcel.Location = new System.Drawing.Point(312, 1);
            this.buttonExtExcel.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.buttonExtExcel.Name = "buttonExtExcel";
            this.buttonExtExcel.Size = new System.Drawing.Size(24, 24);
            this.buttonExtExcel.TabIndex = 29;
            this.toolTip.SetToolTip(this.buttonExtExcel, "Send data on grid to excel");
            this.buttonExtExcel.UseVisualStyleBackColor = true;
            this.buttonExtExcel.Click += new System.EventHandler(this.buttonExtExcel_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 20000;
            this.toolTip.InitialDelay = 250;
            this.toolTip.ReshowDelay = 100;
            // 
            // rollUpPanelTop
            // 
            this.rollUpPanelTop.AutoSize = true;
            this.rollUpPanelTop.Controls.Add(this.panelControls);
            this.rollUpPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.rollUpPanelTop.HiddenMarkerWidth = 400;
            this.rollUpPanelTop.Location = new System.Drawing.Point(0, 0);
            this.rollUpPanelTop.Name = "rollUpPanelTop";
            this.rollUpPanelTop.PinState = true;
            this.rollUpPanelTop.RolledUpHeight = 5;
            this.rollUpPanelTop.RollUpAnimationTime = 500;
            this.rollUpPanelTop.RollUpDelay = 1000;
            this.rollUpPanelTop.SecondHiddenMarkerWidth = 0;
            this.rollUpPanelTop.ShowHiddenMarker = true;
            this.rollUpPanelTop.Size = new System.Drawing.Size(748, 26);
            this.rollUpPanelTop.TabIndex = 4;
            this.rollUpPanelTop.UnrollHoverDelay = 1000;
            // 
            // panelControls
            // 
            this.panelControls.AutoSize = true;
            this.panelControls.BackColor = System.Drawing.SystemColors.Control;
            this.panelControls.Controls.Add(this.extCheckBoxStar);
            this.panelControls.Controls.Add(this.checkBoxMaterials);
            this.panelControls.Controls.Add(this.checkBoxMaterialsRare);
            this.panelControls.Controls.Add(this.checkBoxCustomHideFullMats);
            this.panelControls.Controls.Add(this.checkBoxMoons);
            this.panelControls.Controls.Add(this.buttonSize);
            this.panelControls.Controls.Add(this.checkBoxEDSM);
            this.panelControls.Controls.Add(this.chkShowOverlays);
            this.panelControls.Controls.Add(this.extCheckBoxDisplaySystemAlways);
            this.panelControls.Controls.Add(this.extButtonHighValue);
            this.panelControls.Controls.Add(this.buttonExtExcel);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(748, 26);
            this.panelControls.TabIndex = 32;
            // 
            // panelStars
            // 
            this.panelStars.CheckEDSM = false;
            this.panelStars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStars.HideFullMaterials = false;
            this.panelStars.Location = new System.Drawing.Point(0, 26);
            this.panelStars.Name = "panelStars";
            this.panelStars.ShowMaterials = false;
            this.panelStars.ShowMaterialsRare = false;
            this.panelStars.ShowMoons = false;
            this.panelStars.ShowOverlays = false;
            this.panelStars.Size = new System.Drawing.Size(748, 656);
            this.panelStars.TabIndex = 5;
            this.panelStars.ValueLimit = 50000;
            // 
            // UserControlScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelStars);
            this.Controls.Add(this.rollUpPanelTop);
            this.Name = "UserControlScan";
            this.Size = new System.Drawing.Size(748, 682);
            this.Resize += new System.EventHandler(this.UserControlScan_Resize);
            this.rollUpPanelTop.ResumeLayout(false);
            this.rollUpPanelTop.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private ExtendedControls.ExtCheckBox checkBoxMaterials;
        private ExtendedControls.ExtCheckBox checkBoxMoons;
        private ExtendedControls.ExtCheckBox checkBoxMaterialsRare;
        private ExtendedControls.ExtCheckBox checkBoxEDSM;
        private ExtendedControls.ExtButton buttonExtExcel;
        private ExtendedControls.ExtCheckBox chkShowOverlays;
        private ExtendedControls.ExtPanelRollUp rollUpPanelTop;
        private ExtendedControls.ExtCheckBox checkBoxCustomHideFullMats;
        private ScanDisplayUserControl panelStars;
        private ExtendedControls.ExtButton buttonSize;
        private ExtendedControls.ExtButton extButtonHighValue;
        private ExtendedControls.ExtCheckBox extCheckBoxStar;
        private ExtendedControls.ExtCheckBox extCheckBoxDisplaySystemAlways;
        private System.Windows.Forms.FlowLayoutPanel panelControls;
    }
}
