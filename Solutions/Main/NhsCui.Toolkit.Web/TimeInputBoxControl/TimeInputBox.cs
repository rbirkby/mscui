//-----------------------------------------------------------------------
// <copyright file="TimeInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter a time.</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;

    using AjaxControlToolkit;

    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// The control used to enter a time. 
    /// </summary>
    [DefaultProperty("Value"), DefaultEvent("ValueChanged")]
    [ToolboxData("<{0}:TimeInputBox runat=server></{0}:TimeInputBox>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeInputBox.bmp")]
    [Designer(typeof(TimeInputBoxDesigner))]
    [ValidationProperty("Text")]
    public class TimeInputBox : CompositeControl
    {
        /// <summary>
        /// ToDo
        /// </summary>
        private TimeInputBoxExtender extender;

        /// <summary>
        /// Basic textbox used as a basis for the control.
        /// </summary>
        private TextBox textBox;       

        /// <summary>
        /// Occurs when the content of the value of the input box changes between posts to the server. 
        /// </summary>
        [Description("Fires when the value of control is changed")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Specifies whether to display a checkbox for the approximate flag. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to display a checkbox for the approximation")]
        [DefaultValue(false)]
        public bool AllowApproximate
        {
            get
            {
                this.EnsureChildControls();
                return this.Extender.AllowApproximate;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.AllowApproximate = value;
            }
        }

        /// <summary>
        /// Specifies whether seconds should be displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether seconds should be displayed")]
        public bool DisplaySeconds
        {
            get
            {
                this.EnsureChildControls();
                return this.Extender.DisplaySeconds;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.DisplaySeconds = value;
            }
        }

        /// <summary>
        /// Specifies whether hours should be displayed in 12-hour or 24-hour format. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. If the time is 1:30 in the afternoon, this displays as 01:30 in 12-hour format and 13:30 in 24-hour format.
        /// Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
        /// for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
        /// the clinical context.
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether hours should be displayed as 12 hour or 24 hour")]
        public bool Display12Hour
        {
            get
            {
                this.EnsureChildControls();
                return this.Extender.Display12Hour;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.Display12Hour = value;
            }
        }

        /// <summary>
        /// Specifies whether an AM/PM suffix should be included. 
        /// </summary>
        /// <remarks> 
        /// Defaults to false.  AM refers to times from 00:00 to 11:59:59.  PM refers to times from 12:00 to 23:59:59
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether the AM/PM suffix should be included")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public bool DisplayAMPM
        {
            get
            {
                this.EnsureChildControls();
                return this.Extender.DisplayAMPM;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.DisplayAMPM = value;
            }
        }

        /// <summary>
        /// Specifies the functionality exposed by the TimeInputBox as "Simple" or "Complex".
        /// </summary>
        /// <remarks>
        /// Defaults to "Complex" so the 
        /// TimeInputBox’s complete functionality is exposed. If this is set to "Simple", only 
        /// a simple time can be entered such as TimeInputBox.Time.TimeValue. If the functionality is set to "Simple", the TimeInputBox allows other 
        /// values, such as <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.AllowApproximate">AllowApproximate</see> and
        /// <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.TimePeriod">TimePeriod</see>, to be got and set; the control does not, however, respond to 
        /// these values. In addition, attempting 
        /// to set the Value.TimeType or <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.TimeType">TimeType</see> to any value other than TimeType.Exact 
        /// throws an argument exception.
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies the functionality exposed by the TimeInputBox (defaults to Complex). Setting Functionality to Simple sets Value.TimeType to Exact.")]
        [DefaultValue(0)]
        public TimeFunctionality Functionality
        {
            get
            {
                this.EnsureChildControls();
                return (TimeFunctionality)this.Extender.Functionality;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.Functionality = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings which identify different types of null index times.
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list.
        /// </remarks>
        [Category("Behavior"), DefaultValue(null), TypeConverter(typeof(StringArrayConverter))]
        [Description("Gets or sets a list of localized strings which identify different types of null index times.")] 
        [RefreshProperties(RefreshProperties.All)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                this.EnsureChildControls();
                if (this.Extender.NullStrings != null)
                {
                    return (string[])this.Extender.NullStrings.Clone();
                }

                return new string[0];
            }

            set
            {
                this.EnsureChildControls();
                NullStringUtil.TrimAndValidate(value, true);
                this.Extender.NullStrings = value;
                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// Gets or sets the time entered in the input box.
        /// </summary>
        [Category("Behavior")]
        [Description("The time entered in the text box")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true), DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhsTime Value
        {
            get
            {
                this.EnsureChildControls();
                return this.Extender.Value;
            }

            set
            {
                this.EnsureChildControls();
                this.Extender.Value = value;

                this.RefreshDisplayText();
            }
        }

        /// <summary>
        ///Gets or sets the CSS class used for the CheckBox.
        /// </summary>
        /// <remarks>
        /// This is visual behaviour only; 
        /// the value of the control is unaffected.
        /// </remarks>
        [Category("Behavior")]
        [Description("CheckBox text to appear when the control has no value")]
        [DefaultValue("")]
        public string CheckBoxCssClass
        {
            get
            {
                EnsureChildControls();
                return this.extender.CheckBoxCssClass;
            }

            set
            {
                EnsureChildControls();
                this.extender.CheckBoxCssClass = value;
            }
        }

        /// <summary>
        /// Gets the text value of the internal text box
        /// </summary>
        [Browsable(false)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Text
        {
            get
            {
                EnsureChildControls();
                return this.TextBox.Text;
            }

            set
            {
                EnsureChildControls();
                this.TextBox.Text = value;

                // Try to parse the value string into the value property. If it fails set TimeType to TimeType.Null
                NhsTime result;
                if (NhsTime.TryParseExact(value, out result, CultureInfo.CurrentCulture))
                {
                    this.Value = result;
                }
                else
                {
                    this.Value.TimeType = TimeType.Null;
                }
            }
        }        

        /// <summary>
        /// The wrapper for Value.TimeType. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.TimeType")]
        [RefreshProperties(RefreshProperties.All)]
        public TimeType TimeType
        {
            get
            {
                return this.Value.TimeType;
            }

            set
            {
                this.Value.TimeType = value;
                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// The wrapper for Value.TimeValue. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.TimeValue")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime TimeValue
        {
            get
            {
                return this.Value.TimeValue;
            }

            set
            {
                this.Value.TimeValue = value;

                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.NullIndex")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(-1)]
        public int NullIndex
        {
            get
            {
                return this.Value.NullIndex;
            }

            set
            {
                this.Value.NullIndex = value;

                this.RefreshDisplayText();
            }
        }      

        /// <summary>
        /// The underlying text box.
        /// </summary>
        private TextBox TextBox
        {
            get
            {
                this.EnsureChildControls();
                return this.textBox;
            }
        }

        /// <summary>
        /// The Ajax extender.
        /// </summary>
        private TimeInputBoxExtender Extender
        {
            get
            {
                this.EnsureChildControls();
                return this.extender;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls
        /// that use composition-based implementation to create any child 
        /// controls they contain in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.textBox = new TextBox();
            this.textBox.ID = ID + "_TextBox";
            this.textBox.AutoCompleteType = AutoCompleteType.Disabled;
            this.textBox.TextChanged += new EventHandler(this.TextBoxTextChanged);

            Controls.Add(this.textBox);

            // Create the extender.
            this.extender = new TimeInputBoxExtender();
            this.extender.ID = ID + "_TimeInputBoxExtender";
            this.extender.TargetControlID = this.textBox.ID;

            Controls.Add(this.extender);           
        }

        /// <summary>
        /// Init time processing
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnInit(EventArgs e)
        {
            // If it is not a postback initialise the TextBox from the current value of NhsTime
            if (this.Page.IsPostBack == false)
            {
                this.RefreshDisplayText();
                if (this.DesignMode || String.IsNullOrEmpty(this.ToolTip))
                {
                    this.ToolTip = TimeInputBoxControl.Resources.FirstUseTooltipText;
                }
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterClientScriptVariables();
            base.OnPreRender(e);
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the 
        /// specified HtmlTextWriterTag. This method is used primarily by 
        /// control developers. 
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output 
        /// stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // since we have passed all our attributes and style onto the
            // textbox nothing else to do here
            if (this.ID != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // PL Do we need this
            // if (this.DesignMode)
            // {
            //    // Since prerender doesn't get raised in design mode
            //    this.RefreshDisplayText();
            // }

            // Transfer our style and attributes to the textbox
            this.TextBox.ControlStyle.CopyFrom(this.ControlStyle);
            this.TextBox.CopyBaseAttributes(this);

            base.RenderContents(writer);
        }

        /// <summary>
        /// Recreates the child controls in a control derived from CompositeControl. 
        /// </summary>
        protected override void RecreateChildControls()
        {
            // no need to recreate child controls
            this.EnsureChildControls();
        }

        /// <summary>
        /// Raise the ValueChanged event.
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        /// <summary>
        /// Refresh the text displayed in the text box.
        /// </summary>
        private void RefreshDisplayText()
        {
            NhsTime value = this.Value;
            string[] nullStrings = this.NullStrings;

            if (value.TimeType != TimeType.NullIndex || nullStrings == null ||
                        value.NullIndex < 0 || value.NullIndex >= nullStrings.Length)
            {
                this.TextBox.Text = value.ToString(
                                            false,
                                            CultureInfo.CurrentCulture,
                                            this.DisplaySeconds,
                                            this.Display12Hour,
                                            this.DisplayAMPM);
            }
            else
            {
                this.TextBox.Text = nullStrings[value.NullIndex];
            }
        }

        /// <summary>
        /// Handle text changed event of our underlying text box.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }     

        /// <summary>
        /// Register client script variables via the Page's ClientScriptManager
        /// </summary>
        private void RegisterClientScriptVariables()
        {
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valAttachedServerSide", Validation.ValidatorAttached(typeof(TimeInputBoxValidator), this.Page, false).ToString());                                                                                                                                          
        }
    }
}
