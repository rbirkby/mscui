<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.Finder.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="landscape">
					<img src="../../images/adr0036.png" alt="An address input form supplemented with a blue vertical line to which the labels and input boxes are aligned" />
				</div>
				<div class="guidealone">
					<p class="number">ADR-0036</p>
					<p>
                    Display the text input boxes vertically with left alignment
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">ADR-0037</p>
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