<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="month.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.Month" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">            
            <div class="line">
                <div class="illustration">
                    <img src="../images/dta0002.png" alt="A short date:&#39;14-Aug-1980&#39; with an arrow pointing to the month" />
                </div>
                <div class="guidetext">
                    <p class="number">D+Ta-0002</p>
                    <p>Display the month textually, not numerically</p>
                    <p class="mandatory">Mandatory</p>
                    <p class="number">D+Ta-0003</p>
                    <p>Display the month with only the first letter in capitals</p>
                    <p class="mandatory">Mandatory</p>
                    <p class="number">D+Ta-0008</p>
                    <p>When displaying the month, do not include any punctuation, such as a full stop</p>
                    <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
