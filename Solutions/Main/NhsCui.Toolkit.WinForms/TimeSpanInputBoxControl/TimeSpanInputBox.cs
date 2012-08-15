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
// <date>9-Jul-2007</date>
// <summary>The control used to enter a timespan.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{                
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using NhsCui.Toolkit.DateAndTime;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using NhsCui.Toolkit.DateAndTime.Resources;    

    /// <summary>
    /// The control used to enter a timespan. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeSpanInputBox.bmp")]
    public partial class TimeSpanInputBox : UserControl, INotifyPropertyChanged
    {
        #region Const Values
        /// <summary>
        /// Start width of the control.
        /// </summary>
        private const int StartWidth = 120;

        /// <summary>
        /// Start height of the control.
        /// </summary>
        private const int StartHeight = 20;

        /// <summary>
        /// Start font size of the control.
        /// </summary>
        private const double StartFont = 8.25;

        /// <summary>
        /// Base Reg Ex pattern
        /// </summary>
        private const string TimeSpanParseRegExFormat = @"(?:(?<##GROUP##>\d+)\s*(?:##UNITLIST##))?";
        #endregion

        #region Member Vars
        /// <summary>
        /// Value of the control.
        /// </summary>
        /// <remarks>
        /// Saves the value of 'Value' property.
        /// </remarks>
        private NhsTimeSpan timeValue;

        /// <summary>
        /// unit length for the control.
        /// </summary>
        /// <remarks>
        /// Saves the value of property UnitLength.
        /// </remarks>
        private TimeSpanUnitLength unitLength = TimeSpanUnitLength.Short;

        /// <summary>
        /// Status of the control's value.
        /// </summary>
        private bool valid = true;

        /// <summary>
        /// Validation manager of the control.
        /// </summary>
        private IValidationManager validationManager;

        /// <summary>
        /// Behavior of the control for invalid text.
        /// </summary>
        private bool cancelOnError = true;       

        /// <summary>
        /// Indicates if the control value is validated or not.
        /// </summary>
        /// <remarks>
        /// This flag is used to make sure that DateInputBox_Leave executes only when
        /// TxtInput_Leave is not executed.
        /// </remarks>
        private bool validated;

        /// <summary>
        /// invalid text of the control.
        /// </summary>
        private String invalidText;

        /// <summary>
        /// Tooltip to be displayed.
        /// </summary>
        /// <remarks>
        /// Saves the value of the property ToolTipText
        /// </remarks>
        private String tooltipText = String.Empty;

        /// <summary>
        /// Tooltip for the control.
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a TimeSpanInputBox object.
        /// </summary>
        public TimeSpanInputBox()
        {
            this.InitializeComponent();
            this.toolTip1 = new ToolTip();
            this.AccessibleName = TimeSpanInputBoxControl.Resources.AccessibleName;
            this.AccessibleDescription = TimeSpanInputBoxControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;

            this.RefreshDisplayText();
            this.SetToolTip();
        }
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// The foreground color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "Empty")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                if (base.ForeColor != value)
                {
                    base.ForeColor = value;
                    if (value == SystemColors.ControlText)
                    {
                        this.txtInput.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        this.txtInput.ForeColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// The background color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "Empty")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;
                    if (value == SystemColors.Control)
                    {
                        this.txtInput.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        this.txtInput.BackColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.From. 
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the start of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Description("The DateTime value which marks the start of time span.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime From
        {
            get
            {
                return this.Value.From;
            }

            set
            {
                this.Value.From = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value");
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
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Years), Description("The granularity of time span.")]
        [RefreshProperties(RefreshProperties.All)]
        public TimeSpanUnit Granularity
        {
            get
            {
                return this.Value.Granularity;                
            }

            set
            {
                this.Value.Granularity = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value");
            }
        }

        /// <summary>
        /// The wrapper for Value.IsAge. 
        /// </summary>
        /// <remarks>
        /// Specifies whether the time span represents an age. 
        /// </remarks> 
        [Bindable(true), Category("Behavior"), DefaultValue(false), Description("Specifies whether the time span represents an age or not.")]
        [RefreshProperties(RefreshProperties.All)]
        public bool IsAge
        {
            get
            {
                return this.Value.IsAge;
            }

            set
            {
                this.Value.IsAge = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value");
            }
        }

        /// <summary>
        /// The wrapper for Value.ToString() when getting and Value.ParseExact when setting. 
        /// </summary>
        /// <remarks>
        /// Returns or accepts the time-span as a formatted string. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Description("The text associated with the control"), Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        public override string Text
        {
            get
            {
                return this.txtInput.Text;
            }

            set
            {                                
                NhsTimeSpan newNhsTimeSpan;
                string preParsedValue = TimeSpanInputBox.PreParseUnits(value == null ? String.Empty : value);

                // Setting for design mode. Don't throw exception.
                if (this.DesignMode)
                {
                    if (NhsTimeSpan.TryParse(preParsedValue, out newNhsTimeSpan, CultureInfo.CurrentCulture, this.IsAge))
                    {
                        this.Value.From = newNhsTimeSpan.From;
                        this.Value.To = newNhsTimeSpan.To;
                        this.RefreshDisplayText();
                        this.NotifyPropertyChanged("Value");
                    }
                }

                // Setting for run time mode
                else
                {                    
                    newNhsTimeSpan = NhsTimeSpan.Parse(preParsedValue, CultureInfo.CurrentCulture, this.IsAge);
                    if (newNhsTimeSpan != null)
                    {
                        this.Value.From = newNhsTimeSpan.From;
                        this.Value.To = newNhsTimeSpan.To;
                        this.RefreshDisplayText();
                        this.NotifyPropertyChanged("Value");
                    }
                }                                                                       
            }
        }

        /// <summary>
        /// The wrapper for Value.To. 
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the end of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Description("The DateTime value which marks the end of time span.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime To
        {
            get
            {
                return this.Value.To;
            }

            set
            {
                this.Value.To = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value");
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
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Seconds), Description("Threshold of timespan.")]
        [RefreshProperties(RefreshProperties.All)]
        public TimeSpanUnit Threshold
        {
            get
            {
                return this.Value.Threshold;
            }

            set
            {
                this.Value.Threshold = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value");
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
                if (this.timeValue == null)
                {
                    this.timeValue = new NhsTimeSpan();
                }

                return this.timeValue;
            }

            set
            {
                this.timeValue = value;
                this.RefreshDisplayText();
                this.NotifyPropertyChanged("Value"); 
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
        [RefreshProperties(RefreshProperties.All)]
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                return this.unitLength;                
            }

            set
            {
                if (!Enum.IsDefined(typeof(TimeSpanUnitLength), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.unitLength = value;
                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// Gets the status of the control's value.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Status of the value of the control.")]
        public bool IsValid
        {
            get { return this.valid; }
        }

        /// <summary>
        /// Gets or sets the validation manager of the control.
        /// </summary>
        [DefaultValue(null)]
        [Category("Behavior")]
        [Description("The validation manager for the control.")]
        public IValidationManager ValidationManager
        {
            get { return this.validationManager; }
            set { this.validationManager = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the behavior of control for invalid text.
        /// </summary>
        /// <remarks>
        /// If true user can not lose focus from the control if the text entered is invalid.
        /// </remarks>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Indicates whether this component allows focus change for invalid value or not.")]
        public bool CancelOnError
        {
            get { return this.cancelOnError; }
            set { this.cancelOnError = value; }
        }

        /// <summary>
        /// Gets the text displayed in the ToolTip.
        /// </summary>       
        [Category("Behavior")]
        [Description("The text that is displayed in the tooltip.")]
        [ResourceDefaultValue(typeof(TimeSpanInputBoxControl.Resources), "TooltipText")]
        public string TooltipText
        {
            get
            {
                if (this.tooltipText.Trim().Length > 0)
                {
                    return this.tooltipText;
                }

                return TimeSpanInputBoxControl.Resources.TooltipText;
            }

            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (this.tooltipText != value)
                {
                    this.tooltipText = value;
                    this.SetToolTip();
                }
            }
        }

        #endregion

        #region Protected Properties
        /// <summary>
        /// Overriden. Returns the default size for the control.
        /// </summary>        
        protected override Size DefaultSize
        {
            get
            {
                return new Size(StartWidth, StartHeight);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Retrieves the resource string for the specified key.
        /// </summary>
        /// <param name="key"> Key value for which resource string will be returned.</param>
        /// <returns> Resource value for the specified key</returns>
        private static String GetResourceString(String key)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("NhsCui.Toolkit.WinForms.Common.NhsTimeSpanResources", typeof(TimeSpanInputBox).Assembly);
            return rm.GetString(key, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Implements the extra parsing logic to recognise truncated forms of long TimeSpanUnits
        /// </summary>
        /// <param name="value">Input string to preparse for truncated TSUs</param>
        /// <returns>A string regularised to the long TimeSpanUnits version of the input string</returns>        
        private static string PreParseUnits(string value)
        {
            string result;
            if (!TryPreParseUnits(value, out result))
            {
                throw new FormatException(GetResourceString("ParseCalledWithBadFormat"));
            }

            return result;              
        }

        /// <summary>
        /// Implements the extra parsing logic to recognise truncated forms of long TimeSpanUnits
        /// </summary>
        /// <param name="value">Input string to preparse for truncated TSUs</param>
        /// <param name="parsedValue">A string regularised to the long TimeSpanUnits version of the input string</param>
        /// <returns>status of update</returns>
        private static bool TryPreParseUnits(string value, out string parsedValue)
        {
            // NOT the same as the NhsTimeSpan version...
            string parseExpression = TimeSpanInputBox.BuildParseRegularExpression();
            Regex regEx = new Regex(parseExpression, RegexOptions.IgnoreCase);
            Match wordMatch = regEx.Match(value);
            string returnValue = "";
            parsedValue = null;
            long result;
            if (wordMatch != null)
            {
                // now convert to full length versions for easier parsing by NhsTimeSpan...
                if (wordMatch.Groups["years"] != null && string.IsNullOrEmpty(wordMatch.Groups["years"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["years"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("YearsLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["months"] != null && string.IsNullOrEmpty(wordMatch.Groups["months"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["months"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("MonthsLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["weeks"] != null && string.IsNullOrEmpty(wordMatch.Groups["weeks"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["weeks"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("WeeksLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["days"] != null && string.IsNullOrEmpty(wordMatch.Groups["days"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["days"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("DaysLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["hours"] != null && string.IsNullOrEmpty(wordMatch.Groups["hours"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["hours"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("HoursLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["minutes"] != null && string.IsNullOrEmpty(wordMatch.Groups["minutes"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["minutes"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("MinutesLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }

                if (wordMatch.Groups["seconds"] != null && string.IsNullOrEmpty(wordMatch.Groups["seconds"].Value) == false)
                {
                    if (long.TryParse(wordMatch.Groups["seconds"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    {
                        returnValue += result + GetResourceString("SecondsLongUnit") + " ";
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (returnValue != null && returnValue.Length > 0)
            {
                parsedValue = returnValue.TrimEnd();
                return true;
            }
            else
            {
                 parsedValue = value;
                 return true;
            }
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
        /// Updates text of the control.
        /// </summary>
        private void RefreshDisplayText()
        {
            this.txtInput.Text = this.Value.ToString(this.UnitLength, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Sets the control's current value as invalid.
        /// </summary>
        /// <param name="status">Update status. Set to false by the function.</param>
        private void SetInvalid(ref bool status)
        {
            this.valid = false;
            this.invalidText = this.txtInput.Text;
            status = false;
            if (this.ValidationManager != null)
            {
                this.ValidationManager.SetError(this, DateInputBoxControl.Resources.invalidValue);
            }
        }

        /// <summary>
        /// Sets the control's current value as valid.
        /// </summary>
        private void SetValid()
        {
            this.valid = true;
            this.invalidText = String.Empty;
            if (this.ValidationManager != null)
            {
                this.ValidationManager.ClearError(this);
            }
        }

        /// <summary>
        /// Refresh the text displayed in the text box.
        /// </summary>
        /// <returns>
        /// Status of update.
        /// </returns>
        private bool ValidateAndUpdateValue()
        {
            bool status = true;
            this.SetValid();
            this.txtInput.Text = this.txtInput.Text.Trim();
            string preParsedValue;
            if (!string.IsNullOrEmpty(this.txtInput.Text))
            {
                NhsTimeSpan tmp;
                if (TryPreParseUnits(this.txtInput.Text.Trim(), out preParsedValue))
                {
                    try
                    {
                        if (NhsTimeSpan.TryParse(preParsedValue, out tmp, CultureInfo.CurrentCulture, this.IsAge))
                        {
                            this.Value.From = tmp.From;
                            this.Value.To = tmp.To;
                            this.RefreshDisplayText();
                            this.NotifyPropertyChanged("Value");
                        }
                        else
                        {
                            this.SetInvalid(ref status);
                        } 
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        this.SetInvalid(ref status);
                    }                   
                }
                else
                {
                    this.SetInvalid(ref status);
                }
            }
            else
            {
                this.Value = new NhsTimeSpan();
                this.RefreshDisplayText();
            }

            return status;
        }

        /// <summary>
        /// Sets default tooltip for the control.
        /// </summary>
        private void SetToolTip()
        {
            if (!string.IsNullOrEmpty(this.TooltipText))
            {
                this.toolTip1.SetToolTip(this.txtInput, this.TooltipText);
            }
            else
            {
                this.toolTip1.RemoveAll();
            }
        }
        #endregion

        #region Event Handlers     
        /// <summary>
        /// Handles the font changed event of control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TimeSpanInputBox_FontChanged(object sender, EventArgs e)
        {
            if (this.Font.Size > StartFont)
            {
                double scaleFactor = this.Font.Size / StartFont;
                this.Height = (int)(StartHeight * scaleFactor);
                this.Width = (int)(StartWidth * scaleFactor);
            }
            else
            {
                this.Height = StartHeight;
                this.Width = StartWidth;
            }
        }

        /// <summary>
        /// Handles enter event of text box.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_Enter(object sender, EventArgs e)
        {
            this.validated = false;
        }

        /// <summary>
        /// Handles validating event of text box.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_Validating(object sender, CancelEventArgs e)
        {
            if (!this.validated)
            {
                this.validated = true;
                if (!this.ValidateAndUpdateValue())
                {
                    e.Cancel = this.cancelOnError;
                }
            }
        }

        /// <summary>
        /// Handles the leave event of control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TimeSpanInputBox_Leave(object sender, EventArgs e)
        {
            if (!this.validated)
            {
                this.validated = true;
                if (!this.ValidateAndUpdateValue() && this.cancelOnError)
                {
                    this.txtInput.Focus();
                }
            }
        }

        /// <summary>
        /// Handles the keypress event of TextBox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                this.ValidateAndUpdateValue();
                this.txtInput.SelectionLength = 0;
                this.txtInput.SelectionStart = this.txtInput.TextLength;
                e.Handled = true;
                return;
            }
        }
        #endregion                                        
    }
}
