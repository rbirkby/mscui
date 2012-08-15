<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="areacode.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Display.AreaCode" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">   
                
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0005.png" alt="A text input box labelled &#39;Work&#39; in which the number &#39;0118 123 4567&#39; is displayed" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0005</p>
					<p>
                    For UK telephone numbers, the area code 
                    must not be displayed with brackets around it
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0006.png" alt="Diagram of a text input box in which the number &#39;0118 123 4567&#39; is displayed with an arrow pointing to the space between &#39;0118&#39; and &#39;123&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0006</p>
					<p>
                    For UK telephone numbers, the area code must 
                    be separated from subsequent numbers by a space
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
         </div>
    </div>
</asp:Content>
