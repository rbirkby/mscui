<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="misselection.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Formatting.MisSelection" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp0700.png" alt="A search results list containing seven results in which the text of the third (quiNIDine) and fourth (quiNINE) are followed by &#39;this drug is often mis-selected&#39; in grey italic text" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0700</p>
                <p>
                Where drug names associated with mis-selection errors are listed 
                in the search results, use formatting to draw attention to them
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0710</p>
                <p>
                Where drug names associated with mis-selection errors are listed 
                in the search results, highlight the row with a pale background 
                colour
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0720.png" alt="Diagram of a search results list containing seven results in which the text of the third (quinidine) and fourth (quinine) are followed by &#39;this drug is often mis-selected&#39; in grey italic text. A call out labelled &#39;warning messages&#39; points to the grey italic text." />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0720</p>
                <p>
                Where drug names associated with mis-selection errors are listed in 
                the search results, supplement the drug name with a brief warning message
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0730</p>
                <p>
                Display mis-selection warning messages in grey italics and in a smaller 
                font size than the drug name
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            

        </div>
    </div>
</asp:Content>
