<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="formatting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Display.Formatting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance"> 
                 
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0009.png" alt="A diagram of a text input box in which the text &#39;0118 123 4567&#39; is displayed and with an arrow pointing to the space between &#39;0118&#39; and &#39;123&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0009</p>
					<p>
                    For UK telephone numbers, if there are more than six digits 
                    in the local number, (in other words, not the country code, 
                    area code or extension number), then a space must be inserted 
                    before the final four digits
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
         </div>
    </div>
</asp:Content>
