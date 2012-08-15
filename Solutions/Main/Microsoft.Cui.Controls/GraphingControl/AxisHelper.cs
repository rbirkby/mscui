//-----------------------------------------------------------------------
// <copyright file="AxisHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Helper class for Axis.</summary>
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
    using Microsoft.Cui.Controls.GraphingControl;
    #endregion

    /// <summary>
    /// Helper class for Axis.
    /// </summary>
    internal class AxisHelper
    {
        /// <summary>
        /// Private constructor to prevent the class from being instantiated and also for compiler not to generate a default constructor.
        /// </summary>
        private AxisHelper()
        {
        }

        /// <summary>
        /// Gets the Axis design based on the interval.
        /// </summary>
        /// <param name="visibleWindowTicks">Number of ticks in the visible window.</param>
        /// <returns>Returns TimeFrequency with Major and Minor intervals based on the graph interval.</returns>
        internal static TimeFrequency GetAxisDesign(long visibleWindowTicks)
        {
            TimeSpan timeSpanOfWindow = new TimeSpan(visibleWindowTicks);
            TimeFrequency frequency = new TimeFrequency();

            if (timeSpanOfWindow == TimeSpan.FromMinutes(5))
            {
                // 5 Minutes
                frequency.Unit = TimeFrequency.TimeUnit.Minute;
                frequency.Value = 5;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Second;
                frequency.MinorValue = 30;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMinute;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMinute;
            }
            else if (timeSpanOfWindow == TimeSpan.FromMinutes(10))
            {
                // 10 Minutes
                frequency.Unit = TimeFrequency.TimeUnit.Minute;
                frequency.Value = 10;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 2;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMinute;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMinute;
            }
            else if (timeSpanOfWindow == TimeSpan.FromMinutes(15))
            {
                // 15 Minutes
                frequency.Unit = TimeFrequency.TimeUnit.Minute;
                frequency.Value = 15;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 3;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMinute;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMinute;
            }
            else if (timeSpanOfWindow == TimeSpan.FromMinutes(30))
            {
                // 30 Minutes
                frequency.Unit = TimeFrequency.TimeUnit.Minute;
                frequency.Value = 30;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 5;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMinute;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMinute;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(1))
            {
                // 1 Hour
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 1;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 15;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 5;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(2))
            {
                // 2 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 2;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MajorValue = 30;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 10;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(4))
            {
                // 4 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 4;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 15;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(6))
            {
                // 6 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 6;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Minute;
                frequency.MinorValue = 30;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(8))
            {
                // 8 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 8;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 2;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(12))
            {
                // 12 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 12;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 4;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(24))
            {
                // 24 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 24;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 6;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHour;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHour;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(36))
            {
                // 36 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 36;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 6;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 3;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHourDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHourDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(48))
            {
                // 48 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 48;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 12;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 3;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHourDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHourDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromHours(72))
            {
                // 72 Hours
                frequency.Unit = TimeFrequency.TimeUnit.Hour;
                frequency.Value = 72;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MajorValue = 24;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 6;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatHourDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatHourDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(5))
            {
                // 5 Days
                frequency.Unit = TimeFrequency.TimeUnit.Day;
                frequency.Value = 5;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 12;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(7))
            {
                // 1 Week or 7 Days
                frequency.Unit = TimeFrequency.TimeUnit.Week;
                frequency.Value = 1;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Hour;
                frequency.MinorValue = 12;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(10))
            {
                // 10 Days
                frequency.Unit = TimeFrequency.TimeUnit.Day;
                frequency.Value = 10;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 2;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(14))
            {
                // 2 Weeks or 14 Days
                frequency.Unit = TimeFrequency.TimeUnit.Week;
                frequency.Value = 2;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(15))
            {
                // 15 Days
                frequency.Unit = TimeFrequency.TimeUnit.Day;
                frequency.Value = 15;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 3;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(20))
            {
                // 20 Days
                frequency.Unit = TimeFrequency.TimeUnit.Day;
                frequency.Value = 20;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 5;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(28))
            {
                // 4 Weeks or 28 Days
                frequency.Unit = TimeFrequency.TimeUnit.Week;
                frequency.Value = 4;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(30))
            {
                // 30 Days
                frequency.Unit = TimeFrequency.TimeUnit.Day;
                frequency.Value = 30;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MajorValue = 10;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 5;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatDay;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatDay;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(31))
            {
                // 1 Month
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 1;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Day;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonth;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonth;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(62))
            {
                // 2 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 2;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonth;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonth;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(92))
            {
                // 3 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 3;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonth;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonth;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(184))
            {
                // 6 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 6;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Week;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonthYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonthYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(276))
            {
                // 9 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 9;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 3;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonthYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonthYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(366))
            {
                // 1 Year or 12 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 12;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 3;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonthYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonthYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(550))
            {
                // 18 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 18;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 3;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonthYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonthYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(731))
            {
                // 2 Years or 24 Months
                frequency.Unit = TimeFrequency.TimeUnit.Month;
                frequency.Value = 24;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MajorValue = 6;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 2;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatMonthYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatMonthYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(1827))
            {
                // 5 Years
                frequency.Unit = TimeFrequency.TimeUnit.Year;
                frequency.Value = 5;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MajorValue = 1;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 3;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(3653))
            {
                // 10 Years
                frequency.Unit = TimeFrequency.TimeUnit.Year;
                frequency.Value = 10;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MajorValue = 2;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Month;
                frequency.MinorValue = 6;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(7305))
            {
                // 20 Years
                frequency.Unit = TimeFrequency.TimeUnit.Year;
                frequency.Value = 20;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MajorValue = 5;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MinorValue = 1;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatYear;
            }
            else if (timeSpanOfWindow == TimeSpan.FromDays(36525))
            {
                // 100 Years
                frequency.Unit = TimeFrequency.TimeUnit.Year;
                frequency.Value = 100;
                frequency.MajorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MajorValue = 20;
                frequency.MinorUnit = TimeFrequency.TimeUnit.Year;
                frequency.MinorValue = 5;
                frequency.LabelFormat = GraphingResources.AxisHelperLabelFormatYear;
                frequency.TitleFormat = GraphingResources.AxisHelperTitleFormatYear;
            }

            System.Diagnostics.Debug.Assert(frequency.Value != 0, GraphingResources.AxisHelperInvalidFrequency);
            return frequency;
        }

        /// <summary>
        /// Gets the start date of the Axis based on the data supplied and the time interval.
        /// </summary>
        /// <param name="dateTime">System.DateTime for the first data point.</param>
        /// <param name="frequency">TimeFrequency to be used for calculating the axis end date.</param>
        /// <returns>DateTime to be used for the first interval.</returns>
        internal static DateTime GetAxisStartDate(DateTime dateTime, TimeFrequency frequency)
        {
            DateTime startTime = DateTime.MinValue;
            DateTime intervalTime;
            TimeSpan tt;
            long ticks;
            int month;

            switch (frequency.MajorUnit)
            {
                case TimeFrequency.TimeUnit.Year:
                case TimeFrequency.TimeUnit.Month:
                case TimeFrequency.TimeUnit.Week:
                    ticks = AxisHelper.GetTickIncrease(frequency.MinorInterval, dateTime, frequency.MinorUnit, frequency.MinorValue);
                    if (dateTime.Ticks > ticks)
                    {
                        // ensure year aligned to the major boundary - start of year
                        intervalTime = dateTime.AddTicks(-ticks);
                        if (frequency.MajorUnit == TimeFrequency.TimeUnit.Year)
                        {
                            startTime = new DateTime(intervalTime.Year, 1, 1);
                        }
                        else if (frequency.MajorUnit == TimeFrequency.TimeUnit.Month)
                        {
                            // ensure month aligned to major boundary value - start of month
                            month = intervalTime.Month - ((intervalTime.Month - 1) % frequency.MajorValue);
                            startTime = new DateTime(intervalTime.Year, month, 1);
                        }
                        else
                        {
                            // ensure week aligned to major boundary value - start of week
                            tt = intervalTime.Subtract(DateTime.MinValue);
                            int days = (int)tt.TotalDays;
                            days -= (days % 7);
                            startTime = DateTime.MinValue.Add(new TimeSpan(days, 0, 0, 0));
                        }
                    }
                    
                    break;
                case TimeFrequency.TimeUnit.Day:
                case TimeFrequency.TimeUnit.Hour:
                case TimeFrequency.TimeUnit.Minute:
                case TimeFrequency.TimeUnit.Second:
                default:
                    intervalTime = dateTime;

                    // Just round down to the nearest minor interval
                    ticks = AxisHelper.GetTickIncrease(frequency.MinorInterval, intervalTime, frequency.MinorUnit, frequency.MinorValue);
                    double remainingTicks = intervalTime.Ticks % ticks;
                    remainingTicks = ticks + remainingTicks;
                    if (intervalTime.Ticks - remainingTicks > 0)
                    {
                        intervalTime = intervalTime.Subtract(TimeSpan.FromTicks((long)remainingTicks));
                    }

                    if (frequency.MajorUnit == TimeFrequency.TimeUnit.Minute)
                    {
                        // align to a whole minute boundary based on major interval
                        tt = intervalTime.Subtract(DateTime.MinValue);
                        int minutes = (int)tt.TotalMinutes;
                        minutes -= (minutes % frequency.MajorValue);
                        startTime = DateTime.MinValue.Add(new TimeSpan(0, minutes, 0));
                    }
                    else if (frequency.MajorUnit == TimeFrequency.TimeUnit.Hour)
                    {
                        // align to boundary that in hours is divisible by the major interval value
                        tt = intervalTime.Subtract(DateTime.MinValue);
                        int hours = (int)tt.TotalHours;
                        if (frequency.MinorUnit == TimeFrequency.TimeUnit.Hour)
                        {
                            hours -= (hours % frequency.MajorValue);
                        }

                        startTime = DateTime.MinValue.Add(new TimeSpan(hours, 0, 0));
                    }
                    else
                    {
                        // ensure aligned to start of a day
                        tt = intervalTime.Subtract(DateTime.MinValue);
                        int days = (int)tt.TotalDays;
                        startTime = DateTime.MinValue.Add(new TimeSpan(days, 0, 0, 0));
                    }

                    break;
            }

            return startTime;
        }

        /// <summary>
        /// Gets the end date for the Axis based on the data supplied and the time frequency.
        /// </summary>
        /// <param name="dateTime">System.DateTime for the last data point.</param>
        /// <param name="frequency">TimeFrequency to be used for calculating the axis end date.</param>
        /// <returns>DateTime to be used for the last interval.</returns>
        internal static DateTime GetAxisEndDate(DateTime dateTime, TimeFrequency frequency)
        {
            DateTime endTime = DateTime.MaxValue;
            DateTime intervalTime;
            long ticks;
            int month, days;

            switch (frequency.MajorUnit)
            {
                case TimeFrequency.TimeUnit.Year:
                case TimeFrequency.TimeUnit.Month:
                case TimeFrequency.TimeUnit.Week:
                    // ensure aligned to the minor boundary
                    if (frequency.MinorUnit == TimeFrequency.TimeUnit.Year)
                    {
                        // ensure last day of the year
                        ticks = AxisHelper.GetTickIncrease(frequency.MinorInterval, dateTime, frequency.MinorUnit, frequency.MinorValue);
                        intervalTime = dateTime.AddTicks(ticks);
                        endTime = new DateTime(intervalTime.Year, 12, 31);

                        // now ensure that the actual end time is the first day of the next period
                        endTime = endTime.AddDays(1);
                    }
                    else if (frequency.MinorUnit == TimeFrequency.TimeUnit.Month)
                    {
                        // ensure last day of the month
                        ticks = AxisHelper.GetTickIncrease(frequency.MinorInterval, dateTime, frequency.MinorUnit, frequency.MinorValue);
                        intervalTime = dateTime.AddTicks(ticks);
                        month = (((intervalTime.Month - 1) / frequency.MinorValue) + 1) * frequency.MinorValue;
                        days = DateTime.DaysInMonth(intervalTime.Year, month);
                        endTime = new DateTime(intervalTime.Year, month, days);

                        // now ensure that the actual end time is the first day of the next period
                        endTime = endTime.AddDays(1);
                    }
                    else if (frequency.MinorUnit == TimeFrequency.TimeUnit.Week)
                    {
                        // ensure last day of the week
                        TimeSpan tt = dateTime.Subtract(DateTime.MinValue);
                        days = (int)tt.TotalDays;
                        days = days - (days % 7) + 7 + (frequency.MinorValue * 7);
                        endTime = DateTime.MinValue.Add(new TimeSpan(days, 0, 0, 0));
                    }
                    else
                    {
                        ticks = AxisHelper.GetTickIncreaseRounded(frequency.MinorInterval, dateTime, frequency.MinorUnit, frequency.MinorValue);
                        endTime = dateTime.AddTicks(ticks);
                    }

                    break;
                case TimeFrequency.TimeUnit.Day:
                case TimeFrequency.TimeUnit.Hour:
                case TimeFrequency.TimeUnit.Minute:
                case TimeFrequency.TimeUnit.Second:
                default:
                    ticks = AxisHelper.GetTickIncreaseRounded(frequency.MinorInterval, dateTime, frequency.MinorUnit, frequency.MinorValue);
                    endTime = dateTime.AddTicks(ticks);

                    break;
            }

            return endTime;            
        }

        /// <summary>
        /// Gets the adjusted start date of the Axis based on the data supplied and the time interval.
        /// </summary>
        /// <param name="startTime">System.DateTime for the Start axis time.</param>
        /// <param name="endTime">System.DateTime for the End axis time.</param>
        /// <param name="frequency">TimeFrequency to be used for calculating the axis end date.</param>
        internal static void AdjustAxisStartDate(ref DateTime startTime, ref DateTime endTime, TimeFrequency frequency)
        {
            // see if points are less than the visible window and if so adjust start date
            long tickRange = endTime.Ticks - startTime.Ticks;
            if (tickRange < frequency.Ticks)
            {
                DateTime adjustedTime;
                long pastTicks, majorTicks;

                pastTicks = frequency.Ticks - tickRange;
                if (startTime.Ticks > pastTicks)
                {
                    if (frequency.Unit == TimeFrequency.TimeUnit.Year)
                    {
                        adjustedTime = startTime.AddTicks(-pastTicks);
                        adjustedTime = new DateTime(adjustedTime.Year, 1, 1);
                    }
                    else
                    {
                        majorTicks = AxisHelper.GetTickIncrease(frequency.MajorInterval, startTime, frequency.MajorUnit, frequency.MajorValue);
                        adjustedTime = startTime.AddTicks(-pastTicks + majorTicks);
                        adjustedTime = AxisHelper.GetAxisStartDate(adjustedTime, frequency);
                    }
                }
                else
                {
                    adjustedTime = DateTime.MinValue;
                }

                // have to adjust the start and end dates but not lower the end date
                DateTime newStartTime = adjustedTime;
                DateTime newEndTime = adjustedTime.AddTicks(frequency.Ticks);

                if (newEndTime < endTime)
                {
                    if (frequency.Unit == TimeFrequency.TimeUnit.Year)
                    {
                        // set the start time but only if still lowers the start date                       
                        if (newStartTime.AddYears(1) < startTime)
                        {
                            newStartTime = newStartTime.AddYears(1);
                        }

                        newEndTime = newEndTime.AddYears(1);
                    }
                    else
                    {
                        majorTicks = AxisHelper.GetTickIncrease(frequency.MajorInterval, newEndTime, frequency.MajorUnit, frequency.MajorValue);
                        newEndTime = newEndTime.AddTicks(majorTicks);

                        // increment the start time but only if still lowers the start date
                        majorTicks = AxisHelper.GetTickIncrease(frequency.MajorInterval, newStartTime, frequency.MajorUnit, frequency.MajorValue);
                        if (newStartTime.AddTicks(majorTicks) < startTime)
                        {
                            newStartTime = newStartTime.AddTicks(majorTicks);
                        }
                    }
                }

                // set the new start and end time
                startTime = newStartTime;
                endTime = newEndTime;
            }

            return;
        }
        
        /// <summary>
        /// Determines the Ticks to be plotted for a given TimeUnit and Value.
        /// </summary>
        /// <param name="timespan">The TimeSpane for the calculation.</param>
        /// <param name="datetime">The DateTime for which the calculation is being made.</param>
        /// <param name="timeunit">The TimeUnit for the increase.</param>
        /// <param name="timevalue">The number of TimeUnits for which the increase is to be made.</param>
        /// <returns>Tick value for teh specified parameters.</returns>
        internal static long GetTickIncrease(TimeSpan timespan, DateTime datetime, TimeFrequency.TimeUnit timeunit, int timevalue)
        {
            long ticks;
            DateTime forwardDate;

            switch (timeunit)
            {
                case TimeFrequency.TimeUnit.Year:
                    // Determine the number of ticks represented by the increase
                    forwardDate = datetime.AddYears(timevalue);
                    ticks = forwardDate.Ticks - datetime.Ticks;
                    break;
                case TimeFrequency.TimeUnit.Month:
                    // Determine the number of ticks represented by the increase
                    forwardDate = datetime.AddMonths(timevalue);
                    ticks = forwardDate.Ticks - datetime.Ticks;
                    break;
                case TimeFrequency.TimeUnit.Week:
                case TimeFrequency.TimeUnit.Day:
                case TimeFrequency.TimeUnit.Hour:
                case TimeFrequency.TimeUnit.Minute:
                case TimeFrequency.TimeUnit.Second:
                    // Just add the normal ticks
                    ticks = timespan.Ticks;
                    break;
                default:
                    // Just add the normal ticks
                    ticks = timespan.Ticks;
                    break;
            }

            return ticks;
        }

        /// <summary>
        /// Determines the Ticks to be plotted for a given TimeUnit and Value rounded to the interval.
        /// </summary>
        /// <param name="timespan">The TimeSpane for the calculation.</param>
        /// <param name="datetime">The DateTime for which the calculation is being made.</param>
        /// <param name="timeunit">The TimeUnit for the increase.</param>
        /// <param name="timevalue">The number of TimeUnits for which the increase is to be made.</param>
        /// <returns>Tick value for teh specified parameters.</returns>
        internal static long GetTickIncreaseRounded(TimeSpan timespan, DateTime datetime, TimeFrequency.TimeUnit timeunit, int timevalue)
        {
            long ticks = AxisHelper.GetTickIncrease(timespan, datetime, timeunit, timevalue);
            DateTime endTime = datetime.AddTicks(ticks);

            // Round Up to the nearest Interval
            double remainingTicks = endTime.Ticks % ticks;
            long ticksToAdd = ticks - (long)remainingTicks;
            if (endTime.Ticks + ticksToAdd < DateTime.MaxValue.Ticks)
            {
                ticks += ticksToAdd;
            }

            return ticks;
        }
    }
}
