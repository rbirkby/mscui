//-----------------------------------------------------------------------
// <copyright file="TimeInputBoxTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>03-Jan-2007</date>
// <summary>Client-side javascript for TimeInputBox tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

var baseNamingContainer="ctl00_ContentPlaceHolder1_TimeInputBox_TimeInputBox_";
var controlId=baseNamingContainer + "TimeInputBoxExtender";
var textboxId=baseNamingContainer + "TextBox";
var checkboxId=textboxId + "_approximate";


function hasElement(id) {
    var doc = testHarness.getDocument();
    var element = doc.getElementById(id);
    return !!element;
} 

/// <summary>
/// Unit tests to test the TimeInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var NhsTimeTest = NhsCui.Toolkit.Web.Tests.TimeInputBoxTests = function() {
}

NhsCui.Toolkit.Web.Tests.TimeInputBoxTests.prototype = {    
    setAllowApproxOff : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        obj.set_allowApproximate(false);
        
        Assert.AreEqual("12:00", elt.value, "TextBox is out of sync with properties");
        //Below is not a valid test to determine if allow approximate is allowed. 
        //If this is set int he designer, and then turned off via code like above, the item will not be visible, yet the control could be a valid element.
        
        //Assert.IsTrue(!hasElement(checkboxId), "AllowApprox checkbox exists when it should be missing");
    },
    setAllowApproxOn : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        obj.set_allowApproximate(true);
        
        Assert.AreEqual("12:00", elt.value, "TextBox is out of sync with properties");
        Assert.IsTrue(hasElement(checkboxId), "AllowApprox checkbox doesn't exist");
        
        var check=testHarness.getElement(checkboxId);
        
        Assert.AreEqual(false, elt.checked, "AllowApprox checkbox is incorrect");
    },
  
/* REMOVE THIS TEST
    setNamedTime : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        var namedTimes=["Morning", "Evening", "Night", "Afternoon"];
        
        for(var i=0; i<namedTimes.length; i++) {
            obj.set_value(NhsTime.parse(namedTimes[i]));        
            Assert.AreEqual(namedTimes[i], elt.value, "TextBox is out of sync with properties");
        }
    },
    setInvalidNamedTime : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);

        obj.set_value(NhsTime.parse("Blah"));        
    },
    
*/
    setExactTime : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        obj.set_value(NhsTime.parse("23:59"));    
        
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
    },
    setApproximateTime : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        obj.set_allowApproximate(true);
        
        obj.set_value(NhsTime.parse("Approx 23:59"));    
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
        
        var check=testHarness.getElement(checkboxId);        
        Assert.AreEqual(true, check.checked, "Checkbox should be checked");
    },  
    setApproximateTimeBelatedAllowApproximate : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        obj.set_value(NhsTime.parse("Approx 23:59"));    
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
        
        obj.set_allowApproximate(true);        
        var check=testHarness.getElement(checkboxId);        
        Assert.AreEqual(true, check.checked, "Checkbox should be checked");
    },  
    testDisplaySeconds : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        // ensure default values
        obj.set_displaySeconds(false);
        obj.set_display12Hour(false);
        obj.set_displayAMPM(false);
        
        obj.set_value(NhsTime.parse("23:59:31"));    
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
        
        obj.set_displaySeconds(true);        
        Assert.AreEqual("23:59:31", elt.value, "TextBox is out of sync with properties");
    },  
    testDisplay12Hour : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
        // ensure default values
        obj.set_displaySeconds(false);
        obj.set_display12Hour(false);
        obj.set_displayAMPM(false);
        
        obj.set_value(NhsTime.parse("23:59:31"));    
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
        
        obj.set_display12Hour(true);        
        Assert.AreEqual("11:59", elt.value, "TextBox is out of sync with properties");
    },  
    testDisplaySeconds : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);
        
         // ensure default values
        obj.set_displaySeconds(false);
        obj.set_display12Hour(false);
        obj.set_displayAMPM(false);
        
        obj.set_value(NhsTime.parse("11:59:31"));    
        Assert.AreEqual("11:59", elt.value, "TextBox is out of sync with properties");
        
        obj.set_displayAMPM(true);        
        Assert.AreEqual("11:59 (am)", elt.value, "TextBox is out of sync with properties");
        
        obj.set_value(NhsTime.parse("23:59:31"));    
        // no pm for 24hour clock 
        Assert.AreEqual("23:59", elt.value, "TextBox is out of sync with properties");
    },setCheckBoxCssToRedThenBlue : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(textboxId);        
           
        obj.set_allowApproximate(true);
        
        obj.set_value(NhsTime.parse("23:59"));

        obj.set_checkBoxCssClass("RedCheckBox");
        
        // get element to loose focus then do async wait
        elt.focus();
        elt.blur();        

        obj.set_checkBoxCssClass("BlueCheckBox");
        
        // get element to loose focus then do async wait
        elt.focus();
        elt.blur();
  
    }
}
/*
NhsTimeTest.prototype.setInvalidNamedTime.expectedException={name:"NhsCui.Toolkit.Web.FormatException",
                                                        message:"Should throw NhsCui.Toolkit.Web.FormatException"};
*/


