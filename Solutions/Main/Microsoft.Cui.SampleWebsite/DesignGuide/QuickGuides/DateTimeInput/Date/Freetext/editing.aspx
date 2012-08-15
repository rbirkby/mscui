<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="editing.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.Freetext.Editing" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
			
			<div class="midline">
				<div class="illustration">
					<img src="../../images/dtc0003.png" alt="A four step diagram showing date controls containing: 1. 05-May-2006; 2. &#39;05-May-2006&#39; with &#39;May&#39; selected; 3. 05-Se-2006 with a text cursor after &#39;Se&#39;; 4. 05-Sep-2006"/>
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0003</p>
					<p>
                    Allow the date elements to be individually edited 
                    (day, month and year)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../../images/dtc0046.png" alt="A two step diagram showing date controls containing: 1. &#39;05-May-2006&#39; with the whole date selected; 2. 17-J with a text cursor at the end" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0046</p>
					<p>
                    Within the date input control, allow users to select the entire 
                    value to facilitate rapid editing or entry of arithmetic shortcuts 
                    relating to date (only)
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
