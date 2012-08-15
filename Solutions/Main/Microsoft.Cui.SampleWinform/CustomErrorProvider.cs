//-----------------------------------------------------------------------
// <copyright file="CustomErrorProvider.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-June-2007</date>
// <summary>A custom error provider.</summary>
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
    /// Custom error provider class.
    /// </summary>
    public class CustomErrorProvider : NhsErrorProvider
    {
        /// <summary>
        /// Overrides. Sets error to the specified control.
        /// </summary>
        /// <param name="control"> Control having error. </param>
        /// <param name="value">Error description. </param>
        public override void SetError(Control control, string value)
        {
            if (control != null)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    control.ForeColor = Color.Red;
                }
                else
                {
                    control.ForeColor = SystemColors.ControlText;
                }   
            }          
        }
    }
}
