//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-May-2007</date>
// <summary>TimeSpanInputBox Extender, class to provide server-side configuration 
// of the TimeSpanInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.TimeSpanInputBoxControl.TimeSpanInputBox.js", "text/javascript")]

// May need some resources...
// [assembly: System.Web.UI.ScriptResource("NhsCui.Toolkit.Web.TimeSpanInputBoxControl.TimeSpanInputBox.js", "NhsCui.Toolkit.Web.TimeSpanInputBoxControl.Resources.resources", "NhsCui.Toolkit.Web.TimeSpanInputBoxResources")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing.Design;
    using System.Web.UI;
    using AjaxControlToolkit;
    using NhsCui.Toolkit.DateAndTime;
    using System.ComponentModel;
    using System.Web.UI.WebControls;
    using System.Web.Script.Serialization;
    using System.Globalization;

    /// <summary>
    /// TimeSpanInputBox Extender, class to provide server-side configuration of 
    /// the TimeSpanInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(NhsDateScripts), 2)]
    [RequiredScript(typeof(NhsTimeScripts), 3)]
    [RequiredScript(typeof(NhsTimeSpanScripts), 4)]
    [RequiredScript("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js")]
    [ClientScriptResource("NhsCui.Toolkit.Web.TimeSpanInputBox", "NhsCui.Toolkit.Web.TimeSpanInputBoxControl.TimeSpanInputBox.js")]
    internal class TimeSpanInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private TimeSpanInputClientState state;

        /// <summary>NhsTimeSpan value as string</summary>
        private string text = "";

        /// <summary>
        /// NhsDate value
        /// </summary>
        private NhsTimeSpan value;

        /// <summary>
        /// Specifies whether long or short units are displayed.
        /// </summary>
        private TimeSpanUnitLength unitLength = TimeSpanUnitLength.Short;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeSpanInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new TimeSpanInputClientState();
            this.value = new NhsTimeSpan();

            this.ClientStateValuesLoaded += new EventHandler(this.TimeSpanInputBoxExtender_ClientStateValuesLoaded);
        }

        /// <summary>NhsTimeSpan from as string</summary>
        public DateTime From
        {
            get
            {
                return this.Value.From;
            }

            set
            {
                this.state.From = value.ToString("yyyy-mmm-dd", CultureInfo.InvariantCulture);
                this.Value.From = value;
            }
        }

        /// <summary>NhsTimeSpan granularity as string</summary>
        public TimeSpanUnit Granularity
        {
            get
            {
                return this.Value.Granularity;
            }

            set
            {
                this.state.Granularity = (int)value;
                this.Value.Granularity = value;
            }
        }

        /// <summary>NhsTimeSpan isAge as string</summary>
        public bool IsAge
        {
            get
            {
                return this.Value.IsAge;
            }

            set
            {
                this.state.IsAge = value;
                this.Value.IsAge = value;
            }
        }

        /// <summary>NhsTimeSpan text as string</summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.state.Text = value;
                this.text = value;
            }
        }

        /// <summary>NhsTimeSpan to as string</summary>
        public DateTime To
        {
            get
            {
                return this.value.To;
            }

            set
            {
                this.state.To = value.ToString("yyyy-mmm-dd", CultureInfo.InvariantCulture);
                this.Value.To = value;
            }
        }

        /// <summary>NhsTimeSpan threshold as string</summary>
        public TimeSpanUnit Threshold
        {
            get
            {
                return (TimeSpanUnit)this.state.Threshold;
            }

            set
            {
                this.state.Threshold = (int)value;
                this.Value.Threshold = value;
            }
        }

        /// <summary>
        /// The NhsTimeSpan entered in the text box
        /// </summary>
        [Description("The NhsTimeSpan entered in the text box")]
        public NhsTimeSpan Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.state.Value = value;
            }
        }

        /// <summary>
        /// Specifies whether long or short units are displayed. 
        /// </summary>      
        [Description("Specifies the length of units.")]
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                return (TimeSpanUnitLength)this.unitLength;
            }

            set
            {
                if (!Enum.IsDefined(typeof(TimeSpanUnitLength), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.state.UnitLength = value;                
                this.unitLength = value;                
            }
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {          
            this.state.Value = this.Value;
            this.state.UnitLength = this.unitLength;

            // Register the NhsCuiValidation.js script with ASP.NET Ajax
            ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference("NhsCui.Toolkit.Web.Validators.NhsCuiValidation.js", "NhsCui.Toolkit.Web"));
           
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new NhsTimeSpanJavascriptConverter() });

            this.ClientState = jss.Serialize(this.state);

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void TimeSpanInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                // this.state = new JavaScriptSerializer().Deserialize<TimeSpanInputClientState>(ClientState);

                // this.Value.From = DateTime.Parse(this.state.From, CultureInfo.CurrentCulture);
                // this.Value.Granularity = (TimeSpanUnit)this.state.Granularity;
                // this.Value.IsAge = (bool)this.state.IsAge;
                // this.text = this.state.Text;
                // this.Value.To = DateTime.Parse(this.state.To, CultureInfo.CurrentCulture);
                // this.Value.Threshold = (TimeSpanUnit)this.state.Threshold;
                // this.value = (NhsTimeSpan)this.state.Value;

                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.RegisterConverters(new JavaScriptConverter[] { new NhsTimeSpanJavascriptConverter() });

                this.state = jss.Deserialize<TimeSpanInputClientState>(ClientState);

                this.value = this.state.Value;
                this.unitLength = (TimeSpanUnitLength)this.state.UnitLength;
            }
        }
    }
}
