<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.Display" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1030.png" alt="Three step outline diagram: 1. Search results list 2. Selection in search results list and a cascading list to the right 3. Selection in cascading list and a list of template prescriptions to the right" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1030</p>
                <p>
                Require at least drug name and route (or attributes from which the type 
                of medication can be derived) to be selected before template prescriptions 
                are displayed
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1040</p>
                <p>
                 Display template prescriptions only after selections have been made 
                 (manually or automatically) in all other cascading lists
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1050</p>
                <p>
                 Keep the number of template prescriptions displayed to a 
                 practical and useful minimum
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
