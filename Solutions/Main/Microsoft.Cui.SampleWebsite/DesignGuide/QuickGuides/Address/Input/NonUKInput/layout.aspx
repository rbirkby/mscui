<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.NonUKInput.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
  <div id="page"> 
    <div id="guidance">        
        <div class="line">
            <div class="illustration">
                <img src="../../images/adr0053.png" alt="Diagram of an input form with a vertical line to which labels (on the left) are right-aligned and input boxes (on the right) are left-aligned" />
            </div>
            <div class="guidetext">
                <p class="number">ADR-0053</p>
                <p>
                    Display the input boxes vertically with left alignment
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">ADR-0054</p>
                <p>
                    Display the labels immediately to the left of their corresponding text input box,
                    mutually right-aligning the labels
                </p>
                <p class="recommended">Recommended</p>
            </div>
        </div>
    </div>
  </div> 
</asp:Content>