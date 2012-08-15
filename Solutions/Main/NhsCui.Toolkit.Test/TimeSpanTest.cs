//-----------------------------------------------------------------------
// <copyright file="TimeSpanTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Unit tests to test the TimeSpan class in the NhsCui.Toolkit.TimeAndTime namespace</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Unit tests to test the TimeSpan class in the NhsCui.Toolkit.TimeAndTime namespace
    /// </summary>
    [TestClass,
    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
    public class TimeSpanTest
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TimeSpanTest()
        {
        }

        /// <summary>
        /// Base date from which to calculate timespans
        /// </summary>
        private static DateTime BaseDate
        {
            get
            {
                return new DateTime(2007, 1, 21);
            }
        }

        /// <summary>
        /// Test the constructor NhsTimeSpan(DateTime from) and FixGranularityAndThreshold for Years > 18
        /// </summary>
        [TestMethod]
        public void TimeSpanFromOnlyConstructorTest()
        {
            int fromYear = 1980;
            int fromMonth = 1;
            int fromDay = 1;

            DateTime from = new DateTime(fromYear, fromMonth, fromDay);
            NhsTimeSpan ts = new NhsTimeSpan(from);
            ts.IsAge = true;

            DateTime thisYearsBirthday = new DateTime(DateTime.Today.Year, fromMonth, fromDay);
            int expectedAge = 0;
            while (thisYearsBirthday > from)
            {
                thisYearsBirthday = thisYearsBirthday.AddYears(-1);
                expectedAge++;
            }

            Assert.AreEqual(expectedAge, ts.Years, "Age returned does not match");
        }

        /// <summary>
        /// Test the constructor NhsTimeSpan(DateTime from, DateTime to)
        /// </summary>
        [TestMethod]
        public void TimeSpanFromAndToConstructorTest()
        {
            int fromYear = 1980;
            int fromMonth = 6;
            int fromDay = 6;

            DateTime thisYearsBirthday = new DateTime(DateTime.Today.Year, fromMonth, fromDay);
            DateTime from = new DateTime(fromYear, fromMonth, fromDay);
            NhsTimeSpan ts = new NhsTimeSpan(from, thisYearsBirthday);
            ts.IsAge = true;

            int expectedAge = 0;
            while (thisYearsBirthday > from)
            {
                thisYearsBirthday = thisYearsBirthday.AddYears(-1);
                expectedAge++;
            }

            Assert.AreEqual(expectedAge, ts.Years, "Age returned does not match");
        }

        /// <summary>
        /// Test that ToString works for an Age Less than 2 hours
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan2Hours()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddMinutes(90);

            ts.IsAge = true;

            Assert.AreEqual("90min", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age Less than 2 days
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_GreaterThan2HoursLessThan2Days()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(1).AddHours(2).AddMinutes(5);

            ts.IsAge = true;

            Assert.AreEqual("26hrs", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan4WeeksGreaterThan2Days
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan4WeeksGreaterThan2Days()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(3).AddHours(17).AddMinutes(7);

            ts.IsAge = true;

            Assert.AreEqual("3d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age Less Than Month
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan1Month()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(27).AddHours(5).AddMinutes(2);

            ts.IsAge = true;

            Assert.AreEqual("27d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age Less Than 1 Year Is 4 Weeks
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan1YearIs4Weeks()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(28).AddHours(5).AddMinutes(2);

            ts.IsAge = true;

            Assert.AreEqual("4w", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan1YearGreaterThan4WeeksAndADay
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan1YearGreaterThan4WeeksAndADay()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(29).AddHours(5).AddMinutes(2);

            ts.IsAge = true;

            Assert.AreEqual("4w 1d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan2YearsGreaterThan1Year
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan2YearsGreaterThan1Year()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddYears(1).AddDays(1).AddHours(5);

            ts.IsAge = true;

            Assert.AreEqual("12m 1d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan2YearsGreaterThan1YearAndAWeek
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan2YearsGreaterThan1YearAndAWeek()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddYears(1).AddDays(8).AddHours(5);

            ts.IsAge = true;

            Assert.AreEqual("12m 1w 1d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan2YearsGreaterThan13Months
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan2YearsGreaterThan13Months()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = DateTime.Parse("21 January 2007", CultureInfo.CurrentUICulture);

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddYears(1).AddDays(39).AddHours(5);

            ts.IsAge = true;

            Assert.AreEqual("13m 1w 1d", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for an Age LessThan18YearsGreaterThan4Years
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_IsAge_LessThan18YearsGreaterThan4Years()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddYears(4).AddDays(39);

            ts.IsAge = true;

            Assert.AreEqual("4y 1m", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for Less than 2 days
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_LessThan2Hours_MinutesAndMinutes()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddMinutes(90);

            ts.IsAge = false;

            ts.Granularity = TimeSpanUnit.Minutes;
            ts.Threshold = TimeSpanUnit.Minutes;

            Assert.AreEqual("90min", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for Less than 2 hours
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_LessThan2Hours_HoursAndMinutes()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddMinutes(90);

            ts.IsAge = false;

            ts.Granularity = TimeSpanUnit.Hours;
            ts.Threshold = TimeSpanUnit.Minutes;

            Assert.AreEqual("1hr 30min", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for GreaterThan2Hours_LessThan2Days with Granularity and Threshold set to HoursAndMinutes
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_GreaterThan2Hours_LessThan2Days_HoursToMinutes()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.To = baseDateTime.AddDays(1).AddHours(2).AddMinutes(5);

            ts.Granularity = TimeSpanUnit.Hours;
            ts.Threshold = TimeSpanUnit.Minutes;

            Assert.AreEqual("26hrs 5min", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for GreaterThan2Hours_LessThan2Days with Granularity and Threshold set to DaysToMinutes
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_GreaterThan2Hours_LessThan2Days_DaysToMinutes()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.Granularity = TimeSpanUnit.Days;
            ts.Threshold = TimeSpanUnit.Minutes;

            ts.To = baseDateTime.AddDays(1).AddHours(2).AddMinutes(5);

            Assert.AreEqual("1d 2hrs 5min", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for 3Days17Hours7Minutes with Granularity and Threshold set to Hours to Hours
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_3Days17Hours7Minutes()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.Granularity = TimeSpanUnit.Hours;
            ts.Threshold = TimeSpanUnit.Hours;

            ts.To = baseDateTime.AddDays(3).AddHours(17).AddMinutes(7);

            Assert.AreEqual("89hrs", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for 4Years39Days with Granularity and Threshold set to Months to Months
        /// </summary>
        [TestMethod,
        SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
        public void ToString_1Year1Day5Hours()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.Granularity = TimeSpanUnit.Months;
            ts.Threshold = TimeSpanUnit.Months;

            ts.To = baseDateTime.AddYears(4).AddDays(39);

            Assert.AreEqual("49m", ts.ToString());
        }

        /// <summary>
        /// Test that ToString works for long format
        /// </summary>
        [TestMethod]
        public void ToStringLongUnitLength()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.Granularity = TimeSpanUnit.Years;
            ts.Threshold = TimeSpanUnit.Minutes;

            ts.To = baseDateTime.AddYears(3).AddDays(1).AddHours(2).AddMinutes(5);
            string formattedTimeSpan = ts.ToString(TimeSpanUnitLength.Long, CultureInfo.CurrentCulture);

            Assert.AreEqual("3years 1day 2hours 5minutes", formattedTimeSpan);
        }

        /// <summary>
        /// Test that ToString works for automatic format
        /// </summary>
        [TestMethod]
        public void ToStringAutomaticUnitLength()
        {
            NhsTimeSpan ts = new NhsTimeSpan();

            DateTime baseDateTime = BaseDate;
            ts.IsAge = false;

            ts.From = baseDateTime;

            ts.Granularity = TimeSpanUnit.Years;
            ts.Threshold = TimeSpanUnit.Minutes;

            ts.To = baseDateTime.AddDays(2).AddHours(1).AddMinutes(5);
            ts.IsAge = false;
            string formattedTimeSpan = ts.ToString(TimeSpanUnitLength.Automatic, CultureInfo.CurrentCulture);

            Assert.AreEqual("2d 1hr 5min", formattedTimeSpan);

            ts.IsAge = true;
            formattedTimeSpan = ts.ToString(TimeSpanUnitLength.Automatic, CultureInfo.CurrentCulture);

            // IsAge=true, 2days < timespan < 4 weeks, granularity=days, threshold=days
            Assert.AreEqual("2d", formattedTimeSpan);

            ts.To = ts.To.AddYears(2);
            formattedTimeSpan = ts.ToString(TimeSpanUnitLength.Automatic, CultureInfo.CurrentCulture);

            // IsAge=true, 2 years < timespan < 18 years, granularity=years, threshold=months
            Assert.AreEqual("2years", formattedTimeSpan);
        }

        /// <summary>
        /// Test Parse for valid but repeated TimeSpan units
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public void Parse_InvalidTextRepeatedValues()
        {
            DateTime baseDateTime = BaseDate;

            NhsTimeSpan timeSpan = new NhsTimeSpan();

            timeSpan.To = baseDateTime.AddDays(3).AddHours(17);

            NhsTimeSpan parsedTimeSpan;

            // Pass a TimeSpan in twice to simulate multiple entries for the same unit e.g "3d 3d"
            Assert.IsFalse(NhsTimeSpan.TryParse(string.Format(CultureInfo.InvariantCulture, "{0} {1}", timeSpan.ToString(), timeSpan.ToString()), out parsedTimeSpan, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Test Parse for valid but repeated TimeSpan units
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public void ParseInvalidText()
        {
            NhsTimeSpan parsedTimeSpan;

            // Pass rubbish into TimeSpan
            Assert.IsFalse(NhsTimeSpan.TryParse("hsh sdoih", out parsedTimeSpan, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Test Parse for a free text
        /// </summary>
        [TestMethod, SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public void Parse_Text()
        {
            NhsTimeSpan timeSpan = NhsTimeSpan.Parse("3d 17hrs 7min", CultureInfo.InvariantCulture);
            DateTime expectedTo = timeSpan.From.AddDays(3).AddHours(17).AddMinutes(7);

            Assert.AreEqual(timeSpan.To, expectedTo, "TimeSpan To does not match expected date");
        }

        //// <summary>
        //// Test Parse for a free text
        //// </summary>
        //// [TestMethod,
        //// SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        //// public void Parse_Text_YearsToMinutes()
        //// {
        ////     NhsTimeSpan timeSpan = NhsTimeSpan.Parse("1y 3d 17hrs 7min", CultureInfo.InvariantCulture);
        ////     DateTime expectedTo = timeSpan.From.AddYears(1).AddDays(3).AddHours(17).AddMinutes(7);
        //// 
        ////     Assert.AreEqual(timeSpan.To, expectedTo, "TimeSpan To does not match expected date");
        //// }

        //// <summary>
        //// Test Parse for a text containing long units
        //// </summary>
        //// [TestMethod,
        //// SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        //// public void Parse_LongText_YearsToMinutes()
        //// {
        ////     NhsTimeSpan timeSpan = NhsTimeSpan.Parse("1years 3days 17hours 7minutes", CultureInfo.InvariantCulture);
        ////     DateTime expectedTo = timeSpan.From.AddYears(1).AddDays(3).AddHours(17).AddMinutes(7);
        //// 
        ////     Assert.AreEqual(timeSpan.To, expectedTo, "TimeSpan To does not match expected date");
        //// }

        /// <summary>
        /// Test the ArgumentNullException for Parse method
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Parse method")]
        public void NullArgumentTimeSpanParseTest()
        {
            NhsTimeSpan.Parse(null, CultureInfo.InvariantCulture);
        }
    }
}
