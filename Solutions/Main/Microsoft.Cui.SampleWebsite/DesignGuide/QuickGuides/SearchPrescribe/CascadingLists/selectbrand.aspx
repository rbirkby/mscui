<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="selectbrand.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.SelectBrand" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp0980.png" alt="Diagram of a search text input box and search results list in which the selected option &#39;atenolol &#45; TENORMIN&#39; has a callout labelled &#39;selected brand&#39;. A cascading list containing &#39;atenolol &#45; oral&#39;, &#39;TENORMIN &#45; oral&#39; and &#39;other&#39; is displayed to the right" />
                </div>
                <div class="guideleft">
                    <p class="number">MSP-0980</p>
                    <p>
                    When a brand name is selected for which generic equivalents are 
                    available, present a cascading list that includes options for the 
                    selected brand and for generic equivalents
                    </p>
                    <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                    <p class="number">MSP-1000</p>
                    <p>
                    When a cascading list is presented that includes options for the 
                    selected brand and for generic equivalents, include the drug names 
                    (generic and brand respectively) in the cascading list
                    </p>
                    <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp0990.png" alt="Diagram of a search results list and a cascading list in which the first option is &#39;DILZEM SR &#45; oral &#45; modified-release&#39; with a callout labelled &#39;brand option only &#39;"/>
                </div>
                <div class="guidealone">
                <p class="number">MSP-0990</p>
                <p>
                When a brand name is selected for which there are no generic 
                equivalents displayed, present template prescriptions for the 
                brand (or proceed to a <span class="nowrap">step-by-step</span> 
                approach)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
