//-----------------------------------------------------------------------
// <copyright file="TimeTest.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for NhsTime tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

/// <summary>
/// Unit tests to test the Time class in the NhsCui.Toolkit.DateAndTime namespace
/// </summary>
var NhsTimeTest = NhsCui.Toolkit.Web.Tests.NhsTimeTest = function() {
}

NhsCui.Toolkit.Web.Tests.NhsTimeTest.prototype = {
    /// <summary>
    /// Test the ability to do simple arithemtic updates to date by asking Time to add 1 hour
    /// </summary>
    Add_AddHour : function()
    {
        var baseDateTime = new Date(); // :-)

        var time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.IsTrue(NhsTime.isAddInstruction("+1h"));

        time.add("+1h");
        baseDateTime.setHours(baseDateTime.getHours() + 1)
        Assert.AreEqual(time.get_timeValue(), baseDateTime );
                    
        // Now try it without the "+", this will check that + is the default
        // Reset
        baseDateTime = new Date();
        time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.IsTrue(NhsTime.isAddInstruction("1h"), "1h was not recognised as a Time Add Instruction");
        
        time.add("1h");

        baseDateTime.setHours(baseDateTime.getHours() + 1)
        Assert.AreEqual(time.get_timeValue(), baseDateTime);
    },

    /// <summary>
    /// Test the ability to do complex arithemtic updates to date by asking Time to add 1 hour and 5 minutes
    /// </summary>
    Add_AddHoursAndMinutes : function()
    {
        var baseDateTime = new Date(); // :-)

        var time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.IsTrue(NhsTime.isAddInstruction("+1h+5m"), "+1h+5m was not recognised as an Time Add Instruction");

        time.add("+1h+5m");
        baseDateTime.setHours(baseDateTime.getHours() + 1);
        baseDateTime.setMinutes(baseDateTime.getMinutes() + 5);
        Assert.AreEqual(time.get_timeValue(), baseDateTime, "Failed using +1h+5m");

        // Now try without +

        // Reset
        baseDateTime = new Date();
        time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.IsTrue(NhsTime.isAddInstruction("1h5m"), "1h5m was not recognised as an Time Add Instruction");

        time.add("1h5m");

        baseDateTime.setHours(baseDateTime.getHours() + 1);
        baseDateTime.setMinutes(baseDateTime.getMinutes() + 5);
        Assert.AreEqual(time.get_timeValue(), baseDateTime, "Failed using 1h5m");
    },
    
    /// <summary>
    /// Test the ability to do simple arithemtic updates to time by asking Time to Subtract 5 hours
    /// </summary>
    Add_Subtract5Hours : function()
    {
        var nowAsItWasAtTheStartofTheTest = new Date(); // :-)

        var time = new NhsTime();
        time.set_timeValue(nowAsItWasAtTheStartofTheTest);

        Assert.IsTrue(NhsTime.isAddInstruction("-5h"), "-5h was not recognised as a Time Add Instruction");
        
        time.add("-5h");

        nowAsItWasAtTheStartofTheTest.setHours(nowAsItWasAtTheStartofTheTest.getHours()-5)
        Assert.AreEqual(nowAsItWasAtTheStartofTheTest, time.get_timeValue());
    },

    /// <summary>
    /// Test the ability to do complex arithemtic updates to time by asking Time to Subtract 2 hours AND add 25 minutes
    /// </summary>
    Add_Subtract2HoursAdd25Minutes : function()
    {
        var nowAsItWasAtTheStartofTheTest = new Date(); // :-)

        var time = new NhsTime();
        time.set_timeValue(nowAsItWasAtTheStartofTheTest);

        Assert.IsTrue(NhsTime.isAddInstruction("-2h+25m"), "-2h+25m is not recognised as a Time Add instructuion");

        time.add("-2h+25m");

        nowAsItWasAtTheStartofTheTest.setHours(nowAsItWasAtTheStartofTheTest.getHours()-2);
        nowAsItWasAtTheStartofTheTest.setMinutes(nowAsItWasAtTheStartofTheTest.getMinutes()+25);
        Assert.AreEqual(nowAsItWasAtTheStartofTheTest, time.get_timeValue());
    },

    /// <summary>
    /// Test ToString for TimeType Approx
    /// </summary>
    ToStringApproximate : function()
    {
        var baseDateTime = new Date();
        var gs = new GlobalizationService();

        var time = new NhsTime();
        time.set_timeValue(baseDateTime);
        time.set_timeType(TimeType.Approximate);

        Assert.AreEqual("Approx " + baseDateTime.format(gs.shortTimePattern), time.toString());

        Assert.AreEqual("Approx " + baseDateTime.format(gs.shortTimePattern), time.toString(true));


        time.set_timeType(TimeType.Exact);
        Assert.AreEqual(baseDateTime.format(gs.shortTimePattern), time.toString());
    },

    /// <summary>
    /// Test ToString can output seconds
    /// </summary>
    ToStringDisplaySeconds : function()
    {
        var time = new NhsTime();

        var formattedTime = time.toString(false, Sys.CultureInfo.CurrentCulture, true, false, false);
        var parsedTime = NhsTime.parse(formattedTime);

        Assert.AreEqual(parsedTime.get_timeValue().getSeconds(), time.get_timeValue().getSeconds(), "NhsTime DisplaySeconds incorrect");
    },

    /// <summary>
    /// Test ToString display in 12 / 24 hour clock
    /// </summary>
    ToStringDisplay12Hour : function()
    {
        var hours = 14;
        var dateTime = new Date(2006, 1, 1, hours, 20, 11);
        var time = new NhsTime();
        time.set_timeValue(dateTime);

        var formattedTime = time.toString(false, Sys.CultureInfo.CurrentCulture, false, true, false);
        var parsedDateTime = NhsTime.parse(formattedTime);

        Assert.AreEqual(parsedDateTime.get_timeValue().getHours(), hours - 12, "NhsTime Display12Hour incorrect");
    },

    /// <summary>
    /// Test ToString display in AMPM indicator
    /// </summary>
    ToStringDisplayAMPM : function()
    {
        var morningTime = NhsTime.parse("10:00:00");
        var afternoonTime = NhsTime.parse("15:00:00");

        var formattedMorningTime = morningTime.toString(false, Sys.CultureInfo.CurrentCulture, false, true, true);
        var formattedAfternoonTime = afternoonTime.toString(false, Sys.CultureInfo.CurrentCulture, false, true, true);
        var amDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.AMDesignator.toLowerCase();
        var pmDesignator = Sys.CultureInfo.CurrentCulture.dateTimeFormat.PMDesignator.toLowerCase();
        Assert.IsTrue(formattedMorningTime.indexOf(amDesignator) >= 0);
        Assert.IsTrue(formattedAfternoonTime.indexOf(pmDesignator) >= 0);
    },

    /// <summary>
    /// Test Parse for an var set to a time (not approx)
    /// </summary>
    Parse_NotApproximate : function()
    {
        var baseDateTime = new Date();

        var time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.AreEqual(time.get_timeType(), TimeType.Exact, "TimeType should be Exact when set to a date");

        var parsedTime;

        Assert.IsTrue((parsedTime = NhsTime.tryParse(time.toString())) !== null, String.format("TryParse failed with time, {0}", time.toString()));

        Assert.AreEqual(time.get_timeType(), parsedTime.get_timeType(), "Parse did not error but TimeType does not match");

        Assert.AreEqual(time.get_timeValue().format("HH:mm"), parsedTime.get_timeValue().format("HH:mm"), "Parse did not error but TimeValue does not match");
    },

    /// <summary>
    /// Test Parse for an var set to a time (not approx, but approx = true passed to ToString)
    /// </summary>
//    [TestMethod, 
//    SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"),
//    ExpectedException(typeof(System.ArgumentOutOfRangeException), "Should throw ArgumentOutOfRangeException because oassing True is only valid when TimeType is Approximate")]
    Parse_NotApproximate_TruePassedToToString : function()
    {
        var baseDateTime = new Date();
        
        var time = new NhsTime();
        time.set_timeValue(baseDateTime);

        Assert.AreEqual(time.get_timeType(), TimeType.Exact, "TimeType should be Exact when set to a date");

        NhsTime.parse(time.toString(true));
    },

    /// <summary>
    /// Test Parse for an var set to a time (IS approx)
    /// </summary>
    Parse_Approximate : function()
    {
        var baseDateTime = new Date();

        var time = new NhsTime();
        time.set_timeValue(baseDateTime);
        time.set_timeType(TimeType.Approximate);
        
        Assert.AreEqual(time.get_timeType(), TimeType.Approximate, "TimeType should be Approxinmate when set to a date");

        var parsedTime;

        Assert.IsTrue((parsedTime = NhsTime.tryParse(time.toString())) !== null, String.format("TryParse failed with time, {0}", time.toString()));

        Assert.AreEqual(time.get_timeType(), parsedTime.get_timeType(), "Parse did not error but TimeType does not match");

        Assert.AreEqual(time.get_timeValue().format("HH:mm"), parsedTime.get_timeValue().format("HH:mm"), "Parse did not error but TimeValue does not match");
    },

    /// <summary>
    /// Test Parse for a Null
    /// </summary>
    ParseNullIndex : function()
    {
        var time = new NhsTime();

        time.set_timeType(TimeType.NullIndex);
        time.set_nullIndex(4);

        var parsedTime;
        
        Assert.IsTrue((parsedTime = NhsTime.tryParse(time.toString())) !== null, String.format("TryParse failed with time, {0}", time.toString()));
                
        Assert.AreEqual(time.get_timeType(), parsedTime.get_timeType(), "Parse did not error but TimeType does not match");

        Assert.AreEqual(time.get_nullIndex(), parsedTime.get_nullIndex(), "Parse did not error but NullIndex does not match");
    },

    /// <summary>
    /// Test Parse for an var set to Afternoon
    /// </summary>
    //[TestMethod,
    //ExpectedException(typeof(System.FormatException), "Should throw format exception because of badly formatted text")]
    ParseShouldFail : function()
    {
        NhsTime.parse("barf");
    },

    /// <summary>
    /// Test Parse for an NhsTime set to a time (not approx) with seconds
    /// </summary>
    ParseWithSeconds : function()
    {
        var time = NhsTime.parse("10:30:23");

        Assert.AreEqual(time.get_timeValue().getSeconds(), 23, "Parse failed to set seconds");
    },

    /// <summary>
    /// Test Parse with AM designator
    /// </summary>
    ParseWithAMPMDesignator : function()
    {
        var gs = new GlobalizationService();
        var now = new Date();
        var formattedTime = now.format(gs.shortTimePatternWithSecondsAMPM);
        var time = NhsTime.parse(formattedTime);

        Assert.AreEqual(now.getHours(), time.get_timeValue().getHours(), "Parse failed to set hours with am/pm designator");
        Assert.AreEqual(now.getMinutes(), time.get_timeValue().getMinutes(), "Parse failed to set minutes with am/pm designator");
        Assert.AreEqual(now.getSeconds(), time.get_timeValue().getSeconds(), "Parse failed to set seconds with am/pm designator");
    }

}
NhsTimeTest.prototype.ParseShouldFail.expectedException={name:"NhsCui.Toolkit.Web.FormatException", 
                                                         message:"Should throw format exception because of badly formatted text"};

NhsTimeTest.prototype.Parse_NotApproximate_TruePassedToToString.expectedException={name:"Sys.ArgumentOutOfRangeException",
                                                        message:"Should throw ArgumentOutOfRangeException because passing True is only valid when TimeType is Approximate"};