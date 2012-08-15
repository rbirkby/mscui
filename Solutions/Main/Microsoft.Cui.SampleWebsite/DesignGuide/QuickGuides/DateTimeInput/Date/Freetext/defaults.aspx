<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="defaults.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.Freetext.Defaults" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">	
				
			<div class="midline">
				<div class="illustration">
					<img src="../../images/dtc0009.png" alt="A date input control containing the text &#39;01-Jan-2010&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0009</p>
					<p>
                    Display a default input within the free text date input control
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../../images/dtc0042.png" alt="A date input control containing the prompt text &#39;DD-Mmm-YYYY&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0042</p>
					<p>
                    When displaying a default input within the free text input 
                    box, provide the user with an example of date with either 
                    a non-specific value (such as the input mask) or a date 
                    appropriate to the clinical context (for example, &#39;today&#39;s date&#39;)
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>
