<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="labels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.Labels" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0012.png" alt="A form with labels and input controls for Line 1, Line 2, Town &#047; City, County and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0012</p>
					<p>
                    Where text input boxes are used, they must be labelled as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="note"><strong>Note</strong> This guidance applies to the text 
                    of the labels, not the formatting</p>
				</div>
			</div>
			
        </div>
    </div>
</asp:Content>