<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="daymonth.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Date.LongDate.DayMonth" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		
            
			<div class="midline">
				<div class="illustration">
					<img src="../../images/dta0019.png" alt="Two examples of long dates: &#39;01 January 1999&#39;, with an arrow pointing to the &#39;01&#39; and &#39;1st January 1999&#39;, with an arrow pointing to the &#39;1st&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0019</p>
					<p>
					Display the day value using two digits (values less than 10 should 
					appear with a zero in the first position, unless the day value is 
					displayed in ordinal form)
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>rd</td>
                            <td>3<sup>rd</sup></td>
                        </tr>
                        <tr>
                            <td>th</td>
                            <td>4<sup>th</sup></td>
                        </tr>
                    </table>
                    <table class="leftdata">
                        <tr>
                            <td>st</td>
                            <td>1<sup>st</sup></td>
                        </tr>
                        <tr>
                            <td>nd</td>
                            <td>2<sup>nd</sup></td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0020</p>
					<p>When displaying the day value as an ordinal number, 
                    the suffix used must be one of those illustrated</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../../images/dta0021.png" alt="A long date: &#39;1st January 1999&#39;, with an arrow pointing to the &#39;1st&#39; and labelled &#39;ordinal number&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Ta-0021</p>
					<p>
					When displaying the day value as an ordinal number, the two-letter suffix must be 
					displayed in lower case and as a superscript immediately after the number
					</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
			<div class="line">
                <div class="illustration">
                    <img src="../../images/dta0012.png" alt="A long date: &#39;01 January 1999&#39;, with an arrow pointing to the month" />
                </div>
                <div class="guidetext">
                    <p class="number">D+Ta-0012</p>
                    <p>Display the month name in full</p>
                    <p class="mandatory">Mandatory</p>
                </div>
			</div>
			
		</div>
	</div>
</asp:Content>