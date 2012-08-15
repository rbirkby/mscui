<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="inputcontrols.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.StepByStep.InputControls" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1380.png" alt="Three step diagram showing that the selection of a different value in the administration times box also changes the value in the first dose box" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1370</p>
                <p>
                Restrict the number of input controls to the minimum 
                required to enter information needed to complete the 
                prescription
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1380</p>
                <p>
                When presenting fields step by step, support pre-filling 
                of one or more of the fields that are already displayed 
                when a selection is made in a related field
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1390</p>
                <p>
                When presenting fields step by step, allow the contents of 
                all fields to be reselected such that a pre-filled value, 
                previous choice or text entry can be changed (even if the 
                associated selection list has only one option)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
        
        </div>
    </div>              
</asp:Content>
