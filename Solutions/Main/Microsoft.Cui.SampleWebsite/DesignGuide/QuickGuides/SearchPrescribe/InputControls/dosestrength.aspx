<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="dosestrength.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.DoseStrength" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp2070.png" alt="Illustration of a field label &#39;Dose&#39; with an input box with a spin control containing the text &#39;2.5&#39; and a drop-down list control containing the text &#39;mg&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-2070</p>
                <p>
                When a dose field (or equivalent) is displayed, also display 
                a label for the dose (either within or outside of the input 
                control)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
             </div>
             
           <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp2080.png" alt="Example of an input box containing a label &#39;DOSE&#39; and two input controls: an input box with spin control containing the text &#39;50,000&#39; and a drop-down list control with the drop-down list displayed and containing &#39;units&#39; and &#39;other...&#39;" />
                </div>             
                <div class="guidetext">
                <p class="number">MSP-2080</p>
                <p>
                If possible, do not allow the selection of a unit of measurement 
                for a dose that would result in an invalid value when combined 
                with the number entered for the dose amount
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

           <div class="line">
            	<div class="landscape">
                	<img src="../images/msp2090.png" alt="Diagram of the top part of a prescription form in which the label &#39;Strength, Form and Device&#39; is followed by two text input boxes side by side containing &#39;60 mg&#39; and &#39;modified-release capsules&#39;" />
                </div>             
                <div class="guideleft">
                <p class="number">MSP-2090</p>
                <p>
                When a strength field is displayed, also display a label for 
                the strength field or a group label including the word 
                &#39;strength&#39;
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div>
                <div class="guidetext">
                <p class="number">MSP-2100</p>
                <p>
                Do not present strength and dose input controls next to each 
                other (side by side) in a detailed prescription form
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>     
</asp:Content>
