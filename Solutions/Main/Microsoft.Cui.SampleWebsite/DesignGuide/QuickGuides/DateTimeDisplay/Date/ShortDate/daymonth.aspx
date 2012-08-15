<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="daymonth.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.ShortDate.DayMonth" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">			
			<div class="midline">
				<div class="illustration">
					<img src="../../images/dta0018.png" alt="A short date: &#39;14-Aug-1980&#39; with an arrow pointing to the &#39;14&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0018</p>
					<p>
					Display the day value using two digits (values less than 10 should 
					appear with a zero in the first position)
					</p>
					<p class="mandatory">Mandatory</p>
				</div>

			</div>

			<div class="line">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>Jul</td>
                            <td>July</td>
                        </tr>
                        <tr>
                            <td>Aug</td>
                            <td>August</td>
                        </tr>
                        <tr>
                            <td>Sep</td>
                            <td>September</td>
                        </tr>
                        <tr>
                            <td>Oct</td>
                            <td>October</td>
                        </tr>
                        <tr>
                            <td>Nov</td>
                            <td>November</td>
                        </tr>
                        <tr>
                            <td>Dec</td>
                            <td>December</td>
                        </tr>
                    </table>
                    <table class="leftdata">
                        <tr>
                            <td>Jan</td>
                            <td>January</td>
                        </tr>
                        <tr>
                            <td>Feb</td>
                            <td>February</td>
                        </tr>
                        <tr>
                            <td>Mar</td>
                            <td>March</td>
                        </tr>
                        <tr>
                            <td>Apr</td>
                            <td>April</td>
                        </tr>
                        <tr>
                            <td>May</td>
                            <td>May</td>
                        </tr>
                        <tr>
                            <td>Jun</td>
                            <td>June</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0007</p>
					<p>Display the month as a three letter abbreviation, with May being displayed in full</p>
					<p class="mandatory">Mandatory</p>
				</div>

			</div>
		</div>
	</div>
</asp:Content>