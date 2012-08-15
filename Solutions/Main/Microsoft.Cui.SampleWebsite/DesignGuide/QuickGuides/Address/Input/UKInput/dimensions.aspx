<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dimensions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.Dimensions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0015.png" alt="A postcode input box wide enough to display 8 characters" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0015</p>
					<p>
                    Set the length of the postcode input box to 8 characters
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0016.png" alt="A County input box wide enough to display 18 characters" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0016</p>
					<p>
                    Set the length of the &#39;County&#39; input box to 18 characters
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0017.png" alt="Two input boxes with vertical arrows to the right of each indicating the height" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0017</p>
					<p>
                    Set the height of each text input box to the largest 
                    character height in the currently active display font, 
                    taking the user's settings into account
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

        </div>
    </div>
</asp:Content>