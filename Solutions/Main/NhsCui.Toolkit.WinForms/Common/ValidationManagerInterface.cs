//-----------------------------------------------------------------------
// <copyright file="ValidationManagerInterface.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The file contains interfaces for WinForms.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Specifies methods for the validation manager.
    /// </summary>
    public interface IValidationManager
    {
        #region Public Methods
        /// <summary>
        /// Sets the error message for the specified control.
        /// </summary>
        /// <param name="control"> Control to set error message. </param>
        /// <param name="errorMessage"> Error Message. </param>
        void SetError(Control control, string errorMessage);

        /// <summary>
        /// Clears the error for the specified control.
        /// </summary>
        /// <param name="control"> Control to clear the error message. </param>
        void ClearError(Control control);
        #endregion       
    }
}
