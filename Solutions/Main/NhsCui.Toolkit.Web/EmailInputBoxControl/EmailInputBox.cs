//-----------------------------------------------------------------------
// <copyright file="EmailInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>The control used to enter an Email. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.ComponentModel;

    /// <summary>
    /// The control used to enter an Email. 
    /// </summary>
    [ToolboxItem(false)]
    public class EmailInputBox : CompositeControl
    {
        /// <summary>
        /// Extender control to configure client-side AJAX functionality
        /// </summary>
        private EmailInputBoxExtender emailInputBoxExtender;

        /// <summary>
        /// The basic textbox used as the basis for the control.
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// Occurs when the value of the input box changes between posts to the server. 
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.textBox = new TextBox();
            this.textBox.ID = ID + "_TextBox";
            this.textBox.AutoCompleteType = AutoCompleteType.Disabled;
            this.textBox.TextChanged += new EventHandler(this.TextBoxTextChanged);
            Controls.Add(this.textBox);

            // Create the extender
            this.emailInputBoxExtender = new EmailInputBoxExtender();
            this.emailInputBoxExtender.ID = this.ID + "_EmailInputBoxExtender";
            this.emailInputBoxExtender.TargetControlID = this.textBox.ID;
            this.Controls.Add(this.emailInputBoxExtender);
        }

        /// <summary>
        /// Raise the ValueChanged event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        /// <summary>
        /// Handle text changed event of our underlying text box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }
    }
}
