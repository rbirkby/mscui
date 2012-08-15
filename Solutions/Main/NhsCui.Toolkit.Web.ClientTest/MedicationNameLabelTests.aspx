<%@ Page Language="C#" MasterPageFile="~/Default.master" Title="Untitled Page" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
        
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Place Control to Test Here -->    
    <NhsCui:MedicationNameLabel
                ID="medicationNameLabel" runat="server" 
                CssClass="medicationNameLabelSample"                                
                DrugNameStyle="drugName"
                ShowGraphics="true"                
                IndicatorGraphic="Images/EmptyStar.png"
                CriticalAlertGraphic="Images/SavedStar.png" >
        </NhsCui:MedicationNameLabel>

    
    <script type="text/javascript" src="MedicationNameLabelTests.js" ></script>
    <script type="text/javascript">
        var testHarness = null;
        var testClass = NhsCui.Toolkit.Web.Tests.MedicationNameLabelTests.prototype;

        function proxyByCurrying(func) {
            return function() {
                try {
                    func();
                } catch(e) {
                    if(e.name==func.expectedException.name) {
                        // OK. Received the expected exception
                    } else {
                        throw e + " : " + func.expectedException.message;
                    }
                }
            }
        }

        function registerTests(harness) {
            testHarness = harness;
            
            // Add event tests       
            addClickEventTest();                    
            
            for(var testMethod in testClass) {
                var test = testHarness.addTest(testMethod);
                
                // Is success of the test method predicated by throwing an exception?
                if(testClass[testMethod].expectedException) {
                    // proxy the method by currying
                    test.addStep(proxyByCurrying(testClass[testMethod]));
                } else {    
                    test.addStep(testClass[testMethod]);
                }
            }
        }

        var Assert={};
        Assert.AreEqual=function(expected, actual, message) {
            return testHarness.assertEqual(expected, actual, message);
        }

        Assert.IsTrue=function(condition, message) {
            return testHarness.assertTrue(condition, message);
        }
    </script>
</asp:Content>


