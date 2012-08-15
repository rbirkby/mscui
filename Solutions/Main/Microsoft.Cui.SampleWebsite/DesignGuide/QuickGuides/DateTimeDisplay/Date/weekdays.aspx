<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="weekdays.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.WeekDays" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">			
			<div class="midline">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>Mon</td>
                            <td>Monday</td>
                        </tr>
                        <tr>
                            <td>Tue</td>
                            <td>Tuesday</td>
                        </tr>
                        <tr>
                            <td>Wed</td>
                            <td>Wednesday</td>
                        </tr>
                        <tr>
                            <td>Thu</td>
                            <td>Thursday</td>
                        </tr>
                        <tr>
                            <td>Fri</td>
                            <td>Friday</td>
                        </tr>
                        <tr>
                            <td>Sat</td>
                            <td>Saturday</td>
                        </tr>
                        <tr>
                            <td>Sun</td>
                            <td>Sunday</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0016</p>
					<p>When displaying the day of the week, use one of the illustrated abbreviations</p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			<div class="line">
                <div class="illustration">
                    <img src="../images/dta0017.png" alt="A short date: &#39;Tue 05-Jan-2009&#39;, with an arrow pointing to the day of the week" />
                </div>
                <div class="guidetext">
                    <p class="number">D+Ta-0017</p>
                    <p>
                    Displaying the day of the week is optional, but when displayed, it must be 
                    placed immediately before the day value, with a single space separating the 
                    permitted abbreviated form of the day, from the day value
                    </p>
                    <p class="recommended">Recommended</p>
                </div>
			</div>
			               
		</div>
	</div>
</asp:Content>