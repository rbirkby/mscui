<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="appwindow.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.Context.AppWindow" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0010.png" alt="Outline diagram of a screen area, with a Patient Banner at the top and a rectangular area below labelled &#39;Prescribing Area&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0010</p>
                <p>
                Do not allow the prescribing area to occlude the Patient Banner
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0040.png" alt="Outline diagram of a screen area, with a Patient Banner at the top, a yellow strip below labelled &#39;Incomplete Prescription&#39; and a rectangular area below that labelled &#39;Medications List&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0040</p>
                <p>
                If it is possible to navigate away from the prescribing area 
                before completing a prescription, ensure that a clear indication 
                that there is an incomplete prescription remains displayed on screen
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
