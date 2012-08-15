//-----------------------------------------------------------------------
// <copyright file="ValidationManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Base class for validation manager.</summary>
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
    /// Base class for validation manager.
    /// </summary>
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "Validator.bmp")]
    [ToolboxItem(false)]
    public class ValidationManager : Control
    {
        /// <summary>
        /// Gets the text associated with the control.
        /// </summary>
        [Browsable(false)]
        public new String Text
        {
            get
            {
                return this.Name;
            }           
        }
    }
}
