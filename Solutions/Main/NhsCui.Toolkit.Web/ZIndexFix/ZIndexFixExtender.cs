//-----------------------------------------------------------------------
// <copyright file="ZIndexFixExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>Fix for IE (pre version 7) windowed control z-index issue</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.ZIndexFixExtender.Javascript, "text/javascript")]

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using AjaxControlToolkit;

    /// <summary>
    /// Fix for internet explorer (pre version 7) windowed control z-index issue
    /// see http://support.microsoft.com/kb/177378 for details on how internet
    /// explorer handle z-indexes
    /// </summary>
    [Designer(typeof(ZIndexFixDesigner))]
    [ClientScriptResource("NhsCui.Toolkit.Web.ZIndexFixBehavior", ZIndexFixExtender.Javascript)]
    [TargetControlType(typeof(Control))]
    internal class ZIndexFixExtender : ExtenderControlBase
    {
        #region Const Values
        /// <summary>
        /// name of extender's javascript resource
        /// </summary>
        public const string Javascript = "NhsCui.Toolkit.Web.ZIndexFix.ZIndexFixBehavior.js";
        #endregion

        #region Public Properties
        /// <summary>
        /// Test request to see if the fix needs applying
        /// </summary>
        /// <param name="request">request to test</param>
        /// <returns>true if the fix should be applied</returns>
        public static bool IsRequired(HttpRequest request)
        {
            // this fix is for IE only. It isn't needed for IE7 and
            // will only work for IE5.5+
            HttpBrowserCapabilities browser = request.Browser;
            return browser.IsBrowser("IE") && (browser.MajorVersion == 6 ||
                browser.MajorVersion == 5 && browser.MinorVersion >= 5.0);
        }
        #endregion
    }
}
