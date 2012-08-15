<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="defaultfields.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.DetailedForms.DefaultFields" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1590.png" alt="Diagram of a detailed prescription form with two callouts: 1. &#39;Required field &#39;, pointing to an input box that contains prompt text 2. &#39;Control for accessing an optional field&#39;, pointing to a &#39;&#43;&#39; button next to which is the label &#39;Patient&#39;s own medications&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1590</p>
                <p>
                Present the required fields by default when 
                a detailed prescription form is opened
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1600.png" alt="A detailed prescription form within a tabbed view in which the current tab is labelled &#39;Drug Details&#39; and the second (inactive) tab is labelled &#39;Administration Schedule&#39;"/>
                </div>
                <div class="guidealone">
                <p class="number">MSP-1600</p>
                <p>
                Provide access to a detailed prescription form that 
                presents the most important attributes by default and 
                from which all fields can be accessed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>   
</asp:Content>
