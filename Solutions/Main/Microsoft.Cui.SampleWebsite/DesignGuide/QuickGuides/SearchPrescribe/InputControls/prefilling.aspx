<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="prefilling.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.PreFilling" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp2040.png" alt="Two step diagram illustrating the highlighting of fields whose values were automatically updated on selection of a frequency" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2040</p>
                <p>
                When a value is selected in a field, pre-fill appropriate 
                fields that have defaults (or only one possible value) based 
                on the selected value (for example, pre-fill administration 
                times when a frequency is selected)
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-2020</p>
                <p>
                Support pre-filling of fields (or sets of fields) when they 
                are first displayed and ensure that the pre-filled values are 
                based on at least the drug name and route (or attributes from 
                which the type of medication can be derived)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2030</p>
                <p>
                Allow the contents of all fields to be reselected such that a 
                pre-filled value, previous choice or text entry can be changed 
                (even if the associated selection list has only one option)
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-2050</p>
                <p>
                Use formatting (such as highlighting) to draw attention to a 
                field whose contents have changed automatically rather than 
                directly by the user
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-2060</p>
                <p>
                Pre-fill administration times and time of first dose (or 
                equivalent for once only and as required medications) when 
                frequency has been selected
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>     
</asp:Content>
