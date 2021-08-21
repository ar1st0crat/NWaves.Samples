﻿using System.Drawing;
using NWaves.DemoForms.UserControls;

namespace NWaves.DemoForms
{
    partial class SignalsForm
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
            this.openFileButton = new System.Windows.Forms.Button();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.builderParametersListBox = new System.Windows.Forms.ListBox();
            this.signalPanel = new NWaves.DemoForms.UserControls.SignalPlot();
            this.generatedSignalPanel = new NWaves.DemoForms.UserControls.SignalPlot();
            this.superimposedSignalPanel = new NWaves.DemoForms.UserControls.SignalPlot();
            this.spectrumPanel = new NWaves.DemoForms.UserControls.LinePlot();
            this.builderComboBox = new System.Windows.Forms.ComboBox();
            this.operationComboBox = new System.Windows.Forms.ComboBox();
            this.generateSignalButton = new System.Windows.Forms.Button();
            this.signalOperationButton = new System.Windows.Forms.Button();
            this.durationTextBox = new System.Windows.Forms.TextBox();
            this.operationSamplesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.leftSliceTextBox = new System.Windows.Forms.TextBox();
            this.signalSliceButton = new System.Windows.Forms.Button();
            this.rightSliceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mfccToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lpcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulationSpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adaptiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hpssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.stftButton = new System.Windows.Forms.Button();
            this.waveletsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(1122, 31);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 37);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Open...";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Location = new System.Drawing.Point(12, 38);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(1104, 22);
            this.filenameTextBox.TabIndex = 1;
            // 
            // builderParametersListBox
            // 
            this.builderParametersListBox.FormattingEnabled = true;
            this.builderParametersListBox.ItemHeight = 16;
            this.builderParametersListBox.Location = new System.Drawing.Point(13, 99);
            this.builderParametersListBox.Name = "builderParametersListBox";
            this.builderParametersListBox.Size = new System.Drawing.Size(177, 628);
            this.builderParametersListBox.TabIndex = 2;
            // 
            // signalPanel
            // 
            this.signalPanel.AutoScroll = true;
            this.signalPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.signalPanel.ForeColor = System.Drawing.Color.Blue;
            this.signalPanel.Gain = 1F;
            this.signalPanel.Location = new System.Drawing.Point(196, 99);
            this.signalPanel.Name = "signalPanel";
            this.signalPanel.PaddingX = 24;
            this.signalPanel.PaddingY = 5;
            this.signalPanel.Signal = null;
            this.signalPanel.Size = new System.Drawing.Size(1001, 165);
            this.signalPanel.Stride = 64;
            this.signalPanel.TabIndex = 3;
            // 
            // generatedSignalPanel
            // 
            this.generatedSignalPanel.AutoScroll = true;
            this.generatedSignalPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.generatedSignalPanel.ForeColor = System.Drawing.Color.Red;
            this.generatedSignalPanel.Gain = 1F;
            this.generatedSignalPanel.Location = new System.Drawing.Point(196, 332);
            this.generatedSignalPanel.Name = "generatedSignalPanel";
            this.generatedSignalPanel.PaddingX = 24;
            this.generatedSignalPanel.PaddingY = 5;
            this.generatedSignalPanel.Signal = null;
            this.generatedSignalPanel.Size = new System.Drawing.Size(610, 184);
            this.generatedSignalPanel.Stride = 64;
            this.generatedSignalPanel.TabIndex = 4;
            // 
            // superimposedSignalPanel
            // 
            this.superimposedSignalPanel.AutoScroll = true;
            this.superimposedSignalPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.superimposedSignalPanel.ForeColor = System.Drawing.Color.SeaGreen;
            this.superimposedSignalPanel.Gain = 1F;
            this.superimposedSignalPanel.Location = new System.Drawing.Point(197, 543);
            this.superimposedSignalPanel.Name = "superimposedSignalPanel";
            this.superimposedSignalPanel.PaddingX = 24;
            this.superimposedSignalPanel.PaddingY = 5;
            this.superimposedSignalPanel.Signal = null;
            this.superimposedSignalPanel.Size = new System.Drawing.Size(1000, 181);
            this.superimposedSignalPanel.Stride = 64;
            this.superimposedSignalPanel.TabIndex = 5;
            // 
            // spectrumPanel
            // 
            this.spectrumPanel.AutoScroll = true;
            this.spectrumPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.spectrumPanel.ForeColor = System.Drawing.Color.Blue;
            this.spectrumPanel.Location = new System.Drawing.Point(812, 332);
            this.spectrumPanel.Name = "spectrumPanel";
            this.spectrumPanel.PaddingX = 30;
            this.spectrumPanel.PaddingY = 20;
            this.spectrumPanel.Size = new System.Drawing.Size(385, 184);
            this.spectrumPanel.Stride = 1;
            this.spectrumPanel.TabIndex = 5;
            this.spectrumPanel.Thickness = 1;
            // 
            // builderComboBox
            // 
            this.builderComboBox.FormattingEnabled = true;
            this.builderComboBox.Items.AddRange(new object[] {
            "Sinusoid",
            "Sawtooth",
            "Triangle Wave",
            "Square Wave",
            "Pulse Wave",
            "Chirp",
            "Sinc",
            "Ramp",
            "White Noise",
            "AWGN",
            "Pink Noise",
            "Red Noise",
            "Perlin Noise"});
            this.builderComboBox.Location = new System.Drawing.Point(197, 271);
            this.builderComboBox.Name = "builderComboBox";
            this.builderComboBox.Size = new System.Drawing.Size(124, 24);
            this.builderComboBox.TabIndex = 6;
            this.builderComboBox.Text = "Sinusoid";
            // 
            // operationComboBox
            // 
            this.operationComboBox.FormattingEnabled = true;
            this.operationComboBox.Items.AddRange(new object[] {
            "Delay by",
            "Repeat times"});
            this.operationComboBox.Location = new System.Drawing.Point(198, 301);
            this.operationComboBox.Name = "operationComboBox";
            this.operationComboBox.Size = new System.Drawing.Size(123, 24);
            this.operationComboBox.TabIndex = 7;
            this.operationComboBox.Text = "Delay by";
            // 
            // generateSignalButton
            // 
            this.generateSignalButton.Location = new System.Drawing.Point(471, 271);
            this.generateSignalButton.Name = "generateSignalButton";
            this.generateSignalButton.Size = new System.Drawing.Size(38, 23);
            this.generateSignalButton.TabIndex = 8;
            this.generateSignalButton.Text = ">>";
            this.generateSignalButton.UseVisualStyleBackColor = true;
            this.generateSignalButton.Click += new System.EventHandler(this.generateSignalButton_Click);
            // 
            // signalOperationButton
            // 
            this.signalOperationButton.Location = new System.Drawing.Point(471, 303);
            this.signalOperationButton.Name = "signalOperationButton";
            this.signalOperationButton.Size = new System.Drawing.Size(37, 23);
            this.signalOperationButton.TabIndex = 9;
            this.signalOperationButton.Text = ">>";
            this.signalOperationButton.UseVisualStyleBackColor = true;
            this.signalOperationButton.Click += new System.EventHandler(this.signalOperationButton_Click);
            // 
            // durationTextBox
            // 
            this.durationTextBox.Location = new System.Drawing.Point(327, 272);
            this.durationTextBox.Name = "durationTextBox";
            this.durationTextBox.Size = new System.Drawing.Size(71, 22);
            this.durationTextBox.TabIndex = 10;
            // 
            // operationSamplesTextBox
            // 
            this.operationSamplesTextBox.Location = new System.Drawing.Point(327, 303);
            this.operationSamplesTextBox.Name = "operationSamplesTextBox";
            this.operationSamplesTextBox.Size = new System.Drawing.Size(71, 22);
            this.operationSamplesTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "samples";
            // 
            // leftSliceTextBox
            // 
            this.leftSliceTextBox.Location = new System.Drawing.Point(610, 303);
            this.leftSliceTextBox.Name = "leftSliceTextBox";
            this.leftSliceTextBox.Size = new System.Drawing.Size(60, 22);
            this.leftSliceTextBox.TabIndex = 15;
            // 
            // signalSliceButton
            // 
            this.signalSliceButton.Location = new System.Drawing.Point(769, 303);
            this.signalSliceButton.Name = "signalSliceButton";
            this.signalSliceButton.Size = new System.Drawing.Size(37, 23);
            this.signalSliceButton.TabIndex = 14;
            this.signalSliceButton.Text = ">>";
            this.signalSliceButton.UseVisualStyleBackColor = true;
            this.signalSliceButton.Click += new System.EventHandler(this.signalSliceButton_Click);
            // 
            // rightSliceTextBox
            // 
            this.rightSliceTextBox.Location = new System.Drawing.Point(702, 303);
            this.rightSliceTextBox.Name = "rightSliceTextBox";
            this.rightSliceTextBox.Size = new System.Drawing.Size(60, 22);
            this.rightSliceTextBox.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "from";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(676, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "to";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.playbackToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.pitchToolStripMenuItem,
            this.mfccToolStripMenuItem,
            this.lpcToolStripMenuItem,
            this.modulationSpectrumToolStripMenuItem,
            this.effectsToolStripMenuItem,
            this.featuresToolStripMenuItem,
            this.noiseToolStripMenuItem,
            this.modulationToolStripMenuItem,
            this.onlineToolStripMenuItem,
            this.adaptiveToolStripMenuItem,
            this.hpssToolStripMenuItem,
            this.waveletsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1207, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.openToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(142, 26);
            this.openToolStripMenuItem1.Text = "&Open...";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.saveToolStripMenuItem.Text = "&Save as...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // playbackToolStripMenuItem
            // 
            this.playbackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.toolStripSeparator2,
            this.recordToolStripMenuItem,
            this.toolStripSeparator3,
            this.stopToolStripMenuItem});
            this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            this.playbackToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.playbackToolStripMenuItem.Text = "Playback";
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.playToolStripMenuItem.Text = "&Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.pauseToolStripMenuItem.Text = "Pa&use";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.recordToolStripMenuItem.Text = "&Record";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.recordToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.stopToolStripMenuItem.Text = "&Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.filtersToolStripMenuItem.Text = "Filters";
            this.filtersToolStripMenuItem.Click += new System.EventHandler(this.filtersToolStripMenuItem_Click);
            // 
            // pitchToolStripMenuItem
            // 
            this.pitchToolStripMenuItem.Name = "pitchToolStripMenuItem";
            this.pitchToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.pitchToolStripMenuItem.Text = "P&itch";
            this.pitchToolStripMenuItem.Click += new System.EventHandler(this.pitchToolStripMenuItem_Click);
            // 
            // mfccToolStripMenuItem
            // 
            this.mfccToolStripMenuItem.Name = "mfccToolStripMenuItem";
            this.mfccToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.mfccToolStripMenuItem.Text = "&Mfcc";
            this.mfccToolStripMenuItem.Click += new System.EventHandler(this.mfccToolStripMenuItem_Click);
            // 
            // lpcToolStripMenuItem
            // 
            this.lpcToolStripMenuItem.Name = "lpcToolStripMenuItem";
            this.lpcToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.lpcToolStripMenuItem.Text = "&Lpc";
            this.lpcToolStripMenuItem.Click += new System.EventHandler(this.lpcToolStripMenuItem_Click);
            // 
            // modulationSpectrumToolStripMenuItem
            // 
            this.modulationSpectrumToolStripMenuItem.Name = "modulationSpectrumToolStripMenuItem";
            this.modulationSpectrumToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.modulationSpectrumToolStripMenuItem.Text = "&Ams";
            this.modulationSpectrumToolStripMenuItem.Click += new System.EventHandler(this.modulationSpectrumToolStripMenuItem_Click);
            // 
            // effectsToolStripMenuItem
            // 
            this.effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            this.effectsToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.effectsToolStripMenuItem.Text = "&Effects";
            this.effectsToolStripMenuItem.Click += new System.EventHandler(this.effectsToolStripMenuItem_Click);
            // 
            // featuresToolStripMenuItem
            // 
            this.featuresToolStripMenuItem.Name = "featuresToolStripMenuItem";
            this.featuresToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.featuresToolStripMenuItem.Text = "Fea&tures";
            this.featuresToolStripMenuItem.Click += new System.EventHandler(this.featuresToolStripMenuItem_Click);
            // 
            // noiseToolStripMenuItem
            // 
            this.noiseToolStripMenuItem.Name = "noiseToolStripMenuItem";
            this.noiseToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.noiseToolStripMenuItem.Text = "&Noise";
            this.noiseToolStripMenuItem.Click += new System.EventHandler(this.noiseToolStripMenuItem_Click);
            // 
            // modulationToolStripMenuItem
            // 
            this.modulationToolStripMenuItem.Name = "modulationToolStripMenuItem";
            this.modulationToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.modulationToolStripMenuItem.Text = "Mo&dulation";
            this.modulationToolStripMenuItem.Click += new System.EventHandler(this.modulationToolStripMenuItem_Click);
            // 
            // onlineToolStripMenuItem
            // 
            this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
            this.onlineToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.onlineToolStripMenuItem.Text = "&Online";
            this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
            // 
            // adaptiveToolStripMenuItem
            // 
            this.adaptiveToolStripMenuItem.Name = "adaptiveToolStripMenuItem";
            this.adaptiveToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.adaptiveToolStripMenuItem.Text = "Adaptive";
            this.adaptiveToolStripMenuItem.Click += new System.EventHandler(this.adaptiveToolStripMenuItem_Click);
            // 
            // hpssToolStripMenuItem
            // 
            this.hpssToolStripMenuItem.Name = "hpssToolStripMenuItem";
            this.hpssToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.hpssToolStripMenuItem.Text = "&Hpss";
            this.hpssToolStripMenuItem.Click += new System.EventHandler(this.hpssToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Builder parameters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "samples";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 519);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Superimposed signal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(690, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Signal";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(976, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Spectrum";
            // 
            // stftButton
            // 
            this.stftButton.Location = new System.Drawing.Point(1135, 305);
            this.stftButton.Name = "stftButton";
            this.stftButton.Size = new System.Drawing.Size(62, 28);
            this.stftButton.TabIndex = 25;
            this.stftButton.Text = "More...";
            this.stftButton.UseVisualStyleBackColor = true;
            this.stftButton.Click += new System.EventHandler(this.stftButton_Click);
            // 
            // waveletsToolStripMenuItem
            // 
            this.waveletsToolStripMenuItem.Name = "waveletsToolStripMenuItem";
            this.waveletsToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.waveletsToolStripMenuItem.Text = "&Wavelets";
            this.waveletsToolStripMenuItem.Click += new System.EventHandler(this.waveletsToolStripMenuItem_Click);
            // 
            // SignalsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 735);
            this.Controls.Add(this.stftButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rightSliceTextBox);
            this.Controls.Add(this.leftSliceTextBox);
            this.Controls.Add(this.signalSliceButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.operationSamplesTextBox);
            this.Controls.Add(this.durationTextBox);
            this.Controls.Add(this.signalOperationButton);
            this.Controls.Add(this.generateSignalButton);
            this.Controls.Add(this.operationComboBox);
            this.Controls.Add(this.builderComboBox);
            this.Controls.Add(this.spectrumPanel);
            this.Controls.Add(this.superimposedSignalPanel);
            this.Controls.Add(this.generatedSignalPanel);
            this.Controls.Add(this.signalPanel);
            this.Controls.Add(this.builderParametersListBox);
            this.Controls.Add(this.filenameTextBox);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SignalsForm";
            this.Text = "Signals";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.ListBox builderParametersListBox;
        private SignalPlot signalPanel;
        private SignalPlot generatedSignalPanel;
        private SignalPlot superimposedSignalPanel;
        private LinePlot spectrumPanel;
        private System.Windows.Forms.ComboBox builderComboBox;
        private System.Windows.Forms.ComboBox operationComboBox;
        private System.Windows.Forms.Button generateSignalButton;
        private System.Windows.Forms.Button signalOperationButton;
        private System.Windows.Forms.TextBox durationTextBox;
        private System.Windows.Forms.TextBox operationSamplesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox leftSliceTextBox;
        private System.Windows.Forms.Button signalSliceButton;
        private System.Windows.Forms.TextBox rightSliceTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mfccToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lpcToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem modulationSpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem featuresToolStripMenuItem;
        private System.Windows.Forms.Button stftButton;
        private System.Windows.Forms.ToolStripMenuItem noiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adaptiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hpssToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveletsToolStripMenuItem;
    }
}

