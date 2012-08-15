<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
        
    <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0018.png" alt="An address input form supplemented with a blue vertical line to which the labels and input boxes are aligned" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0018</p>
					<p>
                    Display the text input boxes vertically with left alignment
                    </p>
					<p class="recommended">Recommended </p>
					<p class="number">ADR-0019</p>
					<p>
                    Display the labels immediately to the left of their corresponding 
                    text input box, mutually right-aligning the labels
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>