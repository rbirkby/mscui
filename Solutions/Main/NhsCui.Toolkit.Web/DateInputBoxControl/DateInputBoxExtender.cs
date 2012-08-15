//-----------------------------------------------------------------------
// <copyright file="DateInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>DateInputBox Extender, class to provide server-side configuration 
// of the DateInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBox.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.Calendar.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.arrow-left.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.arrow-right.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.calendar.png", "image/png")]
[assembly: System.Web.UI.ScriptResource("NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBox.js", "NhsCui.Toolkit.Web.DateInputBoxControl.Resources.resources", "NhsCui.Toolkit.Web.DateInputBoxResources")]

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

    /// <summary>
    /// DateInputBox Extender, class to provide server-side configuration of 
    /// the DateInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(PopupExtender), 2)]
    [RequiredScript(typeof(TimerScript), 3)]
    [RequiredScript(typeof(NhsDateScripts), 4)]
    [RequiredScript(typeof(PickerExtender), 6)]
    [RequiredScript(typeof(TextBoxWatermarkExtender), 7)]
    [RequiredScript("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js")]
    [ClientCssResource("NhsCui.Toolkit.Web.DateInputBoxControl.Calendar.css")]
    [ClientScriptResource("NhsCui.Toolkit.Web.DateInputBox", "NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBox.js")]
    internal class DateInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private DateInputClientState state;

        /// <summary>
        /// Position for the calendar.
        /// </summary>
        private PositioningMode calendarPosition = PositioningMode.BottomLeft;

        /// <summary>
        /// NhsDate value
        /// </summary>
        private NhsDate value;

        /// <summary>‍
        /// Default constructor
        /// </summary>
        public DateInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new DateInputClientState();
            this.value = new NhsDate();

            this.ClientStateValuesLoaded += new EventHandler(this.DateInputBoxExtender_ClientStateValuesLoaded);
        }

        /// <summary>
        /// Specifies the functionality exposed by the DateInputBox (defaults to Simple). 
        /// Setting Functionality to Simple sets Value.DateType to Exact.
        /// </summary>
        [DefaultValue(1)]
        [Description("Specifies the functionality exposed by the DateInputBox (defaults to Complex). Setting Functionality to Simple sets Value.DateType to Exact.")]
        public int Functionality
        {
            get
            {
                return this.state.Functionality;
            }

            set
            {
                this.state.Functionality = value;
            }
        }

        /// <summary>
        /// Specifies whether to display a checkbox for the approximation"
        /// </summary>
        [DefaultValue(false)]
        [Description("Specifies whether to display a checkbox for the approximation")]
        public bool AllowApproximate
        {
            get
            {
                return this.state.AllowApproximate;
            }

            set
            {
                this.state.AllowApproximate = value;
            }
        }

        /// <summary>
        /// Whether the date is approximate
        /// </summary>
        [DefaultValue(false)]
        public bool DateIsApproximate
        {
            get
            {
                return this.state.DateIsApproximate;
            }

            set
            {
                this.state.DateIsApproximate = value;
            }
        }

        /// <summary>
        /// Position to display the calendar
        /// </summary>
        public PositioningMode CalendarPosition
        {
            get
            {
                return this.calendarPosition;
            }

            set
            {
                this.state.CalendarPosition = value;
                this.calendarPosition = value;
            }
        }

        /// <summary>
        /// The date entered in the text box
        /// </summary>
        [Description("The date entered in the text box")]
        public NhsDate Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Specifies whether to substitute Today, 
        /// Tomorrow or Yesterday for actual dates (defaults to false)
        /// </summary>
        [Description("Specifies whether to substitute Today, Tomorrow or Yesterday for actual dates (defaults to false)")]
        public bool DisplayDateAsText
        {
            get
            {
                return this.state.DisplayDateAsText;
            }

            set
            {
                this.state.DisplayDateAsText = value;
            }
        }

        /// <summary>
        /// Specifies whether to display the day of week (defaults to false)
        /// </summary>
        [Description("Specifies whether to display the day of week (defaults to false)")]
        public bool DisplayDayOfWeek
        {
            get
            {
                return this.state.DisplayDayOfWeek;
            }

            set
            {
                this.state.DisplayDayOfWeek = value;
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
            get { return GetPropertyValue("CssClass", string.Empty); }
            set { SetPropertyValue("CssClass", value); }
        }

        /// <summary>
        /// Gets the calendar Image Url
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("calendarImage")]
        public string CalendarImageUrl
        {
            get
            {
                return this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "NhsCui.Toolkit.Web.DateInputBoxControl.calendar.png");
            }
        }

        /// <summary>
        /// A list of localized strings that identify different types of null times (defaults to an empty list).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        [Category("Behavior")]
        [ExtenderControlProperty(true)]
        [Description("A list of localized strings that identify different types of null times (defaults to an empty list).")]
        [TypeConverter(typeof(StringArrayConverter))]
        [ClientPropertyName("nullStrings")]
        public string[] NullStrings
        {
            get { return GetPropertyValue<string[]>("NullStrings", null); }
            set { SetPropertyValue<string[]>("NullStrings", value); }
        }

        /// <summary>
        /// Watermark text to appear when the control has no value
        /// </summary>
        public string WatermarkText
        {
            get
            {
                return this.state.WatermarkText;
            }

            set
            {
                this.state.WatermarkText = value;
            }
        }
        
        /// <summary>
        /// Checkbox css class 
        /// </summary>
        public string CheckBoxCssClass
        {
            get
            {
                return this.state.CheckBoxCssClass;
            }

            set
            {
                this.state.CheckBoxCssClass = value;
            }
        }

        /// <summary>
        /// Watermark css class 
        /// </summary>
        public string WatermarkCssClass
        {
            get
            {
                return this.state.WatermarkCssClass;
            }

            set
            {
                this.state.WatermarkCssClass = value;
            }
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            this.state.Value = this.Value;
            this.state.CalendarPosition = this.calendarPosition;

            // Register the NhsCuiValidation.js script with ASP.NET Ajax
            ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js", "NhsCui.Toolkit.Web"));

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
        private void DateInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new NhsDateJavascriptConverter() });

                this.state = jss.Deserialize<DateInputClientState>(ClientState);

                this.value = this.state.Value;
                this.calendarPosition = this.state.CalendarPosition;
            }
        }
    }
}
