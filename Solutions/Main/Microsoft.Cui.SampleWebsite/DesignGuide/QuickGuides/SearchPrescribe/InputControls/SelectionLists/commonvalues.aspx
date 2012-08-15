<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="commonvalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.SelectionLists.CommonValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp1930.png" alt="Diagram of a drop-down list for administration times in which the first row includes the supplementary text &#39;Standard times&#39; and the second row includes the supplementary text &#39;Non-standard times&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1920</p>
                <p>
                Prioritise the items displayed in a selection 
                list by separating them into sections
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1930</p>
                <p>
                Limit the options available in the first section 
                of a selection list (and in automatically presented 
                cascading lists) to relevant values
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp1900.png" alt="Two step diagram for the input box &#39;Give when...&#39;: 1. A list in which the last option is &#39;other...&#39; 2. The text &#39;other...&#39; is replaced by a text cursor and prompt text: &#39;Enter a description of when this medication should be given&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1940</p>
                <p>
                Where there are further choices than are displayed 
                by default in a prioritised list, provide access to 
                further options with an additional section of the list
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>        
</asp:Content>
