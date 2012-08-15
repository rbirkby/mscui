//-----------------------------------------------------------------------
// <copyright file="ErrorProviderValidationManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-Jun-2007</date>
// <summary>Validation manager for error handling.</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.WinForms
{   
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.ComponentModel;

    /// <summary>
    /// Validation manager for error handling for ErrorProvider controls and sub-classes.
    /// </summary>
    [DefaultProperty("ErrorProvider")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "Validator.bmp")]
    [ToolboxItem(true)]
    public class ErrorProviderValidationManager : ValidationManager, IValidationManager
    {
        #region Member Vars

        /// <summary>
        /// Error provider.
        /// </summary>
        /// <remarks>
        /// Saves the value of property ErrorProvider.
        /// </remarks>
        private ErrorProvider errorProvider;
        #endregion

        #region Public Properties
        /// <summary>
        /// Error provider for the validations.
        /// </summary>
        public ErrorProvider ErrorProvider
        {
            get
            {
                return this.errorProvider;
            }

            set
            {
                if (this.errorProvider != value)
                {
                    this.errorProvider = value;
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
            if (this.errorProvider != null)
            {
                NhsErrorProvider tmp = this.errorProvider as NhsErrorProvider;
                if (tmp != null)
                {
                    tmp.SetError(control, errorMessage);
                }
                else
                {
                    this.errorProvider.SetError(control, errorMessage);
                }
            }
        }

        /// <summary>
        /// Clears the error for the specified control.
        /// </summary>
        /// <param name="control"> Control to clear the error message. </param>
        public void ClearError(Control control)
        {
            if (this.errorProvider != null)
            {
                NhsErrorProvider tmp = this.errorProvider as NhsErrorProvider;
                if (tmp != null)
                {
                    tmp.SetError(control, String.Empty);
                }
                else
                {
                    this.errorProvider.SetError(control, String.Empty);
                }
            }
        }
        #endregion       
    }
}
