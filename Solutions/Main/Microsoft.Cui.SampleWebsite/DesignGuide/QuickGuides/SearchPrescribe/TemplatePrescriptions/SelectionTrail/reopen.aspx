<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="reopen.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.SelectionTrail.ReOpen" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp1080.png" alt="Three step outline diagram: 1. Three boxes (labelled &#39;selection trail&#39;) containing the text &#39;drug name&#39;, &#39;route&#39; and &#39;form&#39;, followed by a list of template prescriptions 2. The text &#39;Click on the route in the selection trail&#39; 3. Three lists in a row, the first two showing selections and the last (a list of forms) labelled &#39;cascading list&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1080</p>
                <p>
                When an item in a selection trail is selected, 
                reopen the lists for all the items in the selection trail
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1090</p>
                <p>
                When lists are reopened before a template prescription has 
                been selected, remove the list of template prescriptions from 
                view
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1100</p>
                <p>
                When lists are reopened, display them all as they were before 
                the template prescriptions were displayed
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
