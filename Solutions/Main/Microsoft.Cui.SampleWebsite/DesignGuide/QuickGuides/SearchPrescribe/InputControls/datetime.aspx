<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="datetime.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.DateTime" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp2150.png" alt="Illustration of a prescription form with two input controls in the &#39;Administration&#39; section that contain the text &#39;at these times 08:00, 20:00&#39; and &#39;first dose 02-Apr-2010 20:00" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2150</p>
                <p>
                For all prescriptions, require a date and time to be 
                defined (or pre-filled) for:
                </p>
                <ul>
                    <li>The first dose (for regular medications)</li>
                    <li>The starting date and time (for as required medications)</li>
                    <li>The only dose (for once only medications)</li>
                </ul>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2160</p>
                <p>
                Use unique labels for the following fields:
                </p>
                <ul>
                    <li>The first dose (for regular medications)</li>
                    <li>The starting date and time (for as required medications)</li>
                    <li>The only dose (for once only medications)</li>
                </ul>
                <p class="recommended">Recommended</p>
                <p class="note">
                <strong>Note</strong> This guidance point does not constitute a recommendation 
                for the specific text of those labels
                </p>
                </div>
            </div>
                        
        </div>
    </div>     
</asp:Content>
