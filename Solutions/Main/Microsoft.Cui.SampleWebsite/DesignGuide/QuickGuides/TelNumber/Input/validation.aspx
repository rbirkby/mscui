<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="validation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Input.Validation" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">

			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0013.png" alt="A three step diagram showing the entry of a number using hyphens and the redisplay of that number with spaces instead of hyphens" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0013</p>
					<p>
                    If the number can be identified as a valid type, 
                    the input box should strip out formatting upon losing focus and 
                    replace it with a reformatted equivalent
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0015.png" alt="A three step diagram showing entry of the text &#39;+888 12.34.56.78&#39; and the redisplay of that number with the formatting as it was when it was entered" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0015</p>
					<p>
                    If the number cannot be identified as a valid type, 
                    display the entry to the user as it was entered
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

         </div>
    </div>
</asp:Content>
