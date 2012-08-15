<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="listwidth.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.ListWidth" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0870.png" alt="Two step diagram: 1. A search results list 2. A search results list with a scroll bar and one item highlighted at the bottom of the list and a list to the right containing &#39;oral&#39;, a horizontal line and &#39;other...&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0870</p>
                <p>
                Allow the width of the search results list to extend into available 
                space to accommodate the longest entry when first presented
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0900</p>
                <p>
                    When a cascaded list is displayed and the search results list remains open, reduce
                    the width of the search results list as necessary (following Design Guidance &#8211; 
                    <a href="../../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a> for wrapping)
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0930</p>
                <p>
                    When the width of the search results list is reduced and a scroll 
                    bar is displayed, expand the list to show all results
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
 
        </div>
    </div>
</asp:Content>
