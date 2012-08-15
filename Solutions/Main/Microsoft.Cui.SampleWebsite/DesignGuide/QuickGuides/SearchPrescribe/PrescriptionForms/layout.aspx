<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PrescriptionForms.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1560.png" alt="Diagram of the top part of a prescription form with a vertical line to which the labels on the left are right-aligned and the input controls on the right are left-aligned" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1560</p>
                <p>
                When displaying a prescription form with fields 
                arranged in a column, display field labels right-aligned 
                and on the left with the fields left-aligned and on the right
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1570.png" alt="Illustration of the top part of a prescription form in which each input box has a label above and left-aligned with the input control" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1570</p>
                <p>
                When placing fields labels above input controls, display 
                the labels <span class="nowrap">left-aligned</span> and
                 in a smaller font than the text displayed in the control
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
        </div>
    </div>     
</asp:Content>
