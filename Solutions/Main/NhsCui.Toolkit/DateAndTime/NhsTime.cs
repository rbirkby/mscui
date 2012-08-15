//-----------------------------------------------------------------------
// <copyright file="NhsTime.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Represents an NHS time.  </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.ComponentModel;

    /// <summary>
    /// Represents an NHS time. 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(NhsTimeTypeConverter))]
    [Editor(typeof(NhsTimePropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class NhsTime
    {
        #region Const Values
        /// <summary>
        /// RegEx to enforce only digits.
        /// </summary>
        private const string NonDigitCharactersRegEx = @"\D";
        
        /// <summary>
        /// RegEx for finding the Time Add instructions 
        /// </summary>
        private const string AddInstructionRegExFormat = @"^([+-]?\d+[#0##1#])+?$";

        /// <summary>
        /// Maximum allowed NullIndex
        /// </summary>
        private const int MaximumNullIndex = 99;

        /// <summary>
        /// Minimum allowed NullIndex
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
        /// Constructs an NhsTime object. 
        /// </summary>
        public NhsTime()
        {
            this.timeValue = DateTime.Now;
            this.timeType = TimeType.Exact;
        }       

        /// <summary>
        /// Constructs an NhsTime object, initializes the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeValue">TimeValue</see> to the 
        /// given value and sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> to Exact. 
        /// </summary>
        /// <param name="time">The given time value. </param>
        public NhsTime(DateTime time)
        {
            this.timeValue = time;
            this.timeType = TimeType.Exact;
        }
        
        /// <summary>
        /// Constructs an NhsTime object, initializes the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeValue">TimeValue</see> to the given value 
        /// and sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> to Exact if approximate is false or 
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> to Approximate if approximate is true. 
        /// </summary>
        /// <param name="time">The given time value. </param>
        /// <param name="approximate">If true sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> to TimeType.Approximate; 
        /// otherwise, sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime">TimeType</see> to TimeType.Exact.</param>
        public NhsTime(DateTime time, bool approximate)
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
        /// Constructs an NhsTime object, uses Time.Parse to create a new temporary NhsTime object for 
        /// the given string and copies the property values from the temporary NhsTime object 
        /// to the new NhsTime object. 
        /// </summary>
        /// <param name="time">The time as a string.</param>
        public NhsTime(string time)
        {
            this.timeType = TimeType.Exact;
            this.timeValue = NhsTime.ParseExact(time, CultureInfo.CurrentCulture).timeValue;
        }

        /// <summary>
        /// Supports the NhsTimeTypeConverter. 
        /// </summary>
        /// <param name="time">The time as a string. 
        ///</param>
        /// <param name="parseAll">Parse all. </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "We need a new distinct NhsTime(string) constructor for use by the TypeConverter")]
        public NhsTime(string time, bool parseAll)
        {
            NhsTime newTime = NhsTime.ParseExact(time, CultureInfo.CurrentCulture);
            this.timeValue = newTime.timeValue;
            this.timeType = newTime.timeType;
            this.nullIndex = newTime.nullIndex;            
        }
        #endregion

        #region Public Properties       

        /// <summary>
        /// The TimeType that represents the time. 
        /// </summary>
        /// <remarks>Defaults to TimeType.Exact. TimeType is always assigned a value explicitly; this is not implied by setting an 
        /// associated property. 
        /// </remarks>
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
        /// A number identifying a null type. 
        /// </summary>
        /// <remarks>Defaults to -1. The index has no meaning in and of itself; the meaning is implied by the UI control.</remarks>
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
                    throw new ArgumentOutOfRangeException("NullIndex", string.Format(CultureInfo.CurrentCulture, Resources.NhsTimeResources.NullIndexOutOfAcceptedRange, MinimumNullIndex, MaximumNullIndex));
                }

                this.nullIndex = value;
            }
        }

        /// <summary>
        /// The exact or approximate time value. 
        /// </summary>
        /// <remarks>Default is DateTime.Now. </remarks>
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
        /// Indicates whether the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> is null. 
        /// </summary>
        /// <remarks>
        /// True if TimeType is null; otherwise false. Read-only.
        /// </remarks>
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
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see>. This means true is returned if the operand is valid, 
        /// even if <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> is not Exact or Approximate. </remarks>
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
        /// Parses a string that represents a time and returns a corresponding NhsTime object.
        /// </summary>
        /// <param name="time">The time to be parsed.</param>
        /// <returns>An NhsTime object. </returns>
        /// <exception cref="System.ArgumentNullException">Time is null. </exception>
        /// <exception cref="System.FormatException">Time is not in a recognised format.</exception>
        public static NhsTime ParseExact(string time)
        {
            return NhsTime.ParseExact(time, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Parses a string that represents a time and returns a corresponding NhsTime object.
        /// </summary>
        /// <param name="time">The time to be parsed.</param>
        /// <returns>An NhsTime object. </returns>
        /// <param name="cultureInfo">The culture that should be used to parse the string. If a string is culture-agnostic, 
        /// this should be CultureInfo.InvariantCulture. Defaults to CurrentCulture.</param>
        /// <exception cref="System.ArgumentNullException">Time is null. </exception>
        /// <exception cref="System.FormatException">Time is not in a recognised format.</exception>
        public static NhsTime ParseExact(string time, CultureInfo cultureInfo)
        {
            NhsTime result;
            if (!TryParseExact(time, out result, cultureInfo))
            {
                throw new FormatException(Resources.NhsDateResources.ParseCalledWithBadFormat);
            }

            return result;
        }

        /// <summary>
        /// Parses a string that represents a time.
        /// </summary>
        /// <param name="time">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time. </param>
        /// <returns>True if time string was successfully parsed; otherwise, false. </returns>
        /// <remarks>If the string be parsed, the result parameter is set to an NhsTime object corresponding to the parsed timeString.
        /// If the time string cannot be parsed, the result parameter is set to DateTime.MinValue. </remarks>
        public static bool TryParseExact(string time, out NhsTime result)
        {
            return NhsTime.TryParseExact(time, out result, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Parses a string that represents a time.
        /// </summary>
        /// <param name="time">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time. </param>
        /// <param name="cultureInfo">The culture that should be used to parse the string. If a string is culture-agnostic, 
        /// this should be CultureInfo.InvariantCulture. Defaults to CurrentCulture.</param>
        /// <returns>True if time string was successfully parsed; otherwise, false. </returns>
        /// <remarks>If the string be parsed, the result parameter is set to an NhsTime object corresponding to the parsed timeString.
        /// If the time string cannot be parsed, the result parameter is set to DateTime.MinValue. </remarks>
        public static bool TryParseExact(string time, out NhsTime result, CultureInfo cultureInfo)
        {
            result = null;

            // check for true null
            if (string.IsNullOrEmpty(time))
            {
                result = new NhsTime();
                result.TimeType = TimeType.Null;
                return true;
            }            
           
            bool approxIndicatorPresent = false;

            // first check to see if approx indicator is present
            if (time.IndexOf(Resources.NhsTimeResources.Approximate, StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                time = time.Replace(Resources.NhsTimeResources.Approximate, string.Empty).Trim();
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
                result = new NhsTime(parsedDateTime, approxIndicatorPresent);
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
                            result = new NhsTime();

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
        /// Adds a number of hours or minutes to a time and returns a new NhsTime object. 
        /// </summary>
        /// <param name="sourceTime">The time to be added to. </param>
        /// <param name="instruction">Add instructions such as +12h to add 12 hours or -20m to subtract 20 minutes. </param>
        /// <exception cref="System.ArgumentNullException">If either argument is null.</exception>
        /// <returns>A new NhsTime object.</returns>
        /// <remarks>
        /// The operand can be positive or negative; if the operand is not included, it is assumed to be positive. 
        /// </remarks>
        public static NhsTime Add(NhsTime sourceTime, string instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("instruction");
            }

            if (sourceTime == null)
            {
                throw new ArgumentNullException("sourceTime");
            }

            if (NhsTime.IsAddValid(instruction) == false)
            {
                throw new ArgumentOutOfRangeException("instruction", instruction, Resources.NhsTimeResources.AddInstructionInvalidFormat);
            }
            
            // check resources are valid
            if (Resources.NhsTimeResources.HoursUnit.Length == 0 || Resources.NhsTimeResources.MinutesUnit.Length == 0 ||
                    Resources.NhsTimeResources.HoursUnit == Resources.NhsTimeResources.MinutesUnit)
            {
                throw new InvalidArithmeticSetException(Resources.NhsTimeResources.ArithmeticSetInvalidResources);
            }

            Regex shortcut = new Regex(ResolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

            Match match = shortcut.Match(instruction.Replace(" ", string.Empty));

            if (match.Success == true)
            {
                // We need to skip the first group because with this RegEx it is always the complete string
                int groupNumber = 0;

                foreach (Group g in match.Groups)
                {
                    if (groupNumber > 0) // Skip first group
                    {
                        foreach (Capture c in g.Captures)
                        {
                            int increment;

                            if (int.TryParse(c.Value.Substring(0, c.Value.Length - 1), out increment))
                            {
                                if (c.Value.EndsWith(Resources.NhsTimeResources.HoursUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    // Add hours
                                    sourceTime.TimeValue = sourceTime.TimeValue.AddHours(increment);
                                }
                                else if (c.Value.EndsWith(Resources.NhsTimeResources.MinutesUnit, StringComparison.OrdinalIgnoreCase) == true)
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

            this.TimeValue = NhsTime.Add(this, instruction).TimeValue;
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
        /// Determines whether the specified <see cref="NhsCui.Toolkit.DateAndTime.NhsTime"/> is equal to this instance.
        /// </summary>
        /// <param name="nhsTime">The <see cref="NhsCui.Toolkit.DateAndTime.NhsTime"/> to compare with this instance.</param>
        /// <returns>
        /// Returns	<c>true</c> if the specified <see cref="NhsCui.Toolkit.DateAndTime.NhsTime"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="nhsTime"/> parameter is null.</exception>
        public bool Equals(NhsTime nhsTime)
        {
            if (nhsTime == null)
            {
                throw new ArgumentNullException("nhsTime");
            }

            return this.Equals(nhsTime.TimeValue);
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
        /// <param name="approximate">When the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see> is 
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
        /// <param name="approximate">When the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsTime.TimeType">TimeType</see>  
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
                throw new ArgumentOutOfRangeException("approximate", Resources.NhsTimeResources.ShowApproxIndicatorInvalidForTimeType);
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
                                            Resources.NhsTimeResources.ApproximateTimeFormat,
                                            Resources.NhsTimeResources.Approximate,
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

        /// <summary>
        /// Resolves the tokens on the add instruction regex for the values in the resource file. 
        /// </summary>
        /// <returns>The add instruction regular expression with its tokens resolved.</returns>
        private static string ResolveAddInstructionRegExTokens()
        {
            string resolvedText;

            // I wanted to use string.Format but the {1,2} in the actual regEx was being mistaked for a formatting token

            // Hour token
            resolvedText = AddInstructionRegExFormat.Replace("#0#", Resources.NhsTimeResources.HoursUnit);

            // Minutes token
            resolvedText = resolvedText.Replace("#1#", Resources.NhsTimeResources.MinutesUnit);

            return resolvedText;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the time format to use from the supplied globalization service.
        /// </summary>
        /// <param name="gs">globalization service to get format from</param>
        /// <param name="displaySeconds">whether to display seconds</param>
        /// <param name="display12Hour">whether to use 12 hour or 24 hour clock</param>
        /// <param name="displayAMPM">whether to display am / pm indicator</param>
        /// <returns>time format</returns>
        private static string GetTimeFormat(GlobalizationService gs, bool displaySeconds, bool display12Hour, bool displayAMPM)
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
        #endregion
    }
}
