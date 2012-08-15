//-----------------------------------------------------------------------
// <copyright file="CuiTime.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-May-2008</date>
// <summary>Represents a CUI time. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls.Common.DateAndTime
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a CUI time. 
    /// </summary>
    public class CuiTime
    {
        #region Const Values
        /// <summary>
        /// RegEx to enforce only digits.
        /// </summary>
        private const string NonDigitCharactersRegEx = @"\D";

        /// <summary>
        /// RegEx for finding the Time Add instructions. 
        /// </summary>
        private const string AddInstructionRegExFormat = @"^([+-]?\d+[#0##1#])+?$";

        /// <summary>
        /// Maximum allowed NullIndex.
        /// </summary>
        private const int MaximumNullIndex = 99;

        /// <summary>
        /// Minimum allowed NullIndex.
        /// </summary>
        private const int MinimumNullIndex = -1;
        #endregion

        #region Member Variables
        /// <summary>
        /// The TimeType that represents the time. 
        /// </summary>
        /// <remarks>Defaults to TimeType.Exact. TimeType is always assigned a value explicitly; this is not implied by setting an 
        /// associated property.  
        /// </remarks>
        private TimeType timeType;

        /// <summary>
        /// The exact or approximate time value. 
        /// </summary>
        /// <remarks>Defaults to DateTime.Now. TimeValue retains its value even when TimeType indicates the TimeValue value is not relevant. For example, when TimeType is set 
        /// to TimeType.TimePeriod, TimeValue remains unchanged. 
        /// </remarks>
        private DateTime timeValue;

        /// <summary>
        /// A number identifying a null index type. 
        /// </summary>
        /// <remarks>Defaults to -1. The null index has no meaning in and of itself; the meaning is implied by the UI control.</remarks>
        private int nullIndex = -1;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a CuiTime object. 
        /// </summary>
        public CuiTime()
        {
            this.timeValue = DateTime.Now;
            this.timeType = TimeType.Exact;
        }

        /// <summary>
        /// Constructs a CuiTime object, initializes the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeValue">TimeValue</see> to the 
        /// given value and sets <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> to Exact. 
        /// </summary>
        /// <param name="time">The given time value. </param>
        public CuiTime(DateTime time)
        {
            this.timeValue = time;
            this.timeType = TimeType.Exact;
        }

        /// <summary>
        /// Constructs a CuiTime object, initializes the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeValue">TimeValue</see> to the given value 
        /// and sets <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> to Exact if approximate is false or 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> to Approximate if approximate is true. 
        /// </summary>
        /// <param name="time">The given time value. </param>
        /// <param name="approximate">If true sets <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> to TimeType.Approximate; 
        /// otherwise, sets <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime">TimeType</see> to TimeType.Exact.</param>
        public CuiTime(DateTime time, bool approximate)
        {
            this.timeValue = time;

            if (approximate == true)
            {
                this.timeType = TimeType.Approximate;
            }
            else
            {
                this.timeType = TimeType.Exact;
            }
        }

        /// <summary>
        /// Constructs a CuiTime object, uses Time.Parse to create a new temporary CuiTime object for 
        /// the given string and copies the property values from the temporary CuiTime object 
        /// to the new CuiTime object. 
        /// </summary>
        /// <param name="time">The time as a string.</param>
        public CuiTime(string time)
        {
            this.timeType = TimeType.Exact;
            this.timeValue = CuiTime.ParseExact(time, CultureInfo.CurrentCulture).timeValue;
        }

        /// <summary>
        /// Supports the CuiTimeTypeConverter. 
        /// </summary>
        /// <param name="time">
        /// The time as a string. 
        /// </param>
        /// <param name="parseAll">Parse all. </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "We need a new distinct CuiTime(string) constructor for use by the TypeConverter")]
        public CuiTime(string time, bool parseAll)
        {
            CuiTime newTime = CuiTime.ParseExact(time, CultureInfo.CurrentCulture);
            this.timeValue = newTime.timeValue;
            this.timeType = newTime.timeType;
            this.nullIndex = newTime.nullIndex;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the TimeType that represents the time. 
        /// </summary>
        /// <remarks>Defaults to TimeType.Exact. TimeType is always assigned a value explicitly; this is not implied by setting an 
        /// associated property. 
        /// </remarks>
        /// <value>Type of time.</value>
        public TimeType TimeType
        {
            get
            {
                return this.timeType;
            }

            set
            {
                this.timeType = value;
            }
        }

        /// <summary>
        /// Gets or sets a number identifying a null type. 
        /// </summary>
        /// <remarks>Defaults to -1. The index has no meaning in and of itself; the meaning is implied by the UI control.</remarks>
        /// <value>Null index.</value>
        public int NullIndex
        {
            get
            {
                return this.nullIndex;
            }

            set
            {
                if (value < MinimumNullIndex || value > MaximumNullIndex)
                {
                    throw new ArgumentOutOfRangeException("NullIndex", string.Format(CultureInfo.CurrentCulture, Resources.CuiTimeResources.NullIndexOutOfAcceptedRange, MinimumNullIndex, MaximumNullIndex));
                }

                this.nullIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the exact or approximate time value. 
        /// </summary>
        /// <remarks>Default is DateTime.Now. </remarks>
        /// <value>Time value.</value>
        public DateTime TimeValue
        {
            get
            {
                return this.timeValue;
            }

            set
            {
                this.timeValue = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> is null. 
        /// </summary>
        /// <remarks>
        /// True if TimeType is null; otherwise false. Read-only.
        /// </remarks>
        /// <value>Null status.</value>
        public bool IsNull
        {
            get
            {
                return (this.TimeType == TimeType.Null);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines whether the add instruction operand has a valid value. 
        /// </summary>
        /// <param name="value">The string used to check whether the add instruction operand has a valid value.</param>
        /// <returns>True if the add instruction operand has a valid value; otherwise, false. </returns>
        /// <remarks> The test performed is not dependent on the current setting of 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see>. This means true is returned if the operand is valid, 
        /// even if <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> is not Exact or Approximate. </remarks>
        /// <exception cref="System.ArgumentNullException">Time is null. </exception>
        public static bool IsAddValid(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Regex shortcut = new Regex(ResolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

            Match match = shortcut.Match(value.Replace(" ", string.Empty));

            return match.Success;
        }

        /// <summary>
        /// Parses a string that represents a time and returns a corresponding CuiTime object.
        /// </summary>
        /// <param name="time">The time to be parsed.</param>
        /// <returns>A CuiTime object. </returns>
        /// <exception cref="System.ArgumentNullException">Time is null. </exception>
        /// <exception cref="System.FormatException">Time is not in a recognised format.</exception>
        public static CuiTime ParseExact(string time)
        {
            return CuiTime.ParseExact(time, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Parses a string that represents a time and returns a corresponding CuiTime object.
        /// </summary>
        /// <param name="time">The time to be parsed.</param>
        /// <returns>A CuiTime object. </returns>
        /// <param name="cultureInfo">The culture that should be used to parse the string. If a string is culture-agnostic, 
        /// this should be CultureInfo.InvariantCulture. Defaults to CurrentCulture.</param>
        /// <exception cref="System.ArgumentNullException">Time is null. </exception>
        /// <exception cref="System.FormatException">Time is not in a recognised format.</exception>
        public static CuiTime ParseExact(string time, IFormatProvider cultureInfo)
        {
            CuiTime result;
            if (!TryParseExact(time, out result, cultureInfo))
            {
                throw new FormatException(Resources.CuiDateResources.ParseCalledWithBadFormat);
            }

            return result;
        }

        /// <summary>
        /// Parses a string that represents a time.
        /// </summary>
        /// <param name="time">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time. </param>
        /// <returns>True if time string was successfully parsed; otherwise, false. </returns>
        /// <remarks>If the string be parsed, the result parameter is set to a CuiTime object corresponding to the parsed timeString.
        /// If the time string cannot be parsed, the result parameter is set to DateTime.MinValue. </remarks>
        public static bool TryParseExact(string time, out CuiTime result)
        {
            return CuiTime.TryParseExact(time, out result, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Parses a string that represents a time.
        /// </summary>
        /// <param name="time">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time. </param>
        /// <param name="cultureInfo">The culture that should be used to parse the string. If a string is culture-agnostic, 
        /// this should be CultureInfo.InvariantCulture. Defaults to CurrentCulture.</param>
        /// <returns>True if time string was successfully parsed; otherwise, false. </returns>
        /// <remarks>If the string be parsed, the result parameter is set to a CuiTime object corresponding to the parsed timeString.
        /// If the time string cannot be parsed, the result parameter is set to DateTime.MinValue. </remarks>
        public static bool TryParseExact(string time, out CuiTime result, IFormatProvider cultureInfo)
        {
            result = null;

            // check for true null
            if (string.IsNullOrEmpty(time))
            {
                result = new CuiTime();
                result.TimeType = TimeType.Null;
                return true;
            }

            bool approxIndicatorPresent = false;

            // first check to see if approx indicator is present
            if (time.IndexOf(Resources.CuiTimeResources.Approximate, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                time = time.Replace(Resources.CuiTimeResources.Approximate, string.Empty).Trim();
                approxIndicatorPresent = true;
            }

            DateTime parsedDateTime;

            // try standard datetime parse with our custom formats
            GlobalizationService gs = new GlobalizationService();
            string[] formats = new string[] 
                               {
                                   gs.ShortTimePattern,
                                   gs.ShortTimePatternWithSeconds,
                                   gs.ShortTimePatternAMPM,
                                   gs.ShortTimePatternWithSecondsAMPM,
                                   gs.ShortTimePattern12Hour,
                                   gs.ShortTimePatternWithSeconds12Hour,
                                   gs.ShortTimePattern12HourAMPM,
                                   gs.ShortTimePatternWithSeconds12HourAMPM
                               };

            if (DateTime.TryParseExact(time, formats, cultureInfo, DateTimeStyles.None, out parsedDateTime))
            {
                result = new CuiTime(parsedDateTime, approxIndicatorPresent);
                return true;
            }

            if (!approxIndicatorPresent)
            {
                // Check if 'time' is a Null time
                Regex nullTimeRegEx = new Regex(@"^Null:(?<Index>-?\d+)$", RegexOptions.IgnoreCase);

                Match match = nullTimeRegEx.Match(time);

                if (match.Success == true)
                {
                    // Pull the numeric Index text and see if it is in range
                    int nullIndex;

                    if (int.TryParse(match.Groups[1].Captures[0].Value, out nullIndex) == true)
                    {
                        if (nullIndex >= MinimumNullIndex && nullIndex <= MaximumNullIndex)
                        {
                            result = new CuiTime();

                            result.NullIndex = nullIndex;
                            result.TimeType = TimeType.NullIndex;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a number of hours or minutes to a time and returns a new CuiTime object. 
        /// </summary>
        /// <param name="sourceTime">The time to be added to. </param>
        /// <param name="instruction">Add instructions such as +12h to add 12 hours or -20m to subtract 20 minutes. </param>
        /// <exception cref="System.ArgumentNullException">If either argument is null.</exception>
        /// <returns>A new CuiTime object.</returns>
        /// <remarks>
        /// The operand can be positive or negative; if the operand is not included, it is assumed to be positive. 
        /// </remarks>
        public static CuiTime Add(CuiTime sourceTime, string instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("instruction");
            }

            if (sourceTime == null)
            {
                throw new ArgumentNullException("sourceTime");
            }

            if (CuiTime.IsAddValid(instruction) == false)
            {
                throw new ArgumentOutOfRangeException("instruction", Resources.CuiTimeResources.AddInstructionInvalidFormat);
            }

            // check resources are valid
            if (Resources.CuiTimeResources.HoursUnit.Length == 0 || Resources.CuiTimeResources.MinutesUnit.Length == 0 ||
                    Resources.CuiTimeResources.HoursUnit == Resources.CuiTimeResources.MinutesUnit)
            {
                throw new InvalidArithmeticSetException(Resources.CuiTimeResources.ArithmeticSetInvalidResources);
            }

            Regex shortcut = new Regex(ResolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

            Match match = shortcut.Match(instruction.Replace(" ", string.Empty));

            if (match.Success == true)
            {
                // We need to skip the first group because with this RegEx it is always the complete string
                int groupNumber = 0;

                foreach (Group g in match.Groups)
                {
                    if (groupNumber > 0) 
                    {
                        foreach (Capture c in g.Captures)
                        {
                            int increment;

                            if (int.TryParse(c.Value.Substring(0, c.Value.Length - 1), out increment))
                            {
                                if (c.Value.EndsWith(Resources.CuiTimeResources.HoursUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    // Add hours
                                    sourceTime.TimeValue = sourceTime.TimeValue.AddHours(increment);
                                }
                                else if (c.Value.EndsWith(Resources.CuiTimeResources.MinutesUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    // Add minutes
                                    sourceTime.TimeValue = sourceTime.TimeValue.AddMinutes(increment);
                                }
                            }
                        }
                    }

                    groupNumber++;
                }
            }

            return sourceTime;
        }

        /// <summary>
        /// Gets the time format to use from the supplied globalization service.
        /// </summary>
        /// <param name="gs">Globalization service to get format from.</param>
        /// <param name="displaySeconds">Whether to display seconds.</param>
        /// <param name="display12Hour">Whether to use 12 hour or 24 hour clock.</param>
        /// <param name="displayAMPM">Whether to display am / pm indicator.</param>
        /// <returns>Time format.</returns>
        public static string GetTimeFormat(GlobalizationService gs, bool displaySeconds, bool display12Hour, bool displayAMPM)
        {
            string format;

            if (displaySeconds)
            {
                if (display12Hour)
                {
                    format = displayAMPM ? gs.ShortTimePatternWithSeconds12HourAMPM :
                                            gs.ShortTimePatternWithSeconds12Hour;
                }
                else
                {
                    format = displayAMPM ? gs.ShortTimePatternWithSecondsAMPM :
                                            gs.ShortTimePatternWithSeconds;
                }
            }
            else
            {
                if (display12Hour)
                {
                    format = displayAMPM ? gs.ShortTimePattern12HourAMPM :
                                            gs.ShortTimePattern12Hour;
                }
                else
                {
                    format = displayAMPM ? gs.ShortTimePatternAMPM :
                                            gs.ShortTimePattern;
                }
            }

            return format;
        }

        /// <summary>
        /// Adds a number of hours or minutes to a time. 
        /// </summary>
        /// <param name="instruction">Add instructions such as +12h to add 12 hours or -20m to subtract 20 minutes.</param>
        /// <exception cref="System.ArgumentNullException">Instruction is null.  </exception>
        /// // <remarks>
        /// The operand can be positive or negative; if the operand is not included, it is assumed to be positive.
        /// </remarks>
        public void Add(string instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("instruction");
            }

            this.TimeValue = CuiTime.Add(this, instruction).TimeValue;
        }        

        /// <summary>
        /// Determines whether the specified <see cref="System.DateTime"/> value is equal to this instance.
        /// </summary>
        /// <param name="dateTime">The <see cref="System.DateTime"/> to compare with this instance.</param>
        /// <returns>
        /// Returns	<c>true</c> if the specified <see cref="System.DateTime"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(DateTime dateTime)
        {
            return (int)this.timeValue.Subtract(dateTime).TotalSeconds == 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Microsoft.Cui.Controls.Common.DateAndTime.CuiTime"/> is equal to this instance.
        /// </summary>
        /// <param name="cuiTime">The <see cref="Microsoft.Cui.Controls.Common.DateAndTime.CuiTime"/> to compare with this instance.</param>
        /// <returns>
        /// Returns	<c>true</c> if the specified <see cref="Microsoft.Cui.Controls.Common.DateAndTime.CuiTime"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="cuiTime"/> parameter is null.</exception>
        public bool Equals(CuiTime cuiTime)
        {
            if (cuiTime == null)
            {
                throw new ArgumentNullException("cuiTime");
            }

            return this.Equals(cuiTime.TimeValue);
        }        

        /// <summary>
        /// Returns a string representing the time. 
        /// </summary>
        /// <returns>The time as a string. </returns>
        public override string ToString()
        {
            return this.ToString(this.TimeType == TimeType.Approximate);
        }

        /// <summary>
        /// Returns a string representing the time, with the Approx text indicator displayed if set. 
        /// </summary>
        /// <param name="approximate">When the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see> is 
        /// DateType.Approximate, shows the Approx text indicator. </param>
        /// <returns>The time as a string.
        /// </returns>
        public string ToString(bool approximate)
        {
            return this.ToString(
                    approximate,
                    CultureInfo.CurrentCulture,
                    false,
                    false,
                    false);
        }

        /// <summary>
        /// Returns a string representing the time.
        /// </summary>
        /// <param name="approximate">When the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTime.TimeType">TimeType</see>  
        /// is DateType.Approximate, shows the Approx text indicator. </param>
        /// <param name="cultureInfo">Culture info. </param>
        /// <param name="displaySeconds">Whether to display seconds. </param>
        /// <param name="display12Hour">Whether to use the 12-hour or 24-hour clock. </param>
        /// <param name="displayAMPM">Whether to display an AM/PM indicator.</param>
        /// <returns>The time as a string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "As specified")]
        public string ToString(bool approximate, CultureInfo cultureInfo, bool displaySeconds, bool display12Hour, bool displayAMPM)
        {
            if (approximate && this.TimeType != TimeType.Approximate)
            {
                // This is invalid. You cannot make use of the indicateWhenApproximate flag when the 
                // TimeType is not Approximate
                throw new ArgumentOutOfRangeException("approximate", Resources.CuiTimeResources.ShowApproxIndicatorInvalidForTimeType);
            }

            string formattedTime;

            switch (this.TimeType)
            {
                case TimeType.Exact:
                case TimeType.Approximate:
                    GlobalizationService gs = new GlobalizationService();

                    // we never return a string such as "14:04 (pm)"
                    bool reallyDisplayAMPM = displayAMPM && (display12Hour || this.TimeValue.TimeOfDay.Hours < 12);

                    string format = GetTimeFormat(gs, displaySeconds, display12Hour, reallyDisplayAMPM);

                    formattedTime = this.timeValue.ToString(format, cultureInfo);

                    if (reallyDisplayAMPM)
                    {
                        // make am / pm lowercase
                        formattedTime = formattedTime.ToLower(cultureInfo);
                    }

                    if (approximate)
                    {
                        // prepend "Approx" indicator
                        formattedTime = string.Format(
                                            cultureInfo,
                                            Resources.CuiTimeResources.ApproximateTimeFormat,
                                            Resources.CuiTimeResources.Approximate,
                                            formattedTime);
                    }

                    break;

                case TimeType.NullIndex:
                    formattedTime = string.Format(cultureInfo, "Null:{0}", this.NullIndex);
                    break;

                case TimeType.Null:
                    formattedTime = string.Empty;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            return formattedTime;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Resolves the tokens on the add instruction regex for the values in the resource file. 
        /// </summary>
        /// <returns>The add instruction regular expression with its tokens resolved.</returns>
        private static string ResolveAddInstructionRegExTokens()
        {
            string resolvedText;

            // I wanted to use string.Format but the {1,2} in the actual regEx was being mistaked for a formatting token

            // Hour token
            resolvedText = AddInstructionRegExFormat.Replace("#0#", Resources.CuiTimeResources.HoursUnit);

            // Minutes token
            resolvedText = resolvedText.Replace("#1#", Resources.CuiTimeResources.MinutesUnit);

            return resolvedText;
        }
        #endregion
    }
}
