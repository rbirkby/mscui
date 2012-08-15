<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="drugname.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PrescriptionForms.DrugName" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1550.png" alt="Diagram of the top part of a prescription form showing the drug name in a section at the top and other fields in the section below that has a scroll bar" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1550</p>
                <p>
                Do not allow the drug name to be scrolled out 
                of view. Keep the drug name visible at the top 
                of the prescription form, even when the form has 
                a scroll bar
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>     
</asp:Content>
