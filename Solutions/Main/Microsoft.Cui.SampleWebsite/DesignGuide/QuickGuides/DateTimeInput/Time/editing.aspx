<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="editing.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Time.Editing" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
				
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0023.png" alt="A three step diagram with each step showing an input control containing: 1. &#39;13:54&#39;; 2. &#39;13:54&#39; with &#39;13&#39; selected; 3. &#39;2&#39; followed by a text cursor and single space and then &#39;:54&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0023</p>
					<p>
                    Allow the time elements to be individually edited 
                    (hours, minutes and seconds)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0028.png" alt="A two step diagram showing: 1. An input control containing the time &#39;23:35:01&#39; and a mouse cursor over the top button of the spin control; 2. An input control containing the text &#39;23:35:02&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0028</p>
					<p>
                    Increase/decrease 
                    the whole time by the least significant time unit 
                    if the entire value is selected or if no unit is 
                    selected
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0029.png" alt="A two step diagram, each step showing an input control containing: 1. &#39;23:35:01&#39; with the &#39;23&#39; selected and a mouse cursor over the lower button of the spin control; 2. &#39;23:35:01&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0029</p>
					<p>
                    Provide the ability to spin individual 
                    time units when selected
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
