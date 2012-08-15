<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0920.png" alt="Outline diagram showing a search results list and three cascading lists all displayed in a row. The first, second and third have one row highlighted to show a selection" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0920</p>
                <p>
                Maintain visibility of selections, and the list from which they were 
                selected (including the search results), throughout the cascade select, 
                as long as there is enough space to do so without obscuring other information
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0910</p>
                <p>
                Do not allow any of the results or cascaded lists to obscure one another
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
 
        </div>
    </div>
</asp:Content>
