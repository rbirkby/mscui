<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="prompts.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.Fields.Prompts" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1740.png" alt="Diagram showing two examples of input boxes containing in-field prompts in grey italics and two examples of input boxes containing labels shown in blue text" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1740</p>
                <p>
                Display in-field prompts for fields that have 
                to be completed by the user and would otherwise 
                be blank. (A field does not have to have an 
                <span class="nowrap">in-field</span> prompt if 
                it contains a label)
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1750</p>
                <p>
                Use a phrase that begins with a verb for 
                <span class="nowrap">in-field</span> prompts in
                 fields that have to be completed by the user
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
