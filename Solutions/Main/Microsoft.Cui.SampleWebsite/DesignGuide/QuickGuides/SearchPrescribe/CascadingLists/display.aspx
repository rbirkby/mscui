<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingLists.Display" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0850.png" alt="Diagram with four outlines representing lists with numbers and arrows implying a sequence from left to right: 1 is labelled Drug Names, 2, 3 and 4 are labelled Cascading Lists" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0850</p>
                <p>
                Display a cascading list on the selection of drug name and up to two further 
                cascading lists for basic prescription attributes
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0860.png" alt="Diagram of a search results list in which one list item is highlighted and an outline of a second list to the right labelled &#39;second list&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0860</p>
                <p>
                Present a second list when a selection is made in the search results list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0880a.png" class="captioned" alt="Outline diagram representing three lists side by side in which the first and second have one item selected. The second list is labelled &#39;routes&#39; and the third list is labelled &#39;template prescriptions&#39;" />
                	<p class="caption">Example 1: paracetamol</p><br />
                	<img src="../images/msp0880b.png" class="captioned" alt="Outline diagram representing four lists side by side in which the first, second and third have one item selected. The second list is labelled &#39;routes&#39;, the third list is labelled &#39;forms&#39; and the fourth list is labelled &#39;template prescriptions&#39;" />
                	<p class="caption">Example 2: salbutamol</p><br />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0880</p>
                <p>
                Allow different cascading lists (such as route and form or route and 
                strength) to be presented depending on the drug selected
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0890</p>
                <p>
                Limit the options presented within cascading lists to those that are 
                relevant to the previous selection
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
