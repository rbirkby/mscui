<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="progressive.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.ProgressivePage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0370.png" alt="Two step diagram showing: 1. The text &#39;fil&#39; entered into the search text input box and two entries in the search results list 2. The text &#39;filnar&#39; entered into the search text input box and one entry in the search results list" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0370</p>
                <p>
                Display results using progressive matching where possible
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0380.png" alt="A search text input box with a button labelled &#39;Search&#39; to the right" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0380</p>
                <p>
                In the absence of progressive matching, provide a static search that submits 
                text in the search text input box by pressing the ENTER key and/or activating 
                a control (such as a button) to submit the search
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

                                           

        </div>
    </div>
</asp:Content>
