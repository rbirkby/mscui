//-----------------------------------------------------------------------
// <copyright file="TimeFrequency.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Sep-2008</date>
// <summary>Time frequency class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;
    #endregion

    /// <summary>
    /// Class denoting time frequency.
    /// </summary>
    public class TimeFrequency
    {
        /// <summary>
        /// Member variable to hold the time unit.
        /// </summary>
        private TimeUnit timeUnit = TimeUnit.Year;

        /// <summary>
        /// Member variable to hold value.
        /// </summary>
        private int value;

        /// <summary>
        /// Tick unit.
        /// </summary>
        public enum TimeUnit
        {
            /// <summary>
            /// Units are in seconds.
            /// </summary>
            Second = 0,

            /// <summary>
            /// Units are in minutes.
            /// </summary>
            Minute,

            /// <summary>
            /// Units are in hours.
            /// </summary>
            Hour,

            /// <summary>
            /// Units are in days.
            /// </summary>
            Day,

            /// <summary>
            /// Units are in weeks.
            /// </summary>
            Week,

            /// <summary>
            /// Units are in months.
            /// </summary>
            Month,

            /// <summary>
            /// Units are in Years.
            /// </summary>
            Year
        }

        /// <summary>
        /// Gets the description of the frequency.
        /// </summary>
        /// <value>Description of the frequency.</value>
        public string Description
        {
            get
            {
                string returnString;
                if (this.Value > 1)
                {
                    returnString = this.Value.ToString(CultureInfo.CurrentCulture);
                    returnString += " ";
                    returnString += this.Unit.ToString().ToLower(CultureInfo.CurrentCulture);
                    returnString += "s";
                }
                else
                {
                    returnString = this.Value.ToString(CultureInfo.CurrentCulture);
                    returnString += " ";
                    returnString += this.Unit.ToString().ToLower(CultureInfo.CurrentCulture);
                }

                return returnString;
            }
        }

        /// <summary>
        /// Gets or sets the value of the frequency.
        /// </summary>
        /// <value>Value of the frequency.</value>
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the Unit of frequency.
        /// </summary>
        /// <value>Unit of frequency.</value>
        public TimeUnit Unit
        {
            get { return this.timeUnit; }
            set { this.timeUnit = value; }
        }

        /// <summary>
        /// Gets the visible ticks based on the Unit.
        /// </summary>
        /// <value>Returns the ticks based on the unit of frequency.</value>
        public long Ticks
        {
            get
            {
                TimeSpan span = TimeFrequency.GetUnitTimeSpan(this.timeUnit, this.value);
                return span.Ticks;
            }
        }

        /// <summary>
        /// Gets or sets the format of labels.
        /// </summary>
        /// <value>Format of the label.</value>
        public string LabelFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date format for title.
        /// </summary>
        /// <value>Date format for title.</value>
        public string TitleFormat
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the timespan for major interval.
        /// </summary>
        /// <value>TimeSpan for the major interval.</value>
        public TimeSpan MajorInterval
        {
            get
            {
                return TimeFrequency.GetUnitTimeSpan(this.MajorUnit, this.MajorValue);
            }
        }

        /// <summary>
        /// Gets or sets the timeunit for major interval.
        /// </summary>
        /// <value>TimeUnit for the major interval.</value>
        public TimeUnit MajorUnit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value for major interval.
        /// </summary>
        /// <value>Value for the major interval.</value>
        public int MajorValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the timespan for minor interval.
        /// </summary>
        /// <value>TimeSpan for the minor interval.</value>
        public TimeSpan MinorInterval
        {
            get
            {
                return TimeFrequency.GetUnitTimeSpan(this.MinorUnit, this.MinorValue);
            }
        }

        /// <summary>
        /// Gets or sets the timeunit for minor interval.
        /// </summary>
        /// <value>TimeUnit for the minor interval.</value>
        public TimeUnit MinorUnit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value for minor interval.
        /// </summary>
        /// <value>Value for the minor interval.</value>
        public int MinorValue
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the TimeSpan that represent the Time Unit in question.
        /// </summary>
        /// <param name="timeUnit">The TimeUnit from which to determine the TimeSpan.</param>
        /// <param name="value">The number of TimeUnits to convert.</param>
        /// <returns>TimeSpan for the unit and value.</returns>
        private static TimeSpan GetUnitTimeSpan(TimeUnit timeUnit, int value)
        {
            TimeSpan span = new TimeSpan();
            int years, qtrs, months, days, leaps;

            switch (timeUnit)
            {
                case TimeUnit.Second:
                    span = TimeSpan.FromSeconds(value);
                    break;
                case TimeUnit.Minute:
                    span = TimeSpan.FromMinutes(value);
                    break;
                case TimeUnit.Hour:
                    span = TimeSpan.FromHours(value);
                    break;
                case TimeUnit.Day:
                    span = TimeSpan.FromDays(value);
                    break;
                case TimeUnit.Week:
                    span = TimeSpan.FromDays(value * 7);
                    break;
                case TimeUnit.Month:
                    years = value / 12;
                    qtrs = (value - (years * 12)) / 3;
                    months = value - (years * 12) - (qtrs * 3);
                    leaps = (years / 4) + (years % 4 == 0 ? 0 : 1);
                    days = (years * 365) + (qtrs * 92) + (months * 31) + leaps;
                    span = new TimeSpan(days, 0, 0, 0, 0);
                    break;
                case TimeUnit.Year:
                    years = value;
                    leaps = (years / 4) + (years % 4 == 0 ? 0 : 1);
                    span = TimeSpan.FromDays((years * 365) + leaps);
                    break;
            }

            return span;
        }
    }
}
