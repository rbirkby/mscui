<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="selectiontrail.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.SelectionTrailPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp1060.png" alt="Four Step outline diagram: 1. Search results 2. Selected search result and a cascading list 3. Selection in cascading list and a list of forms 4. Drug name, route and form displayed in a row and labelled &#39;selection trail&#39; with a list of template prescriptions to the right" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1060</p>
                <p>
                When a selection has been made in the last 
                cascading list, display a selection trail
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1070.png" alt="Diagram showing a search text input box (labelled &#39;drug search&#39;) below which is a drug name and route (clarithromycin, oral) dispayed in a row (labelled &#39;selection trail&#39;), below which is a list of template prescriptions" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1070</p>
                <p>
                Where space is limited such that text within 
                the list of template prescriptions may wrap onto 
                a new line, display the whole list of template 
                prescriptions on a new line (below the other 
                input controls)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
