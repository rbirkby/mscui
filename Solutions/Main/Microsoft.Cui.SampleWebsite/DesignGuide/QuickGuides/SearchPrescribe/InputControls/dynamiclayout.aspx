<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="dynamiclayout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.DynamicLayout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1780.png" alt="A three step diagram showing the selection of a button labelled &#39;&#43;&#39; and subsequent display of two input boxes" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1820</p>
                <p>
                Allow some input controls to be defined that are 
                only displayed when certain values are selected in 
                another input control
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1850</p>
                <p>
                When an input causes new input control(s) to appear, 
                allow other input controls to move so that the new one 
                can be placed correctly and consistently
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1860</p>
                <p>
                When an input causes new input control(s) to appear, 
                place the new input controls next (at least in sequence 
                if not in layout) to the control that caused it to appear
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>      
</asp:Content>
