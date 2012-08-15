<%@ Page Language="C#" MasterPageFile="~/Default.master" Title="Untitled Page" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
.RedCheckBox 
{
    background-color:red
}
.BlueCheckBox 
{
    background-color:blue
}
.YellowCheckBox 
{
    background-color:yellow
}
</style>

<NhsCui:DateInputBox id="DateInputBox" runat="server" Functionality="Complex" AllowApproximate="True" CheckBoxCssClass="YellowCheckBox">
</NhsCui:DateInputBox>

<script type="text/javascript" src="DateInputBoxTests.js" ></script>
<script type="text/javascript">
var testHarness = null;
var testClass = NhsCui.Toolkit.Web.Tests.DateInputBoxTests.prototype;

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
    
    for(var testMethod in testClass) {
        var test = testHarness.addTest(testMethod);
        
        // Is success of the test method predicated by throwing an exception?
        if(testClass[testMethod].expectedException) {
            // proxy the method by currying
            test.addStep(proxyByCurrying(testClass[testMethod]));
        } else if(testClass[testMethod].testParams) {
            // async test 
            var params = testClass[testMethod].testParams;
            test.addStep(testClass[testMethod], params.CheckComplete, params.Interval, params.Timeout);
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

