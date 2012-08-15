<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="extension.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Display.Extension" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                   
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0007.png" alt="A text input box in which the text &#39;0118 987 6543 x1234&#39; is displayed" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0007</p>
					<p>
                    For UK telephone numbers, extension numbers can be 
                    displayed with an &#39;x&#39; preceding and adjacent to the number
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/tid0008.png" alt="A diagram of a text input box in which the text &#39;0118 987 6543 x1234&#39; is displayed and an arrow points to the space before the &#39;x&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0008</p>
					<p>
                    For UK telephone numbers where the telephone and extension 
                    numbers are displayed within a single input box, the extension 
                    number must be separated from the rest of the telephone number 
                    by a single space that precedes the &#39;x&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0026.png" alt="Two text input boxes displayed side by side with labels ahove them, each label aligned to the left of the corresponding input box" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0026</p>
					<p>
                    For UK telephone numbers where the extension number is 
                    displayed in a separate input box, a label must be shown 
                    above the input box to indicate the content
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
         </div>
    </div>
</asp:Content>
