//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBoxTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for TimeSpanInputBox tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

var baseNamingContainer="ctl00_ContentPlaceHolder1_TimeSpanInputBox_TimeSpanInputBox_";
var controlId=baseNamingContainer + "timeSpanInputBoxExtender";
var textboxId=baseNamingContainer + "TextBox";
var checkboxId=textboxId + "_approximate";


function hasElement(id) 
{
    var doc = testHarness.getDocument();
    var element = doc.getElementById(id);
    return !!element;
} 

/// <summary>
/// Unit tests to test the TimeSpanInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var TimeSpanInputBoxTests = NhsCui.Toolkit.Web.Tests.TimeSpanInputBoxTests = function() 
{
};

TimeSpanInputBoxTests._get_baseDate = function()
{
    return new Date(2007, 1, 21);
};

NhsCui.Toolkit.Web.Tests.TimeSpanInputBoxTests.prototype = 
{
    // Example 1
    Example01_IsAgeTrueGranularityMinutesThresholdMinutes : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setHours(toDate.getHours() + 1);
        toDate.setMinutes(toDate.getMinutes() + 30);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        var expectedValue = "90min";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Minutes);
        testControlInstance.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },

    // Example 2
    Example02_IsAgeTrueGranularityHoursThresholdHours : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();
        
        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 1);
        toDate.setHours(toDate.getHours() + 2);
        toDate.setMinutes(toDate.getMinutes() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        var expectedValue = "26hrs";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Hours);
        testControlInstance.set_threshold(TimeSpanUnit.Hours);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 3
    Example03_IsAgeTrueGranularityDaysThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 3);
        toDate.setHours(toDate.getHours() + 17);
        toDate.setMinutes(toDate.getMinutes() + 7);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        var expectedValue = "3d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Days);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 4
    Example04_IsAgeTrueGranularityDaysThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 27);
        toDate.setHours(toDate.getHours() + 5);
        toDate.setMinutes(toDate.getMinutes() + 2);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "27d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Days);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 5
    Example05_IsAgeTrueGranularityWeeksThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 28);
        toDate.setHours(toDate.getHours() + 5);
        toDate.setMinutes(toDate.getMinutes() + 2);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "4w";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Weeks);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 6
    Example06_IsAgeTrueGranularityWeeksThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 29);
        toDate.setHours(toDate.getHours() + 5);
        toDate.setMinutes(toDate.getMinutes() + 2);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        var expectedValue = "4w 1d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Weeks);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 7
    Example07_IsAgeTrueGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 1);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "12m 1d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 8
    Example08_IsAgeTrueGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();
        

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 8);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "12m 1w 1d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 9
    Example09_IsAgeTrueGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 39);
        toDate.setHours(toDate.getHours() + 5);
        
        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "13m 1w 3d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 10
    Example10_IsAgeTrueGranularityYearsThresholdMonths : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 4);
        toDate.setDate(toDate.getDate() + 39);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "4y 1m";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(true);
        testControlInstance.set_granularity(TimeSpanUnit.Years);
        testControlInstance.set_threshold(TimeSpanUnit.Months);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 11
    Example11_IsAgeFalseGranularityHoursThresholdMinutes : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setHours(toDate.getHours() + 1);
        toDate.setMinutes(toDate.getMinutes() + 30);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "1hr 30min";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Hours);
        testControlInstance.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 12
    Example12_IsAgeFalseGranularityMinutesThresholdMinutes : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setHours(toDate.getHours() + 1);
        toDate.setMinutes(toDate.getMinutes() + 30);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "90min";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Minutes);
        testControlInstance.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 13
    Example13_IsAgeFalseGranularityHoursThresholdMinutes : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 1);
        toDate.setHours(toDate.getHours() + 2);
        toDate.setMinutes(toDate.getMinutes() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "26hrs 5min";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Hours);
        testControlInstance.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 14
    Example14_IsAgeFalseGranularityDaysThresholdMinutes : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 1);
        toDate.setHours(toDate.getHours() + 2);
        toDate.setMinutes(toDate.getMinutes() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "1d 2hrs 5min";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Days);
        testControlInstance.set_threshold(TimeSpanUnit.Minutes);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 15
    Example15_IsAgeFalseGranularityHoursThresholdHours : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setDate(toDate.getDate() + 3);
        toDate.setHours(toDate.getHours() + 17);
        toDate.setMinutes(toDate.getMinutes() + 7);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "89hrs";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Hours);
        testControlInstance.set_threshold(TimeSpanUnit.Hours);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 16
    Example16_IsAgeFalseGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 1);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "12m 1d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 17
    Example17_IsAgeFalseGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 8);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "12m 8d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 18
    Example18_IsAgeFalseGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        // var fromDate = TimeSpanInputBoxTests._get_baseDate();
        var fromDate = new Date("1 January 2007");

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 39);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "13m 8d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 19
    Example19_IsAgeFalseGranularityMonthsThresholdDays : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = new Date("1 January 2007");
        // var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 1);
        toDate.setDate(toDate.getDate() + 39);
        toDate.setHours(toDate.getHours() + 5);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "1y 1m 8d";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Years);
        testControlInstance.set_threshold(TimeSpanUnit.Days);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    },
    
    // Example 20
    Example20_IsAgeFalseGranularityMonthsThresholdMonths : function() 
    {
        var testControlInstance = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        var fromDate = TimeSpanInputBoxTests._get_baseDate();

        var toDate = new Date(fromDate.getTime());
        toDate.setFullYear(toDate.getFullYear() + 4);
        toDate.setDate(toDate.getDate() + 39);

        var newTimeSpan = new NhsTimeSpan(fromDate, toDate);
        
        var expectedValue = "49m";
        
        // testControlInstance.set_text(newTimeSpan.toString());
        testControlInstance.set_value(newTimeSpan);
        
        testControlInstance.set_isAge(false);
        testControlInstance.set_granularity(TimeSpanUnit.Months);
        testControlInstance.set_threshold(TimeSpanUnit.Months);

        Assert.AreEqual(expectedValue, elt.value, "TextBox value [" + elt.value + "] does not match expected value [" + expectedValue + "]");
    }
};


