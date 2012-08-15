//-----------------------------------------------------------------------
// <copyright file="DateInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter a date. </summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Web.Script;
    using AjaxControlToolkit;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// The control used to enter a date. 
    /// </summary>
    [DefaultProperty("Value"), DefaultEvent("ValueChanged")]
    [ToolboxData("<{0}:DateInputBox runat=\"server\"></{0}:DateInputBox>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateInputBox.bmp")]
    [Designer(typeof(DateInputBoxDesigner))]
    [ValidationProperty("Text")]
    public class DateInputBox : CompositeControl
    {
        /// <summary>
        /// Extender control to configure client-side AJAX functionality
        /// </summary>
        private DateInputBoxExtender dateInputBoxExtender;

        /// <summary>
        /// The basic textbox used as the basis for the control.
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
                EnsureChildControls();
                return this.dateInputBoxExtender.AllowApproximate;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.AllowApproximate = value;
            }
        }

        /// <summary>
        /// Specifies the functionality exposed by the DateInputBox as "Simple" or "Complex". 
        /// </summary>
        /// <remarks>
        /// Defaults to "Complex" so the DateInputBox's complete functionality is exposed. If this is set to "Simple", only a simple date can be entered 
        /// such as DateInputBox.Date.DateValue. If the functionality is set to "Simple", the DateInputBox allows other values such as 
        /// <see cref="P:NhsCui.Toolkit.Web.DateInputBox.NullStrings">Null Strings</see> to be 
        /// got and set; the control itself does not, however, respond to these values. In addition, attempting to set the 
        /// Value.DateType or <see cref="P:NhsCui.Toolkit.Web.DateInputBox.DateType">DateType</see> to any value other than DateType.Exact or 
        /// DateType.Approximate throws an argument exception.
        /// </remarks> 
        [Category("Behavior")]
        [Description("Specifies the functionality exposed by the DateInputBox (defaults to Complex). Setting Functionality to Simple sets Value.DateType to Exact.")]
        [DefaultValue(1)]
        public DateFunctionality Functionality
        {
            get
            {
                EnsureChildControls();
                return (DateFunctionality)this.dateInputBoxExtender.Functionality;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.Functionality = (int)value;
            }
        }

        /// <summary>
        /// Specifies whether the text "Today", "Tomorrow" or "Yesterday" should be displayed in place of the date. 
        /// </summary>
        /// <remarks>
        /// Defaults to false.
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to substitute Today, Tomorrow or Yesterday for actual dates (defaults to false)")]
        [DefaultValue(false)]
        public bool DisplayDateAsText
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.DisplayDateAsText;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.DisplayDateAsText = value;
            }
        }

        /// <summary>
        /// Specifies whether the name of the day is displayed with the date.
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to display the day of week (defaults to false)")]
        [DefaultValue(false)]
        public bool DisplayDayOfWeek
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.DisplayDayOfWeek;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.DisplayDayOfWeek = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(null), TypeConverter(typeof(StringArrayConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                EnsureChildControls();
                if (this.dateInputBoxExtender.NullStrings != null)
                {
                    return (string[])this.dateInputBoxExtender.NullStrings.Clone();
                }

                return new string[0];
            }

            set
            {
                NullStringUtil.TrimAndValidate(value, true);
                EnsureChildControls();
                this.dateInputBoxExtender.NullStrings = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// Gets or sets the date entered in the input box. 
        /// </summary>
        [Category("Behavior")]
        [Description("The date entered in the text box")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhsDate Value
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.Value;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.Value = value;

                this.UpdateTextBox();
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

                // Try to parse the value string into the value property. If it fails set DateType to DateType.Null
                NhsDate result;
                if (NhsDate.TryParseExact(value, out result, System.Threading.Thread.CurrentThread.CurrentCulture))
                {
                    this.Value = result;
                }
                else
                {
                    this.Value.DateType = DateType.Null;
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.DateType. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.DateType")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(DateType.Exact)]
        public DateType DateType
        {
            get
            {
                return this.Value.DateType;
            }

            set
            {
                this.Value.DateType = value;

                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.DateValue. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.DateValue")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime DateValue
        {
            get
            {
                return this.Value.DateValue;
            }

            set
            {
                this.Value.DateValue = value;

                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.Month. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.Month")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(1)]
        public int Month
        {
            get
            {
                return this.Value.Month;
            }

            set
            {
                this.Value.Month = value;

                this.UpdateTextBox();
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

                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.Year. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.Year")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        public int Year
        {
            get
            {
                return this.Value.Year;
            }

            set
            {
                this.Value.Year = value;

                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// Gets or sets the watermark text. 
        /// </summary>
        /// <remarks>
        /// Defaults to dd-MMM-yyyy. This property is localizable and is relevant only when DateType is Null and the control does not have focus. 
        /// In these conditions, the watermark text is displayed using the 
        /// <see cref="P:NhsCui.Toolkit.Web.DateInputBox.WatermarkCssClass">WatermarkCssClass</see> style. This is visual behaviour only; 
        /// the value of the control is unaffected.
        /// </remarks>
        [Category("Behavior"), Localizable(true)]
        [Description("Watermark text to appear when the control has no value")]
        [ResourceDefaultValue(typeof(DateInputBoxControl.Resources), "DefaultWaterMark")]
        public string WatermarkText
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.WatermarkText;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.WatermarkText = value;
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
        [Description("CSS class applied to the CheckBox control")]
        [DefaultValue("")]
        public string CheckBoxCssClass
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.CheckBoxCssClass;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.CheckBoxCssClass = value;
            }
        }

        /// <summary>
        ///Gets or sets the CSS class used for the  <see cref="P:NhsCui.Toolkit.Web.DateInputBox.WatermarkText">WatermarkText</see>.
        /// </summary>
        /// <remarks>
        /// Defaults to an empty string. This property is relevant only when
        /// <see cref="P:NhsCui.Toolkit.Web.DateInputBox.DateType">DateType</see> is null and the control does not have focus. 
        /// In these conditions, the <see cref="P:NhsCui.Toolkit.Web.DateInputBox.WatermarkText">WatermarkText</see> is displayed using the
        /// WatermarkCssClass<see></see> style. This is visual behaviour only; 
        /// the value of the control is unaffected.
        /// </remarks>
        [Category("Behavior")]
        [Description("CSS class applied to the Watermark text")]
        [DefaultValue("")]
        public string WatermarkCssClass
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.WatermarkCssClass;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.WatermarkCssClass = value;
            }
        }

        /// <summary>
        /// Position to display the pop up calendar.
        /// </summary>
        [Category("Behavior")]
        [Description("Position of pop up calendar.")]
        [DefaultValue(PositioningMode.BottomLeft)]
        public PositioningMode CalendarPosition
        {
            get
            {
                EnsureChildControls();
                return this.dateInputBoxExtender.CalendarPosition;
            }

            set
            {
                EnsureChildControls();
                this.dateInputBoxExtender.CalendarPosition = value;
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
        /// Init time processing
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnInit(EventArgs e)
        {
            // If it is not a postback initialise the TextBox from the current value of NhsDate
            if (this.Page.IsPostBack == false)
            {
                this.UpdateTextBox();
                if (this.DesignMode || String.IsNullOrEmpty(this.ToolTip))
                {
                    this.ToolTip = DateInputBoxControl.Resources.FirstUseTooltipText;                           
                }
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.textBox = new TextBox();
            this.textBox.ID = ID + "_TextBox";
            this.textBox.AutoCompleteType = AutoCompleteType.Disabled;
            this.textBox.TextChanged += new EventHandler(this.TextBoxTextChanged);
            Controls.Add(this.textBox);

            // Create the calendar extender
            this.dateInputBoxExtender = new DateInputBoxExtender();
            this.dateInputBoxExtender.ID = this.ID + "_DateInputBoxExtender";
            this.dateInputBoxExtender.TargetControlID = this.textBox.ID;
            this.dateInputBoxExtender.WatermarkText = DateInputBoxControl.Resources.DefaultWaterMark;
            this.Controls.Add(this.dateInputBoxExtender);
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
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.EnsureChildControls();

            // Transfer our style and attributes to the textbox
            this.TextBox.ControlStyle.CopyFrom(this.ControlStyle);
            this.TextBox.CopyBaseAttributes(this);

            base.RenderContents(writer);
        }

        /// <summary>
        /// PreRender time processing
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterClientScriptVariables();

            base.OnPreRender(e);
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
        /// Raise the ValueChanged event
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
        /// update text displayed in textbox
        /// </summary>
        private void UpdateTextBox()
        {
            NhsDate value = this.Value;
            string[] nullStrings = this.NullStrings;

            if (value.DateType != DateType.NullIndex || nullStrings == null ||
                        value.NullIndex < 0 || value.NullIndex >= nullStrings.Length)
            {
                this.TextBox.Text = value.ToString(
                                        this.DisplayDayOfWeek,
                                        false,
                                        this.DisplayDateAsText,
                                        CultureInfo.CurrentCulture);
            }
            else
            {
                this.TextBox.Text = nullStrings[value.NullIndex];
            }
        }

        /// <summary>
        /// Handle text changed event of our underlying text box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        /// <summary>
        /// Renders script to find web resource url at client
        /// </summary>
        /// <returns>URL of embedded resource</returns>
        private string RenderWebResourceScript()
        {
            StringBuilder str = new StringBuilder("<script type = \"text/javascript\"> function NhsDate_GetWebResourceUrl() {");
            string webUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "NhsCui.Toolkit.Web.DateInputBoxControl.calendar.png");
            str.Append("return ('" + webUrl + "')}");
            str.Append("</script>");
            return str.ToString();
        }

        /// <summary>
        /// Register client script variables via the Page's ClientScriptManager
        /// </summary>
        private void RegisterClientScriptVariables()
        {
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valAttachedServerSide", Validation.ValidatorAttached(typeof(DateInputBoxValidator), this.Page, false).ToString());
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "webresource", this.RenderWebResourceScript());
        }
    }
}
