<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="family.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.Family" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0001.png" alt="A Patient Name, SMITH, John (Mr), with the Family Name in all uppercase" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0001</p>
                <p>
                The display must present the Family Name in all uppercase 
                letters to clearly distinguish it from the Given Name
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
          </div>
            
        </div>
    </div>
</asp:Content>
