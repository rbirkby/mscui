//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter a timespan.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.UI;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.DateAndTime.Resources;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// The control used to enter a timespan. 
    /// </summary>
    [DefaultProperty("Value"), DefaultEvent("ValueChanged")]
    [ToolboxData("<{0}:TimeSpanInputBox runat=server></{0}:TimeSpanInputBox>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeSpanInputBox.bmp")]
    [Designer(typeof(TimeSpanInputBoxDesigner))]
    [ValidationProperty("Text")]
    public class TimeSpanInputBox : CompositeControl
    {
        #region Member Vars

        /// <summary>
        /// Base Reg Ex pattern
        /// </summary>
        private const string TimeSpanParseRegExFormat = @"(?:(?<##GROUP##>\d+)\s*(?:##UNITLIST##))?";

        /// <summary>
        /// The Ajax Extender
        /// </summary>
        private TimeSpanInputBoxExtender timeSpanInputBoxExtender;

        /// <summary>
        /// Basic textbox used as a basis for the control.
        /// </summary>
        private TextBox textBox;

        #endregion

        #region Events and Delegates

        /// <summary>
        /// Occurs when the content of the value of the input box changes between posts to the server. 
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// The wrapper for Value.From. 
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the start of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior")]
        public DateTime From
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value.From;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.From = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.Granularity. 
        /// </summary>
        /// <remarks>
        /// Specifies the largest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the 
        /// granularity is set from the time span.
        /// If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is false, the granularity is set from the assigned granularity value. 
        /// The granularity may be seconds, minutes,
        /// weeks, hours, days, weeks, months or years. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Years)]
        public TimeSpanUnit Granularity
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value.Granularity;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.Granularity = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.IsAge. 
        /// </summary>
        /// <remarks>
        /// Specifies whether the time span represents an age. 
        /// </remarks> 
        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        public bool IsAge
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value.IsAge;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.IsAge = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.ToString() when getting and Value.ParseExact when setting. 
        /// </summary>
        /// <remarks>
        /// Returns or accepts the time-span as a formatted string. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        public string Text
        {
            get
            {
                this.EnsureChildControls();
                return this.TextBox.Text;
            }

            set
            {
                this.EnsureChildControls();

                string preParsedValue = TimeSpanInputBox.PreParseUnits(value);
                NhsTimeSpan newNhsTimeSpan = NhsTimeSpan.Parse(preParsedValue, CultureInfo.InvariantCulture);
                if (newNhsTimeSpan != null)
                {
                    // We don't want to assign new NhsTimeSpan object directly as that would
                    // overwrite existing properties like granularity, threshold etc. and we
                    // [also have to offset the 'to' property value from any pre-existing value]
                    // [[Apparently not - just overwite the From and To from the newNhsTimeSpan. 
                    // Doesn't seem like a good ISV experience to me, overwriting values I might already have set...]]
                    // TimeSpan timeSpanTicks = newNhsTimeSpan.To - newNhsTimeSpan.From;
                    // this.Value.To = this.Value.From + timeSpanTicks;
                    this.timeSpanInputBoxExtender.From = newNhsTimeSpan.From;
                    this.timeSpanInputBoxExtender.To = newNhsTimeSpan.To;
                    this.UpdateTextBox();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.To. 
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the end of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior")]
        public DateTime To
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value.To;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.To = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// The wrapper for Value.Threshold. 
        /// </summary>
        /// <remarks>
        /// Specifies the smallest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the threshold 
        /// is set from the time span.
        /// If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is false, the threshold  is set from the assigned threshold value. The 
        /// threshold may be seconds, minutes,
        /// weeks, hours, days, weeks, months or years. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Seconds)]
        public TimeSpanUnit Threshold
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value.Threshold;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.Threshold = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// Gets or sets the timespan entered in the input box. 
        /// </summary>
        [Category("Behavior")]
        [Description("The timespan entered in the text box")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhsTimeSpan Value
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.Value;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.Value = value;
                this.UpdateTextBox();
            }
        }

        /// <summary>
        /// Specifies whether long or short units are displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Short". If long units are specified, the entire unit name is included, such as "month" or "minute". 
        /// If short units are specified, an abbreviated 
        /// form of the unit is included, for example "m" for month or "min" for minute. Singular and plural versions of the 
        /// short and long units are included automatically as appropriate. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(TimeSpanUnitLength.Short), Description("Specifies the length of units.")]
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                this.EnsureChildControls();
                return this.timeSpanInputBoxExtender.UnitLength;
            }

            set
            {
                this.EnsureChildControls();
                this.timeSpanInputBoxExtender.UnitLength = value;
                this.UpdateTextBox();
            }
        }

        #endregion

        #region Private Properties

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

        #endregion

        #region Protected Methods

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
            this.timeSpanInputBoxExtender = new TimeSpanInputBoxExtender();
            this.timeSpanInputBoxExtender.ID = this.ID + "_timeSpanInputBoxExtender";
            this.timeSpanInputBoxExtender.TargetControlID = this.textBox.ID;
            this.Controls.Add(this.timeSpanInputBoxExtender);
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
        /// Init time processing
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnInit(EventArgs e)
        {
            // If it is not a postback initialise the TextBox from the current value of NhsTimeSpan
            if (this.Page.IsPostBack == false)
            {
                this.UpdateTextBox();
                if (this.DesignMode || String.IsNullOrEmpty(this.ToolTip))
                {
                    this.ToolTip = TimeSpanInputBoxControl.Resources.TooltipText;
                }
            }

            base.OnInit(e);
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Implements the extra parsing logic to recognise truncated forms of long TimeSpanUnits
        /// </summary>
        /// <param name="value">Input string to preparse for truncated TSUs</param>
        /// <returns>A string regularised to the long TimeSpanUnits version of the input string</returns>
        private static string PreParseUnits(string value)
        {
            // NOT the same as the NhsTimeSpan version...
            string parseExpression = TimeSpanInputBox.BuildParseRegularExpression();
            Regex regEx = new Regex(parseExpression, RegexOptions.IgnoreCase);
            Match wordMatch = regEx.Match(value);
            string returnValue = "";

            if (wordMatch != null)
            {
                // now convert to full length versions for easier parsing by NhsTimeSpan...
                if (wordMatch.Groups["years"] != null && string.IsNullOrEmpty(wordMatch.Groups["years"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["years"].Value, CultureInfo.InvariantCulture) + GetResourceString("YearsLongUnit") + " ";
                }

                if (wordMatch.Groups["months"] != null && string.IsNullOrEmpty(wordMatch.Groups["months"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["months"].Value, CultureInfo.InvariantCulture) + GetResourceString("MonthsLongUnit") + " ";
                }

                if (wordMatch.Groups["weeks"] != null && string.IsNullOrEmpty(wordMatch.Groups["weeks"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["weeks"].Value, CultureInfo.InvariantCulture) + GetResourceString("WeeksLongUnit") + " ";
                }

                if (wordMatch.Groups["days"] != null && string.IsNullOrEmpty(wordMatch.Groups["days"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["days"].Value, CultureInfo.InvariantCulture) + GetResourceString("DaysLongUnit") + " ";
                }

                if (wordMatch.Groups["hours"] != null && string.IsNullOrEmpty(wordMatch.Groups["hours"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["hours"].Value, CultureInfo.InvariantCulture) + GetResourceString("HoursLongUnit") + " ";
                }

                if (wordMatch.Groups["minutes"] != null && string.IsNullOrEmpty(wordMatch.Groups["minutes"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["minutes"].Value, CultureInfo.InvariantCulture) + GetResourceString("MinutesLongUnit") + " ";
                }

                if (wordMatch.Groups["seconds"] != null && string.IsNullOrEmpty(wordMatch.Groups["seconds"].Value) == false)
                {
                    returnValue += int.Parse(wordMatch.Groups["seconds"].Value, CultureInfo.InvariantCulture) + GetResourceString("SecondsLongUnit") + " ";
                }
            }

            if (returnValue != null && returnValue.Length > 0)
            {
                return returnValue.TrimEnd();
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Builds the RegEx that allows us to preParse a time span string.
        /// </summary>
        /// <returns>regular expression string</returns>
        /// <summary>
        /// Retrieves the resource string for the specified key.
        /// </summary>
        /// <param name="key"> Key value for which resource string will be returned.</param>
        /// <returns> Resource value for the specified key</returns>
        private static String GetResourceString(String key)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("NhsCui.Toolkit.Web.Common.NhsTimeSpanResources", typeof(TimeSpanInputBox).Assembly);
            return rm.GetString(key, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Builds the RegEx that allows us to preParse a time span string.
        /// </summary>
        /// <returns>regular expression string</returns>
        private static string BuildParseRegularExpression()
        {
            StringBuilder regExPattern = new StringBuilder();
            regExPattern.Append("^");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "years",
                                                GetResourceString("YearsUnit"),
                                                GetResourceString("YearUnit"),
                                                GetResourceString("YearsLongUnit"),
                                                GetResourceString("YearLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "months",
                                                GetResourceString("MonthsUnit"),
                                                GetResourceString("MonthUnit"),
                                                GetResourceString("MonthsLongUnit"),
                                                GetResourceString("MonthLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "weeks",
                                                GetResourceString("WeeksUnit"),
                                                GetResourceString("WeekUnit"),
                                                GetResourceString("WeeksLongUnit"),
                                                GetResourceString("WeekLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "days",
                                                GetResourceString("DaysUnit"),
                                                GetResourceString("DayUnit"),
                                                GetResourceString("DaysLongUnit"),
                                                GetResourceString("DayLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "hours",
                                                GetResourceString("HoursUnit"),
                                                GetResourceString("HourUnit"),
                                                GetResourceString("HoursLongUnit"),
                                                GetResourceString("HourLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "minutes",
                                                GetResourceString("MinutesUnit"),
                                                GetResourceString("MinuteUnit"),
                                                GetResourceString("MinutesLongUnit"),
                                                GetResourceString("MinuteLongUnit")));
            regExPattern.Append("\\s*");
            regExPattern.Append(TimeSpanInputBox.BuildUnitRegEx(
                                                "seconds",
                                                GetResourceString("SecondsUnit"),
                                                GetResourceString("SecondUnit"),
                                                GetResourceString("SecondsLongUnit"),
                                                GetResourceString("SecondLongUnit")));
            regExPattern.Append("$");
            return regExPattern.ToString();
        }

        /// <summary>
        /// Build reg ex to match single time span units
        /// </summary>
        /// <param name="groupName">regex group name</param>
        /// <param name="unitNames">names used for unit</param>
        /// <returns>reg ex pattern</returns>
        private static string BuildUnitRegEx(string groupName, params string[] unitNames)
        {
            List<string> enhancedUnitNamesList = new List<string>();

            for (int nameIndex = 0; nameIndex < unitNames.Length; nameIndex++)
            {
                StringBuilder partString = new StringBuilder();
                for (int charIndex = 0; charIndex < unitNames[nameIndex].Length; charIndex++)
                {
                    partString.Append(unitNames[nameIndex].Substring(charIndex, 1));
                    if (enhancedUnitNamesList.Contains(partString.ToString()) == false)
                    {
                        enhancedUnitNamesList.Add(partString.ToString());
                    }
                }
            }

            string[] enhancedUnitNames = new string[enhancedUnitNamesList.Count];
            enhancedUnitNamesList.CopyTo(enhancedUnitNames);

            string regExPattern = TimeSpanParseRegExFormat.Replace("##GROUP##", groupName);
            regExPattern = regExPattern.Replace("##UNITLIST##", string.Join("|", enhancedUnitNames));

            return regExPattern;
        }

        /// <summary>
        /// update text displayed in textbox
        /// </summary>
        private void UpdateTextBox()
        {
            this.TextBox.Text = this.Value.ToString(this.UnitLength, CultureInfo.CurrentCulture);
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
        /// Register client script variables via the Page's ClientScriptManager
        /// </summary>
        private void RegisterClientScriptVariables()
        {
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valAttachedServerSide", Validation.ValidatorAttached(typeof(TimeSpanInputBoxValidator), this.Page, false).ToString());                                                                                                                                                                                                                                                                                                                                                                                                               
        }

        /// <summary>
        /// Check if there is a Validator attached to this TimeInputBox
        /// </summary>
        /// <returns>True if one is attached</returns>
        private bool TimeSpanInputBoxValidatorAttached()
        {
            TimeSpanInputBoxValidator timeSpanInputBoxValidator;

            foreach (IValidator validator in this.Page.Validators)
            {
                timeSpanInputBoxValidator = validator as TimeSpanInputBoxValidator;

                if (timeSpanInputBoxValidator != null && timeSpanInputBoxValidator.ControlToValidate == this.ID)
                {
                    return true;
                }
            }

            return false;
        }       
        #endregion
    }
}
