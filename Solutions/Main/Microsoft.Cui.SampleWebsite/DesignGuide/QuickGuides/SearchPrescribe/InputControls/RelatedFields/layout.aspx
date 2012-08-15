<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.RelatedFields.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../../images/msp2000.png" alt="Four step diagram illustrating: Opening a drop-down list; Clicking on &#39;other&#39;; Display of additional options within the drop-down list; Selection of one of those additional options" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2000</p>
                <p>
                As far as possible, present input controls for fields 
                that are <span class="nowrap">inter-dependent</span> close to one another
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1980</p>
                <p>
                When displaying list items that are not valid in 
                relation to values selected in other fields, list 
                them in a separate section in the selection list
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
