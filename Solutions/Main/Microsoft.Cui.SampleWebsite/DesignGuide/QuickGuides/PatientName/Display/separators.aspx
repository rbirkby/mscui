<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="separators.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.Separators" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">           
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0002.png" alt="&#39;COOPER, Catherine (Dr)&#39;, with an arrow pointing to the comma" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0002</p>
                <p>
                The display must separate the Family Name and 
                Given Name using a comma to further establish 
                that the Family Name is being placed first
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0006.png" alt="&#39;JOHNSON, Brian (Mr)&#39;, with an arrow pointing to the space between &#39;Brian&#39; and &#39;(Mr)&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0006</p>
                <p>
                The display must separate the presentation 
                of Given Name and Title by a single space
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
