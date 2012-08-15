<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="labels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DrugSearch.Prioritised.Labels" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/msp0640.png" alt="Diagram of a search results list divided into two sections, labelled &#39;Commonly prescribed matches&#39; and &#39;Standard matches&#39; respectively" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0640</p>
                <p>
                Provide a label for the prioritised results that 
                gives a brief indication of how they are prioritised
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0650</p>
                <p>
                Ensure that the labels are sufficiently different 
                from list items in the search results
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0660</p>
                <p>
                Label results that are not prioritised with &#39;Standard Matches&#39;
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../../images/msp0670.png" alt="Illustration in which the search results list has no sections or section labels" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0670</p>
                <p>
                When there are no prioritised matches, omit the 
                prioritised section, horizontal line and label
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
         
        </div>
    </div>
</asp:Content>
