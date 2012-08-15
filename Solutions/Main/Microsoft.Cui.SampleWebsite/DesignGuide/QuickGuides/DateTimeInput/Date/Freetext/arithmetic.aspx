<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="arithmetic.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.Freetext.Arithmetic" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
				
			<div class="line">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td class="narrow">&#43;</td>
                            <td>For later dates</td>
                        </tr>
                        <tr>
                            <td class="narrow">&#45;</td>
                            <td>For earlier dates</td>
                        </tr>
                        <tr>
                            <td class="narrow">d</td>
                            <td>days</td>
                        </tr>
                        <tr>
                            <td class="narrow">w</td>
                            <td>weeks</td>
                        </tr>
                        <tr>
                            <td class="narrow">m</td>
                            <td>months</td>
                        </tr>
                        <tr>
                            <td class="narrow">y</td>
                            <td>years</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0007</p>
					<p>
                    Allow users to enter arithmetic shortcuts, such as 
                    &#39;+3m&#39; for three months later or &#39;-2d&#39; for two days 
                    earlier. The relevant operators are as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0041</p>
					<p>
                    Within the date input control, do not allow users to 
                    input arithmetic shortcuts relating to time
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>
