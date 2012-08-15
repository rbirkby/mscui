<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="redisplay.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Input.Redisplay" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0013.png" alt="A three step diagram showing the entry of a number with hyphens and the redisplay of that number with spaces instead of hyphens" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0014</p>
					<p>
                    Display a reformatted entry to the user which places 
                    spaces in logical locations for readability
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0016.png" alt="A two step diagram showing the entry of a number beginning &#39;+44&#39; and the redisplay of the number with the &#39;+44&#39; omitted" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0016</p>
					<p>
                    Remove the UK country code from display after it is committed
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">TID-0018</p>
					<p>
                    Do not display UK numbers with the international prefix
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0017.png" alt="A two step diagram showing the entry of a number beginning &#39;+1&#39; and the redisplay of that number including the &#39;+1&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0017</p>
					<p>
                    Retain all other country codes
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">TID-0019</p>
					<p>
                    Display non-UK numbers with a + prefixed to the country code
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
         </div>
    </div>
</asp:Content>
