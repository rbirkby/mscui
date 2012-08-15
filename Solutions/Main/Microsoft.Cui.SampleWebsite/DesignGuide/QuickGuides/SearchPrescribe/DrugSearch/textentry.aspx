<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="textentry.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.TextEntry" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0310.png" alt="Diagram of a search text input box below which is a text input box labelled &#39;Short codes&#39;. Below the diagram is the following Note: In this example, a separate control is provided for entering codes" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0310</p>
                <p>
                Do not support entry of codes in the search text input box. (This does 
                not preclude the use of spelling matching or the provision of an alternative 
                box for entering codes)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0320.png" alt="A search text input box in which the text &#39;para&#39; and a text cursor are displayed" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0320</p>
                <p>
                Do not provide auto-complete in the search text input box
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0330.png" alt="Diagram of a search text input box and the top few lines of a search results list. A callout labelled &#39;focus&#39; draws attention to the text cursor in the search input box" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0330</p>
                <p>
                Retain focus in the search text input box until a selection 
                is made in the search results list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>            
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0340.png" alt="Diagram of a search text input box and the top few lines of a search results list. The first entry in the search results list has a callout labelled &#39;focus&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0340</p>
                <p>
                When focus is first switched to the results list, set focus to 
                the first entry in the list by default
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
