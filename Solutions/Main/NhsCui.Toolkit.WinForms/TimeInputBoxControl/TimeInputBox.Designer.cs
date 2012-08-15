//-----------------------------------------------------------------------
// <copyright file="TimeInputBox.Designer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Apr-2007</date>
// <summary>The designer for the control used to enter a time.</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

namespace NhsCui.Toolkit.WinForms
{
    /// <summary>
    /// The control used to enter a time. 
    /// </summary>
    public partial class TimeInputBox
    {
        /// <summary>
        /// Gets the input from the end user.
        /// </summary>
        private NhsTextBox txtInput;

        /// <summary>
        /// Up spin button
        /// </summary>
        private System.Windows.Forms.Button btnDown;

        /// <summary>
        /// Down spin button
        /// </summary>
        private System.Windows.Forms.Button btnUp;

        /// <summary>
        /// Checkbox for approximate input.
        /// </summary>
        private System.Windows.Forms.CheckBox chkApprox;

        /// <summary>
        /// Tooltip for the control.
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;
        
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
            this.chkApprox = new System.Windows.Forms.CheckBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.txtInput = new NhsCui.Toolkit.WinForms.NhsTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chkApprox
            // 
            this.chkApprox.AutoSize = true;
            this.chkApprox.Location = new System.Drawing.Point(162, 2);
            this.chkApprox.Name = "chkApprox";
            this.chkApprox.Size = new System.Drawing.Size(15, 14);
            this.chkApprox.TabIndex = 2;
            this.chkApprox.UseVisualStyleBackColor = true;
            this.chkApprox.Visible = false;
            this.chkApprox.CheckedChanged += new System.EventHandler(this.ChkApprox_CheckedChanged);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::NhsCui.Toolkit.WinForms.Properties.Resources.arrow_top;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(88, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(10, 9);
            this.btnUp.TabIndex = 4;
            this.btnUp.TabStop = false;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::NhsCui.Toolkit.WinForms.Properties.Resources.arrow_down;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(88, 10);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(10, 9);
            this.btnDown.TabIndex = 3;
            this.btnDown.TabStop = false;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.SystemColors.Window;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(90, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.WordWrap = false;
            this.txtInput.Enter += new System.EventHandler(this.TextBox1_Enter);
            this.txtInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBox1_MouseClick);
            this.txtInput.FieldDoubleClicked += new System.EventHandler(this.TxtInput_FieldDoubleClicked);
            this.txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtInput_KeyUp);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            this.txtInput.Validating += new System.ComponentModel.CancelEventHandler(this.TxtInput_Validating);
            this.txtInput.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // TimeInputBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.chkApprox);
            this.Controls.Add(this.txtInput);
            this.Name = "TimeInputBox";
            this.Size = new System.Drawing.Size(110, 20);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TimeInputBox_MouseClick);
            this.Leave += new System.EventHandler(this.TimeInputBox_Leave);
            this.FontChanged += new System.EventHandler(this.TimeInputBox_FontChanged);
            this.Resize += new System.EventHandler(this.UserControl1_Resize);
            this.ForeColorChanged += new System.EventHandler(this.TimeInputBox_ForeColorChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TimeInputBox_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion                            
    }
}
