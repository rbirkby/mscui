//-----------------------------------------------------------------------
// <copyright file="NhsDate.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Represents dates used within the NHS.</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using System.ComponentModel;

    /// <summary>
    /// Represents an NHS date. 
    /// </summary>
    /// <remarks>
    /// An NHS date may be exact, approximate, a month and year, a year, a null index or a null value.   
    /// </remarks>
    [Serializable, TypeConverter(typeof(NhsDateTypeConverter))]
    [Editor(typeof(NhsDatePropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class NhsDate
    {
        #region Const Values
        /// <summary>
        /// RegEx to enforce only digits.
        /// </summary>
        private const string NonDigitCharactersRegEx = @"\D";

        /// <summary>
        /// Minimum allowed month.
        /// </summary>
        private const int MinimumMonth = 1;

        /// <summary>
        /// Maximum allowed month.
        /// </summary>
        private const int MaximumMonth = 12;

        /// <summary>
        /// Minimum allowed year.
        /// </summary>
        private const int MinimumYear = 0;

        /// <summary>
        /// Minimum allowed NullIndex.
        /// </summary>
        private const int MinimumNullIndex = -1;

        /// <summary>
        /// Maximum allowed NullIndex.
        /// </summary>
        private const int MaximumNullIndex = 99;

        /// <summary>
        /// RegEx for finding the date. Add instructions.
        /// </summary>
        private const string AddInstructionRegExFormat = @"^([+-]?\d{1,2}[#0##1##2##3#])+?$"; // @"^([+-]?\d{1,2}[dmwyDMWY])+?$"
        #endregion

        #region Member Variables
        /// <summary>
        /// The DateType that represents the date. Defaults to DateType.Exact. 
        /// </summary>
        private DateType dateType = DateType.Exact;
        
        /// <summary>
        /// The exact or approximate date value. 
        /// </summary>
        /// <remarks>
        /// Defaults to DateTime.Now. 
        /// </remarks>
        private DateTime dateValue = DateTime.Now;
        
        /// <summary>
        /// The month in a DateType.YearMonth date. 
        /// </summary>
        /// <remarks>Defaults to 1.  </remarks>
        private int month = 1;                       
        
        /// <summary>
        /// The year in a DateType.Year or DateType.YearMonth. 
        /// </summary>
        /// <remarks>Defaults to 0.  </remarks>
        private int year;
        
        /// <summary>
        /// A number identifying a null type. The index has no meaning in and of itself; the meaning is implied by the UI control.
        /// </summary>
        /// <remarks>Defaults to -1. </remarks>
        private int nullIndex = -1;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an NhsDate object. 
        /// </summary>
        public NhsDate()
        {
        }

        /// <summary>
        /// Constructs an NhsDate object, initializes the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateValue">DateValue</see> to 
        /// the given value and sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to Exact. 
        /// </summary>
        /// <param name="date">The date. </param>
        public NhsDate(DateTime date)
        {
            this.DateValue = date;
            this.DateType = DateType.Exact;
        }

        /// <summary>
        /// Constructs an NhsDate object, initializes the
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateValue">DateValue</see>
        /// to the given value and sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to Exact if approximate is false 
        /// or sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to Approximate if approximate is true. 
        /// </summary>
        /// <param name="date">The given date value.</param>
        /// <param name="approximate">If true sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to DateType.Approximate; 
        /// otherwise, sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to DateType.Exact.</param>
        public NhsDate(DateTime date, bool approximate)
        {
            this.DateValue = date;

            if (approximate)
            {
                this.DateType = DateType.Approximate;
            }
            else
            {
                this.DateType = DateType.Exact;
            }
        }     

        /// <summary>
        /// Constructs an NhsDate object, initializes the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.Year">Year</see> to the given value 
        /// and sets <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see>
        /// to <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.Year">Year</see>.
        /// </summary>
        /// <param name="year">The year.</param>
        public NhsDate(int year)
        {
            this.Year = year;
            this.DateType = DateType.Year;
        }

        /// <summary>
        /// Constructs an NhsDate object, initializes the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.Year">Year</see> and 
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.Month">Month</see> to the given values and sets 
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> to YearMonth. 
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        public NhsDate(int year, int month)
        {
            this.DateType = DateType.YearMonth;
            this.Year = year;
            this.Month = month;
        }

        /// <summary>
        /// Constructs an NhsDate object, uses NhsDate.ParseExact to create a new temporary NhsDate object for the given string 
        /// and copies the property values from the temporary NhsDate object to the new NhsDate object. 
        /// </summary>
        /// <param name="date">The date.</param>
        public NhsDate(string date)
        {
            NhsDate nd = NhsDate.ParseExact(date, CultureInfo.InvariantCulture);

            this.DateValue = nd.DateValue;
            this.DateType = nd.DateType;
            this.Month = nd.Month;
            this.Year = nd.Year;            
            this.NullIndex = nd.NullIndex;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The DateType that represents the date. 
        /// </summary>
        /// <remarks>Defaults to DateType.Exact. DateType is always assigned a value explicitly; this is not implied by setting an associated property. </remarks>
        public DateType DateType
        {
            get 
            {
                return this.dateType; 
            }
            
            set 
            {
                if (!Enum.IsDefined(typeof(DateType), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.dateType = value; 
            }
        }
        
        /// <summary>
        /// The exact or approximate date value. 
        /// </summary>
        /// <remarks>
        ///Defaults to DateTime.Now. 
        ///</remarks>
        public DateTime DateValue
        {
            get 
            { 
                return this.dateValue; 
            }
            
            set 
            {
                this.dateValue = value; 
            }
        }
               
        /// <summary>
        /// The month in a DateType.YearMonth date. 
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown if Month is less than 1 or greater than 12. </exception>
        /// <remarks>
        /// Defaults to 1.
        /// Month is only relevant if <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is DateType.YearMonth. 
        /// It is not a reflection of the month of the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateValue">DateValue</see>. 
        /// </remarks>
        public int Month
        {
            get 
            { 
                return this.month; 
            }

            set 
            {
                if (value < MinimumMonth || value > MaximumMonth)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.month = value; 
            }
        }

        /// <summary>
        /// A number identifying a null index type.
        /// </summary>
        /// <remarks>
        /// Defaults to -1. The index has no meaning in and of itself; the meaning is implied by the UI control. 
        /// </remarks>
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
                    throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.NhsDateResources.NullIndexOutOfAcceptedRange, MinimumNullIndex, MaximumNullIndex));
                }
                
                this.nullIndex = value; 
            }
        }       

        /// <summary>
        /// The year in a DateType.Year or DateType.YearMonth
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown if Year is less than 0. </exception>
        /// <remarks>
        /// Defaults to 0. Year is only relevant if <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is 
        /// DateType.Year or DateType.YearMonth. It is not a reflection 
        /// of the year of the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateValue">DateValue</see>. 
        /// </remarks>
        public int Year
        {
            get
            { 
                return this.year; 
            }

            set 
            {
                if (value < MinimumYear)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.year = value; 
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is null. 
        /// </summary>
        /// <remarks>
        /// True if DateType is null; otherwise false. Read-only.
        /// </remarks>
        public bool IsNull
        {
            get
            {
                return (this.DateType == DateType.Null);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parses a string that represents a date and returns a corresponding NhsDate object.
        /// </summary>
        /// <param name="date">The date to be parsed. </param>
        /// <returns>A new NhsDate object. </returns>
        /// <exception cref="System.ArgumentNullException">Date is null. </exception>
        /// <exception cref="System.FormatException">Date is not in a recognised format. </exception>
        public static NhsDate ParseExact(string date)
        {
            return NhsDate.ParseExact(date, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Parses a string that represents a date and returns a corresponding NhsDate object, with the culture
        /// used to parse the string specified.
        /// </summary>
        /// <param name="date">The date to be parsed.</param>
        /// <param name="cultureInfo">The culture that should be used to parse the string. If a string is culture-agnostic, 
        /// this should be CultureInfo.InvariantCulture. Defaults to CurrentCulture.</param>
        /// <returns>A new NhsDate object. </returns>
        /// <exception cref="System.ArgumentNullException">Date is null. </exception>
        /// <exception cref="System.FormatException">Date is not in a recognised format.</exception>
        public static NhsDate ParseExact(string date, CultureInfo cultureInfo)
        {
            NhsDate result;

            if (!NhsDate.TryParseExact(date, out result, cultureInfo))
            {
                throw new FormatException(Resources.NhsDateResources.ParseCalledWithBadFormat);
            }

            return result;
        }

        /// <summary>
        /// Parses a string that represents a date. 
        /// </summary>
        /// <param name="date">A string containing the value to be parsed. </param>
        /// <param name="result">A container for a successfully-parsed date. </param>
        /// <returns>True if the date string was successfully parsed; otherwise, false.</returns>
        /// <remarks>
        /// If the string can be parsed, 
        /// the result parameter is set to an NhsDate object corresponding to 
        /// the parsed dateString. If the string cannot be parsed, the result parameter is set to DateTime.MinValue. 
        /// </remarks>
        public static bool TryParseExact(string date, out NhsDate result)
        {
            return NhsDate.TryParseExact(date, out result, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses a string that represents a date. 
        /// </summary>
        /// <param name="date">A string containing the value to be converted.   </param>
        /// <param name="result">A container for a successfully-parsed date.  </param>
        /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
        /// <returns>True if the date string was successfully parsed; otherwise, false.  </returns>
        /// <remarks>
        ///  If the string can be parsed, 
        /// the result parameter is set to an NhsDate object corresponding to 
        /// the parsed dateString. If the string cannot be parsed, the result parameter is set to DateTime.MinValue. 
        /// </remarks>
        public static bool TryParseExact(string date, out NhsDate result, CultureInfo cultureInfo)
        {
            // check for true null
            if (string.IsNullOrEmpty(date))
            {
                result = new NhsDate();
                result.DateType = DateType.Null;
                return true;
            }

            // first check for numeric year month as this can be confused as a date
            Regex numericYearMonthRegEx = new Regex("^(?<Month>0?[1-9]|10|11|12)[-\\s/](?<Year>\\d{4}|\\d{2})$");
            Match numericYearMonthRegExResults = numericYearMonthRegEx.Match(date);

            if (numericYearMonthRegExResults.Success)
            {
                result = new NhsDate();
                result.DateType = DateType.YearMonth;
                result.Month = int.Parse(numericYearMonthRegExResults.Groups["Month"].Value, cultureInfo);
                result.Year = NhsDate.ParseYear(numericYearMonthRegExResults.Groups["Year"].Value, cultureInfo);

                return true;
            }

            // check to see if approx indicator is present
            bool approxIndicatorPresent = (date.IndexOf(Resources.NhsDateResources.Approximate, StringComparison.CurrentCultureIgnoreCase) >= 0);

            if (approxIndicatorPresent)
            {
                date = date.Replace(Resources.NhsDateResources.Approximate, string.Empty).Trim();
            }

            // try datetime parse with our standard formats
            GlobalizationService gs = new GlobalizationService();
            string[] standardFormats = new string[] 
                                   {
                                       gs.ShortDatePattern,
                                       gs.ShortDatePatternWithDayOfWeek,
                                   };

            DateTime parsedDateTime;
            if (DateTime.TryParseExact(date, standardFormats, cultureInfo, DateTimeStyles.None, out parsedDateTime))
            {
                result = new NhsDate(parsedDateTime);
                result.DateType = (approxIndicatorPresent ? DateType.Approximate : DateType.Exact);
                return true;
            }

            // Check if 'date' is a year and a month
            Regex yearMonthRegEx = BuildMonthYearRegEx(cultureInfo);
            Match yearMonthMatch = yearMonthRegEx.Match(date);

            // Regex doesn't take care of year as zero
            if (yearMonthMatch.Success && NhsDate.ParseYear(yearMonthMatch.Groups["Year"].Value, cultureInfo) > 0)
            {
                int month = GetMonthNumberFromMonthName(yearMonthMatch.Groups["Month"].Value, cultureInfo);
                int year = NhsDate.ParseYear(yearMonthMatch.Groups["Year"].Value, cultureInfo);             
                result = new NhsDate(year, month);
                return true;
            }           

            // Check if 'date' is a year
            Regex yearRegEx = new Regex(@"^(\d{4}|\d{2})$");

            // Regex doesn't take care of year as zero
            if (yearRegEx.IsMatch(date) && NhsDate.ParseYear(date, cultureInfo) > 0)
            {
                result = new NhsDate(NhsDate.ParseYear(date, cultureInfo));
                return true;
            }

            // Check if 'date' is a Null date
            Regex nullDateRegEx = new Regex(@"^Null:(?<Index>-?\d+)$", RegexOptions.IgnoreCase);

            Match match = nullDateRegEx.Match(date);

            if (match.Success)
            {
                // Pull the numeric Index text and see if it is in range
                int nullIndex;

                if (int.TryParse(match.Groups[1].Captures[0].Value, out nullIndex))
                {
                    if (nullIndex >= MinimumNullIndex && nullIndex <= MaximumNullIndex)
                    {
                        result = new NhsDate();
                        result.NullIndex = nullIndex;
                        result.DateType = DateType.NullIndex;

                        return true;
                    }
                }
            }

            // try alternative formats
            string[] alternativeFormats = new string[] 
                                   {
                                        "d-MMM-yyyy", "d-M-yyyy", "d-MM-yyyy", "d-MMMM-yyyy",
                                        "d/MMM/yyyy", "d/M/yyyy", "d/MM/yyyy", "d/MMMM/yyyy",
                                        "d MMM yyyy", "d M yyyy", "d MM yyyy", "d MMMM yyyy",
                                        "ddd d-MMM-yyyy", "ddd d-M-yyyy", "ddd d-MM-yyyy", "ddd d-MMMM-yyyy",
                                        "ddd d/MMM/yyyy", "ddd d/M/yyyy", "ddd d/MM/yyyy", "ddd d/MMMM/yyyy",
                                        "ddd d MMM yyyy", "ddd d M yyyy", "ddd d MM yyyy", "ddd d MMMM yyyy",
                                        "d-MMM-yy", "d-M-yy", "d-MM-yy", "d-MMMM-yy",
                                        "d/MMM/yy", "d/M/yy", "d/MM/yy", "d/MMMM/yy",
                                        "d MMM yy", "d M yy", "d MM yy", "d MMMM yy",
                                        "ddd d-MMM-yy", "ddd d-M-yy", "ddd d-MM-yy", "ddd d-MMMM-yy",
                                        "ddd d/MMM/yy", "ddd d/M/yy", "ddd d/MM/yy", "ddd d/MMMM/yy",
                                        "ddd d MMM yy", "ddd d M yy", "ddd d MM yy", "ddd d MMMM yy"
                                  };

            if (DateTime.TryParseExact(date, alternativeFormats, cultureInfo, DateTimeStyles.None, out parsedDateTime))
            {
                result = new NhsDate(parsedDateTime);
                result.DateType = (approxIndicatorPresent ? DateType.Approximate : DateType.Exact);
                return true;
            }

            // no match
            result = null;
            return false;
        }

        /// <summary>
        /// Determines whether the add instruction operand has a valid value. 
        /// </summary>
        /// <param name="value">The string used to check whether the add instruction operand has a valid value. </param>
        /// <returns>True if the add instruction operand has a valid value; otherwise, false. </returns>
        /// <remarks>
        /// The test performed is not dependent on the current setting of <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see>. 
        /// This means true is returned if the operand is valid, even if <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is not Exact or Approximate. 
        /// </remarks>
        public static bool IsAddValid(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            Regex shortcut = new Regex(NhsDate.ResolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);

            Match match = shortcut.Match(value.Replace(" ", string.Empty));

            return match.Success;
        }
      
        /// <summary>
        /// Adds a number of months, weeks, days or years to a date. 
        /// </summary>
        /// <param name="sourceDate">The date to be added to. </param>
        /// <param name="instruction">Add instructions such as +2w to add 2 weeks or -3m to subtract 3 months. </param>
        /// <remarks>
        /// The operand can be positive or negative; if the operand is not included, it is assumed to be positive. 
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">If either argument is null.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">If <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> 
        /// is null. </exception>
        /// <exception cref="System.ArgumentException">If <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> 
        /// is <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.Year">Year</see> and the unit in the instruction is 
        /// not DateArithmeticResources.YearsUnit. Or if <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see>
        /// is YearMonth and the unit in the instruction is 
        /// not DateArithmeticResources.YearsUnit or DateArithmeticResources.MonthsUnit. Or if the instruction is not
        /// of the expected format.</exception>
        /// <exception cref="NhsCui.Toolkit.DateAndTime.InvalidArithmeticSetException">If the set of letters in
        /// the DateArithmeticResources is not unique or if values are not each a single character long. 
        /// </exception>
        /// <returns>A new NhsDate object. </returns>
        public static NhsDate Add(NhsDate sourceDate, string instruction)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("instruction");
            }

            if (sourceDate == null)
            {
                throw new ArgumentNullException("sourceDate");
            }

            // check the set of resource strings  used by this method are
            // valid
            if (!CheckArithmeticSetResources())
            {
                throw new InvalidArithmeticSetException(Resources.NhsDateResources.ArithmeticSetInvalidResources);
            }

            if (sourceDate.DateType == DateType.NullIndex)
            {
                throw new InvalidOperationException(Resources.NhsDateResources.AddNotAllowedForDateType);
            }

            if (NhsDate.IsAddValid(instruction) == false)
            {
                throw new ArgumentException(Resources.NhsDateResources.AddInstructionInvalidFormat, "instruction");
            }

            Regex shortcut = new Regex(NhsDate.ResolveAddInstructionRegExTokens(), RegexOptions.IgnoreCase);
                        
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
                                if (c.Value.EndsWith(Resources.NhsDateResources.MonthsUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    if (sourceDate.DateType == DateType.Exact || sourceDate.DateType == DateType.Approximate)
                                    {
                                        sourceDate.DateValue = sourceDate.DateValue.AddMonths(increment);
                                    }
                                    else if (sourceDate.DateType == DateType.YearMonth)
                                    {
                                        sourceDate.Year += increment / 12;
                                        sourceDate.Month += increment % 12;
                                    }
                                    else
                                    {
                                        throw new ArgumentException(
                                                                  string.Format(CultureInfo.CurrentCulture, Resources.NhsDateResources.AddInstructionNotAllowedForDateType, "Month", sourceDate.DateType),
                                                                  "instruction");
                                    }
                                }
                                else if (c.Value.EndsWith(Resources.NhsDateResources.WeeksUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    if (sourceDate.DateType == DateType.Exact || sourceDate.DateType == DateType.Approximate)
                                    {
                                        // Add weeks using AddDays and multiplying number of weeks to add by 7
                                        sourceDate.DateValue = sourceDate.DateValue.AddDays(increment * 7);
                                    }
                                    else
                                    {
                                        throw new ArgumentException(
                                                                    string.Format(CultureInfo.CurrentCulture, Resources.NhsDateResources.AddInstructionNotAllowedForDateType, "Week", sourceDate.DateType),
                                                                    "instruction");
                                    }
                                }
                                else if (c.Value.EndsWith(Resources.NhsDateResources.DaysUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    if (sourceDate.DateType == DateType.Exact || sourceDate.DateType == DateType.Approximate)
                                    {
                                        sourceDate.DateValue = sourceDate.DateValue.AddDays(increment);
                                    }
                                    else
                                    {
                                        throw new ArgumentException(
                                                                    string.Format(CultureInfo.CurrentCulture, Resources.NhsDateResources.AddInstructionNotAllowedForDateType, "Day", sourceDate.DateType),
                                                                    "instruction");
                                    }
                                }
                                else if (c.Value.EndsWith(Resources.NhsDateResources.YearsUnit, StringComparison.OrdinalIgnoreCase) == true)
                                {
                                    if (sourceDate.DateType == DateType.Exact || sourceDate.DateType == DateType.Approximate)
                                    {
                                        sourceDate.DateValue = sourceDate.DateValue.AddYears(increment);
                                    }
                                    else if (sourceDate.DateType == DateType.Year || sourceDate.DateType == DateType.YearMonth)
                                    {
                                        sourceDate.Year += increment;
                                    }
                                    else
                                    {
                                        throw new ArgumentException(
                                                                  string.Format(CultureInfo.CurrentCulture, Resources.NhsDateResources.AddInstructionNotAllowedForDateType, "Year", sourceDate.DateType),
                                                                  "instruction");
                                    }
                                }
                            }
                        }
                    }

                    groupNumber++;
                }
            }

            return sourceDate;
        }
        
        /// <summary>
        /// Adds a number of months, weeks, days or years to a date. 
        /// </summary>
        /// <param name="instruction">Add instructions such as +2w to add 2 weeks or -3m to subtract 3 months.</param>
        /// <remarks>
        /// The operand can be positive or negative; if the operand is not included, it is assumed to be positive. 
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Instruction is null.</exception>
        public void Add(string instruction)
        {
            this.DateValue = NhsDate.Add(this, instruction).DateValue;
        }

        /// <summary>
        /// Returns a string representing the date.
        /// </summary>
        /// <returns>The date as a string. </returns>
        public override string ToString()
        {
            return this.ToString(false, false, false, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representing the date, including the day of the week.
        /// </summary>
        /// <param name="includeDayOfWeek">Includes the day of the week in the string.</param>
        /// <returns>The date as a string. </returns>
        public string ToString(bool includeDayOfWeek)
        {
            return this.ToString(includeDayOfWeek, false, false, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representing the date, including the day of the week and with the Approx text indicator displayed if the 
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is Approximate. 
        /// </summary>
        /// <param name="includeDayOfWeek">Includes the day of the week in the string. </param>
        /// <param name="approximate">When the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> 
        /// is DateType.Approximate, shows the Approx text indicator. </param>
        /// <returns>The date as a string. </returns>
        public string ToString(bool includeDayOfWeek, bool approximate)
        {
            return this.ToString(includeDayOfWeek, approximate, false, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representing the date, including the day of the week, with the Approx text indicator displayed if the 
        /// <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> is Approximate and the relative day of the week 
        /// displayed if it is Today, Tomorrow or Yesterday.
        /// </summary>
        /// <param name="includeDayOfWeek">Includes the day of the week in the string. </param>
        /// <param name="approximate">When the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> 
        /// is DateType.Approximate, shows the Approx text indicator.</param>
        /// <param name="showRelativeDayText">If the date is Today, Tomorrow or Yesterday, returns a string rather than the date.</param>
        /// <returns>The date as a string.
        /// </returns>
        public string ToString(bool includeDayOfWeek, bool approximate, bool showRelativeDayText)
        {
            return this.ToString(includeDayOfWeek, approximate, showRelativeDayText, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representing the date.
        /// </summary>
        /// <param name="includeDayOfWeek">Includes the day of the week in the string. </param>
        /// <param name="approximate">When the <see cref="P:NhsCui.Toolkit.DateAndTime.NhsDate.DateType">DateType</see> 
        /// is DateType.Approximate, shows the Approx text indicator.</param>
        /// <param name="showRelativeDayText">If the date is Today, Tomorrow or Yesterday, returns a string rather than the date. </param>
        /// <param name="cultureInfo">The culture that should be used to parse the string. </param>
        /// <returns>The date as a string. </returns>
        public string ToString(bool includeDayOfWeek, bool approximate, bool showRelativeDayText, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                throw new ArgumentNullException("cultureInfo");
            }

            if (this.DateType != DateType.Approximate && approximate == true)
            {
                // This is invalid. You cannot make use of the indicateWhenApproximate flag when the 
                // Date is not Approximate
                throw new ArgumentOutOfRangeException("approximate", Resources.NhsDateResources.ShowApproxIndicatorInvalidForDateType);
            }

            string formattedDate = null;

            switch (this.dateType)
            {
                case DateType.Exact:
                case DateType.Approximate:

                    if (showRelativeDayText)
                    {
                        DateTime date = this.DateValue.Date;
                        if (date == System.DateTime.Today)
                        {
                            formattedDate = Resources.NhsDateResources.Today;
                        }
                        else if (date == System.DateTime.Today.AddDays(-1))
                        {
                            formattedDate = Resources.NhsDateResources.Yesterday;
                        }
                        else if (date == System.DateTime.Today.AddDays(1))
                        {
                            formattedDate = Resources.NhsDateResources.Tomorrow;
                        }
                    }

                    if (formattedDate == null)
                    {
                        GlobalizationService gs = new GlobalizationService();
                        formattedDate = this.DateValue.ToString(includeDayOfWeek ? gs.ShortDatePatternWithDayOfWeek : gs.ShortDatePattern, cultureInfo);
                    }

                    if (this.DateType == DateType.Approximate && approximate)
                    {
                        formattedDate = string.Format(
                                        cultureInfo,
                                        Resources.NhsDateResources.ApproximateDateFormat,
                                        Resources.NhsDateResources.Approximate,
                                        formattedDate);
                    }

                    break;               

                case DateType.Year:
                    formattedDate = this.Year.ToString("0000", cultureInfo);
                    break;

                case DateType.YearMonth:
                    formattedDate = string.Format(
                                    cultureInfo, 
                                    "{0}-{1}", 
                                    cultureInfo.DateTimeFormat.MonthNames[this.Month - 1], 
                                    this.Year.ToString("0000", cultureInfo));
                    break;

                case DateType.NullIndex:
                    formattedDate = string.Format(cultureInfo, "Null:{0}", this.NullIndex);
                    break;

                case DateType.Null:
                    formattedDate = string.Empty;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            return formattedDate;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Resolves the tokens on the add instruction regex for the values in the resource file. 
        /// </summary>
        /// <returns>The add instruction regular expression with its tokens resolved</returns>
        private static string ResolveAddInstructionRegExTokens()
        {
            string resolvedText;

            // I wanted to use string.Format but the {1,2} in the actual regEx was being mistaked for a formatting token

            // Day token
            resolvedText = AddInstructionRegExFormat.Replace("#0#", Resources.NhsDateResources.DaysUnit);

            // Months token
            resolvedText = resolvedText.Replace("#1#", Resources.NhsDateResources.MonthsUnit);

            // Weeks token
            resolvedText = resolvedText.Replace("#2#", Resources.NhsDateResources.WeeksUnit);

            // Years token
            resolvedText = resolvedText.Replace("#3#", Resources.NhsDateResources.YearsUnit);

            return resolvedText;
        }

        /// <summary>
        /// Parse string into year adjusting two digit years if necessary
        /// </summary>
        /// <param name="value">value to parse</param>
        /// <param name="culture">culture</param>
        /// <returns>year</returns>
        private static int ParseYear(string value, CultureInfo culture)
        {
            int year = int.Parse(value, culture);

            // expands 2-digit year into 4 digits.
            if (value.Length <= 2)
            {
                int currentYear = DateTime.Now.Year;
                year += currentYear - (currentYear % 100);
                if (year > culture.DateTimeFormat.Calendar.TwoDigitYearMax)
                {
                    year -= 100;
                }
            }

            return year;
        }

        /// <summary>
        /// Check that the set of resource strings used by the Add Method are valid and
        /// if not throw an exception
        /// </summary>
        /// <returns>true if the resources are valid</returns>
        private static bool CheckArithmeticSetResources()
        {
            string[] resources = new string[]
                                                {
                                                    Resources.NhsDateResources.DaysUnit,
                                                    Resources.NhsDateResources.WeeksUnit,
                                                    Resources.NhsDateResources.MonthsUnit,
                                                    Resources.NhsDateResources.YearsUnit
                                                };
            bool valid = true;

            // the set of letters in the DateArithmeticResources must be unique and
            // a single character long
            for (int i = 0; i < resources.Length && valid; i++)
            {
                valid = (resources[i].Length == 1);
                for (int j = i + 1; j < resources.Length && valid; j++)
                {
                    valid = (string.Compare(resources[i], resources[j], true, CultureInfo.CurrentCulture) != 0);
                }
            }

            return valid;
        }    

        /// <summary>
        /// Builds a RegEx object that will help parse a text date looking for a Month - Year string. 
        /// </summary>
        /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
        /// <returns>A RegEx object</returns>
        private static Regex BuildMonthYearRegEx(CultureInfo cultureInfo)
        {
            System.Text.StringBuilder monthsNames = new System.Text.StringBuilder();

            string monthYearRegExFormat = @"^(?<Month>##Months##)[-\s/](?<Year>\d{4}|\d{2})$";

            foreach (String monthName in cultureInfo.DateTimeFormat.MonthNames)
            {
                // DateTimeFormat.MonthNames is 13 entries long with the 13th month being empty in many cultures, such as en-GB.
                // Do not use an empty Month name in the Reg Ex

                if (monthName.Length > 0)
                {
                    if (monthsNames.Length > 0)
                    {
                        monthsNames.Append("|");
                    }

                    monthsNames.Append(monthName);
                }
            }

            foreach (String monthName in cultureInfo.DateTimeFormat.AbbreviatedMonthNames)
            {
                // DateTimeFormat.AbbreviatedMonthNames is 13 entries long with the 13th month being empty in many cultures, such as en-GB.
                // Do not use an empty Month name in the Reg Ex

                if (monthName.Length > 0)
                {
                    if (monthsNames.Length > 0)
                    {
                        monthsNames.Append("|");
                    }

                    monthsNames.Append(monthName);
                }
            }

            return new Regex(monthYearRegExFormat.Replace("##Months##", monthsNames.ToString()), RegexOptions.IgnoreCase);
        }    

        /// <summary>
        /// Gets the month number from the month name. 
        /// </summary>
        /// <param name="monthName">The name of the month. For example, if the month number is 3, the monthName is "March". </param>
        /// <param name="cultureInfo">The culture that should be used to parse the string.</param>
        /// <returns>The Month number</returns>
        private static int GetMonthNumberFromMonthName(string monthName, CultureInfo cultureInfo)
        {
            int index = FindCaseInsensitiveEntry(cultureInfo.DateTimeFormat.MonthNames, monthName, cultureInfo);
            
            if (index == -1)
            {
                index = FindCaseInsensitiveEntry(cultureInfo.DateTimeFormat.AbbreviatedMonthNames, monthName, cultureInfo);
            }
            
            return (index >= 0 ? index + 1 : -1);
        }

        /// <summary>
        /// Find entry an entry in supplied string array by case insensitive match
        /// </summary>
        /// <param name="values">values to search</param>
        /// <param name="item">item to search for</param>
        /// <param name="cultureInfo">culture to use for comparisons</param>
        /// <returns>index in the array of the item ,or -1 if not found</returns>
        private static int FindCaseInsensitiveEntry(string[] values, string item, CultureInfo cultureInfo)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (string.Compare(values[i], item, true, cultureInfo) == 0)
                {
                    return i;
                }
            }

            return -1;
        }    
        #endregion
    }
}
