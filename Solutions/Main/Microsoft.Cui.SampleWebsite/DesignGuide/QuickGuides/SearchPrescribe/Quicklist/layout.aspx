<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.QuickList.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0100.png" alt="Two diagrams of a search text input box with a Quick List below. 1. A space is displayed between the search text input box and the Quick List 2. The search text input box has a drop-down control and the Quick List is displayed as a drop-down list" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0100</p>
                <p>
                Display the Quick List below (or as a drop-down list extension 
                of) the search text input box
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0160.png" alt="Outline diagram of alternate row shading (no text)" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0160</p>
                <p>
                Use alternate row shading in the Quick List, as in the search 
                results list
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
