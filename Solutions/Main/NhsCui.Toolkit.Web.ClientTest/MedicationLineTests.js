//-----------------------------------------------------------------------
// <copyright file="MedicationLineTests.js" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Feb-2007</date>
// <summary>Client-side javascript for MedicationLine tests</summary>
//-----------------------------------------------------------------------

Type.registerNamespace("NhsCui.Toolkit.Web.Tests");

var baseNamingContainer="ctl00_ContentPlaceHolder1_medicationLine";
var controlId=baseNamingContainer + "_medicationLine_Extender";

/// <summary>
/// Unit tests to test the TimeInputBox control in the NhsCui.Toolkit.Web namespace
/// </summary>
var MedicationLineTest = NhsCui.Toolkit.Web.Tests.MedicationLineTests = function() {
};

NhsCui.Toolkit.Web.Tests.MedicationLineTests.prototype = {

    /// <summary>
    /// Test show dosage details flag
    /// </summary>
    testShowDosageDetails : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set Show Dosage Details  to true
        obj.set_showDosageDetails(true); 
        Assert.AreEqual(true, obj.get_showDosageDetails(), "ShowDosageDetails was not set correctly to true.");
        
        // Set Show Dosage Details  to false
        obj.set_showDosageDetails(false); 
        Assert.AreEqual(false, obj.get_showDosageDetails(), "ShowDosageDetails was not set correctly to false.");        
    },
    
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
    /// Test Show StatusDate Flag
    /// </summary>
    testShowStatusDate: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set Show Dosage Details  to true
        obj.set_showStatusDate(true); 
        Assert.AreEqual(true, obj.get_showStatusDate(), "ShowStatusDate was not set correctly to true.");
        
        // Set Show Dosage Details  to false
        obj.set_showStatusDate(false); 
        Assert.AreEqual(false, obj.get_showStatusDate(), "ShowStatusDate was not set correctly to false.");       
    },

    /// <summary>
    /// Test Show Status Flag
    /// </summary>
    testShowStatus: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set Show Dosage Details  to true
        obj.set_showStatus(true); 
        Assert.AreEqual(true, obj.get_showStatus(), "ShowStatus was not set correctly to true.");
        
        // Set Show Dosage Details  to false
        obj.set_showStatus(false); 
        Assert.AreEqual(false, obj.get_showStatus(), "ShowStatus was not set correctly to false.");       
    },
    /// <summary>
    /// Test Show Reason Flag
    /// </summary>
    testShowReason: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set Show Dosage Details  to true
        obj.set_showReason(true); 
        Assert.AreEqual(true, obj.get_showReason(), "ShowReason was not set correctly to true.");
        
        // Set Show Dosage Details  to false
        obj.set_showReason(false); 
        Assert.AreEqual(false, obj.get_showReason(), "ShowReason was not set correctly to false.");       
    },

    /// <summary>
    /// Test Simple Mode 
    /// </summary>
    testSimpleMode: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        Assert.AreEqual(true, obj.get_simpleMode(), "SimpleMode was not set correctly to true.");
        
        // Set SimpleMode to false
        obj.set_simpleMode(false); 
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly to false.");       
    },
    
    /// <summary>
    /// Test Simple Mode overrides the other flags
    /// </summary>
    testSimpleModeOverride: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to false, and all other properties to true               
        obj.set_simpleMode(false); 
        obj.set_showDosageDetails(true); 
        obj.set_showGraphics(true); 
        obj.set_showReason(true); 
        obj.set_showStatus(true);
        obj.set_showStatusDate(true);  
                
        // Set Simple Mode to true - all other properties should be reset to false
        obj.set_simpleMode(true); 
                
        Assert.AreEqual(false, obj.get_showDosageDetails(), "DosageDetails was not set correctly to false.");               
        Assert.AreEqual(false, obj.get_showGraphics(), "ShowGraphics was not set correctly to false.");               
        Assert.AreEqual(false, obj.get_showReason(), "ShowReason was not set correctly to false.");               
        Assert.AreEqual(false, obj.get_showStatus(), "ShowStatus was not set correctly to false.");               
        Assert.AreEqual(false, obj.get_showStatusDate(), "ShowStatusDate was not set correctly to false.");               
    },            
    
    /// <summary>
    /// Test show dosage details flag overrides SimpleMode
    /// </summary>
    testShowDosageDetailsOverridesSimpleMode : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        
        // Set Show Dosage Details  to true
        obj.set_showDosageDetails(true); 
        
        // Simple Mode should now be reset to false
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly reset to false.");
    },

    /// <summary>
    /// Test show graphics flag overrides SimpleMode
    /// </summary>
    testShowGraphicsOverridesSimpleMode : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        
        // Set Show Graphics  to true
        obj.set_showGraphics(true); 
        
        // Simple Mode should now be reset to false
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly reset to false.");
    },

    /// <summary>
    /// Test ShowReason flag overrides SimpleMode
    /// </summary>
    testShowReasonOverridesSimpleMode : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        
        // Set Show Reason to true
        obj.set_showReason(true); 
        
        // Simple Mode should now be reset to false
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly reset to false.");
    },

    /// <summary>
    /// Test ShowStatus flag overrides SimpleMode
    /// </summary>
    testShowStatusOverridesSimpleMode : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        
        // Set Show Status to true
        obj.set_showStatus(true); 
        
        // Simple Mode should now be reset to false
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly reset to false.");
    },

    /// <summary>
    /// Test ShowStatusDate flag overrides SimpleMode
    /// </summary>
    testShowStatusDateOverridesSimpleMode : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set SimpleMode to true
        obj.set_simpleMode(true); 
        
        // Set Show Status to true
        obj.set_showStatusDate(true); 
        
        // Simple Mode should now be reset to false
        Assert.AreEqual(false, obj.get_simpleMode(), "SimpleMode was not set correctly reset to false.");
    } , 
    
    /// <summary>
    /// Test IsSelected flag
    /// </summary>
    testIsSelected : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set IsSelected to true
        obj.set_isSelected(true); 
        Assert.AreEqual(true, obj.get_isSelected(), "IsSelected was not set correctly to true.");
        
        // Set IsSelected to false
        obj.set_isSelected(false); 
        Assert.AreEqual(false, obj.get_isSelected(), "IsSelected was not set correctly to false.");        
    },

    /// <summary>
    /// Test CallbackID property
    /// </summary>
    testCallBackID : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set CallBackID to TestID
        obj.set_callbackID("TestID"); 
        Assert.AreEqual("TestID", obj.get_callbackID(), "CallBackID was not set correctly to TestID.");        
    },

    /// <summary>
    /// Test ClickPostBack flag
    /// </summary>
    testClickPostBack : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set ClickPostBack to true
        obj.set_clickPostBack(true); 
        Assert.AreEqual(true, obj.get_clickPostBack(), "ClickPostBack was not set correctly to true.");        

        // Set ClickPostBack to false
        obj.set_clickPostBack(false); 
        Assert.AreEqual(false, obj.get_clickPostBack(), "ClickPostBack was not set correctly to false.");        
        
    },

    /// <summary>
    /// Test DblClickPostBack flag
    /// </summary>
    testDblClickPostBack : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Set DblClickPostBack to true
        obj.set_dblclickPostBack(true); 
        Assert.AreEqual(true, obj.get_dblclickPostBack(), "DblClickPostBack was not set correctly to true.");        

        // Set DblClickPostBack to false
        obj.set_dblclickPostBack(false); 
        Assert.AreEqual(false, obj.get_dblclickPostBack(), "DblClickPostBack was not set correctly to false.");                
    },    

    /// <summary>
    /// Test AddClick/Remove Click Handlers
    /// </summary>
    testAddRemoveClick : function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Add then remove this function as a click handler
        obj.add_click(clickTestHandler);         
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
    },                      

    /// <summary>
    /// Test Parameter validation Add DoubleClick Handler
    /// </summary>
    testInvalidAddDblClick: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Add invalid function
        obj.add_doubleClick(false);                 
    },                       

    /// <summary>
    /// Test Parameter validation Remove DoubleClick Handler
    /// </summary>
    testInvalidRemoveDblClick: function() {
        var obj = testHarness.getObject(controlId);
        var elt = testHarness.getElement(baseNamingContainer);                
                
        // Remove invalid function
        obj.remove_doubleClick(false);                 
    }             
};

MedicationLineTest.prototype.testInvalidAddClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};
MedicationLineTest.prototype.testInvalidRemoveClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};

MedicationLineTest.prototype.testInvalidAddDblClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};
MedicationLineTest.prototype.testInvalidRemoveDblClick.expectedException={name:"Sys.ArgumentTypeException",
                                                        message:"Should throw Sys.ArgumentTypeException"};

// Validation Function 
function clickTestHandler(sender, args)
{
}

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

/// <summary>
/// Test that the Double Click Event is Fired
/// </summary>
function addDoubleClickEventTest()
{
    var pollInterval = 200, timeout = 1000;
                
    var elt = testHarness.getElement(baseNamingContainer);                    
    var obj = testHarness.getObject(controlId);
    
    obj.add_doubleClick(function() { doubleClickCompleted = true; });
    var testDoubleClickEvent = testHarness.addTest('testDoubleClickEvent');                        
    
    testDoubleClickEvent.addStep(function() { doubleClickCompleted = false; });            
    testDoubleClickEvent.addStep(function() {
        testHarness.fireEvent(elt, 'ondblclick'); }, 
        function() { return doubleClickCompleted; }, 
        pollInterval, timeout);            
}                    
                






