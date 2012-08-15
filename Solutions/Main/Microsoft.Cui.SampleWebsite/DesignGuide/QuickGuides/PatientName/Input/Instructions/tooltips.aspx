<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="tooltips.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Instructions.ToolTips" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/nid0060.png" alt="An input form in which a mouse cursor points to the prompt text in the Middle Names text box and a tooltip is displayed containing the text &#39;Enter the person's middle name(s)&#39; " />
                </div>
                <div class="guidetext">
                <p class="number">NID-0060</p>
                <p>
                Each field in a name input control should have instructional text (for example, a tooltip)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration">
                <table class="datavalues">
                    <tr>
                        <td>Title</td>
                        <td>Select a Title from the list or simply type in a different Title</td>
                    </tr>
                    <tr>
                        <td>Family Name</td>
                        <td>Enter the person's Family Name (surname)</td>
                    </tr>
                    <tr>
                        <td>Given Name</td>
                        <td>Enter the person's Given Name (forename or Christian name)</td>
                    </tr>
                    <tr>
                        <td>Middle Name(s)</td>
                        <td>Enter the person's middle name(s)</td>
                    </tr>
                    <tr>
                        <td>Suffix</td>
                        <td>Enter the person's suffix name (e.g. 'Junior' or 'The Third')</td>
                    </tr>
                    <tr>
                        <td>Known as</td>
                        <td>Enter the name a person likes to be referred to as</td>
                    </tr>
                </table>
                </div>
                <div class="guidetext">
                <p class="number">NID-0061</p>
                <p>
                Tooltip values should be those illustrated
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
