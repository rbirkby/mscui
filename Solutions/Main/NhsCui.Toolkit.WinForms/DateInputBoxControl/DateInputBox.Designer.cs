//-----------------------------------------------------------------------
// <copyright file="DateInputBox.Designer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved.
//
// CERTAIN PARTS OF THIS WORK CONTAIN SOFTWARE CODE THAT IS LICENSED 
// FOR USE UNDER THE MICROSOFT PUBLIC LICENSE. DISTRIBUTION, IN SOURCE CODE 
// OR OBJECT CODE FORM, OF THOSE PARTS MUST COMPLY WITH THE TERMS OF THE 
// PUBLIC LICENSE. SEE http://www.microsoft.com/opensource/licenses.mspx 
// FOR DETAILS.  
// IF YOU BRING A PATENT CLAIM AGAINST ANY CONTRIBUTOR OVER PATENTS THAT 
// YOU CLAIM ARE INFRINGED BY THE PUBLIC LICENSE SOFTWARE, YOUR PATENT 
// LICENSE FROM SUCH CONTRIBUTOR TO THE SOFTWARE ENDS AUTOMATICALLY.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>3-May-2007</date>
// <summary>The designer for the control used to enter a date.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    /// <summary>
    /// The control used to enter a date. 
    /// </summary>
    public partial class DateInputBox
    {
        /// <summary>
        /// textbox to display date.
        /// </summary>
        private NhsTextBox txtInput;

        /// <summary>
        /// Tooltip for the control.
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// Picture box for calendar image
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBox1;

        /// <summary>
        /// Panel for the textbox and calendar button.
        /// </summary>
        private NhsTextBox mainPanel;

        /// <summary>
        /// button to pop up calendar.
        /// </summary>
        private System.Windows.Forms.Button btnCalendar;

        /// <summary>
        /// Checkbox for approximate input.
        /// </summary>
        private System.Windows.Forms.CheckBox chkApprox;

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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges", Justification = "Timer will tick only once and then it will be disabled.")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.chkApprox = new System.Windows.Forms.CheckBox();
            this.mainPanel = new NhsTextBox();
            this.txtInput = new NhsCui.Toolkit.WinForms.NhsTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCalendar
            // 
            this.btnCalendar.BackColor = System.Drawing.SystemColors.Control;
            this.btnCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCalendar.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendar.Location = new System.Drawing.Point(90, 0);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(27, 22);
            this.btnCalendar.TabIndex = 2;
            this.btnCalendar.UseVisualStyleBackColor = false;
            this.btnCalendar.Enter += new System.EventHandler(this.BtnCalendar_Enter);
            this.btnCalendar.Click += new System.EventHandler(this.Button1_Click);
            this.btnCalendar.Leave += new System.EventHandler(this.BtnCalendar_Leave);
            // 
            // chkApprox
            // 
            this.chkApprox.AutoSize = true;
            this.chkApprox.BackColor = System.Drawing.SystemColors.Control;
            this.chkApprox.Location = new System.Drawing.Point(162, 2);
            this.chkApprox.Name = "chkApprox";
            this.chkApprox.Size = new System.Drawing.Size(15, 14);
            this.chkApprox.TabIndex = 3;
            this.chkApprox.UseVisualStyleBackColor = false;
            this.chkApprox.Visible = false;
            this.chkApprox.CheckedChanged += new System.EventHandler(this.ChkApprox_CheckedChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainPanel.Controls.Add(this.txtInput);
            this.mainPanel.Controls.Add(this.btnCalendar);
            this.mainPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Multiline = true;
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(125, 24);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.TabStop = false;
            // 
            // txtInput
            // 
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(94, 22);
            this.txtInput.TabIndex = 1;
            this.txtInput.WordWrap = false;
            this.txtInput.Enter += new System.EventHandler(this.TxtInput_Enter);
            this.txtInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TxtInput_MouseClick);
            this.txtInput.FieldDoubleClicked += new System.EventHandler(this.TxtInput_FieldDoubleClicked);
            this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtInput_KeyUp);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInput_KeyPress);
            this.txtInput.Validating += new System.ComponentModel.CancelEventHandler(this.TxtInput_Validating);
            this.txtInput.TextChanged += new System.EventHandler(this.TxtInput_TextChanged);
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtInput_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::NhsCui.Toolkit.WinForms.Properties.Resources.calendar;
            this.pictureBox1.Location = new System.Drawing.Point(102, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // DateInputBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.chkApprox);
            this.Name = "DateInputBox";
            this.Size = new System.Drawing.Size(130, 22);
            this.Leave += new System.EventHandler(this.DateInputBox_Leave);
            this.FontChanged += new System.EventHandler(this.DateInputBox_FontChanged);
            this.Resize += new System.EventHandler(this.DateInputBox_Resize);
            this.ForeColorChanged += new System.EventHandler(this.DateInputBox_ForeColorChanged);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
