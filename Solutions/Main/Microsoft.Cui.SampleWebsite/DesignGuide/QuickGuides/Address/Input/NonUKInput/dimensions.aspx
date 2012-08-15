<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dimensions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.NonUKInput.Dimensions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0052.png" alt="Two input boxes with vertical arrows to the right of each indicating the height" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0052</p>
					<p>
                    Set the height of each text input box to the largest character 
                    height in the currently active display font, taking the user&#39;s 
                    settings into account
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>