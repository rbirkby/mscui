//-----------------------------------------------------------------------
// <copyright file="PatientBannerTemplateBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation.
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
// <date>03-Jan-2007</date>
// <summary>base class for templates classes used by patient banner</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// base class for templates classes used by patient banner
    /// </summary>
    internal abstract class PatientBannerTemplateBase : WebControl
    {
        #region Protected Methods
        /// <summary>
        /// instantiate a dummy panel to clear preceding floats
        /// </summary>
        /// <param name="parent">parent control to instantiate in</param>
        protected static void InstantiateClearingPanel(Control parent)
        {
            Panel panel = new Panel();
            panel.Style["clear"] = "both";
            panel.Style["float"] = "none";
            parent.Controls.Add(panel);
        }
        #endregion
    }
}
