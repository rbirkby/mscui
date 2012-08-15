<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inputboxes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Input.InputBoxes" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0011.png" alt="A text input box in which the text &#39;e.g. 01234 567890&#39; is displayed in grey italics" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0011</p>
					<p>
                    Use a free-text input box for the entry of telephone number
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0027.png" alt="Two text input boxes displayed side by side with the labels &#39;Telephone Number&#39; and &#39;Extension&#39; displayed above each" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0027</p>
					<p>
                    Use a free-text input box where extension 
                    number is input into a separate input box
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
         </div>
    </div>
</asp:Content>
