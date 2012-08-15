<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="promotedoptional.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.Fields.PromotedOptional" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1730.png" alt="Diagram of part of a prescription form in which an optional field, Reason for Prescribing, has a drop-down list associated in which there are three entries, each separated by a horizontal line: &#39;Select from a list&#39;; &#39;None (leave blank)&#39;; &#39;Enter a reason for prescribing&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1720</p>
                <p>
                Only when it is important to encourage the completion 
                of an optional field, promote it by displaying an 
                input control for it
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1730</p>
                <p>
                When an optional input control is promoted, support 
                the entry or selection of a null value and require 
                it to be completed
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>       
</asp:Content>
