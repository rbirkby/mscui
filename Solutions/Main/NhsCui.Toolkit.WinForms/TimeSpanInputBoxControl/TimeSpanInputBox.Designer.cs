//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBox.Designer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>9-Jul-2007</date>
// <summary>The designer for the control used to enter a timespan.</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.WinForms
{
    /// <summary>
    /// The designer class for the control used to enter a timespan. 
    /// </summary>
    public partial class TimeSpanInputBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Input text box
        /// </summary>
        private NhsTextBox txtInput;

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
        private void InitializeComponent()
        {
            this.txtInput = new NhsCui.Toolkit.WinForms.NhsTextBox();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(120, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.WordWrap = false;
            this.txtInput.Enter += new System.EventHandler(this.TxtInput_Enter);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInput_KeyPress);
            this.txtInput.Validating += new System.ComponentModel.CancelEventHandler(this.TxtInput_Validating);
            // 
            // TimeSpanInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtInput);
            this.Name = "TimeSpanInputBox";
            this.Size = new System.Drawing.Size(120, 20);
            this.Leave += new System.EventHandler(this.TimeSpanInputBox_Leave);
            this.FontChanged += new System.EventHandler(this.TimeSpanInputBox_FontChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
