<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="administrationtimes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.AdministrationTimes" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp2130.png" alt="Illustration of a prescription form in which the administration field has focus and the drop-down list contains two sets of pre-defined administration times and the option &#39;other&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2130</p>
                <p>
                Provide a selection list containing predefined 
                sets of administration times
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-2110</p>
                <p>
                When displaying a list of administration times, 
                display the dose for the first scheduled 
                administration in bold
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2120</p>
                <p>
                Do not display a horizontal <span class="nowrap">(text-only)</span> list of 
                administration times for schedules containing more 
                than six administration events in 24 hours
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-2140</p>
                <p>
                Do not display input controls for entering or editing 
                individual administration times within the view that 
                shows all the required fields for a prescription
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
                        
        </div>
    </div>     
</asp:Content>
