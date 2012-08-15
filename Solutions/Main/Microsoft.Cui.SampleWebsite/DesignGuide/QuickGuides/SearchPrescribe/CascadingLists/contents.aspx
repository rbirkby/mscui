<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="contents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.Contents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0940.png" alt="Diagram showing a selected drug name (diltiazem) and a list of routes. An ellipse draws attention to the last option in the list of routes: &#39;other...&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0940</p>
                <p>
                Include a list item in each cascading list that provides access 
                to values that are not in the list (where they exist)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0950.png" alt="Diagram of a list divided into two parts separated by a horizontal line. The top half lists routes (oral, rectal intravenous infusion) and the single list item below the line is &#39;other...&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0950</p>
                <p>
                Place the list item that provides access to values that are not 
                in the list last in the list and separate it from the rest of the 
                list items with a horizontal line
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0960</p>
                <p>
                Do not provide keyboard shortcuts for the item that provides 
                access to values that are not in the list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0970.png" alt="Illustration of a list containing the following options: tablets and capsules, modified-release, liquid, other..." />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0970</p>
                <p>
                Where relevant, allow a selection to be made from a cascading list 
                that differentiates preparations with different bio-availability 
                properties (such as modified release)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
   
        </div>
    </div>
</asp:Content>
