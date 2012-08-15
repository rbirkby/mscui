//-----------------------------------------------------------------------
// <copyright file="MedicationNameLabelExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>31-Jan-2007</date>
// <summary>extender to render MedicationNameLabel
// focus</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MedicationNameLabelControl.MedicationNameLabelBehavior.js", "text/javascript")]

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
    using System.Web.Script.Serialization;

    /// <summary>
    /// extender to show tooltip for associated element when it receives the input
    /// focus
    /// </summary>
    [ToolboxItem(false)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]    
    [TargetControlType(typeof(MedicationNameLabel))]
    [ClientScriptResource("NhsCui.Toolkit.Web.MedicationNameLabelBehavior", "NhsCui.Toolkit.Web.MedicationNameLabelControl.MedicationNameLabelBehavior.js")]
    internal class MedicationNameLabelExtender : ExtenderControlBase
    {        
        #region Properties
        /// <summary>
        /// Place the critical and indicator graphics. If true, space for the image will be provided even if the image is not supplied.
        /// </summary>
        [DefaultValue(""), ExtenderControlProperty]
        [ClientPropertyName("showGraphics")]
        public bool ShowGraphics
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowGraphics", true);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowGraphics", value);
            }
        }

        /// <summary>
        /// Javascript function to invoke OnClick event on client
        /// </summary>
        /// <remarks>
        /// Note: OnClientClick isn't a behavior property, it's a behavior event.
        /// It's specified as a property here so that ExtenderBase wrte it
        /// to the xml-script so that ASP.NET AJAX will hook up the event
        /// properly.
        /// </remarks>
        [ExtenderControlProperty()]
        [DefaultValue("")]
        [ClientPropertyName("click")]
        public string OnClientClick
        {
            get
            {
                return GetPropertyValue<string>("OnClientClick", string.Empty);
            }

            set
            {
                SetPropertyValue<string>("OnClientClick", value);
            }
        }
        #endregion
    }
}
