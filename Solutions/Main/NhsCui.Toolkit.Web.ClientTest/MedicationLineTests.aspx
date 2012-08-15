<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="MedicationLineTests.aspx.cs" Inherits="MedicationLineTests" %>
<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Place Control to Test Here -->
    <NhsCui:MedicationLine
                ID="medicationLine" runat="server" 
                CssClass="medicationLineSample"  >                                              
    </NhsCui:MedicationLine>
    <asp:Panel runat="server" ID="MedicationLineContainer">
    </asp:Panel>
    
    
    <script type="text/javascript" src="MedicationLineTests.js" ></script>
    <script type="text/javascript">
        var testHarness = null;
        var testClass = NhsCui.Toolkit.Web.Tests.MedicationLineTests.prototype;

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
            addDoubleClickEventTest();        
                            
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


