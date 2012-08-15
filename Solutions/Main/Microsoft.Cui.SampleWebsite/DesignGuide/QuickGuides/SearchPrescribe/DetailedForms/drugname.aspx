<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="drugname.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DetailedForms.DrugName" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1640.png" alt="Two step diagram showing the top part of a detailed prescription form. In the second step, the &#39;Drug&#39; input box has a drop-down list below with the text &#39;Edit drug name and route...&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1640</p>
                <p>
                When displaying a detailed prescription form, 
                combine the drug name and route (or drug name 
                and attributes that allow the type of medication 
                to be determined) into a single control
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1650</p>
                <p>
                When the combined drug name and route field is 
                selected, provide an option to change the drug 
                name and route
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
        
        </div>
    </div>   
</asp:Content>
