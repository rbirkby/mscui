<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inputbox.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Time.InputBox" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0020.png" alt="An input box with spin control containing &#39;13:54&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0020</p>
					<p>
                    Adopt the guidance provided in
                    <a href="../../../Pdfs/Design%20Guidance%20--%20Time%20Display.pdf">
                    Design Guidance - Time Display</a> 
                    for the format of any dates displayed within the time input control
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0022.png" alt="An input box with spin control containing &#39;23:35&#39; followed by a check box that has been checked and the label &#39;Approx&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0022</p>
					<p>
                    Use an &#39;Approx&#39; check box to allow the user to indicate an approximate time
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
