//-----------------------------------------------------------------------
// <copyright file="FocusToolTipExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>extender to show tooltip for associated element when it receives the input
// focus</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource(NhsCui.Toolkit.Web.FocusToolTipExtender.Javascript, "text/javascript")]

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Diagnostics.CodeAnalysis;
    
    using AjaxControlToolkit;

    /// <summary>
    /// extender to show tooltip for associated element when it receives the input
    /// focus
    /// </summary>
    [Designer(typeof(FocusToolTipDesigner))]
    [ClientScriptResource("NhsCui.Toolkit.Web.FocusToolTipBehavior", FocusToolTipExtender.Javascript)]
    [TargetControlType(typeof(Control))]
    internal class FocusToolTipExtender : ExtenderControlBase
    {
        #region Const Values
        /// <summary>
        /// name of extender's javascript resource
        /// </summary>
        public const string Javascript = "NhsCui.Toolkit.Web.FocusToolTip.FocusToolTipBehavior.js";
        #endregion

        #region Public Properties
        /// <summary>
        /// Css class to use for tooltip
        /// </summary>
        [DefaultValue(""), ExtenderControlProperty]
        public string CssClass
        {
            get
            {
                return this.GetPropertyValue("CssClass", string.Empty); 
            }

            set
            {
                this.SetPropertyValue("CssClass", value);
            }
        }

        /// <summary>
        /// The ID of the control whose title (or alt) attribute supplies the tooltip text
        /// can be left unset if same as target control
        /// </summary>
        [IDReferenceProperty(typeof(Control)), DefaultValue(""), ExtenderControlProperty]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        public string ToolTipSourceControlID
        {
            get
            {
                return this.GetPropertyValue("ToolTipSourceControlID", string.Empty); 
            }

            set
            {
                this.SetPropertyValue("ToolTipSourceControlID", value);
            }
        }
        #endregion
    }
}
