<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="disambiguation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Duration.Disambiguation" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0037.png" alt="A duration input control containing the text &#39;6m&#39; and a tooltip below with the text &#39;Did you mean: 6 months or 6 minutes?&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0037</p>
					<p>
                    Provide the facility for a user to disambiguate input which 
                    could be interpreted in more than one way, for example, &#39;m&#39; 
                    (which could represent months or minutes)
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
		</div>
	</div>
</asp:Content>
