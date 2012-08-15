<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="structure.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DetailedForms.Structure" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1640.png" alt="Two step diagram showing the top part of a detailed prescription form. In the second step, the &#39;Drug&#39; input box has a drop-down list below with the text &#39;Edit drug name and route...&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1680</p>
                <p>
                Display the drug name and route (or drug name and 
                attributes that allow the type of medication to be 
                determined) in a section at the top of the detailed 
                prescription view
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1690.png" alt="Diagram of a detailed prescription form in which callouts indicate the field label &#39;Drug&#39; at the top and the group label &#39;Duration&#39; which is left aligned to the left of the duration input box and a control for accessing an optional attribute" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1690</p>
                <p>
                Display the first field in each section on a new line
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1700</p>
                <p>
                When section labels are provided, display them 
                at the top of the section
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1710</p>
                <p>
                Label at least each input control, 
                group of input controls or section
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
         
        </div>
    </div>    
</asp:Content>
