//-----------------------------------------------------------------------
// <copyright file="CuiTimeSpan.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Represents a time span between two DateTimes. </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls.Common.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// Represents a time span between two DateTimes. 
    /// </summary>
    public class CuiTimeSpan
    {
        #region Const Values
        /// <summary>
        /// RegEx used to parse a time span string representation. 
        /// </summary>
        private const string TimeSpanParseRegExFormat = @"(?:\b(?<##GROUP##>\d+)\s*(?:##UNITLIST##)\b)?";
        #endregion

        #region Member Variables
        /// <summary>
        /// Flag that specifies whether the time span represents an age. 
        /// </summary>
        private bool valueIsAnAge;

        /// <summary>
        /// The granularity of the time span returned by ToString when IsAge is false. 
        /// </summary>
        private TimeSpanUnit granularity = TimeSpanUnit.Years;

        /// <summary>
        /// The threshold of the time span returned by ToString when IsAge is false. 
        /// </summary>
        private TimeSpanUnit threshold = TimeSpanUnit.Seconds;

        /// <summary>
        /// The granularity of the time span returned by ToString when IsAge is true.
        /// </summary>
        /// <remarks>This value is not surfaced publically. </remarks>
        private TimeSpanUnit granularityForAnAge = TimeSpanUnit.Years;

        /// <summary>
        /// The threshold of the time span returned by ToString when IsAge is true.
        /// </summary>
        /// <remarks>This value is not surfaced publically.</remarks>
        private TimeSpanUnit thresholdForAnAge = TimeSpanUnit.Minutes;

        /// <summary>
        /// Private store the actual TimeSpan.
        /// </summary>
        private System.TimeSpan internalClrTimeSpan = new System.TimeSpan();

        /// <summary>
        /// The number of whole months between the start and the end of the TimeSpan, once years has been taken into consideration. 
        /// </summary>
        private int calculatedWholeMonths;

        /// <summary>
        /// The total number of whole months that make up the time span. 
        /// </summary>
        /// <remarks>Unlike calculatedWholeMonths this will be 13 months for 1 year 1 month, calculatedWholeMonths would have been 1 month.</remarks>
        private int calculatedTotalMonths;

        /// <summary>
        /// The number of whole years between the start and end of the time span.
        /// </summary>
        private int calculatedWholeYears;

        /// <summary>
        /// The number of whole days between the start and the end of the time span, once years and months have been calculated.
        /// </summary>
        /// <remarks>
        /// See TotalDays for all the days. 
        /// </remarks>
        private int calculatedWholeDays;

        /// <summary>
        /// The DateTime that marks the beginning of the time span. 
        /// </summary>
        private DateTime dateFrom;

        /// <summary>
        /// DateTime that marks the end of the time span.
        /// </summary>
        private DateTime dateTo;

        /// <summary>
        /// Indicates whether DateFrom is later than DateTo.
        /// </summary>
        private bool isnegativeTimeSpan;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a CuiTimeSpan object which initializes the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.From">From</see> and 
        /// and <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.To">To</see> properties from DateTime.Now.     
        /// </summary>
        public CuiTimeSpan()
        {
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;

            this.SynchronizeInternalTimeSpanData();
        }

        /// <summary>
        /// Constructs a CuiTimeSpan object which initializes the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.From">From</see> property from the 
        /// From parameter and the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.To">To</see> property from DateTime.Now.     
        /// </summary>
        /// <param name="from">The DateTime the CuiTimeSpan starts from. </param>
        public CuiTimeSpan(DateTime from)
        {
            this.dateFrom = from;
            this.dateTo = DateTime.Now;

            this.SynchronizeInternalTimeSpanData();
        }

        /// <summary>
        /// Constructs a CuiTimeSpan object which initializes the <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.From">From </see> and 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.To">To</see> properties from the respective parameters. 
        /// </summary>
        /// <param name="from">The DateTime the CuiTimeSpan starts from. </param>
        /// <param name="to">The DateTime the CuiTimeSpan ends at.  </param>
        public CuiTimeSpan(DateTime from, DateTime to)
        {
            this.dateFrom = from;
            this.dateTo = to;

            this.SynchronizeInternalTimeSpanData();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the DateTime that marks the beginning of the time span. 
        /// </summary>
        /// <value>Start datetime.</value>
        public DateTime From
        {
            get
            {
                return this.dateFrom;
            }

            set
            {
                this.dateFrom = value;
                this.SynchronizeInternalTimeSpanData();
            }
        }

        /// <summary>
        /// Gets or sets the DateTime that marks the end of the time span. 
        /// </summary>
        /// <value>End Datetime.</value>
        public DateTime To
        {
            get
            {
                return this.dateTo;
            }

            set
            {
                this.dateTo = value;
                this.SynchronizeInternalTimeSpanData();
            }
        }

        /// <summary>
        /// Gets the number of whole years. 
        /// </summary>
        /// <value>Years value.</value>
        public int Years
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.calculatedWholeYears * -1;
                }

                return this.calculatedWholeYears;
            }
        }

        /// <summary>
        /// Gets the number of whole months. 
        /// </summary>
        /// <value>Month value.</value>
        public int Months
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.calculatedWholeMonths * -1;
                }

                return this.calculatedWholeMonths;
            }
        }

        /// <summary>
        /// Gets the current span of time expressed as total whole months. 
        /// </summary>
        /// <remarks>A span of 1 year and 1 month would have a TotalMonths value of 13, whereas 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Months">Months</see> would be 1.</remarks>
        /// <value>Total months.</value>
        public int TotalMonths
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.calculatedTotalMonths * -1;
                }

                return this.calculatedTotalMonths;
            }
        }

        /// <summary>
        /// Gets the current span of time expressed as total whole weeks.
        /// </summary>
        /// <remarks>A span of 1 year and 1 week would have a TotalWeeks value of 53, whereas 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Weeks">Weeks </see> would be 1.</remarks>
        /// <value>Total weeks.</value>
        public int TotalWeeks
        {
            get
            {
                // Only under one of the previous conditions is TotalWeeks valid. Return it;
                if (this.WeeksAreRelevant)
                {
                    int totalWeeks = (int)Math.Floor(this.internalClrTimeSpan.TotalDays / 7d);

                    // Return negative values in case of DateFrom is later than DateTo
                    if (this.isnegativeTimeSpan)
                    {
                        return totalWeeks * -1;
                    }

                    return totalWeeks;
                }
                else
                {
                    // Weeks is irrelevant so return 0
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the number of whole weeks.
        /// </summary>
        /// <value>Weeks value.</value>
        public int Weeks
        {
            get
            {
                if (this.WeeksAreRelevant)
                {
                    int weeks = (int)Math.Floor(this.Days / 7d);

                    // Return negative values in case of DateFrom is later than DateTo
                    if (this.isnegativeTimeSpan)
                    {
                        return weeks * -1;
                    }

                    return weeks;
                }
                else
                {
                    // Weeks is irrelevant so return 0
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the number of whole days.   
        /// </summary>
        /// <remarks> 
        /// This value is read-only.   
        /// </remarks>
        /// <value>
        /// Days value.
        /// </value>
        public int Days
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.calculatedWholeDays * -1;
                }

                return this.calculatedWholeDays;
            }
        }

        /// <summary>
        /// Gets the number of whole hours.
        /// </summary>
        /// <remarks> 
        /// This value is read-only. 
        /// </remarks>
        /// <value>Hours value.</value>
        public double Hours
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.internalClrTimeSpan.Hours * -1;
                }

                return this.internalClrTimeSpan.Hours;
            }
        }

        /// <summary>
        /// Gets the number of whole minutes. 
        /// </summary>
        /// <remarks>
        /// This value is read-only.
        /// </remarks>
        /// <value>
        /// Minutes value.
        /// </value>
        public double Minutes
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.internalClrTimeSpan.Minutes * -1;
                }

                return this.internalClrTimeSpan.Minutes;
            }
        }

        /// <summary>
        /// Gets the number of whole seconds. 
        /// </summary>
        /// <remarks>
        /// This value is read-only. 
        /// </remarks>
        /// <value>Seconds value.</value>
        public double Seconds
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.internalClrTimeSpan.Seconds * -1;
                }

                return this.internalClrTimeSpan.Seconds;
            }
        }

        /// <summary>
        /// Gets the number of whole milliseconds. 
        /// </summary>
        ///  <remarks>
        /// This value is read-only. 
        /// </remarks>
        /// <value>Milli seconds.</value>
        public double Milliseconds
        {
            get
            {
                // Return negative values in case of DateFrom is later than DateTo
                if (this.isnegativeTimeSpan)
                {
                    return this.internalClrTimeSpan.Milliseconds * -1;
                }

                return this.internalClrTimeSpan.Milliseconds;
            }
        }

        /// <summary>
        /// Gets or sets the granularity of the time span returned by <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.ToString">ToString</see> when 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is false.
        /// </summary>
        /// <remarks>Defaults to TimeSpanUnit.Years. </remarks>
        /// <value>Granularity value.</value>
        public TimeSpanUnit Granularity
        {
            get
            {
                return this.granularity;
            }

            set
            {
                this.granularity = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a time span represents an age. 
        /// </summary>
        /// <value>Is age value.</value>
        public bool IsAge
        {
            get
            {
                return this.valueIsAnAge;
            }

            set
            {
                this.valueIsAnAge = value;

                if (value == true)
                {
                    this.FixGranularityAndThreshold();
                }
            }
        }

        /// <summary>
        /// Gets or sets the threshold of the time span returned by <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.ToString">ToString</see> when 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is false. 
        /// </summary>  
        /// <remarks>Defaults to TimeSpanUnit.Seconds. </remarks>
        /// <value>Threshold value.</value>
        public TimeSpanUnit Threshold
        {
            get
            {
                return this.threshold;
            }

            set
            {
                this.threshold = value;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets a value indicating whether the number of complete weeks represented by the time span. 
        /// </summary>
        /// <remarks>
        /// The Weeks property is a wrapper for the Days property; it does not have its own internal storage. This means an assignment to Weeks
        /// is equivalent to an assignment to <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Days">Days</see> multiplied by 7. 
        /// </remarks>
        private bool WeeksAreRelevant
        {
            get
            {
                return (this.IsAge == true || this.Granularity == TimeSpanUnit.Weeks || this.Threshold == TimeSpanUnit.Weeks);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parses a string that represents a time span and returns a corresponding CuiTimeSpan object. 
        /// </summary>
        /// <param name="timeSpan">The time span to be parsed. </param>
        /// <returns>A new CuiTimeSpan object. </returns>
        /// <exception cref="System.ArgumentNullException">The timeSpan is null. </exception>
        /// <exception cref="System.FormatException">The timeSpan is not in a recognised format.</exception>
        public static CuiTimeSpan Parse(string timeSpan)
        {
            return Parse(timeSpan, CultureInfo.InvariantCulture, false);
        }

        /// <summary>
        /// Parses a string that represents a time span and returns a corresponding CuiTimeSpan object. 
        /// </summary>
        /// <param name="timeSpan">The time span to be parsed. </param>
        /// <param name="age">Specifies whether the timespan should be considered as an age or not.</param>
        /// <returns>A new CuiTimeSpan object. </returns>
        /// <exception cref="System.ArgumentNullException">The timeSpan is null. </exception>
        /// <exception cref="System.FormatException">The timeSpan is not in a recognised format.</exception>
        public static CuiTimeSpan Parse(string timeSpan, bool age)
        {
            return Parse(timeSpan, CultureInfo.InvariantCulture, age);
        }        

        /// <summary>
        /// Parses a string that represents a time span and returns a corresponding CuiTimeSpan object, with CultureInfo used to parse the string. 
        /// </summary>
        /// <param name="timeSpan">The time span to be parsed. </param>
        /// <param name="cultureInfo">The CultureInfo used to parse the string. </param>
        /// <returns>A new CuiTimeSpan object.</returns>
        /// <param name="age">Specifies whether the timespan should be considered as an age or not.</param>
        /// <exception cref="System.ArgumentNullException">timeSpan is null. </exception>
        /// <exception cref="System.FormatException">timeSpan is not in a recognised format. </exception>
        public static CuiTimeSpan Parse(string timeSpan, CultureInfo cultureInfo, bool age)
        {
            CuiTimeSpan result;

            if (!TryParse(timeSpan, out result, age))
            {
                throw new FormatException(Resources.CuiTimeSpanResources.ParseCalledWithBadFormat);
            }

            return result;
        }

        /// <summary>
        /// Parses a string that represents a time span.
        /// </summary>
        /// <param name="timeSpan">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time span. </param>
        /// <returns>True if the value was successfully parsed; otherwise, false. </returns>
        /// <remarks>
        /// If the string could be parsed, the result parameter is set to an 
        /// CuiTimeSpan object corresponding to the parsed timeSpanString. If it could not be parsed, the result is set to null.
        /// </remarks>
        public static bool TryParse(string timeSpan, out CuiTimeSpan result)
        {
            return TryParse(timeSpan, out result, false);
        }       

        /// <summary>
        ///  Parses a string that represents a time span, with CultureInfo used to parse the string. 
        /// </summary>
        /// <param name="timeSpan">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed time span. </param>
        /// <param name="age">Specifies whether the timespan should be considered as an age or not.</param>
        /// <returns>True if the value was successfully converted; otherwise, false. .</returns>
        /// /// <remarks>
        /// If the string could be parsed, the result parameter is set to an 
        /// CuiTimeSpan object corresponding to the parsed timeSpanString. If it could not be parsed, the result is set to null.
        /// </remarks>
        public static bool TryParse(string timeSpan, out CuiTimeSpan result, bool age)
        {
            if (timeSpan == null)
            {
                throw new ArgumentNullException("timeSpan");
            }

            string parseExpression = BuildParseRegularExpression();
            Regex regEx = new Regex(parseExpression);
            Match m = regEx.Match(timeSpan);
            if (m.Success)
            {
                DateTime from = DateTime.Now;
                DateTime to = from;
                DateTime span = from;
                long value;

                if (ParseGroupIntegerValue(m.Groups["seconds"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddSeconds(value);
                }

                if (ParseGroupIntegerValue(m.Groups["minutes"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddMinutes(value);
                }

                if (ParseGroupIntegerValue(m.Groups["hours"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddHours(value);
                }

                if (ParseGroupIntegerValue(m.Groups["weeks"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddDays(value * 7);
                }

                if (ParseGroupIntegerValue(m.Groups["days"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddDays(value);
                }

                if (ParseGroupIntegerValue(m.Groups["months"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddMonths((int)value);
                }

                if (ParseGroupIntegerValue(m.Groups["years"], out value))
                {
                    value = age ? value * -1 : value;
                    span = span.AddYears((int)value);
                }

                if (age)
                {
                    from = span;
                }
                else
                {
                    to = span;
                }

                result = new CuiTimeSpan(from, to);
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Returns a string that represents a time span. 
        /// </summary>
        /// <returns>
        /// A string that represents the CuiTimeSpan Object. 
        /// </returns>
        /// <remarks>
        /// The string returned by ToString is dependent on the granularity and threshold of the time span. 
        /// If <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is true, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the time span. If 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is false, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the assigned Granularity value. 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see>
        /// may be seconds, minutes, weeks, hours, days, 
        /// weeks, months or years. 
        /// </remarks>
        public override string ToString()
        {
            return this.ToString(TimeSpanUnitLength.Short, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string that represents a time span, with the length of units to be used specified. 
        /// </summary>
        /// <param name="unitLength">The length of the units to use.</param>
        /// <returns>
        /// A string that represents the CuiTimeSpan Object. 
        /// </returns>
        /// <remarks>
        /// The string returned by ToString is dependent on the granularity and threshold of the time span. 
        /// If <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is true, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the time span. If 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is false, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the assigned Granularity value. 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see>
        /// may be seconds, minutes, weeks, hours, days, 
        /// weeks, months or years. 
        /// </remarks>
        public string ToString(TimeSpanUnitLength unitLength)
        {
            return this.ToString(unitLength, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///  Returns a string that represents a time span, with CultureInfo used to parse the string. 
        /// </summary>
        /// <param name="unitLength">Length of the units to use.</param>
        /// <param name="culture">Culture to use for formatting.</param>
        /// <returns>
        /// A string that represents the CuiTimeSpan Object.
        /// </returns>
        /// <remarks>
        /// The string returned by ToString is dependent on the granularity and threshold of the time span. 
        /// If <see cref="P:Microsoft.Cui.Controls.Common.Web.CuiTimeSpan.IsAge">IsAge</see> is true, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the time span. If 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.IsAge">IsAge</see> is false, 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see> is set from the assigned Granularity value. 
        /// <see cref="P:Microsoft.Cui.Controls.Common.DateAndTime.CuiTimeSpan.Granularity">Granularity</see>
        /// may be seconds, minutes, weeks, hours, days, 
        /// weeks, months or years. 
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "As specified")]
        public string ToString(TimeSpanUnitLength unitLength, CultureInfo culture)
        {
            StringBuilder workingToStringValue = new StringBuilder();
            bool longUnits;

            // decide whether to use long or short units
            if (unitLength == TimeSpanUnitLength.Automatic)
            {
                longUnits = this.IsAge && this.Years >= 2;
            }
            else
            {
                longUnits = (unitLength == TimeSpanUnitLength.Long);
            }

            // relevantGranularity holds the Granularity that is relevant for the current state of CuiTimeSpan (see if block below)
            TimeSpanUnit relevantGranularity;

            // relevantThreshold holds the Threshold that is relevant for the current state of CuiTimeSpan (see if block below)
            TimeSpanUnit relevantThreshold;

            if (this.IsAge == true)
            {
                relevantGranularity = this.granularityForAnAge;
                relevantThreshold = this.thresholdForAnAge;
            }
            else
            {
                relevantGranularity = this.Granularity;
                relevantThreshold = this.Threshold;
            }

            for (int timeSpanUnit = (int)relevantGranularity; timeSpanUnit >= (int)TimeSpanUnit.Seconds; timeSpanUnit--)
            {
                // Get the relevant part of the TimeSpan, flag up if the is the beginning part of the the ToString()
                long unitValue = this.GetToStringPartValue(timeSpanUnit == (int)relevantGranularity, (TimeSpanUnit)timeSpanUnit, relevantGranularity);

                if (unitValue < 0)
                {
                    unitValue = unitValue * -1;
                }

                if (unitValue > 0)
                {
                    // if necessary append a leading space
                    if (workingToStringValue.Length > 0)
                    {
                        workingToStringValue.Append(" ");
                    }

                    // Get the necessary unit text such as 'd' for days
                    string timeSpanUnitsString = GetTimeSpanUnitsString(unitValue, (TimeSpanUnit)timeSpanUnit, longUnits);

                    workingToStringValue.Append(unitValue.ToString(culture));
                    workingToStringValue.Append(timeSpanUnitsString);
                }

                if (timeSpanUnit == (int)relevantThreshold)
                {
                    break;
                }
            }

            // Negate the unit values if the dateFrom is later than dateTo
            if (this.isnegativeTimeSpan && !string.IsNullOrEmpty(workingToStringValue.ToString()))
            {
                return string.Format(CultureInfo.CurrentCulture, "-({0})", workingToStringValue.ToString());
            }

            return workingToStringValue.ToString();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Builds the RegEx that allows us to parse a time span string. 
        /// </summary>
        /// <returns>Regular expression string.</returns>
        private static string BuildParseRegularExpression()
        {
            StringBuilder regEx = new StringBuilder();
            regEx.Append(@"^");
            regEx.Append(BuildUnitRegEx(
                                    "years",
                                    Resources.CuiTimeSpanResources.YearsUnit,
                                    Resources.CuiTimeSpanResources.YearUnit,
                                    Resources.CuiTimeSpanResources.YearsLongUnit,
                                    Resources.CuiTimeSpanResources.YearLongUnit));
            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "months",
                                    Resources.CuiTimeSpanResources.MonthsUnit,
                                    Resources.CuiTimeSpanResources.MonthUnit,
                                    Resources.CuiTimeSpanResources.MonthsLongUnit,
                                    Resources.CuiTimeSpanResources.MonthLongUnit));
            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "weeks",
                                    Resources.CuiTimeSpanResources.WeeksUnit,
                                    Resources.CuiTimeSpanResources.WeekUnit,
                                    Resources.CuiTimeSpanResources.WeeksLongUnit,
                                    Resources.CuiTimeSpanResources.WeekLongUnit));
            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "days",
                                    Resources.CuiTimeSpanResources.DaysUnit,
                                    Resources.CuiTimeSpanResources.DayUnit,
                                    Resources.CuiTimeSpanResources.DaysLongUnit,
                                    Resources.CuiTimeSpanResources.DayLongUnit));
            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "hours",
                                    Resources.CuiTimeSpanResources.HoursUnit,
                                    Resources.CuiTimeSpanResources.HourUnit,
                                    Resources.CuiTimeSpanResources.HoursLongUnit,
                                    Resources.CuiTimeSpanResources.HourLongUnit));

            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "minutes",
                                    Resources.CuiTimeSpanResources.MinutesUnit,
                                    Resources.CuiTimeSpanResources.MinuteUnit,
                                    Resources.CuiTimeSpanResources.MinutesLongUnit,
                                    Resources.CuiTimeSpanResources.MinuteLongUnit));
            regEx.Append(@"\s*");
            regEx.Append(BuildUnitRegEx(
                                    "seconds",
                                    Resources.CuiTimeSpanResources.SecondsUnit,
                                    Resources.CuiTimeSpanResources.SecondUnit,
                                    Resources.CuiTimeSpanResources.SecondsLongUnit,
                                    Resources.CuiTimeSpanResources.SecondLongUnit));

            regEx.Append(@"$");
            return regEx.ToString();
        }

        /// <summary>
        /// Parse integer from a regex group.
        /// </summary>
        /// <param name="g">Group to parse.</param>
        /// <param name="value">Result value.</param>
        /// <returns>True group had a value.</returns>
        private static bool ParseGroupIntegerValue(Group g, out long value)
        {
            bool hasValue = (g != null && !string.IsNullOrEmpty(g.Value));

            if (hasValue)
            {
                // if the group has a value it must be an integer
                value = long.Parse(g.Value, NumberStyles.Number, CultureInfo.CurrentCulture);
            }
            else
            {
                value = 0;
            }

            return hasValue;
        }

        /// <summary>
        /// Build reg ex to match single time span units.
        /// </summary>
        /// <param name="groupName">Regex group name.</param>
        /// <param name="unitNames">Names used for unit.</param>
        /// <returns>Reg ex value.</returns>
        private static string BuildUnitRegEx(string groupName, params string[] unitNames)
        {
            string regEx = TimeSpanParseRegExFormat.Replace("##GROUP##", groupName);
            regEx = regEx.Replace("##UNITLIST##", string.Join("|", unitNames));
            return regEx;
        }

        /// <summary>
        /// Gets the TimeSpanUnit's text.
        /// </summary>
        /// <param name="value">The value of the TimeSpanUnit. </param>
        /// <param name="timeSpanUnit">The TimeSpanUnit, TimeSpanUnits.Minutes.</param>
        /// <returns>The text for the TimeSpanUnit. This is  e.g. 'd' for TimeSpanUnits.Days.</returns>
        /// <param name="longUnits">If true use long units rather than short ones.</param>
        private static string GetTimeSpanUnitsString(long value, TimeSpanUnit timeSpanUnit, bool longUnits)
        {
            if (longUnits)
            {
                switch (timeSpanUnit)
                {
                    case TimeSpanUnit.Years:
                        return (value > 1 ? Resources.CuiTimeSpanResources.YearsLongUnit : Resources.CuiTimeSpanResources.YearLongUnit);
                    case TimeSpanUnit.Months:
                        return (value > 1 ? Resources.CuiTimeSpanResources.MonthsLongUnit : Resources.CuiTimeSpanResources.MonthLongUnit);
                    case TimeSpanUnit.Weeks:
                        return (value > 1 ? Resources.CuiTimeSpanResources.WeeksLongUnit : Resources.CuiTimeSpanResources.WeekLongUnit);
                    case TimeSpanUnit.Days:
                        return (value > 1 ? Resources.CuiTimeSpanResources.DaysLongUnit : Resources.CuiTimeSpanResources.DayLongUnit);
                    case TimeSpanUnit.Hours:
                        return (value > 1 ? Resources.CuiTimeSpanResources.HoursLongUnit : Resources.CuiTimeSpanResources.HourLongUnit);
                    case TimeSpanUnit.Minutes:
                        return (value > 1 ? Resources.CuiTimeSpanResources.MinutesLongUnit : Resources.CuiTimeSpanResources.MinuteLongUnit);
                    case TimeSpanUnit.Seconds:
                        return (value > 1 ? Resources.CuiTimeSpanResources.SecondsLongUnit : Resources.CuiTimeSpanResources.SecondLongUnit);
                }
            }
            else
            {
                switch (timeSpanUnit)
                {
                    case TimeSpanUnit.Years:
                        return (value > 1 ? Resources.CuiTimeSpanResources.YearsUnit : Resources.CuiTimeSpanResources.YearUnit);
                    case TimeSpanUnit.Months:
                        return (value > 1 ? Resources.CuiTimeSpanResources.MonthsUnit : Resources.CuiTimeSpanResources.MonthUnit);
                    case TimeSpanUnit.Weeks:
                        return (value > 1 ? Resources.CuiTimeSpanResources.WeeksUnit : Resources.CuiTimeSpanResources.WeekUnit);
                    case TimeSpanUnit.Days:
                        return (value > 1 ? Resources.CuiTimeSpanResources.DaysUnit : Resources.CuiTimeSpanResources.DayUnit);
                    case TimeSpanUnit.Hours:
                        return (value > 1 ? Resources.CuiTimeSpanResources.HoursUnit : Resources.CuiTimeSpanResources.HourUnit);
                    case TimeSpanUnit.Minutes:
                        return (value > 1 ? Resources.CuiTimeSpanResources.MinutesUnit : Resources.CuiTimeSpanResources.MinuteUnit);
                    case TimeSpanUnit.Seconds:
                        return (value > 1 ? Resources.CuiTimeSpanResources.SecondsUnit : Resources.CuiTimeSpanResources.SecondUnit);
                }
            }

            throw new ArgumentOutOfRangeException("timeSpanUnit");
        }

        /// <summary>
        /// Gets part of the TimeSpan data used to make up a string representation of the current TimeSpan.
        /// </summary>
        /// <param name="beginningOfToString">Is this part the first part in the string representation.</param>
        /// <param name="timeSpanUnit">The part of the TimeSpan required.</param>
        /// <param name="relevantGranularity">Relevant granularity.</param>
        /// <returns>A numeric value for the part.</returns>
        private long GetToStringPartValue(bool beginningOfToString, TimeSpanUnit timeSpanUnit, TimeSpanUnit relevantGranularity)
        {
            switch (timeSpanUnit)
            {
                case TimeSpanUnit.Years:
                    return this.Years;
                case TimeSpanUnit.Months:
                    if (beginningOfToString == true)
                    {
                        return this.TotalMonths;
                    }
                    else
                    {
                        return this.Months;
                    }

                case TimeSpanUnit.Weeks:
                    if (beginningOfToString == true)
                    {
                        return this.TotalWeeks;
                    }
                    else
                    {
                        return this.Weeks;
                    }

                case TimeSpanUnit.Days:
                    if (beginningOfToString == true)
                    {
                        return (long)this.internalClrTimeSpan.TotalDays;
                    }
                    else if (this.WeeksAreRelevant == false)
                    {
                        // If weeks are not relevant then they will have not been 
                        // used as part of the ToString so use Days rather than calculating the remaining
                        return this.Days;
                    }
                    else
                    {
                        if (relevantGranularity == TimeSpanUnit.Weeks)
                        {
                            return (long)this.internalClrTimeSpan.TotalDays % 7;
                        }
                        else
                        {
                            // The remaining days once weeks have been taken into consideration.

                            // Of course we know that we only want remaining days because this is 
                            // not beginningOfToString so Weeks have already been read off
                            return this.Days % 7;
                        }
                    }

                case TimeSpanUnit.Hours:
                    if (beginningOfToString == true)
                    {
                        return (long)this.internalClrTimeSpan.TotalHours;
                    }
                    else
                    {
                        return (long)this.Hours;
                    }

                case TimeSpanUnit.Minutes:
                    if (beginningOfToString == true)
                    {
                        return (long)this.internalClrTimeSpan.TotalMinutes;
                    }
                    else
                    {
                        return (long)this.Minutes;
                    }

                case TimeSpanUnit.Seconds:
                    if (beginningOfToString == true)
                    {
                        return (long)this.internalClrTimeSpan.TotalSeconds;
                    }
                    else
                    {
                        return (long)this.Seconds;
                    }
            }

            throw new ArgumentOutOfRangeException("timeSpanUnit");
        }

        /// <summary>
        /// The backing store for the actual span of time is a System.TimeSpan.
        /// </summary>
        private void SynchronizeInternalTimeSpanData()
        {
            this.internalClrTimeSpan = this.dateTo.Subtract(this.dateFrom);

            this.CalculateYearsMonthsAndDays();

            if (this.IsAge == true)
            {
                // Set the Granularity and Threshold rules for the new time span
                this.FixGranularityAndThreshold();
            }
        }

        /// <summary>
        /// If the TimeSpan represents an Age, the Granularity and Threshold values must be set to an agreed combination of values. 
        /// </summary>
        private void FixGranularityAndThreshold()
        {
            if (this.internalClrTimeSpan.TotalHours < 2)
            {
                this.granularityForAnAge = TimeSpanUnit.Minutes;
                this.thresholdForAnAge = TimeSpanUnit.Minutes;
            }
            else if (this.internalClrTimeSpan.TotalDays < 2)
            {
                this.granularityForAnAge = TimeSpanUnit.Hours;
                this.thresholdForAnAge = TimeSpanUnit.Hours;
            }
            else if (this.TotalWeeks < 4)
            {
                this.granularityForAnAge = TimeSpanUnit.Days;
                this.thresholdForAnAge = TimeSpanUnit.Days;
            }
            else if (this.Years < 1)
            {
                this.granularityForAnAge = TimeSpanUnit.Weeks;
                this.thresholdForAnAge = TimeSpanUnit.Days;
            }
            else if (this.Years < 2)
            {
                this.granularityForAnAge = TimeSpanUnit.Months;
                this.thresholdForAnAge = TimeSpanUnit.Days;
            }
            else if (this.Years < 18)
            {
                this.granularityForAnAge = TimeSpanUnit.Years;
                this.thresholdForAnAge = TimeSpanUnit.Months;
            }
            else if (this.Years >= 18)
            {
                this.granularityForAnAge = TimeSpanUnit.Years;
                this.thresholdForAnAge = TimeSpanUnit.Years;
            }
        }

        /// <summary>
        /// Based on the fromDate and toDate calulate the whole years, whole months and whole days.
        /// </summary>
        private void CalculateYearsMonthsAndDays()
        {
            DateTime dateFrom = this.dateFrom;
            DateTime dateTo = this.dateTo;

            if (this.dateFrom.Ticks > this.dateTo.Ticks)
            {
                this.isnegativeTimeSpan = true;
                dateFrom = this.dateTo;
                dateTo = this.dateFrom;
            }
            else
            {
                this.isnegativeTimeSpan = false;
                dateFrom = this.dateFrom;
                dateTo = this.dateTo;
            }

            this.calculatedWholeYears = dateTo.Year - dateFrom.Year;

            if (dateTo.Month > dateFrom.Month || (dateTo.Month == dateFrom.Month && dateTo.Day >= dateFrom.Day))
            {
                this.calculatedWholeMonths = dateTo.Month - dateFrom.Month;
            }
            else
            {
                this.calculatedWholeYears--;
                this.calculatedWholeMonths = (12 - dateFrom.Month) + dateTo.Month;
            }

            if (dateTo.Day >= dateFrom.Day)
            {
                this.calculatedWholeDays = dateTo.Day - dateFrom.Day;
            }
            else
            {
                // case to handle month transitions for e.g. 31/07/07 - 30/08/07 is one month.
                if (dateTo.Day == DateTime.DaysInMonth(dateTo.Year, dateTo.Month) && dateFrom.Day == DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month))
                {
                    this.calculatedWholeDays = 0;
                }
                else
                {
                    this.calculatedWholeMonths--;
                    this.calculatedWholeDays = (DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month) - dateFrom.Day) + dateTo.Day;
                }
            }

            if (dateTo.Hour < dateFrom.Hour)
            {
                // Not a full day so subtract fom the calculated days
                this.calculatedWholeDays--;
            }

            this.calculatedTotalMonths = (this.calculatedWholeYears * 12) + this.calculatedWholeMonths;
        }
        #endregion
    }
}
