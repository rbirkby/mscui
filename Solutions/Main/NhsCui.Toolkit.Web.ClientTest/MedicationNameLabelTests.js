//-----------------------------------------------------------------------
// <copyright file="MedicationNameLabelTests.js.jsxTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Feb-2007</date>
// <summary>Client-side javascript for MedicationNameLabel tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");


var baseNamingContainer="ctl00_ContentPlaceHolder1_medicationNameLabel";
var controlId=baseNamingContainer + "_Extender";

/// <summary>
/// Unit tests to test the TimeInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var MedicationNameTest = NhsCui.Toolkit.Web.Tests.MedicationNameLabelTests = function() {
};

NhsCui.Toolkit.Web.Tests.MedicationNameLabelTests.prototype = {
    /// <summary>
    /// Test Show Graphics Flag
    /// </summary>
    testShowGraphics: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set Show Dosage Details  to true
        obj.set_showGraphics(true); 
        Assert.AreEqual(true, obj.get_showGraphics(), "ShowGraphics was not set correctly to true.");
        
        // Set Show Dosage Details  to false
        obj.set_showGraphics(false); 
        Assert.AreEqual(false, obj.get_showGraphics(), "ShowGraphics was not set correctly to false.");       
    },
    
    /// <summary>
    /// Test AddClick/Remove Click Handlers
    /// </summary>
    testAddRemoveClick : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Add then remove this function as a click handler
        obj.add_click(function clickTestHandler(sender, args) {} );         
        obj.remove_click(clickTestHandler);                 
    },                    

    /// <summary>
    /// Test Parameter validation Add Click Handler
    /// </summary>
    testInvalidAddClick: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Add invalid function
        obj.add_click(false);                 
    },                       

    /// <summary>
    /// Test Parameter validation Remove Click Handler
    /// </summary>
    testInvalidRemoveClick: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Remove invalid function
        obj.remove_click(false);                 
    }                          
};


MedicationNameTest.prototype.testInvalidAddClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};
MedicationNameTest.prototype.testInvalidRemoveClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};

/// <summary>
/// Test that the Click Event is Fired
/// </summary>
function addClickEventTest()
{
    var pollInterval = 200, timeout = 1000;
                
    var elt = testHarness.getElement(baseNamingContainer);                    
    var obj = testHarness.getObject(controlId);
    
    obj.add_click(function() { clickCompleted = true; });
    var testClickEvent = testHarness.addTest('testClickEvent');                        
    
    testClickEvent.addStep(function() { clickCompleted = false; });            
    testClickEvent.addStep(function() {
        testHarness.fireEvent(elt, 'onclick'); }, 
        function() { return clickCompleted; }, 
        pollInterval, timeout);            
}                
