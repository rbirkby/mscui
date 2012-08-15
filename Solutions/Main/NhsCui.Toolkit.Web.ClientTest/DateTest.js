//-----------------------------------------------------------------------
// <copyright file="DateTest.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NhsDate tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

/// <summary>
/// Unit tests to test the Time class in the NhsCui.Toolkit.DateAndTime namespace
/// </summary>
var NhsDateTest = NhsCui.Toolkit.Web.Tests.NhsDateTest = function() {
}

NhsCui.Toolkit.Web.Tests.NhsDateTest.prototype = {

    /// <summary>
    /// Test the ArgumentNullException for instruction parameter
    /// </summary>
//    [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
    NullInstructionAddTest : function()
    {
        var nowAsItWasAtTheStartofTheTest = new Date();
        var date = new NhsDate(nowAsItWasAtTheStartofTheTest);
        NhsDate.add(date, null);            
    },

    /// <summary>
    /// Test the ArgumentNullException for sourcedate parameter
    /// </summary>
//    [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from Add method")]
    NullSourceDateAddTest : function()
    {
        NhsDate.add(null, String.format("+1{0}", NhsDateResources.DaysUnit));
    },
    
    /// <summary>
    /// Test the ArgumentOutOfRangeException for instruction parameter
    /// </summary>
//    [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Should throw ArgumentOutOfRangeException from Add method")]
    InvalidInstructionAddTest : function()
    {
        var date = new Date();
        var sourcedate = new NhsDate(date);
        NhsDate.add(sourcedate, String.format("+1{0}", "X"));
    },

    /// <summary>
    /// Test the ArgumentOutOfRangeException for sourcedate parameter being a year
    /// </summary>
//    [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Should throw ArgumentOutOfRangeException from Add method")]
    SourceDateYearAddTest : function()
    {
        var year = 2007;
        var date = new NhsDate(year);
        NhsDate.add(date, String.format("+1{0}", NhsDateResources.DaysUnit));
    },

    /// <summary>
    /// Test the ability to do simple arithemtic updates to date by asking Date to add 1 day
    /// </summary>
    DataAddDay : function()
    {
        var nowAsItWasAtTheStartofTheTest = new Date(); 

        var date = new NhsDate(nowAsItWasAtTheStartofTheTest);

        date.add(String.format("+1{0}", NhsDateResources.DaysUnit));

        nowAsItWasAtTheStartofTheTest.setDate(nowAsItWasAtTheStartofTheTest.getDate() + 1)
        
        Assert.AreEqual(date.get_dateValue().valueOf(), nowAsItWasAtTheStartofTheTest.valueOf(), "Dates should match");
    },

    /// <summary>
    /// Test the ability to do complex arithemtic updates to date by asking Date to add 1 month and 1 day
    /// </summary>
    DataAddMonthAndDay : function()
    {
        var dateToAddTo = new Date(2007,0,29); 

        var date = new NhsDate(dateToAddTo);

        date.add(String.format("+1{0}+1{1}", NhsDateResources.MonthsUnit, NhsDateResources.DaysUnit));
        
        Assert.AreEqual(new Date(2007,2,1).valueOf(), date.get_dateValue().valueOf(), "Dates should match");
    },

    /// <summary>
    /// Test the ability to do simple arithemtic updates to date by asking Date to Add 7 years
    /// </summary>
    DataAdd7Years : function()
    {
        var dateToAddTo = new Date(2001,1,2); 

        var date = new NhsDate(dateToAddTo);

        date.add(String.format("+7{0}", NhsDateResources.YearsUnit));

        //Sys.Debug.trace("DataAdd7Years: ACTUAL " + date.get_dateValue().valueOf())
                
        Assert.AreEqual(new Date(2008,1,2).valueOf(), date.get_dateValue().valueOf(), "Dates should match");
    },

    /// <summary>
    /// Test the ability to do simple arithemtic updates to date by asking Date to Subtract 7 years
    /// </summary>
    DataSubtract7Years : function()
    {
        var dateToSubtractFrom = new Date(2009,3,4); 

        var date = new NhsDate(dateToSubtractFrom);

        date.add(String.format("-7{0}", NhsDateResources.YearsUnit));
        
        //Sys.Debug.trace("DataSubtract7Years: ACTUAL " + date.get_dateValue().valueOf())
        
        Assert.AreEqual(new Date(2002,3,4).valueOf(), date.get_dateValue().valueOf(), "Dates should match");
    },

    /// <summary>
    /// Test the ability to do complex arithemtic updates to date by asking Date to Subtract 2 years AND add 5 days
    /// </summary>
    DataSubtract2YearsAdd5Days : function()
    {
        var dateToAddTo = new Date(2004, 1, 28); 

        var date = new NhsDate(dateToAddTo);

        date.add(String.format("-2{0}+5{1}", NhsDateResources.YearsUnit, NhsDateResources.DaysUnit));
        
        Sys.Debug.trace("DataSubtract2YearsAdd5Days: ACTUAL " + date.get_dateValue().valueOf())
        
        Assert.AreEqual(new Date(2002, 2, 5).valueOf(), date.get_dateValue().valueOf(), "Dates should match");
    },

    /// <summary>
    /// Test ToString on DateType Exact
    /// </summary>
    ToStringExact : function()
    {
        var date = new NhsDate(new Date(1974, 2, 26));

        Assert.AreEqual("26-Mar-1974", date.toString(), "Exact date should match");

        Assert.AreEqual("26-Mar-1974", date.toString(false), "Check that default for 'include day of week' flag is correct");

        // try with includeDayOfWeek flag
        Assert.AreEqual("Tue 26-Mar-1974", date.toString(true), "Exact date with DayOfWeek should match");
    },

    /// <summary>
    /// Test ToString on DateType Exact
    /// </summary>
    ToStringExactGetRelativeText : function()
    {
        var date = new NhsDate(new Date());

        var gc = new GlobalizationService();

        // Today works
        Assert.AreEqual(NhsDateResources.Today, date.toString(false, false, true), "RelativeText of Today is not working");

        date.get_dateValue().setDate(date.get_dateValue().getDate()+1);

        Assert.AreEqual(NhsDateResources.Tomorrow, date.toString(false, false, true), "RelativeText of Tomorrow is not working");

        date.get_dateValue().setDate(date.get_dateValue().getDate()-2);

        Assert.AreEqual(NhsDateResources.Yesterday, date.toString(false, false, true), "RelativeText of Yesterday is not working");
        
        // Check the "Who Wins condition when "Show Day of Week" and "Show Relative Text" are both true
        Assert.AreEqual(NhsDateResources.Yesterday, date.toString(true, false, true), "When includeDayOfWeek, showRelativeText are both true, showRelativeText should win. It is not");

        var testDate = new Date(1974, 3, 26);

        date.set_dateValue(testDate);

        Assert.AreEqual(testDate.format(gc.shortDatePattern), date.toString(), "Check that default for 'include day of week' flag is correct");

        // try with includeDayOfWeek flag

        Assert.AreEqual(testDate.format(gc.shortDatePatternWithDayOfWeek), date.toString(true), "Check that default for 'include day of week' flag is correct");
    },

    /// <summary>
    /// Test ToString on DateType Year
    /// </summary>
    ToStringYear : function()
    {
        var baseDateTime = new Date();

        var date = new NhsDate(baseDateTime);

        date.set_dateType(DateType.Year);

        Assert.AreEqual("0000", date.toString(), "Check the default of Year when an explicit date is passed to ctor");

        date = new NhsDate(baseDateTime.getYear());

        date.set_dateType(DateType.Year);

        Assert.AreEqual(baseDateTime.getYear().toString(), date.toString());
    },

    /// <summary>
    /// Test ToString on DateType YearMonth
    /// </summary>
    ToStringYearMonth : function()
    {
        var baseDateTime = new Date();

        var date = new NhsDate(baseDateTime);

        date.set_dateType(DateType.YearMonth);

        Assert.AreEqual(String.format("{0}-{1}", Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames[1 - 1], "0000"), date.toString(), "Check the default of Month and Year when an explicit date is passed to ctor");
        
        // Now set the date to a year and a month which is what DateType = YearMonth prefers
        date = new NhsDate(1974, 3);

        Assert.AreEqual(
            String.format("{0}-{1}", Sys.CultureInfo.CurrentCulture.dateTimeFormat.MonthNames[3 - 1], "1974"), date.toString());
    },

    /// <summary>
    /// Test ToString on DateType Null
    /// </summary>
    ToStringNull : function()
    {
        var date = new NhsDate();
        date.set_dateType(DateType.Null);
        Assert.IsTrue(date.toString().length === 0, "Expecting ToString() on null date to return emptystring");
    },

    /// <summary>
    /// Test Parse for a Month and Year
    /// </summary>
    ParseMonthAndYear : function()
    {
        var baseDateTime = new Date();

        var date = new NhsDate(baseDateTime);

        date.set_dateType(DateType.YearMonth);
        date.set_year(1974);
        date.set_month(3);

        var parsedDate;

        Assert.IsTrue((parsedDate=NhsDate.tryParse(date.toString()))!=null, String.format("TryParse failed with date, {0}", date.toString()));

        Assert.AreEqual(date.get_month(), parsedDate.get_month(), "Parse did not error but Month does not match");
        
        Assert.AreEqual(date.get_dateType(), parsedDate.get_dateType(), "Parse did not error but DateType does not match");
        Assert.AreEqual(date.get_year(), parsedDate.get_year(), "Parse did not error but Year does not match");
    },

    /// <summary>
    /// Test Parse for a Null
    /// </summary>
    ParseNullIndex : function()
    {
        var date = new NhsDate();

        date.set_dateType(DateType.NullIndex);
        date.set_nullIndex(4);
        
        // Muddy the water by assigning Month and year
        date.set_year(2007);
        date.set_month(3);

        var parsedDate;

        Assert.IsTrue((parsedDate=NhsDate.tryParse(date.toString()))!=null, String.format("TryParse failed with date, {0}", date.toString()));

        Assert.AreEqual(date.get_dateType(), parsedDate.get_dateType(), "Parse did not error but DateType does not match");
        Assert.AreEqual(date.get_nullIndex(), parsedDate.get_nullIndex(), "Parse did not error but NullIndex does not match");
    },

    /// <summary>
    /// Test Parse for a free text
    /// </summary>
    ParseDateText : function()
    {
        var date = new NhsDate("26-Mar-1974");

        var parsedDate;

        Assert.IsTrue((parsedDate=NhsDate.tryParse(date.toString()))!=null, String.format("TryParse failed with date, {0}", date.toString()));

        Assert.AreEqual(date.get_dateType(), parsedDate.get_dateType(), "Parse did not error but DateType does not match");
        Assert.AreEqual(date.get_dateValue().toString(), parsedDate.get_dateValue().toString(), "Parse did not error but DateValue does not match");
    },
    
    /// <summary>
    /// Test Parse null
    ///</summary>
    ParseNull : function()
    {
        var date = NhsDate.parse(null);
        Assert.AreEqual(date.get_dateType(), DateType.Null, "Parse did not error but DateType does not match");
    },

    /// <summary>
    /// Test Parse empty string
    ///</summary>
    ParseEmptyString : function()
    {
        var date = NhsDate.parse("");
        Assert.AreEqual(date.get_dateType(), DateType.Null, "Parse did not error but DateType does not match");
    },

    /// <summary>
    /// Test IsNull Property
    ///</summary>
    IsNullProperty : function()
    {
        var date = new NhsDate();
        // default datetype is exact
        Assert.IsTrue(!date.get_isNull());
        date.set_dateType(DateType.Null);
        Assert.IsTrue(date.get_isNull());
    },

    /// <summary>
    /// Test the ArgumentNullException for IsAddInstruction method
    ///</summary>
//    [ExpectedException(typeof(System.ArgumentNullException), "Should throw Null argument exception from IsAddInstruction method")]
    NullIsAddInstructionTest : function()
    {
        NhsDate.isAddInstruction(null);
    }
}

NhsDateTest.prototype.NullInstructionAddTest.expectedException={name:"Sys.ArgumentNullException", 
                                                         message:"Should throw Null argument exception from Add method"};
NhsDateTest.prototype.NullSourceDateAddTest.expectedException={name:"Sys.ArgumentNullException", 
                                                         message:"Should throw Null argument exception from Add method"};                                                         
NhsDateTest.prototype.InvalidInstructionAddTest.expectedException={name:"Sys.ArgumentOutOfRangeException", 
                                                         message:"Should throw ArgumentOutOfRangeException from Add method"};                                                          
NhsDateTest.prototype.SourceDateYearAddTest.expectedException={name:"Sys.ArgumentOutOfRangeException", 
                                                         message:"Should throw ArgumentOutOfRangeException from Add method"};                                                            
NhsDateTest.prototype.NullIsAddInstructionTest.expectedException={name:"Sys.ArgumentNullException", 
                                                         message:"Should throw Null argument exception from IsAddInstruction method"};                                                          
                                                         
                                                         
                                                         
