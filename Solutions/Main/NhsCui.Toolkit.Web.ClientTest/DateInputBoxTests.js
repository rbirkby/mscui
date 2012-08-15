//-----------------------------------------------------------------------
// <copyright file="DateInputBoxTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Client-side javascript for DateInputBox tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

var baseNamingContainer="ctl00_ContentPlaceHolder1_DateInputBox_DateInputBox_";
var controlId=baseNamingContainer + "DateInputBoxExtender";
var textboxId=baseNamingContainer + "TextBox";


/// <summary>
/// Unit tests to test the DateInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var NhsDateTest = NhsCui.Toolkit.Web.Tests.DateInputBoxTests = function() {
};

NhsCui.Toolkit.Web.Tests.DateInputBoxTests.prototype = {
    checkDisplayDateAsText : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);

        obj.set_value(new NhsDate(new Date())); // Today
        obj.set_displayDateAsText(true);
        
        Assert.AreEqual("Today", elt.value, "TextBox is out of sync with properties");
    },
    checkDisplayDayOfWeek : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);

        obj.set_value(new NhsDate("01-Jan-2007")); // Monday
        obj.set_displayDayOfWeek(true);
        
        Assert.AreEqual("Mon 01-Jan-2007", elt.value, "TextBox is out of sync with properties");
    },
        
    /* NOT WORKING SO COMMENTED OUT
    checkAmbiguousDateEvent : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        var eventFired=false;
        var OnAmbiguousDate=function(sender, args) {
   	        eventFired=true;
        };

        obj.add_ambiguousDate(OnAmbiguousDate);
        obj.set_value(NhsDate.parse("02/01/2005"));
        
        // this test isn't working
        testHarness.cancel();
        
        //if(eventFired===false) {
        //    throw "ambiguousDate event should have fired but didn't";
        //}
    },
    */   

    setExactDate : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        obj.set_value(NhsDate.parse("23-Mar-2006"));    
        
        Assert.AreEqual("23-Mar-2006", elt.value, "TextBox is out of sync with properties");
    },
    setInvalidNamedDate : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);

        obj.set_value(NhsDate.parse("Blah"));        
    },   
    setNullDate : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        var date = new NhsDate();
        
        date.set_dateType(DateType.Null);
        obj.set_value(date);    
        
        Assert.AreEqual(obj.get_dateType(), DateType.Null, "Null date not set properly");
        // because of watermark need to focus and do async wait until textbox is set
        elt.focus();
    },
    setWatermarkText : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        var date = new NhsDate();
        
        date.set_dateType(DateType.Null);
        obj.set_value(date);
        obj.set_watermarkText("Watermark");
        
        Assert.AreEqual("Watermark", obj.get_watermarkText(), "Watermark property not set properly");
        // get element to loose focus so watermark is set then do async wait
        elt.focus();
        elt.blur();
    },
    setCheckBoxCssToRedThenBlue : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        var date = new NhsDate();
        
        date.set_dateType(DateType.Null);
        obj.set_allowApproximate(true);
        
        obj.set_value(date);

        obj.set_checkBoxCssClass("RedCheckBox");
        
        // get element to loose focus then do async wait
        elt.focus();
        elt.blur();        

        obj.set_checkBoxCssClass("BlueCheckBox");
        
        // get element to loose focus then do async wait
        elt.focus();
        elt.blur();
  
    }
};
NhsDateTest.prototype.setInvalidNamedDate.expectedException={name:"NhsCui.Toolkit.Web.FormatException",
                                                        message:"Should throw NhsCui.Toolkit.Web.FormatException"};
