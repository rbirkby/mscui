//-----------------------------------------------------------------------
// <copyright file="TimeSpanTest.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09-May-2007</date>
// <summary>Client-side javascript for NhsTimeSpan tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

/// <summary>
/// Unit tests to test the Time class in the NhsCui.Toolkit.DateAndTime namespace
/// </summary>
var NhsTimeSpanTest = NhsCui.Toolkit.Web.Tests.NhsTimeSpanTest = function() 
{
};

NhsTimeSpanTest._get_baseDate = function()
{
    return new Date(2007, 1, 21);
};

NhsCui.Toolkit.Web.Tests.NhsTimeSpanTest.prototype = 
{
    /// <summary>
    /// Test the constructor NhsTimeSpan(Date from) and FixGranularityAndThreshold for Years > 18
    /// </summary>
    TimeSpanFromOnlyConstructorTest : function()
    {
        var fromYear = 1980;
        var fromMonth = 1;
        var fromDay = 1;
        
        var today = new Date();

        var from = new Date(fromYear, fromMonth, fromDay);
        var ts = new NhsTimeSpan(from);
        ts.set_isAge(true);

        var thisYearsBirthday = new Date(today.getFullYear(), fromMonth, fromDay);
        
        var expectedAge = 0;

        while (thisYearsBirthday > from)
        {
            thisYearsBirthday.setFullYear(thisYearsBirthday.getFullYear() - 1);
            expectedAge++;
        }

        Assert.AreEqual(expectedAge, ts.get_years(), "Age returned does not match");
    },

    /// <summary>
    /// Test the constructor NhsTimeSpan(Date from, Date to)
    /// </summary>
    TimeSpanFromAndToConstructorTest : function()
    {
        var fromYear = 1980;
        var fromMonth = 1;
        var fromDay = 1;

        var today = new Date();

        var from = new Date(fromYear, fromMonth, fromDay);
        var ts = new NhsTimeSpan(from, new Date());
        ts.set_isAge(true);

        var thisYearsBirthday = new Date(today.getFullYear(), fromMonth, fromDay);
        
        var expectedAge = 0;
        while (thisYearsBirthday > from)
        {
            thisYearsBirthday.setFullYear(thisYearsBirthday.getFullYear() - 1);
            expectedAge++;
        }

        Assert.AreEqual(expectedAge, ts.get_years(), "Age returned does not match");
    },
                
    /// <summary>
    /// Test that ToString works for an Age Less than 2 hours
    /// </summary>
    ToString_IsAge_LessThan2Hours : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        
        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (90 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("90min", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age Less than 2 days
    /// </summary>
    ToString_IsAge_GreaterThan2HoursLessThan2Days : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (24 * 60 * 60 * 1000) + (2 * 60 * 60 * 1000) + (5 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("26hrs", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan4WeeksGreaterThan2Days
    /// </summary>
    ToString_IsAge_LessThan4WeeksGreaterThan2Days : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (3 * 24 * 60 * 60 * 1000) + (17 * 60 * 60 * 1000) + (7 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("3d", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age Less Than Month
    /// </summary>
    ToString_IsAge_LessThan1Month : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (27 * 24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000) + (2 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("27d", ts.toString());
    },
    
    /// <summary>
    /// Test that ToString works for an Age Less Than 1 Year Is 4 Weeks
    /// </summary>
    ToString_IsAge_LessThan1YearIs4Weeks : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (28 * 24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000) + (2 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("4w", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan1YearGreaterThan4WeeksAndADay
    /// </summary>
    ToString_IsAge_LessThan1YearGreaterThan4WeeksAndADay : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (29 * 24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000) + (2 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("4w 1d", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan2YearsGreaterThan1Year
    /// </summary>
    ToString_IsAge_LessThan2YearsGreaterThan1Year : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(baseDate.getTime() + (365 * 24 * 60 * 60 * 1000) + (24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("12m 1d", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan2YearsGreaterThan1YearAndAWeek
    /// </summary>
    ToString_IsAge_LessThan2YearsGreaterThan1YearAndAWeek : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(baseDate.getTime() + (365 * 24 * 60 * 60 * 1000) + (8 * 24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("12m 1w 1d", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan2YearsGreaterThan13Months
    /// </summary>
    ToString_IsAge_LessThan2YearsGreaterThan13Months : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = new Date("January 21, 2007");

        ts.set_from(baseDate);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(baseDate.getTime() + (365 * 24 * 60 * 60 * 1000) + (39 * 24 * 60 * 60 * 1000) + (5 * 60 * 60 * 1000));
        ts.set_to(newDate);
        
        ts.set_isAge(true);

        Assert.AreEqual("13m 1w 1d", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for an Age LessThan18YearsGreaterThan4Years
    /// </summary>
    ToString_IsAge_LessThan18YearsGreaterThan4Years : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(baseDate.getTime() + (4 * 365 * 24 * 60 * 60 * 1000) + (39 * 24 * 60 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(true);

        Assert.AreEqual("4y 1m", ts.toString());
    },
           
    /// <summary>
    /// Test that ToString works for Less than 2 days
    /// </summary>
    ToString_LessThan2Hours_MinutesAndMinutes : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (90 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(false);

        ts.set_granularity(TimeSpanUnit.Minutes);
        ts.set_threshold(TimeSpanUnit.Minutes);
        
        Assert.AreEqual("90min", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for Less than 2 hours
    /// </summary>
    ToString_LessThan2Hours_HoursAndMinutes : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();

        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (90 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_isAge(false);

        ts.set_granularity(TimeSpanUnit.Hours);
        ts.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual("1hr 30min", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for GreaterThan2Hours_LessThan2Days with Granularity and Threshold set to HoursAndMinutes
    /// </summary>
    ToString_GreaterThan2Hours_LessThan2Days_HoursToMinutes : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);
        
        ts.set_from(baseDate);

        var newDate = new Date(baseDate.getTime() + (24 * 60 * 60 * 1000) + (2 * 60 * 60 * 1000) + (5 * 60 * 1000));
        ts.set_to(newDate);

        ts.set_granularity(TimeSpanUnit.Hours);
        ts.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual("26hrs 5min", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for GreaterThan2Hours_LessThan2Days with Granularity and Threshold set to DaysToMinutes
    /// </summary>
    ToString_GreaterThan2Hours_LessThan2Days_DaysToMinutes : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);

        ts.set_from(baseDate);

        ts.set_granularity(TimeSpanUnit.Days);
        ts.set_threshold(TimeSpanUnit.Minutes);

        var newDate = new Date(baseDate.getTime() + (24 * 60 * 60 * 1000) + (2 * 60 * 60 * 1000) + (5 * 60 * 1000));
        ts.set_to(newDate);

        Assert.AreEqual("1d 2hrs 5min", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for 3Days17Hours7Minutes with Granularity and Threshold set to Hours to Hours
    /// </summary>
    ToString_3Days17Hours7Minutes : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);

        ts.set_from(baseDate);

        ts.set_granularity(TimeSpanUnit.Hours);
        ts.set_threshold(TimeSpanUnit.Hours);

        var newDate = new Date(baseDate.getTime() + (3 * 24 * 60 * 60 * 1000) + (17 * 60 * 60 * 1000) + (7 * 60 * 1000));
        ts.set_to(newDate);

        Assert.AreEqual("89hrs", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for 4Years39Days with Granularity and Threshold set to Months to Months
    /// </summary>
    ToString_1Year1Day5Hours : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);

        ts.set_from(baseDate);

        ts.set_granularity(TimeSpanUnit.Months);
        ts.set_threshold(TimeSpanUnit.Months);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(baseDate.getTime() + (4 * 365 * 24 * 60 * 60 * 1000) + (39 * 24 * 60 * 60 * 1000));
        ts.set_to(newDate);

        Assert.AreEqual("49m", ts.toString());
    },

    /// <summary>
    /// Test that ToString works for long format
    /// </summary>
    ToStringLongUnitLength : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);

        ts.set_from(baseDate);

        ts.set_granularity(TimeSpanUnit.Years);
        ts.set_threshold(TimeSpanUnit.Minutes);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        // NB: Here we have to add extra day for leap year...
        var newDate = new Date(baseDate.getTime() + (3 * 365 * 24 * 60 * 60 * 1000) + (2 * 24 * 60 * 60 * 1000) + (2 * 60 * 60 * 1000) + (5 * 60 * 1000));
        ts.set_to(newDate);
        var formattedTimeSpan = ts.toString(TimeSpanUnitLength.Long);

        Assert.AreEqual("3years 1day 2hours 5minutes", formattedTimeSpan);
    },

    /// <summary>
    /// Test that ToString works for automatic format
    /// </summary>
    ToStringAutomaticUnitLength : function()
    {
        var ts = new NhsTimeSpan();

        var baseDate = NhsTimeSpanTest._get_baseDate();
        ts.set_isAge(false);

        ts.set_from(baseDate);

        ts.set_granularity(TimeSpanUnit.Years);
        ts.set_threshold(TimeSpanUnit.Minutes);

        var newDate = new Date(baseDate.getTime() + (2 * 24 * 60 * 60 * 1000) + (1 * 60 * 60 * 1000) + (5 * 60 * 1000));
        ts.set_to(newDate);
        
        ts.set_isAge(false);
        var formattedTimeSpan = ts.toString(TimeSpanUnitLength.Automatic);

        Assert.AreEqual("2d 1hr 5min", formattedTimeSpan);

        ts.set_isAge(true);
        formattedTimeSpan = ts.toString(TimeSpanUnitLength.Automatic);

        // IsAge=true, 2days < timespan < 4 weeks, granularity=days, threshold=days
        Assert.AreEqual("2d", formattedTimeSpan);

        // Using multiples of 365 days is not strictly an accurate way to add years in all cases
        // but we aren't testing the javaScript Date object...and it will do here
        var newDate = new Date(ts.get_to().getTime() + (2 * 365 * 24 * 60 * 60 * 1000));
        ts.set_to(newDate);
        formattedTimeSpan = ts.toString(TimeSpanUnitLength.Automatic);

        // IsAge=true, 2 years < timespan < 18 years, granularity=years, threshold=months
        Assert.AreEqual("2years", formattedTimeSpan);
    },

    /// <summary>
    /// Test Parse for valid but repeated TimeSpan units
    /// </summary>
    Parse_InvalidTextRepeatedValues : function()
    {
        // Pass a TimeSpan in twice to simulate multiple entries for the same unit e.g "3d 3d"
        Assert.IsTrue(NhsTimeSpan.tryParse("3d 3d") === null);
    },

    /// <summary>
    /// Test Parse for invalid text
    /// </summary>
    ParseInvalidText : function()
    {
        // Pass rubbish into TimeSpan
        Assert.IsTrue(NhsTimeSpan.tryParse("hsh sdoih") === null);
    },

    /// <summary>
    /// Test Parse for a free text
    /// </summary>
    Parse_Text : function()
    {
        var timeSpan = NhsTimeSpan.parseExact("3d 17hrs 7min");
        var expectedTo = new Date(timeSpan.get_from().getTime() + (3 * 24 * 60 * 60 * 1000) + (17 * 60 * 60 * 1000) + (7 * 60 * 1000));
        
        Assert.AreEqual(timeSpan.get_to().getTime(), expectedTo.getTime(), "TimeSpan To does not match expected date");
    },

    /// <summary>
    /// Test Parse for a free text
    /// </summary>
    Parse_Text_YearsToMinutes : function()
    {
        var timeSpan = NhsTimeSpan.parseExact("1y 3d 17hrs 7min");

        // var expectedTo = new Date(timeSpan.get_from().getTime() + (365 * 24 * 60 * 60 * 1000) + (3 * 24 * 60 * 60 * 1000) + (17 * 60 * 60 * 1000) + (7 * 60 * 1000));
        var expectedTo = new Date(timeSpan.get_from().getTime());
        expectedTo.setYear(expectedTo.getYear() + 1);
        expectedTo.setDate(expectedTo.getDate() + 3);
        expectedTo.setHours(expectedTo.getHours() + 17);
        expectedTo.setMinutes(expectedTo.getMinutes() + 7);

        Assert.AreEqual(timeSpan.get_to().getTime(), expectedTo.getTime(), "TimeSpan To does not match expected date");
    },

    /// <summary>
    /// Test Parse for a text containing long units
    /// </summary>
    Parse_LongText_YearsToMinutes : function()
    {
        var timeSpan = NhsTimeSpan.parseExact("1years 3days 17hours 7minutes");

        // var expectedTo = new Date(timeSpan.get_from().getTime() + (365 * 24 * 60 * 60 * 1000) + (3 * 24 * 60 * 60 * 1000) + (17 * 60 * 60 * 1000) + (7 * 60 * 1000));
        var expectedTo = new Date(timeSpan.get_from().getTime());
        expectedTo.setYear(expectedTo.getYear() + 1);
        expectedTo.setDate(expectedTo.getDate() + 3);
        expectedTo.setHours(expectedTo.getHours() + 17);
        expectedTo.setMinutes(expectedTo.getMinutes() + 7);

        Assert.AreEqual(timeSpan.get_to().getTime(), expectedTo.getTime(), "TimeSpan To does not match expected date");
    },

    /// <summary>
    /// Test the ArgumentNullException for Parse method
    /// </summary>

    // [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Parse method")]
    NullArgumentTimeSpanParseTest : function()
    {
        NhsTimeSpan.parseExact(null);
    }
};


 NhsTimeSpanTest.prototype.NullArgumentTimeSpanParseTest.expectedException={name:"Sys.ArgumentNullException", 
                                                         message:"Sys.ArgumentNullException: Value cannot be null."};
//NhsTimeSpanTest.prototype.NullArgumentTimeSpanParseTest.expectedException={name:"TypeError", 
//                                                         message:"Object doesn't support this property or method"};
