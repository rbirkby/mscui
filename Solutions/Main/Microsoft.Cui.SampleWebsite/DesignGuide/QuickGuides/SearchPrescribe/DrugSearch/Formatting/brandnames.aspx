<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="brandnames.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Formatting.BrandNames" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp0810.png" alt="Diagram of a search text input box containing &#39;can&#39; and a search results list in which the list item &#39;fluconazole &#43; hydrocortisone &#45; CANESTEN HC&#39; has two callouts. A callout labelled &#39;generic name&#39; points to &#39;fluconazole &#43; hydrocortisone&#39; and the callout labelled &#39;matched brand name&#39; points to &#39;CANESTEN HC&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0810</p>
                <p>
                When brand names that have a generic name are matched, display the 
                generic drug name and supplement it with the brand name
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0830</p>
                <p>
                Do not display brand names unless they have been matched with text 
                entered in the search text input box
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0820.png" alt="Diagram of a search results list in which the item &#39;diltiazem &#45; DILZEM XL&#39; has a callout drawing attention to the hyphen" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0820</p>
                <p>
                Separate generic drug names and brand names with a hyphen that has a space either side
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0840</p>
                <p>
                Display generic and brand names in the same order as described in Design Guidance &#8211;  
                <a href="../../../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                Medication Line</a>
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
