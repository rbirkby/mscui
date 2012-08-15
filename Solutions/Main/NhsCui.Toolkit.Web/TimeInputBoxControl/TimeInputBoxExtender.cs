//-----------------------------------------------------------------------
// <copyright file="TimeInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Implements TimeInputBox extender</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

#region Assembly Resource Attribute
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.TimeInputBoxControl.TimeInputBox.js", "text/javascript")]
[assembly: System.Web.UI.ScriptResource("NhsCui.Toolkit.Web.TimeInputBoxControl.TimeInputBox.js", "NhsCui.Toolkit.Web.TimeInputBoxControl.Resources.resources", "NhsCui.Toolkit.Web.TimeInputBoxResources")]
#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Web.UI.WebControls;
    using System.Web.UI;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Script.Serialization;

    using AjaxControlToolkit;

    using NhsCui.Toolkit.Web;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>‍</summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts))]
    [RequiredScript(typeof(TimerScript))]
    [RequiredScript(typeof(NhsTimeScripts))]
    [RequiredScript("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js")]
    [ClientScriptResource("NhsCui.Toolkit.Web.TimeInputBox", "NhsCui.Toolkit.Web.TimeInputBoxControl.TimeInputBox.js")]
    internal class TimeInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private TimeInputClientState state;

        /// <summary>
        /// hold time as custom type
        /// </summary>
        private NhsTime value;

        /// <summary>
        /// Default constructor
        /// ‍</summary>
        public TimeInputBoxExtender()
        {
            this.EnableClientState = true;
            this.NullStrings = new string[0];
            this.state = new TimeInputClientState();
            this.ClientStateValuesLoaded += new EventHandler(this.ExtenderClientStateLoaded);
        }

        /// <summary>‍</summary>
        [DefaultValue(0)]
        [Description("Specifies the functionality exposed by the TimeInputBox (defaults to Complex). Setting Functionality to Simple sets Value.TimeType to Exact.")]
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
        /// Specifies whether to display a checkbox for the approximation
        /// ‍</summary>
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
        /// Specifies whether seconds should be displayed
        /// ‍</summary>
        [DefaultValue(false)]
        [Description("Specifies whether seconds should be displayed")]
        public bool DisplaySeconds
        {
            get
            {
                return this.state.DisplaySeconds;
            }

            set
            {
                this.state.DisplaySeconds = value;
            }
        }

        /// <summary>
        /// Specifies whether hours should be displayed as 12 hour or 24 hour
        /// ‍</summary>
        [DefaultValue(false)]
        [Description("Specifies whether hours should be displayed as 12 hour or 24 hour")]
        public bool Display12Hour
        {
            get
            {
                return this.state.Display12Hour;
            }

            set
            {
                this.state.Display12Hour = value;
            }
        }

        /// <summary>
        /// Specifies whether the AM/PM suffix should be included
        /// ‍</summary>
        [DefaultValue(false)]
        [Description("Specifies whether the AM/PM suffix should be included")]
        public bool DisplayAMPM
        {
            get
            {
                return this.state.DisplayAMPM;
            }

            set
            {
                this.state.DisplayAMPM = value;
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

        /// <summary>‍</summary>
        [Description("The time entered in the text box")]
        public NhsTime Value
        {
            get 
            {
                if (this.value == null)
                {
                    this.value = new NhsTime();
                }

                return this.value; 
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>‍</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        [Category("Behavior")]
        [ExtenderControlProperty]
        [Description("A list of localized strings that identify different types of null times (defaults to an empty list).")]
        [TypeConverter(typeof(StringArrayConverter))]
        [ClientPropertyName("nullStrings")]
        public string[] NullStrings
        {
            get
            { 
                return this.GetPropertyValue<string[]>("NullStrings", null); 
            }

            set
            {
                this.SetPropertyValue<string[]>("NullStrings", value); 
            }
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            this.state.Value = this.Value;

            // Register the NhsCuiValidation.js script with ASP.NET Ajax
            ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js", "NhsCui.Toolkit.Web"));

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new NhsTimeJavascriptConverter() });

            this.ClientState = jss.Serialize(this.state);
            
            base.OnPreRender(e);
        }

        /// <summary>‍
        /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void ExtenderClientStateLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new NhsTimeJavascriptConverter() });

                this.state = jss.Deserialize<TimeInputClientState>(ClientState);
                
                this.value = this.state.Value;
            }
        }
    }
}
