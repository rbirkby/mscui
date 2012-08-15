//-----------------------------------------------------------------------
// <copyright file="CustomValidator.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-June-2007</date>
// <summary>A custom validator.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SampleWinform
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.WinForms;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// Custom validator.
    /// </summary>
    public class CustomValidator : ValidationManager, IValidationManager
    {
        #region Member Vars
        /// <summary>
        /// Custom Error Message.
        /// </summary>
        /// <remarks>
        /// Saves the value of property ErrorMessage.
        /// </remarks>
        private String customErrorMessage;

        /// <summary>
        /// Label to display error.
        /// </summary>
        private Label description = new Label();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new instance of custom validator.
        /// </summary>
        public CustomValidator()
        {
            this.description.AutoSize = true;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Custom error message.
        /// </summary>
        public String CustomErrorMessage
        {
            get
            {
                return this.customErrorMessage;
            }

            set
            {
                if (this.customErrorMessage != value)
                {
                    this.customErrorMessage = value;
                }
            }
        }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Sets the error message for the specified control.
        /// </summary>
        /// <param name="control"> Control to set error message. </param>
        /// <param name="errorMessage"> Error Message. </param>
        public void SetError(Control control, string errorMessage)
        {
            if (control != null)
            {
                this.description.Text = String.IsNullOrEmpty(this.CustomErrorMessage) ? errorMessage : this.CustomErrorMessage;
                this.description.Parent = control.Parent;
                this.description.ForeColor = Color.Red;
                this.description.Font = new Font("Verdana", (float)8.25);
                this.description.Top = control.Top + control.Height + 2;
                this.description.Left = control.Left;
                this.description.Visible = true;   
            }           
        }

        /// <summary>
        /// Clears the error for the specified control.
        /// </summary>
        /// <param name="control"> Control to clear the error message. </param>
        public void ClearError(Control control)
        {
            if (control != null)
            {
                this.description.Text = String.Empty;
                this.description.Visible = false;
            }  
        }
        #endregion       
    }
}
