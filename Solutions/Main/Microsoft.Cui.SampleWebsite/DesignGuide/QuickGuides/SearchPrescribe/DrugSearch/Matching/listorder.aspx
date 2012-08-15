<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="listorder.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Matching.ListOrder" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0530.png" alt="Search text input box and results matching &#39;neomycin&#39;. The results are displayed in two sections labelled &#39;Commonly prescribed matches&#39; and &#39;Standard matches&#39;. The results (by section) are: 1. neomycin 2. neomycin &#43; prednisolone, neomycin &#43;triamcinolone, betamethasone &#43;neomycin" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0530</p>
                <p>
                List search results in matched order, such that matches are prioritised by 
                proximity to the beginning of the drug name and matches in generic drug names 
                are prioritised above matches in brand names
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0540</p>
                <p>
                Where relevancy ranking is not implemented, list search results alphabetically 
                within each set that have the same text matched
                </p>
                <p class="mandatory">Mandatory</p>
            </div>
            
        </div>
    </div>
</asp:Content>
