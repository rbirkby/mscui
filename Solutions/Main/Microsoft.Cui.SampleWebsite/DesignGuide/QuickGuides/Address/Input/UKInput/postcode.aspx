<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="postcode.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.Postcode" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0012.png" alt="A form with labels and input controls for Line 1, Line 2, Town &#047; City, County and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0013</p>
					<p>
                    Provide a means to find a postcode, to enhance data quality
                    </p>
					<p class="recommended">Recommended</p>
				<div class="guidetext">
					<p class="number">ADR-0014</p>
					<p>
                    Display a means to find a postcode only if such a service 
                    is supported, positioning it after the postcode input box 
                    and labelling it &#39;Find Postcode&#39;
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
        </div>
    </div>
</asp:Content>