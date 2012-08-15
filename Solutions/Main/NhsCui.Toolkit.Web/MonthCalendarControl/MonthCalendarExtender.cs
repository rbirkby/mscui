//-----------------------------------------------------------------------
// <copyright file="MonthCalendarExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>26-Oct-2007</date>
// <summary>MonthCalendar Extender, class to provide server-side configuration 
// of the MonthCalendar's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MonthCalendarControl.MonthCalendar.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MonthCalendarControl.MonthCalendar.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.ScriptResource("NhsCui.Toolkit.Web.MonthCalendarControl.MonthCalendar.js", "NhsCui.Toolkit.Web.MonthCalendarControl.Resources.resources", "NhsCui.Toolkit.Web.MonthCalendarResources")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using System.Web;
    using System.Web.UI;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    using NhsCui.Toolkit.Web;
    using NhsCui.Toolkit.DateAndTime;

    using AjaxControlToolkit;
    using System.Drawing;

    /// <summary>
    /// MonthCalendar Extender, class to provide server-side configuration of 
    /// the MonthCalendar's AJAX functionality
    /// </summary>
    [ToolboxData("<{0}:MonthCalendarExtender runat=\"server\"></{0}:MonthCalendarExtender>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateInputBox.bmp")]
    [TargetControlType(typeof(Panel))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(NhsDateScripts), 4)]
    [ClientCssResource("NhsCui.Toolkit.Web.MonthCalendarControl.MonthCalendar.css")]
    [ClientScriptResource("NhsCui.Toolkit.Web.MonthCalendar", "NhsCui.Toolkit.Web.MonthCalendarControl.MonthCalendar.js")]
    public class MonthCalendarExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private MonthCalendarClientState state;

        /// <summary>
        /// NhsDate value
        /// </summary>
        private NhsDate value;

        /// <summary>‍
        /// Default constructor
        /// </summary>
        public MonthCalendarExtender()
        {
            this.EnableClientState = true;
            this.state = new MonthCalendarClientState();
            this.value = new NhsDate();
            this.value.DateType = DateType.Exact;

            this.ClientStateValuesLoaded += new EventHandler(this.MonthCalendarExtender_ClientStateValuesLoaded);
        }

        /// <summary>
        /// The date selected in the control
        /// </summary>
        [Description("The date selected in the control")]
        public NhsDate Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value != null)
                {
                    if (value.DateType != DateType.Exact)
                    {
                        throw new ArgumentException(MonthCalendarControl.Resources.DateTypeNotExact);
                    }

                    this.value = value;
                }
            }
        }

        /// <summary>
        /// Get and Sets the CSS class
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("cssClass")]
        public virtual string CssClass
        {
            get
            {
                return GetPropertyValue("CssClass", string.Empty);
            }

            set
            {
                SetPropertyValue("CssClass", value);
            }
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            this.state.Value = this.Value;

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new NhsDateJavascriptConverter() });

            this.ClientState = jss.Serialize(this.state);

            base.OnPreRender(e);
        }

        /// <summary>‍
        /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void MonthCalendarExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new NhsDateJavascriptConverter() });

                this.state = jss.Deserialize<MonthCalendarClientState>(ClientState);

                this.value = this.state.Value;
            }
        }
    }
}
