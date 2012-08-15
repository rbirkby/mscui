<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="findaddress.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.Finder.FindAddress" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
  <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="landscape">
					<img src="../../images/adr0033.png" alt="An input form that includes a button labelled &#39;Find Address&#39;" />
				</div>
				<div class="guidealone">
					<p class="number">ADR-0033</p>
					<p>
                    Display a means to find an address only if such a service 
                    is supported, positioning it after the postcode input box 
                    and labelling it &#39;Find Address&#39;
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>
