<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="arithmetic.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Time.Arithmetic" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		

			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0047.png" alt="A three step diagram showing an input control containing: 1. &#39;13:54&#39; selected; 2. &#39;+3h&#39; and a text cursor; 3. &#39;16:54&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0047</p>
					<p>
                    Within the time input control, allow users to select the entire 
                    value to facilitate rapid editing 
                    or entry of arithmetic shortcuts relating to time (only)
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0044</p>
					<p>
                    Within the time input control do not allow users to input 
                    arithmetic shortcuts relating to date
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>&#43;</td>
                            <td>For later times</td>
                        </tr>
                        <tr>
                            <td>&#45;</td>
                            <td>For earlier times</td>
                        </tr>
                        <tr>
                            <td>h</td>
                            <td>hours</td>
                        </tr>
                        <tr>
                            <td>m</td>
                            <td>minutes</td>
                        </tr>
                        <tr>
                            <td>s</td>
                            <td>seconds</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0025</p>
					<p>
                    Allow users to enter arithmetic shortcuts such as '+3h' for 
                    three hours later and &#39;-2m&#39; for two minutes earlier.  The 
                    relevant operators are as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="note"><strong>Note </strong>Whole numbers are 
                    treated as positive by default</p>
				</div>
			</div>

		</div>
	</div>
</asp:Content>
