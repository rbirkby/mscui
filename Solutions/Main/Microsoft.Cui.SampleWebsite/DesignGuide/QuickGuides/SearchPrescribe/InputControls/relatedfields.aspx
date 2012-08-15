<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="relatedfields.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.RelatedFieldsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1960.png" alt="Four step diagram illustrating: Opening a drop-down list; Clicking on &#39;other&#39;; Display of additional options within the drop-down list; Selection of one of those additional options" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1960</p>
                <p>
                Where relevant, use supplementary text in a drop-down 
                list of options if the selection will affect other 
                options in the form
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1970</p>
                <p>
                Where data is available, update the contents of a 
                selection list based on selections made in related fields
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1990</p>
                <p>
                When a list item is selected that is not valid in 
                relation to values selected in other fields (and 
                data is available to support this) clear the other fields
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-2010</p>
                <p>
                In a system that cannot validate entered values (because 
                decision support checking is not available), when a 
                selection list is reopened and a different value selected, 
                clear entries in all input controls that are interdependent
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
