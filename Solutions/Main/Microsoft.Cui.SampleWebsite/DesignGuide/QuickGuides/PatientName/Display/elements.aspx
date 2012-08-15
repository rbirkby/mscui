<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="elements.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.Elements" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0004.png" alt="Diagram of a Patient Name &#39;SUBRAMANYAN, Chandrasekhar (Mr)&#39; with each element numbered: 1.&#39;SUBRAMANYAN&#39; 2.&#39;Chandrasekhar&#39; 3.&#39;(Mr)&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0004</p>
                <p>
                The display must present the name elements 
                strictly in the order shown
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0005.png" alt="Diagram of a Patient Name 'SMITH, John (Mr)' with all elements displayed in full" title="Diagram of a Patient Name &#39;SMITH, John (Mr)&#39; with all elements displayed in full" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0005</p>
                <p>
                The display must present all data for each 
                specified element (Family Name, Given Name 
                and Title) of the Patient Name in full. 
                Avoid truncation of information where possible
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
