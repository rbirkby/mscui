<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="countrycode.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Display.CountryCode" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                  
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0001.png" alt="A text input box labelled &#39;Home&#39; in which the text &#39;0118 496 0123&#39; is displayed" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0001</p>
					<p>
                    If the country code is for the UK, for example, 
                    &#39;+44&#39; or &#39;0044&#39;, then it must not be displayed
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0002.png" alt="A text input box labelled &#39;Home&#39; in which the text &#39;+1 212 555 2369&#39; is displayed" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0002</p>
					<p>
                    When displayed, the country code must always 
                    be displayed with a &#39;+&#39; sign in front of it
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">TID-0003</p>
					<p>
                    When displayed, the country code 
                    must not display any leading zeros
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

        </div>
   </div> 
</asp:Content>
